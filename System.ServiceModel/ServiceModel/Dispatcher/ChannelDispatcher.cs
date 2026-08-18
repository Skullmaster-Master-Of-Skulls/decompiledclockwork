using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.Text;
using System.Transactions;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000550 RID: 1360
	public class ChannelDispatcher : ChannelDispatcherBase
	{
		// Token: 0x060033D6 RID: 13270 RVA: 0x000C7E4F File Offset: 0x000C604F
		internal ChannelDispatcher(SharedRuntimeState shared)
		{
			this.Initialize(shared);
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x000C7E69 File Offset: 0x000C6069
		public ChannelDispatcher(IChannelListener listener) : this(listener, null, null)
		{
		}

		// Token: 0x060033D8 RID: 13272 RVA: 0x000C7E74 File Offset: 0x000C6074
		public ChannelDispatcher(IChannelListener listener, string bindingName) : this(listener, bindingName, null)
		{
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x000C7E80 File Offset: 0x000C6080
		public ChannelDispatcher(IChannelListener listener, string bindingName, IDefaultCommunicationTimeouts timeouts)
		{
			if (listener == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("listener");
			}
			this.listener = listener;
			this.bindingName = bindingName;
			this.timeouts = new ImmutableCommunicationTimeouts(timeouts);
			this.session = (listener is IChannelListener<IInputSessionChannel> || listener is IChannelListener<IReplySessionChannel> || listener is IChannelListener<IDuplexSessionChannel>);
			this.Initialize(new SharedRuntimeState(true));
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x000C7EFC File Offset: 0x000C60FC
		private void Initialize(SharedRuntimeState shared)
		{
			this.shared = shared;
			this.endpointDispatchers = new ChannelDispatcher.EndpointDispatcherCollection(this);
			this.channelInitializers = this.NewBehaviorCollection<IChannelInitializer>();
			this.channels = new CommunicationObjectManager<IChannel>(base.ThisLock);
			this.pendingChannels = new SynchronizedChannelCollection<IChannel>(base.ThisLock);
			this.errorHandlers = new Collection<IErrorHandler>();
			this.isTransactedReceive = false;
			this.asynchronousTransactedAcceptEnabled = false;
			this.receiveSynchronously = false;
			this.serviceThrottle = null;
			this.transactionTimeout = TimeSpan.Zero;
			this.maxPendingReceives = 1;
			if (this.listener != null)
			{
				this.listener.Faulted += this.OnListenerFaulted;
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x060033DB RID: 13275 RVA: 0x000C7FA2 File Offset: 0x000C61A2
		public string BindingName
		{
			get
			{
				return this.bindingName;
			}
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x060033DC RID: 13276 RVA: 0x000C7FAA File Offset: 0x000C61AA
		public SynchronizedCollection<IChannelInitializer> ChannelInitializers
		{
			get
			{
				return this.channelInitializers;
			}
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x060033DD RID: 13277 RVA: 0x000C7FB2 File Offset: 0x000C61B2
		protected override TimeSpan DefaultCloseTimeout
		{
			get
			{
				if (this.timeouts != null)
				{
					return this.timeouts.CloseTimeout;
				}
				return ServiceDefaults.CloseTimeout;
			}
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x060033DE RID: 13278 RVA: 0x000C7FCD File Offset: 0x000C61CD
		protected override TimeSpan DefaultOpenTimeout
		{
			get
			{
				if (this.timeouts != null)
				{
					return this.timeouts.OpenTimeout;
				}
				return ServiceDefaults.OpenTimeout;
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x060033DF RID: 13279 RVA: 0x000C7FE8 File Offset: 0x000C61E8
		internal EndpointDispatcherTable EndpointDispatcherTable
		{
			get
			{
				return this.filterTable;
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x060033E0 RID: 13280 RVA: 0x000C7FF0 File Offset: 0x000C61F0
		internal CommunicationObjectManager<IChannel> Channels
		{
			get
			{
				return this.channels;
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x060033E1 RID: 13281 RVA: 0x000C7FF8 File Offset: 0x000C61F8
		public SynchronizedCollection<EndpointDispatcher> Endpoints
		{
			get
			{
				return this.endpointDispatchers;
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x060033E2 RID: 13282 RVA: 0x000C8000 File Offset: 0x000C6200
		public Collection<IErrorHandler> ErrorHandlers
		{
			get
			{
				return this.errorHandlers;
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x060033E3 RID: 13283 RVA: 0x000C8008 File Offset: 0x000C6208
		// (set) Token: 0x060033E4 RID: 13284 RVA: 0x000C8010 File Offset: 0x000C6210
		public MessageVersion MessageVersion
		{
			get
			{
				return this.messageVersion;
			}
			set
			{
				this.messageVersion = value;
				this.ThrowIfDisposedOrImmutable();
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x060033E5 RID: 13285 RVA: 0x000C801F File Offset: 0x000C621F
		// (set) Token: 0x060033E6 RID: 13286 RVA: 0x000C8027 File Offset: 0x000C6227
		internal bool IsServiceThrottleReplaced { get; set; }

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x060033E7 RID: 13287 RVA: 0x000C8030 File Offset: 0x000C6230
		internal bool Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x060033E8 RID: 13288 RVA: 0x000C8038 File Offset: 0x000C6238
		public override ServiceHostBase Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x060033E9 RID: 13289 RVA: 0x000C8040 File Offset: 0x000C6240
		// (set) Token: 0x060033EA RID: 13290 RVA: 0x000C804D File Offset: 0x000C624D
		internal bool EnableFaults
		{
			get
			{
				return this.shared.EnableFaults;
			}
			set
			{
				this.ThrowIfDisposedOrImmutable();
				this.shared.EnableFaults = value;
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x060033EB RID: 13291 RVA: 0x000C8061 File Offset: 0x000C6261
		internal bool IsOnServer
		{
			get
			{
				return this.shared.IsOnServer;
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x060033EC RID: 13292 RVA: 0x000C806E File Offset: 0x000C626E
		public bool IsTransactedAccept
		{
			get
			{
				return this.isTransactedReceive && this.session;
			}
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x060033ED RID: 13293 RVA: 0x000C8080 File Offset: 0x000C6280
		// (set) Token: 0x060033EE RID: 13294 RVA: 0x000C8088 File Offset: 0x000C6288
		public bool IsTransactedReceive
		{
			get
			{
				return this.isTransactedReceive;
			}
			set
			{
				this.ThrowIfDisposedOrImmutable();
				this.isTransactedReceive = value;
			}
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x060033EF RID: 13295 RVA: 0x000C8097 File Offset: 0x000C6297
		// (set) Token: 0x060033F0 RID: 13296 RVA: 0x000C809F File Offset: 0x000C629F
		public bool AsynchronousTransactedAcceptEnabled
		{
			get
			{
				return this.asynchronousTransactedAcceptEnabled;
			}
			set
			{
				this.ThrowIfDisposedOrImmutable();
				this.asynchronousTransactedAcceptEnabled = value;
			}
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x060033F1 RID: 13297 RVA: 0x000C80AE File Offset: 0x000C62AE
		// (set) Token: 0x060033F2 RID: 13298 RVA: 0x000C80B6 File Offset: 0x000C62B6
		public bool ReceiveContextEnabled
		{
			get
			{
				return this.receiveContextEnabled;
			}
			set
			{
				this.ThrowIfDisposedOrImmutable();
				this.receiveContextEnabled = value;
			}
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x060033F3 RID: 13299 RVA: 0x000C80C5 File Offset: 0x000C62C5
		// (set) Token: 0x060033F4 RID: 13300 RVA: 0x000C80CD File Offset: 0x000C62CD
		internal bool BufferedReceiveEnabled { get; set; }

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x060033F5 RID: 13301 RVA: 0x000C80D6 File Offset: 0x000C62D6
		public override IChannelListener Listener
		{
			get
			{
				return this.listener;
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x060033F6 RID: 13302 RVA: 0x000C80DE File Offset: 0x000C62DE
		// (set) Token: 0x060033F7 RID: 13303 RVA: 0x000C80E6 File Offset: 0x000C62E6
		public int MaxTransactedBatchSize
		{
			get
			{
				return this.maxTransactedBatchSize;
			}
			set
			{
				if (value < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBeNonNegative")));
				}
				this.ThrowIfDisposedOrImmutable();
				this.maxTransactedBatchSize = value;
			}
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x060033F8 RID: 13304 RVA: 0x000C811E File Offset: 0x000C631E
		// (set) Token: 0x060033F9 RID: 13305 RVA: 0x000C8126 File Offset: 0x000C6326
		public ServiceThrottle ServiceThrottle
		{
			get
			{
				return this.serviceThrottle;
			}
			set
			{
				this.ThrowIfDisposedOrImmutable();
				this.serviceThrottle = value;
			}
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x060033FA RID: 13306 RVA: 0x000C8135 File Offset: 0x000C6335
		// (set) Token: 0x060033FB RID: 13307 RVA: 0x000C8142 File Offset: 0x000C6342
		public bool ManualAddressing
		{
			get
			{
				return this.shared.ManualAddressing;
			}
			set
			{
				this.ThrowIfDisposedOrImmutable();
				this.shared.ManualAddressing = value;
			}
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x060033FC RID: 13308 RVA: 0x000C8156 File Offset: 0x000C6356
		internal SynchronizedChannelCollection<IChannel> PendingChannels
		{
			get
			{
				return this.pendingChannels;
			}
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x060033FD RID: 13309 RVA: 0x000C815E File Offset: 0x000C635E
		// (set) Token: 0x060033FE RID: 13310 RVA: 0x000C8166 File Offset: 0x000C6366
		public bool ReceiveSynchronously
		{
			get
			{
				return this.receiveSynchronously;
			}
			set
			{
				this.ThrowIfDisposedOrImmutable();
				this.receiveSynchronously = value;
			}
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x060033FF RID: 13311 RVA: 0x000C8175 File Offset: 0x000C6375
		// (set) Token: 0x06003400 RID: 13312 RVA: 0x000C817D File Offset: 0x000C637D
		public bool SendAsynchronously
		{
			get
			{
				return this.sendAsynchronously;
			}
			set
			{
				this.ThrowIfDisposedOrImmutable();
				this.sendAsynchronously = value;
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06003401 RID: 13313 RVA: 0x000C818C File Offset: 0x000C638C
		// (set) Token: 0x06003402 RID: 13314 RVA: 0x000C8194 File Offset: 0x000C6394
		public int MaxPendingReceives
		{
			get
			{
				return this.maxPendingReceives;
			}
			set
			{
				this.ThrowIfDisposedOrImmutable();
				this.maxPendingReceives = value;
			}
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06003403 RID: 13315 RVA: 0x000C81A3 File Offset: 0x000C63A3
		// (set) Token: 0x06003404 RID: 13316 RVA: 0x000C81AC File Offset: 0x000C63AC
		public bool IncludeExceptionDetailInFaults
		{
			get
			{
				return this.includeExceptionDetailInFaults;
			}
			set
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					this.ThrowIfDisposedOrImmutable();
					this.includeExceptionDetailInFaults = value;
				}
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06003405 RID: 13317 RVA: 0x000C81F4 File Offset: 0x000C63F4
		internal IDefaultCommunicationTimeouts DefaultCommunicationTimeouts
		{
			get
			{
				return this.timeouts;
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06003406 RID: 13318 RVA: 0x000C81FC File Offset: 0x000C63FC
		// (set) Token: 0x06003407 RID: 13319 RVA: 0x000C8204 File Offset: 0x000C6404
		public IsolationLevel TransactionIsolationLevel
		{
			get
			{
				return this.transactionIsolationLevel;
			}
			set
			{
				if (value > IsolationLevel.Unspecified)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.ThrowIfDisposedOrImmutable();
				this.transactionIsolationLevel = value;
				this.transactionIsolationLevelSet = true;
			}
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06003408 RID: 13320 RVA: 0x000C8233 File Offset: 0x000C6433
		internal bool TransactionIsolationLevelSet
		{
			get
			{
				return this.transactionIsolationLevelSet;
			}
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06003409 RID: 13321 RVA: 0x000C823B File Offset: 0x000C643B
		// (set) Token: 0x0600340A RID: 13322 RVA: 0x000C8244 File Offset: 0x000C6444
		public TimeSpan TransactionTimeout
		{
			get
			{
				return this.transactionTimeout;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRange0")));
				}
				if (TimeoutHelper.IsTooLarge(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRangeTooBig")));
				}
				this.ThrowIfDisposedOrImmutable();
				this.transactionTimeout = value;
			}
		}

		// Token: 0x0600340B RID: 13323 RVA: 0x000C82C0 File Offset: 0x000C64C0
		private void AbortPendingChannels()
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				for (int i = this.pendingChannels.Count - 1; i >= 0; i--)
				{
					this.pendingChannels[i].Abort();
				}
			}
		}

		// Token: 0x0600340C RID: 13324 RVA: 0x000C8324 File Offset: 0x000C6524
		internal override void CloseInput(TimeSpan timeout)
		{
			this.CloseInput();
			if (this.performDefaultCloseInput)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (DiagnosticUtility.ShouldTraceInformation)
					{
						for (int i = 0; i < this.endpointDispatchers.Count; i++)
						{
							EndpointDispatcher endpoint = this.endpointDispatchers[i];
							this.TraceEndpointLifetime(endpoint, 262151, SR.GetString("TraceCodeEndpointListenerClose"));
						}
					}
					ListenerHandler listenerHandler = this.listenerHandler;
					if (listenerHandler != null)
					{
						listenerHandler.CloseInput(timeoutHelper.RemainingTime());
					}
				}
				if (!this.session)
				{
					ListenerHandler listenerHandler2 = this.listenerHandler;
					if (listenerHandler2 != null)
					{
						listenerHandler2.Close(timeoutHelper.RemainingTime());
					}
				}
			}
		}

		// Token: 0x0600340D RID: 13325 RVA: 0x000C83F8 File Offset: 0x000C65F8
		internal void ReleasePerformanceCounters()
		{
			if (PerformanceCounters.PerformanceCountersEnabled)
			{
				for (int i = 0; i < this.endpointDispatchers.Count; i++)
				{
					if (this.endpointDispatchers[i] != null)
					{
						this.endpointDispatchers[i].ReleasePerformanceCounters();
					}
				}
			}
		}

		// Token: 0x0600340E RID: 13326 RVA: 0x000C8441 File Offset: 0x000C6641
		public override void CloseInput()
		{
			this.performDefaultCloseInput = true;
		}

		// Token: 0x0600340F RID: 13327 RVA: 0x000C844A File Offset: 0x000C664A
		private void OnListenerFaulted(object sender, EventArgs e)
		{
			base.Fault();
		}

		// Token: 0x06003410 RID: 13328 RVA: 0x000C8454 File Offset: 0x000C6654
		internal bool HandleError(Exception error)
		{
			ErrorHandlerFaultInfo errorHandlerFaultInfo = default(ErrorHandlerFaultInfo);
			return this.HandleError(error, ref errorHandlerFaultInfo);
		}

		// Token: 0x06003411 RID: 13329 RVA: 0x000C8474 File Offset: 0x000C6674
		internal bool HandleError(Exception error, ref ErrorHandlerFaultInfo faultInfo)
		{
			object thisLock = base.ThisLock;
			ErrorBehavior errorBehavior;
			lock (thisLock)
			{
				if (this.errorBehavior != null)
				{
					errorBehavior = this.errorBehavior;
				}
				else
				{
					errorBehavior = new ErrorBehavior(this);
				}
			}
			return errorBehavior != null && errorBehavior.HandleError(error, ref faultInfo);
		}

		// Token: 0x06003412 RID: 13330 RVA: 0x000C84D4 File Offset: 0x000C66D4
		internal void InitializeChannel(IClientChannel channel)
		{
			base.ThrowIfDisposedOrNotOpen();
			try
			{
				for (int i = 0; i < this.channelInitializers.Count; i++)
				{
					this.channelInitializers[i].Initialize(channel);
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex);
			}
		}

		// Token: 0x06003413 RID: 13331 RVA: 0x000C8538 File Offset: 0x000C6738
		internal EndpointDispatcher Match(Message message, out bool addressMatched)
		{
			object thisLock = base.ThisLock;
			EndpointDispatcher result;
			lock (thisLock)
			{
				result = this.filterTable.Lookup(message, out addressMatched);
			}
			return result;
		}

		// Token: 0x06003414 RID: 13332 RVA: 0x000C8584 File Offset: 0x000C6784
		internal SynchronizedCollection<T> NewBehaviorCollection<T>()
		{
			return new ChannelDispatcher.ChannelDispatcherBehaviorCollection<T>(this);
		}

		// Token: 0x06003415 RID: 13333 RVA: 0x000C858C File Offset: 0x000C678C
		internal bool HasApplicationEndpoints()
		{
			foreach (EndpointDispatcher endpointDispatcher in this.Endpoints)
			{
				if (!endpointDispatcher.IsSystemEndpoint)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003416 RID: 13334 RVA: 0x000C85E4 File Offset: 0x000C67E4
		private void OnAddEndpoint(EndpointDispatcher endpoint)
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				endpoint.Attach(this);
				if (base.State == CommunicationState.Opened)
				{
					if (this.addressTable != null)
					{
						this.addressTable.Add(endpoint.AddressFilter, endpoint.EndpointAddress, endpoint.FilterPriority);
					}
					this.filterTable.AddEndpoint(endpoint);
				}
			}
		}

		// Token: 0x06003417 RID: 13335 RVA: 0x000C8660 File Offset: 0x000C6860
		private void OnRemoveEndpoint(EndpointDispatcher endpoint)
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (base.State == CommunicationState.Opened)
				{
					this.filterTable.RemoveEndpoint(endpoint);
					if (this.addressTable != null)
					{
						this.addressTable.Remove(endpoint.AddressFilter);
					}
				}
				endpoint.Detach(this);
			}
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x000C86D0 File Offset: 0x000C68D0
		protected override void OnAbort()
		{
			if (this.listener != null)
			{
				this.listener.Abort();
			}
			ListenerHandler listenerHandler = this.listenerHandler;
			if (listenerHandler != null)
			{
				listenerHandler.Abort();
			}
			this.AbortPendingChannels();
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x000C8708 File Offset: 0x000C6908
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.listener != null)
			{
				this.listener.Close(timeoutHelper.RemainingTime());
			}
			ListenerHandler listenerHandler = this.listenerHandler;
			if (listenerHandler != null)
			{
				listenerHandler.Close(timeoutHelper.RemainingTime());
			}
			this.AbortPendingChannels();
		}

		// Token: 0x0600341A RID: 13338 RVA: 0x000C8754 File Offset: 0x000C6954
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			List<ICommunicationObject> list = new List<ICommunicationObject>();
			if (this.listener != null)
			{
				list.Add(this.listener);
			}
			ListenerHandler listenerHandler = this.listenerHandler;
			if (listenerHandler != null)
			{
				list.Add(listenerHandler);
			}
			return new CloseCollectionAsyncResult(timeout, callback, state, list);
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x000C8798 File Offset: 0x000C6998
		protected override void OnEndClose(IAsyncResult result)
		{
			try
			{
				CloseCollectionAsyncResult.End(result);
			}
			finally
			{
				this.AbortPendingChannels();
			}
		}

		// Token: 0x0600341C RID: 13340 RVA: 0x000C87C4 File Offset: 0x000C69C4
		protected override void OnClosed()
		{
			base.OnClosed();
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				for (int i = 0; i < this.endpointDispatchers.Count; i++)
				{
					EndpointDispatcher endpoint = this.endpointDispatchers[i];
					this.TraceEndpointLifetime(endpoint, 262151, SR.GetString("TraceCodeEndpointListenerClose"));
				}
			}
		}

		// Token: 0x0600341D RID: 13341 RVA: 0x000C8818 File Offset: 0x000C6A18
		protected override void OnOpen(TimeSpan timeout)
		{
			this.ThrowIfNotAttachedToHost();
			this.ThrowIfNoMessageVersion();
			if (this.listener != null)
			{
				try
				{
					this.listener.Open(timeout);
				}
				catch (InvalidOperationException e)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateOuterExceptionWithEndpointsInformation(e));
				}
			}
		}

		// Token: 0x0600341E RID: 13342 RVA: 0x000C886C File Offset: 0x000C6A6C
		private InvalidOperationException CreateOuterExceptionWithEndpointsInformation(InvalidOperationException e)
		{
			string text = this.CreateContractListString();
			if (string.IsNullOrEmpty(text))
			{
				return new InvalidOperationException(SR.GetString("SFxChannelDispatcherUnableToOpen1", new object[]
				{
					this.listener.Uri
				}), e);
			}
			return new InvalidOperationException(SR.GetString("SFxChannelDispatcherUnableToOpen2", new object[]
			{
				this.listener.Uri,
				text
			}), e);
		}

		// Token: 0x0600341F RID: 13343 RVA: 0x000C88D8 File Offset: 0x000C6AD8
		internal string CreateContractListString()
		{
			Collection<string> collection = new Collection<string>();
			StringBuilder stringBuilder = new StringBuilder();
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				foreach (EndpointDispatcher endpointDispatcher in this.Endpoints)
				{
					if (!collection.Contains(endpointDispatcher.ContractName))
					{
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append(CultureInfo.CurrentCulture.TextInfo.ListSeparator);
							stringBuilder.Append(" ");
						}
						stringBuilder.Append("\"");
						stringBuilder.Append(endpointDispatcher.ContractName);
						stringBuilder.Append("\"");
						collection.Add(endpointDispatcher.ContractName);
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x000C89CC File Offset: 0x000C6BCC
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.ThrowIfNotAttachedToHost();
			this.ThrowIfNoMessageVersion();
			if (this.listener != null)
			{
				try
				{
					return this.listener.BeginOpen(timeout, callback, state);
				}
				catch (InvalidOperationException e)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateOuterExceptionWithEndpointsInformation(e));
				}
			}
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06003421 RID: 13345 RVA: 0x000C8A2C File Offset: 0x000C6C2C
		protected override void OnEndOpen(IAsyncResult result)
		{
			if (this.listener != null)
			{
				try
				{
					this.listener.EndOpen(result);
					return;
				}
				catch (InvalidOperationException e)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateOuterExceptionWithEndpointsInformation(e));
				}
			}
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06003422 RID: 13346 RVA: 0x000C8A78 File Offset: 0x000C6C78
		protected override void OnOpening()
		{
			this.ThrowIfNotAttachedToHost();
			if (TD.ListenerOpenStartIsEnabled())
			{
				this.eventTraceActivity = EventTraceActivity.GetFromThreadOrCreate(false);
				TD.ListenerOpenStart(this.eventTraceActivity, (this.Listener != null) ? this.Listener.Uri.ToString() : string.Empty, (this.host != null && this.host.EventTraceActivity != null) ? this.host.EventTraceActivity.ActivityId : Guid.Empty);
			}
			base.OnOpening();
		}

		// Token: 0x06003423 RID: 13347 RVA: 0x000C8AFC File Offset: 0x000C6CFC
		protected override void OnOpened()
		{
			this.ThrowIfNotAttachedToHost();
			base.OnOpened();
			if (TD.ListenerOpenStopIsEnabled())
			{
				TD.ListenerOpenStop(this.eventTraceActivity);
				this.eventTraceActivity = null;
			}
			this.errorBehavior = new ErrorBehavior(this);
			this.filterTable = new EndpointDispatcherTable(base.ThisLock);
			for (int i = 0; i < this.endpointDispatchers.Count; i++)
			{
				EndpointDispatcher endpointDispatcher = this.endpointDispatchers[i];
				endpointDispatcher.DispatchRuntime.GetRuntime();
				endpointDispatcher.DispatchRuntime.LockDownProperties();
				this.filterTable.AddEndpoint(endpointDispatcher);
				if (this.addressTable != null && endpointDispatcher.OriginalAddress != null)
				{
					this.addressTable.Add(endpointDispatcher.AddressFilter, endpointDispatcher.OriginalAddress, endpointDispatcher.FilterPriority);
				}
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					this.TraceEndpointLifetime(endpointDispatcher, 262152, SR.GetString("TraceCodeEndpointListenerOpen"));
				}
			}
			ServiceThrottle serviceThrottle = this.serviceThrottle;
			if (serviceThrottle == null)
			{
				serviceThrottle = this.host.ServiceThrottle;
			}
			IListenerBinder binder = ListenerBinder.GetBinder(this.listener, this.messageVersion);
			this.listenerHandler = new ListenerHandler(binder, this, this.host, serviceThrottle, this.timeouts);
			this.listenerHandler.Open();
		}

		// Token: 0x06003424 RID: 13348 RVA: 0x000C8C34 File Offset: 0x000C6E34
		internal void ProvideFault(Exception e, FaultConverter faultConverter, ref ErrorHandlerFaultInfo faultInfo)
		{
			object thisLock = base.ThisLock;
			ErrorBehavior errorBehavior;
			lock (thisLock)
			{
				if (this.errorBehavior != null)
				{
					errorBehavior = this.errorBehavior;
				}
				else
				{
					errorBehavior = new ErrorBehavior(this);
				}
			}
			errorBehavior.ProvideFault(e, faultConverter, ref faultInfo);
		}

		// Token: 0x06003425 RID: 13349 RVA: 0x000C8C90 File Offset: 0x000C6E90
		internal void SetEndpointAddressTable(ThreadSafeMessageFilterTable<EndpointAddress> table)
		{
			if (table == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("table");
			}
			this.ThrowIfDisposedOrImmutable();
			this.addressTable = table;
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x000C8CB2 File Offset: 0x000C6EB2
		internal new void ThrowIfDisposedOrImmutable()
		{
			base.ThrowIfDisposedOrImmutable();
			this.shared.ThrowIfImmutable();
		}

		// Token: 0x06003427 RID: 13351 RVA: 0x000C8CC8 File Offset: 0x000C6EC8
		private void ThrowIfNotAttachedToHost()
		{
			if (this.host == null)
			{
				Exception exception = new InvalidOperationException(SR.GetString("SFxChannelDispatcherNoHost0"));
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
			}
		}

		// Token: 0x06003428 RID: 13352 RVA: 0x000C8CFC File Offset: 0x000C6EFC
		private void ThrowIfNoMessageVersion()
		{
			if (this.messageVersion == null)
			{
				Exception exception = new InvalidOperationException(SR.GetString("SFxChannelDispatcherNoMessageVersion"));
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
			}
		}

		// Token: 0x06003429 RID: 13353 RVA: 0x000C8D30 File Offset: 0x000C6F30
		private void TraceEndpointLifetime(EndpointDispatcher endpoint, int traceCode, string traceDescription)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>(3)
				{
					{
						"ContractNamespace",
						endpoint.ContractNamespace
					},
					{
						"ContractName",
						endpoint.ContractName
					},
					{
						"Endpoint",
						endpoint.ListenUri
					}
				};
				TraceUtility.TraceEvent(TraceEventType.Information, traceCode, traceDescription, new DictionaryTraceRecord(dictionary), endpoint, null);
			}
		}

		// Token: 0x0600342A RID: 13354 RVA: 0x000C8D90 File Offset: 0x000C6F90
		protected override void Attach(ServiceHostBase host)
		{
			if (host == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("host");
			}
			this.ThrowIfDisposedOrImmutable();
			if (this.host != null)
			{
				Exception exception = new InvalidOperationException(SR.GetString("SFxChannelDispatcherMultipleHost0"));
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
			}
			this.host = host;
		}

		// Token: 0x0600342B RID: 13355 RVA: 0x000C8DE4 File Offset: 0x000C6FE4
		protected override void Detach(ServiceHostBase host)
		{
			if (host == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("host");
			}
			if (this.host != host)
			{
				Exception exception = new InvalidOperationException(SR.GetString("SFxChannelDispatcherDifferentHost0"));
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
			}
			this.ThrowIfDisposedOrImmutable();
			this.host = null;
		}

		// Token: 0x040027AB RID: 10155
		private ThreadSafeMessageFilterTable<EndpointAddress> addressTable;

		// Token: 0x040027AC RID: 10156
		private string bindingName;

		// Token: 0x040027AD RID: 10157
		private SynchronizedCollection<IChannelInitializer> channelInitializers;

		// Token: 0x040027AE RID: 10158
		private CommunicationObjectManager<IChannel> channels;

		// Token: 0x040027AF RID: 10159
		private ChannelDispatcher.EndpointDispatcherCollection endpointDispatchers;

		// Token: 0x040027B0 RID: 10160
		private Collection<IErrorHandler> errorHandlers;

		// Token: 0x040027B1 RID: 10161
		private EndpointDispatcherTable filterTable;

		// Token: 0x040027B2 RID: 10162
		private ServiceHostBase host;

		// Token: 0x040027B3 RID: 10163
		private bool isTransactedReceive;

		// Token: 0x040027B4 RID: 10164
		private bool asynchronousTransactedAcceptEnabled;

		// Token: 0x040027B5 RID: 10165
		private bool receiveContextEnabled;

		// Token: 0x040027B6 RID: 10166
		private readonly IChannelListener listener;

		// Token: 0x040027B7 RID: 10167
		private ListenerHandler listenerHandler;

		// Token: 0x040027B8 RID: 10168
		private int maxTransactedBatchSize;

		// Token: 0x040027B9 RID: 10169
		private MessageVersion messageVersion;

		// Token: 0x040027BA RID: 10170
		private SynchronizedChannelCollection<IChannel> pendingChannels;

		// Token: 0x040027BB RID: 10171
		private bool receiveSynchronously;

		// Token: 0x040027BC RID: 10172
		private bool sendAsynchronously;

		// Token: 0x040027BD RID: 10173
		private int maxPendingReceives;

		// Token: 0x040027BE RID: 10174
		private bool includeExceptionDetailInFaults;

		// Token: 0x040027BF RID: 10175
		private ServiceThrottle serviceThrottle;

		// Token: 0x040027C0 RID: 10176
		private bool session;

		// Token: 0x040027C1 RID: 10177
		private SharedRuntimeState shared;

		// Token: 0x040027C2 RID: 10178
		private IDefaultCommunicationTimeouts timeouts;

		// Token: 0x040027C3 RID: 10179
		private IsolationLevel transactionIsolationLevel = ServiceBehaviorAttribute.DefaultIsolationLevel;

		// Token: 0x040027C4 RID: 10180
		private bool transactionIsolationLevelSet;

		// Token: 0x040027C5 RID: 10181
		private TimeSpan transactionTimeout;

		// Token: 0x040027C6 RID: 10182
		private bool performDefaultCloseInput;

		// Token: 0x040027C7 RID: 10183
		private EventTraceActivity eventTraceActivity;

		// Token: 0x040027C8 RID: 10184
		private ErrorBehavior errorBehavior;

		// Token: 0x02000C72 RID: 3186
		private class EndpointDispatcherCollection : SynchronizedCollection<EndpointDispatcher>
		{
			// Token: 0x06007812 RID: 30738 RVA: 0x001C113A File Offset: 0x001BF33A
			internal EndpointDispatcherCollection(ChannelDispatcher owner) : base(owner.ThisLock)
			{
				this.owner = owner;
			}

			// Token: 0x06007813 RID: 30739 RVA: 0x001C1150 File Offset: 0x001BF350
			protected override void ClearItems()
			{
				foreach (EndpointDispatcher endpoint in base.Items)
				{
					this.owner.OnRemoveEndpoint(endpoint);
				}
				base.ClearItems();
			}

			// Token: 0x06007814 RID: 30740 RVA: 0x001C11B0 File Offset: 0x001BF3B0
			protected override void InsertItem(int index, EndpointDispatcher item)
			{
				if (item == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
				}
				this.owner.OnAddEndpoint(item);
				base.InsertItem(index, item);
			}

			// Token: 0x06007815 RID: 30741 RVA: 0x001C11DC File Offset: 0x001BF3DC
			protected override void RemoveItem(int index)
			{
				EndpointDispatcher endpoint = base.Items[index];
				base.RemoveItem(index);
				this.owner.OnRemoveEndpoint(endpoint);
			}

			// Token: 0x06007816 RID: 30742 RVA: 0x001C120C File Offset: 0x001BF40C
			protected override void SetItem(int index, EndpointDispatcher item)
			{
				Exception exception = new InvalidOperationException(SR.GetString("SFxCollectionDoesNotSupportSet0"));
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
			}

			// Token: 0x04004481 RID: 17537
			private ChannelDispatcher owner;
		}

		// Token: 0x02000C73 RID: 3187
		private class ChannelDispatcherBehaviorCollection<T> : SynchronizedCollection<T>
		{
			// Token: 0x06007817 RID: 30743 RVA: 0x001C1234 File Offset: 0x001BF434
			internal ChannelDispatcherBehaviorCollection(ChannelDispatcher outer) : base(outer.ThisLock)
			{
				this.outer = outer;
			}

			// Token: 0x06007818 RID: 30744 RVA: 0x001C1249 File Offset: 0x001BF449
			protected override void ClearItems()
			{
				this.outer.ThrowIfDisposedOrImmutable();
				base.ClearItems();
			}

			// Token: 0x06007819 RID: 30745 RVA: 0x001C125C File Offset: 0x001BF45C
			protected override void InsertItem(int index, T item)
			{
				if (item == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
				}
				this.outer.ThrowIfDisposedOrImmutable();
				base.InsertItem(index, item);
			}

			// Token: 0x0600781A RID: 30746 RVA: 0x001C1289 File Offset: 0x001BF489
			protected override void RemoveItem(int index)
			{
				this.outer.ThrowIfDisposedOrImmutable();
				base.RemoveItem(index);
			}

			// Token: 0x0600781B RID: 30747 RVA: 0x001C129D File Offset: 0x001BF49D
			protected override void SetItem(int index, T item)
			{
				if (item == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
				}
				this.outer.ThrowIfDisposedOrImmutable();
				base.SetItem(index, item);
			}

			// Token: 0x04004482 RID: 17538
			private ChannelDispatcher outer;
		}
	}
}
