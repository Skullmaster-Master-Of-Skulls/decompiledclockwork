using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Activation;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200099D RID: 2461
	internal sealed class ServiceChannel : CommunicationObject, IChannel, ICommunicationObject, IClientChannel, IContextChannel, IExtensibleObject<IContextChannel>, IDisposable, IDuplexContextChannel, IOutputChannel, IRequestChannel, IServiceChannel
	{
		// Token: 0x06006008 RID: 24584 RVA: 0x0016640C File Offset: 0x0016460C
		private ServiceChannel(IChannelBinder binder, MessageVersion messageVersion, IDefaultCommunicationTimeouts timeouts)
		{
			if (binder == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("binder");
			}
			this.messageVersion = messageVersion;
			this.binder = binder;
			this.isReplyChannel = (this.binder.Channel is IReplyChannel);
			IChannel channel = binder.Channel;
			this.hasSession = (channel is ISessionChannel<IDuplexSession> || channel is ISessionChannel<IInputSession> || channel is ISessionChannel<IOutputSession>);
			this.IncrementActivity();
			this.openBinder = (binder.Channel.State == CommunicationState.Created);
			this.operationTimeout = timeouts.SendTimeout;
		}

		// Token: 0x06006009 RID: 24585 RVA: 0x001664C0 File Offset: 0x001646C0
		internal ServiceChannel(ServiceChannelFactory factory, IChannelBinder binder) : this(binder, factory.MessageVersion, factory)
		{
			this.factory = factory;
			this.clientRuntime = factory.ClientRuntime;
			this.SetupInnerChannelFaultHandler();
			DispatchRuntime dispatchRuntime = factory.ClientRuntime.DispatchRuntime;
			if (dispatchRuntime != null)
			{
				this.autoClose = dispatchRuntime.AutomaticInputSessionShutdown;
			}
			factory.ChannelCreated(this);
		}

		// Token: 0x0600600A RID: 24586 RVA: 0x00166518 File Offset: 0x00164718
		internal ServiceChannel(IChannelBinder binder, EndpointDispatcher endpointDispatcher, ChannelDispatcher channelDispatcher, ServiceChannel.SessionIdleManager idleManager) : this(binder, channelDispatcher.MessageVersion, channelDispatcher.DefaultCommunicationTimeouts)
		{
			if (endpointDispatcher == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpointDispatcher");
			}
			this.channelDispatcher = channelDispatcher;
			this.endpointDispatcher = endpointDispatcher;
			this.clientRuntime = endpointDispatcher.DispatchRuntime.CallbackClientRuntime;
			this.SetupInnerChannelFaultHandler();
			this.autoClose = endpointDispatcher.DispatchRuntime.AutomaticInputSessionShutdown;
			this.isPending = true;
			IDefaultCommunicationTimeouts defaultCommunicationTimeouts = channelDispatcher.DefaultCommunicationTimeouts;
			this.idleManager = idleManager;
			if (!binder.HasSession)
			{
				this.closeBinder = false;
			}
			if (this.idleManager != null)
			{
				bool flag;
				this.idleManager.RegisterChannel(this, out flag);
				if (flag)
				{
					base.Abort();
				}
			}
		}

		// Token: 0x1700170B RID: 5899
		// (get) Token: 0x0600600B RID: 24587 RVA: 0x001665C5 File Offset: 0x001647C5
		private ServiceChannel.CallOnceManager AutoOpenManager
		{
			get
			{
				if (!this.explicitlyOpened && this.autoOpenManager == null)
				{
					this.EnsureAutoOpenManagers();
				}
				return this.autoOpenManager;
			}
		}

		// Token: 0x1700170C RID: 5900
		// (get) Token: 0x0600600C RID: 24588 RVA: 0x001665E3 File Offset: 0x001647E3
		private ServiceChannel.CallOnceManager AutoDisplayUIManager
		{
			get
			{
				if (!this.explicitlyOpened && this.autoDisplayUIManager == null)
				{
					this.EnsureAutoOpenManagers();
				}
				return this.autoDisplayUIManager;
			}
		}

		// Token: 0x1700170D RID: 5901
		// (get) Token: 0x0600600D RID: 24589 RVA: 0x00166601 File Offset: 0x00164801
		internal EventTraceActivity EventActivity
		{
			get
			{
				if (this.eventActivity == null)
				{
					this.eventActivity = EventTraceActivity.GetFromThreadOrCreate(false);
				}
				return this.eventActivity;
			}
		}

		// Token: 0x1700170E RID: 5902
		// (get) Token: 0x0600600E RID: 24590 RVA: 0x0016661D File Offset: 0x0016481D
		// (set) Token: 0x0600600F RID: 24591 RVA: 0x00166625 File Offset: 0x00164825
		internal bool CloseFactory
		{
			get
			{
				return this.closeFactory;
			}
			set
			{
				this.closeFactory = value;
			}
		}

		// Token: 0x1700170F RID: 5903
		// (get) Token: 0x06006010 RID: 24592 RVA: 0x0016662E File Offset: 0x0016482E
		protected override TimeSpan DefaultCloseTimeout
		{
			get
			{
				return this.CloseTimeout;
			}
		}

		// Token: 0x17001710 RID: 5904
		// (get) Token: 0x06006011 RID: 24593 RVA: 0x00166636 File Offset: 0x00164836
		protected override TimeSpan DefaultOpenTimeout
		{
			get
			{
				return this.OpenTimeout;
			}
		}

		// Token: 0x17001711 RID: 5905
		// (get) Token: 0x06006012 RID: 24594 RVA: 0x0016663E File Offset: 0x0016483E
		internal DispatchRuntime DispatchRuntime
		{
			get
			{
				if (this.endpointDispatcher != null)
				{
					return this.endpointDispatcher.DispatchRuntime;
				}
				if (this.clientRuntime != null)
				{
					return this.clientRuntime.DispatchRuntime;
				}
				return null;
			}
		}

		// Token: 0x17001712 RID: 5906
		// (get) Token: 0x06006013 RID: 24595 RVA: 0x00166669 File Offset: 0x00164869
		internal MessageVersion MessageVersion
		{
			get
			{
				return this.messageVersion;
			}
		}

		// Token: 0x17001713 RID: 5907
		// (get) Token: 0x06006014 RID: 24596 RVA: 0x00166671 File Offset: 0x00164871
		internal IChannelBinder Binder
		{
			get
			{
				return this.binder;
			}
		}

		// Token: 0x17001714 RID: 5908
		// (get) Token: 0x06006015 RID: 24597 RVA: 0x00166679 File Offset: 0x00164879
		internal TimeSpan CloseTimeout
		{
			get
			{
				if (this.IsClient)
				{
					return this.factory.InternalCloseTimeout;
				}
				return this.ChannelDispatcher.InternalCloseTimeout;
			}
		}

		// Token: 0x17001715 RID: 5909
		// (get) Token: 0x06006016 RID: 24598 RVA: 0x0016669A File Offset: 0x0016489A
		internal ChannelDispatcher ChannelDispatcher
		{
			get
			{
				return this.channelDispatcher;
			}
		}

		// Token: 0x17001716 RID: 5910
		// (get) Token: 0x06006017 RID: 24599 RVA: 0x001666A2 File Offset: 0x001648A2
		// (set) Token: 0x06006018 RID: 24600 RVA: 0x001666AC File Offset: 0x001648AC
		internal EndpointDispatcher EndpointDispatcher
		{
			get
			{
				return this.endpointDispatcher;
			}
			set
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					this.endpointDispatcher = value;
					this.clientRuntime = value.DispatchRuntime.CallbackClientRuntime;
				}
			}
		}

		// Token: 0x17001717 RID: 5911
		// (get) Token: 0x06006019 RID: 24601 RVA: 0x00166700 File Offset: 0x00164900
		internal ServiceChannelFactory Factory
		{
			get
			{
				return this.factory;
			}
		}

		// Token: 0x17001718 RID: 5912
		// (get) Token: 0x0600601A RID: 24602 RVA: 0x00166708 File Offset: 0x00164908
		internal IChannel InnerChannel
		{
			get
			{
				return this.binder.Channel;
			}
		}

		// Token: 0x17001719 RID: 5913
		// (get) Token: 0x0600601B RID: 24603 RVA: 0x00166715 File Offset: 0x00164915
		// (set) Token: 0x0600601C RID: 24604 RVA: 0x0016671D File Offset: 0x0016491D
		internal bool IsPending
		{
			get
			{
				return this.isPending;
			}
			set
			{
				this.isPending = value;
			}
		}

		// Token: 0x1700171A RID: 5914
		// (get) Token: 0x0600601D RID: 24605 RVA: 0x00166726 File Offset: 0x00164926
		internal bool HasSession
		{
			get
			{
				return this.hasSession;
			}
		}

		// Token: 0x1700171B RID: 5915
		// (get) Token: 0x0600601E RID: 24606 RVA: 0x0016672E File Offset: 0x0016492E
		internal bool IsClient
		{
			get
			{
				return this.factory != null;
			}
		}

		// Token: 0x1700171C RID: 5916
		// (get) Token: 0x0600601F RID: 24607 RVA: 0x00166739 File Offset: 0x00164939
		internal bool IsReplyChannel
		{
			get
			{
				return this.isReplyChannel;
			}
		}

		// Token: 0x1700171D RID: 5917
		// (get) Token: 0x06006020 RID: 24608 RVA: 0x00166741 File Offset: 0x00164941
		public Uri ListenUri
		{
			get
			{
				return this.binder.ListenUri;
			}
		}

		// Token: 0x1700171E RID: 5918
		// (get) Token: 0x06006021 RID: 24609 RVA: 0x00166750 File Offset: 0x00164950
		public EndpointAddress LocalAddress
		{
			get
			{
				if (this.localAddress == null)
				{
					if (this.endpointDispatcher != null)
					{
						this.localAddress = this.endpointDispatcher.EndpointAddress;
					}
					else
					{
						this.localAddress = this.binder.LocalAddress;
					}
				}
				return this.localAddress;
			}
		}

		// Token: 0x1700171F RID: 5919
		// (get) Token: 0x06006022 RID: 24610 RVA: 0x0016679D File Offset: 0x0016499D
		internal TimeSpan OpenTimeout
		{
			get
			{
				if (this.IsClient)
				{
					return this.factory.InternalOpenTimeout;
				}
				return this.ChannelDispatcher.InternalOpenTimeout;
			}
		}

		// Token: 0x17001720 RID: 5920
		// (get) Token: 0x06006023 RID: 24611 RVA: 0x001667BE File Offset: 0x001649BE
		// (set) Token: 0x06006024 RID: 24612 RVA: 0x001667C8 File Offset: 0x001649C8
		public TimeSpan OperationTimeout
		{
			get
			{
				return this.operationTimeout;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					string @string = SR.GetString("SFxTimeoutOutOfRange0");
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, @string));
				}
				if (TimeoutHelper.IsTooLarge(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRangeTooBig")));
				}
				this.operationTimeout = value;
			}
		}

		// Token: 0x17001721 RID: 5921
		// (get) Token: 0x06006025 RID: 24613 RVA: 0x00166840 File Offset: 0x00164A40
		// (set) Token: 0x06006026 RID: 24614 RVA: 0x0016685A File Offset: 0x00164A5A
		internal object Proxy
		{
			get
			{
				object obj = this.proxy;
				if (obj != null)
				{
					return obj;
				}
				return this;
			}
			set
			{
				this.proxy = value;
				base.EventSender = value;
			}
		}

		// Token: 0x17001722 RID: 5922
		// (get) Token: 0x06006027 RID: 24615 RVA: 0x0016686A File Offset: 0x00164A6A
		internal ClientRuntime ClientRuntime
		{
			get
			{
				return this.clientRuntime;
			}
		}

		// Token: 0x17001723 RID: 5923
		// (get) Token: 0x06006028 RID: 24616 RVA: 0x00166874 File Offset: 0x00164A74
		public EndpointAddress RemoteAddress
		{
			get
			{
				IOutputChannel outputChannel = this.InnerChannel as IOutputChannel;
				if (outputChannel != null)
				{
					return outputChannel.RemoteAddress;
				}
				IRequestChannel requestChannel = this.InnerChannel as IRequestChannel;
				if (requestChannel != null)
				{
					return requestChannel.RemoteAddress;
				}
				return null;
			}
		}

		// Token: 0x17001724 RID: 5924
		// (get) Token: 0x06006029 RID: 24617 RVA: 0x001668AE File Offset: 0x00164AAE
		private ProxyOperationRuntime UnhandledProxyOperation
		{
			get
			{
				return this.ClientRuntime.GetRuntime().UnhandledProxyOperation;
			}
		}

		// Token: 0x17001725 RID: 5925
		// (get) Token: 0x0600602A RID: 24618 RVA: 0x001668C0 File Offset: 0x00164AC0
		public Uri Via
		{
			get
			{
				IOutputChannel outputChannel = this.InnerChannel as IOutputChannel;
				if (outputChannel != null)
				{
					return outputChannel.Via;
				}
				IRequestChannel requestChannel = this.InnerChannel as IRequestChannel;
				if (requestChannel != null)
				{
					return requestChannel.Via;
				}
				return null;
			}
		}

		// Token: 0x17001726 RID: 5926
		// (get) Token: 0x0600602B RID: 24619 RVA: 0x001668FA File Offset: 0x00164AFA
		// (set) Token: 0x0600602C RID: 24620 RVA: 0x00166902 File Offset: 0x00164B02
		internal InstanceContext InstanceContext
		{
			get
			{
				return this.instanceContext;
			}
			set
			{
				this.instanceContext = value;
			}
		}

		// Token: 0x17001727 RID: 5927
		// (get) Token: 0x0600602D RID: 24621 RVA: 0x0016690B File Offset: 0x00164B0B
		// (set) Token: 0x0600602E RID: 24622 RVA: 0x00166913 File Offset: 0x00164B13
		internal ServiceThrottle InstanceContextServiceThrottle
		{
			get
			{
				return this.instanceContextServiceThrottle;
			}
			set
			{
				this.instanceContextServiceThrottle = value;
			}
		}

		// Token: 0x17001728 RID: 5928
		// (get) Token: 0x0600602F RID: 24623 RVA: 0x0016691C File Offset: 0x00164B1C
		// (set) Token: 0x06006030 RID: 24624 RVA: 0x00166924 File Offset: 0x00164B24
		internal ServiceThrottle ServiceThrottle
		{
			get
			{
				return this.serviceThrottle;
			}
			set
			{
				base.ThrowIfDisposed();
				this.serviceThrottle = value;
			}
		}

		// Token: 0x17001729 RID: 5929
		// (get) Token: 0x06006031 RID: 24625 RVA: 0x00166933 File Offset: 0x00164B33
		// (set) Token: 0x06006032 RID: 24626 RVA: 0x0016693B File Offset: 0x00164B3B
		internal InstanceContext WmiInstanceContext
		{
			get
			{
				return this.wmiInstanceContext;
			}
			set
			{
				this.wmiInstanceContext = value;
			}
		}

		// Token: 0x06006033 RID: 24627 RVA: 0x00166944 File Offset: 0x00164B44
		private void SetupInnerChannelFaultHandler()
		{
			this.binder.Channel.Faulted += this.OnInnerChannelFaulted;
		}

		// Token: 0x06006034 RID: 24628 RVA: 0x00166964 File Offset: 0x00164B64
		private void BindDuplexCallbacks()
		{
			IDuplexChannel duplexChannel = this.InnerChannel as IDuplexChannel;
			if (duplexChannel != null && this.factory != null && this.instanceContext != null && this.binder is DuplexChannelBinder)
			{
				((DuplexChannelBinder)this.binder).EnsurePumping();
			}
		}

		// Token: 0x06006035 RID: 24629 RVA: 0x001669B0 File Offset: 0x00164BB0
		internal bool CanCastTo(Type t)
		{
			if (t.IsAssignableFrom(typeof(IClientChannel)))
			{
				return true;
			}
			if (t.IsAssignableFrom(typeof(IDuplexContextChannel)))
			{
				return this.InnerChannel is IDuplexChannel;
			}
			return t.IsAssignableFrom(typeof(IServiceChannel));
		}

		// Token: 0x06006036 RID: 24630 RVA: 0x00166A07 File Offset: 0x00164C07
		internal void CompletedIOOperation()
		{
			if (this.idleManager != null)
			{
				this.idleManager.CompletedActivity();
			}
		}

		// Token: 0x06006037 RID: 24631 RVA: 0x00166A1C File Offset: 0x00164C1C
		private void EnsureAutoOpenManagers()
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (!this.explicitlyOpened)
				{
					if (this.autoOpenManager == null)
					{
						this.autoOpenManager = new ServiceChannel.CallOnceManager(this, ServiceChannel.CallOpenOnce.Instance);
					}
					if (this.autoDisplayUIManager == null)
					{
						this.autoDisplayUIManager = new ServiceChannel.CallOnceManager(this, ServiceChannel.CallDisplayUIOnce.Instance);
					}
				}
			}
		}

		// Token: 0x06006038 RID: 24632 RVA: 0x00166A90 File Offset: 0x00164C90
		private void EnsureDisplayUI()
		{
			ServiceChannel.CallOnceManager callOnceManager = this.AutoDisplayUIManager;
			if (callOnceManager != null)
			{
				callOnceManager.CallOnce(TimeSpan.MaxValue, null);
			}
			this.ThrowIfInitializationUINotCalled();
		}

		// Token: 0x06006039 RID: 24633 RVA: 0x00166ABC File Offset: 0x00164CBC
		private IAsyncResult BeginEnsureDisplayUI(AsyncCallback callback, object state)
		{
			ServiceChannel.CallOnceManager callOnceManager = this.AutoDisplayUIManager;
			if (callOnceManager != null)
			{
				return callOnceManager.BeginCallOnce(TimeSpan.MaxValue, null, callback, state);
			}
			return new ServiceChannel.CallOnceCompletedAsyncResult(callback, state);
		}

		// Token: 0x0600603A RID: 24634 RVA: 0x00166AEC File Offset: 0x00164CEC
		private void EndEnsureDisplayUI(IAsyncResult result)
		{
			ServiceChannel.CallOnceManager callOnceManager = this.AutoDisplayUIManager;
			if (callOnceManager != null)
			{
				callOnceManager.EndCallOnce(result);
			}
			else
			{
				ServiceChannel.CallOnceCompletedAsyncResult.End(result);
			}
			this.ThrowIfInitializationUINotCalled();
		}

		// Token: 0x0600603B RID: 24635 RVA: 0x00166B18 File Offset: 0x00164D18
		private void EnsureOpened(TimeSpan timeout)
		{
			ServiceChannel.CallOnceManager callOnceManager = this.AutoOpenManager;
			if (callOnceManager != null)
			{
				callOnceManager.CallOnce(timeout, this.autoDisplayUIManager);
			}
			this.ThrowIfOpening();
			base.ThrowIfDisposedOrNotOpen();
		}

		// Token: 0x0600603C RID: 24636 RVA: 0x00166B48 File Offset: 0x00164D48
		private IAsyncResult BeginEnsureOpened(TimeSpan timeout, AsyncCallback callback, object state)
		{
			ServiceChannel.CallOnceManager callOnceManager = this.AutoOpenManager;
			if (callOnceManager != null)
			{
				return callOnceManager.BeginCallOnce(timeout, this.autoDisplayUIManager, callback, state);
			}
			this.ThrowIfOpening();
			base.ThrowIfDisposedOrNotOpen();
			return new ServiceChannel.CallOnceCompletedAsyncResult(callback, state);
		}

		// Token: 0x0600603D RID: 24637 RVA: 0x00166B84 File Offset: 0x00164D84
		private void EndEnsureOpened(IAsyncResult result)
		{
			ServiceChannel.CallOnceManager callOnceManager = this.AutoOpenManager;
			if (callOnceManager != null)
			{
				callOnceManager.EndCallOnce(result);
				return;
			}
			ServiceChannel.CallOnceCompletedAsyncResult.End(result);
		}

		// Token: 0x0600603E RID: 24638 RVA: 0x00166BAC File Offset: 0x00164DAC
		public T GetProperty<T>() where T : class
		{
			IChannel innerChannel = this.InnerChannel;
			if (innerChannel != null)
			{
				return innerChannel.GetProperty<T>();
			}
			return default(T);
		}

		// Token: 0x0600603F RID: 24639 RVA: 0x00166BD4 File Offset: 0x00164DD4
		private void PrepareCall(ProxyOperationRuntime operation, bool oneway, ref ProxyRpc rpc)
		{
			OperationContext operationContext = OperationContext.Current;
			if (!oneway)
			{
				DispatchRuntime dispatchRuntime = this.ClientRuntime.DispatchRuntime;
				if (dispatchRuntime != null && dispatchRuntime.ConcurrencyMode == ConcurrencyMode.Single && operationContext != null && !operationContext.IsUserContext && operationContext.InternalServiceChannel == this)
				{
					if (dispatchRuntime.IsOnServer)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxCallbackRequestReplyInOrder1", new object[]
						{
							typeof(ServiceBehaviorAttribute).Name
						})));
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxCallbackRequestReplyInOrder1", new object[]
					{
						typeof(CallbackBehaviorAttribute).Name
					})));
				}
			}
			if (base.State == CommunicationState.Created && !operation.IsInitiating)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxNonInitiatingOperation1", new object[]
				{
					operation.Name
				})));
			}
			if (this.terminatingOperationName != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxTerminatingOperationAlreadyCalled1", new object[]
				{
					this.terminatingOperationName
				})));
			}
			if (this.hasChannelStartedAutoClosing)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("SFxClientOutputSessionAutoClosed")));
			}
			operation.BeforeRequest(ref rpc);
			this.AddMessageProperties(rpc.Request, operationContext);
			if (!oneway && !this.ClientRuntime.ManualAddressing && rpc.Request.Version.Addressing != AddressingVersion.None)
			{
				RequestReplyCorrelator.PrepareRequest(rpc.Request);
				MessageHeaders headers = rpc.Request.Headers;
				EndpointAddress endpointAddress = this.LocalAddress;
				EndpointAddress replyTo = headers.ReplyTo;
				if (replyTo == null)
				{
					headers.ReplyTo = (endpointAddress ?? EndpointAddress.AnonymousAddress);
				}
				if (this.IsClient && endpointAddress != null && !endpointAddress.IsAnonymous)
				{
					Uri uri = endpointAddress.Uri;
					if (replyTo != null && !replyTo.IsAnonymous && uri != replyTo.Uri)
					{
						string @string = SR.GetString("SFxRequestHasInvalidReplyToOnClient", new object[]
						{
							replyTo.Uri,
							uri
						});
						Exception exception = new InvalidOperationException(@string);
						throw TraceUtility.ThrowHelperError(exception, rpc.Request);
					}
					EndpointAddress faultTo = headers.FaultTo;
					if (faultTo != null && !faultTo.IsAnonymous && uri != faultTo.Uri)
					{
						string string2 = SR.GetString("SFxRequestHasInvalidFaultToOnClient", new object[]
						{
							faultTo.Uri,
							uri
						});
						Exception exception2 = new InvalidOperationException(string2);
						throw TraceUtility.ThrowHelperError(exception2, rpc.Request);
					}
					if (this.messageVersion.Addressing == AddressingVersion.WSAddressingAugust2004)
					{
						EndpointAddress from = headers.From;
						if (from != null && !from.IsAnonymous && uri != from.Uri)
						{
							string string3 = SR.GetString("SFxRequestHasInvalidFromOnClient", new object[]
							{
								from.Uri,
								uri
							});
							Exception exception3 = new InvalidOperationException(string3);
							throw TraceUtility.ThrowHelperError(exception3, rpc.Request);
						}
					}
				}
			}
			if (TraceUtility.MessageFlowTracingOnly && Trace.CorrelationManager.ActivityId == Guid.Empty)
			{
				rpc.ActivityId = Guid.NewGuid();
				FxTrace.Trace.SetAndTraceTransfer(rpc.ActivityId, true);
			}
			if (rpc.Activity != null)
			{
				TraceUtility.SetActivity(rpc.Request, rpc.Activity);
				if (TraceUtility.ShouldPropagateActivity)
				{
					TraceUtility.AddActivityHeader(rpc.Request);
				}
			}
			else if (TraceUtility.PropagateUserActivity || TraceUtility.ShouldPropagateActivity)
			{
				TraceUtility.AddAmbientActivityToMessage(rpc.Request);
			}
			operation.Parent.BeforeSendRequest(ref rpc);
			if (FxTrace.Trace.IsEnd2EndActivityTracingEnabled)
			{
				this.TraceClientOperationPrepared(ref rpc);
			}
			TraceUtility.MessageFlowAtMessageSent(rpc.Request, rpc.EventTraceActivity);
			if (MessageLogger.LogMessagesAtServiceLevel)
			{
				MessageLogger.LogMessage(ref rpc.Request, (oneway ? MessageLoggingSource.ServiceLevelSendDatagram : MessageLoggingSource.ServiceLevelSendRequest) | MessageLoggingSource.LastChance);
			}
		}

		// Token: 0x06006040 RID: 24640 RVA: 0x00166FCC File Offset: 0x001651CC
		private void TraceClientOperationPrepared(ref ProxyRpc rpc)
		{
			Guid relatedActivityId = (rpc.EventTraceActivity != null) ? rpc.EventTraceActivity.ActivityId : Guid.Empty;
			EventTraceActivity eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(rpc.Request);
			if (eventTraceActivity == null)
			{
				eventTraceActivity = EventTraceActivity.GetFromThreadOrCreate(false);
				EventTraceActivityHelper.TryAttachActivity(rpc.Request, eventTraceActivity);
			}
			rpc.EventTraceActivity = eventTraceActivity;
			if (TD.ClientOperationPreparedIsEnabled())
			{
				string destination = string.Empty;
				if (this.RemoteAddress != null && this.RemoteAddress.Uri != null)
				{
					destination = this.RemoteAddress.Uri.AbsoluteUri;
				}
				TD.ClientOperationPrepared(rpc.EventTraceActivity, rpc.Action, this.clientRuntime.ContractName, destination, relatedActivityId);
			}
		}

		// Token: 0x06006041 RID: 24641 RVA: 0x0016707B File Offset: 0x0016527B
		internal static IAsyncResult BeginCall(ServiceChannel channel, ProxyOperationRuntime operation, object[] ins, AsyncCallback callback, object asyncState)
		{
			return channel.BeginCall(operation.Action, operation.IsOneWay, operation, ins, channel.operationTimeout, callback, asyncState);
		}

		// Token: 0x06006042 RID: 24642 RVA: 0x0016709A File Offset: 0x0016529A
		internal IAsyncResult BeginCall(string action, bool oneway, ProxyOperationRuntime operation, object[] ins, AsyncCallback callback, object asyncState)
		{
			return this.BeginCall(action, oneway, operation, ins, this.operationTimeout, callback, asyncState);
		}

		// Token: 0x06006043 RID: 24643 RVA: 0x001670B4 File Offset: 0x001652B4
		internal IAsyncResult BeginCall(string action, bool oneway, ProxyOperationRuntime operation, object[] ins, TimeSpan timeout, AsyncCallback callback, object asyncState)
		{
			this.ThrowIfDisallowedInitializationUI();
			this.ThrowIfIdleAborted(operation);
			this.ThrowIfIsConnectionOpened(operation);
			ServiceModelActivity activity = null;
			if (DiagnosticUtility.ShouldUseActivity)
			{
				activity = ServiceModelActivity.CreateActivity(true);
				callback = TraceUtility.WrapExecuteUserCodeAsyncCallback(callback);
			}
			ServiceChannel.SendAsyncResult sendAsyncResult;
			using (ServiceModelActivity.BoundOperation(activity, true))
			{
				if (DiagnosticUtility.ShouldUseActivity)
				{
					ServiceModelActivity.Start(activity, SR.GetString("ActivityProcessAction", new object[]
					{
						action
					}), ActivityType.ProcessAction);
				}
				sendAsyncResult = new ServiceChannel.SendAsyncResult(this, operation, action, ins, oneway, timeout, callback, asyncState);
				if (DiagnosticUtility.ShouldUseActivity)
				{
					sendAsyncResult.Rpc.Activity = activity;
				}
				ServiceChannel.TraceServiceChannelCallStart(sendAsyncResult.Rpc.EventTraceActivity, false);
				sendAsyncResult.Begin();
			}
			return sendAsyncResult;
		}

		// Token: 0x06006044 RID: 24644 RVA: 0x00167174 File Offset: 0x00165374
		internal object Call(string action, bool oneway, ProxyOperationRuntime operation, object[] ins, object[] outs)
		{
			return this.Call(action, oneway, operation, ins, outs, this.operationTimeout);
		}

		// Token: 0x06006045 RID: 24645 RVA: 0x0016718C File Offset: 0x0016538C
		internal object Call(string action, bool oneway, ProxyOperationRuntime operation, object[] ins, object[] outs, TimeSpan timeout)
		{
			this.ThrowIfDisallowedInitializationUI();
			this.ThrowIfIdleAborted(operation);
			this.ThrowIfIsConnectionOpened(operation);
			ProxyRpc proxyRpc = new ProxyRpc(this, operation, action, ins, timeout);
			ServiceChannel.TraceServiceChannelCallStart(proxyRpc.EventTraceActivity, true);
			using (proxyRpc.Activity = (DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateBoundedActivity() : null))
			{
				if (DiagnosticUtility.ShouldUseActivity)
				{
					ServiceModelActivity.Start(proxyRpc.Activity, SR.GetString("ActivityProcessAction", new object[]
					{
						action
					}), ActivityType.ProcessAction);
				}
				this.PrepareCall(operation, oneway, ref proxyRpc);
				if (!this.explicitlyOpened)
				{
					this.EnsureDisplayUI();
					this.EnsureOpened(proxyRpc.TimeoutHelper.RemainingTime());
				}
				else
				{
					this.ThrowIfOpening();
					base.ThrowIfDisposedOrNotOpen();
				}
				try
				{
					ConcurrencyBehavior.UnlockInstanceBeforeCallout(OperationContext.Current);
					if (oneway)
					{
						this.binder.Send(proxyRpc.Request, proxyRpc.TimeoutHelper.RemainingTime());
					}
					else
					{
						proxyRpc.Reply = this.binder.Request(proxyRpc.Request, proxyRpc.TimeoutHelper.RemainingTime());
						if (proxyRpc.Reply == null)
						{
							base.ThrowIfFaulted();
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SFxServerDidNotReply")));
						}
					}
				}
				finally
				{
					this.CompletedIOOperation();
					ServiceChannel.CallOnceManager.SignalNextIfNonNull(this.autoOpenManager);
					ConcurrencyBehavior.LockInstanceAfterCallout(OperationContext.Current);
				}
				proxyRpc.OutputParameters = outs;
				this.HandleReply(operation, ref proxyRpc);
			}
			return proxyRpc.ReturnValue;
		}

		// Token: 0x06006046 RID: 24646 RVA: 0x00167338 File Offset: 0x00165538
		internal object EndCall(string action, object[] outs, IAsyncResult result)
		{
			ServiceChannel.SendAsyncResult sendAsyncResult = result as ServiceChannel.SendAsyncResult;
			if (sendAsyncResult == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SFxInvalidCallbackIAsyncResult")));
			}
			object returnValue;
			using (ServiceModelActivity activity = sendAsyncResult.Rpc.Activity)
			{
				using (ServiceModelActivity.BoundOperation(activity, true))
				{
					if (sendAsyncResult.Rpc.Activity != null && DiagnosticUtility.ShouldUseActivity)
					{
						sendAsyncResult.Rpc.Activity.Resume();
					}
					if (sendAsyncResult.Rpc.Channel != this)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("result", SR.GetString("AsyncEndCalledOnWrongChannel"));
					}
					if (action != "*" && action != sendAsyncResult.Rpc.Action)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("result", SR.GetString("AsyncEndCalledWithAnIAsyncResult"));
					}
					ServiceChannel.SendAsyncResult.End(sendAsyncResult);
					sendAsyncResult.Rpc.OutputParameters = outs;
					this.HandleReply(sendAsyncResult.Rpc.Operation, ref sendAsyncResult.Rpc);
					if (sendAsyncResult.Rpc.Activity != null)
					{
						sendAsyncResult.Rpc.Activity = null;
					}
					returnValue = sendAsyncResult.Rpc.ReturnValue;
				}
			}
			return returnValue;
		}

		// Token: 0x06006047 RID: 24647 RVA: 0x00167484 File Offset: 0x00165684
		internal void DecrementActivity()
		{
			int num = Interlocked.Decrement(ref this.activityCount);
			if (num < 0)
			{
				throw Fx.AssertAndThrowFatal("ServiceChannel.DecrementActivity: (updatedActivityCount >= 0)");
			}
			if (num == 0 && this.autoClose)
			{
				try
				{
					if (base.State == CommunicationState.Opened)
					{
						if (this.IsClient)
						{
							ISessionChannel<IDuplexSession> sessionChannel = this.InnerChannel as ISessionChannel<IDuplexSession>;
							if (sessionChannel != null)
							{
								this.hasChannelStartedAutoClosing = true;
								sessionChannel.Session.CloseOutputSession(this.CloseTimeout);
							}
						}
						else
						{
							base.Close(this.CloseTimeout);
						}
					}
				}
				catch (CommunicationException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
				catch (TimeoutException ex)
				{
					if (TD.CloseTimeoutIsEnabled())
					{
						TD.CloseTimeout(ex.Message);
					}
					DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
				}
				catch (ObjectDisposedException exception2)
				{
					DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
				}
				catch (InvalidOperationException exception3)
				{
					DiagnosticUtility.TraceHandledException(exception3, TraceEventType.Information);
				}
			}
		}

		// Token: 0x06006048 RID: 24648 RVA: 0x00167578 File Offset: 0x00165778
		internal void FireUnknownMessageReceived(Message message)
		{
			EventHandler<UnknownMessageReceivedEventArgs> eventHandler = this.unknownMessageReceived;
			if (eventHandler != null)
			{
				eventHandler(this.proxy, new UnknownMessageReceivedEventArgs(message));
			}
		}

		// Token: 0x06006049 RID: 24649 RVA: 0x001675A4 File Offset: 0x001657A4
		private TimeoutException GetOpenTimeoutException(TimeSpan timeout)
		{
			EndpointAddress endpointAddress = this.RemoteAddress ?? this.LocalAddress;
			if (endpointAddress != null)
			{
				return new TimeoutException(SR.GetString("TimeoutServiceChannelConcurrentOpen2", new object[]
				{
					endpointAddress,
					timeout
				}));
			}
			return new TimeoutException(SR.GetString("TimeoutServiceChannelConcurrentOpen1", new object[]
			{
				timeout
			}));
		}

		// Token: 0x0600604A RID: 24650 RVA: 0x0016760C File Offset: 0x0016580C
		internal void HandleReceiveComplete(RequestContext context)
		{
			if (context == null && this.HasSession)
			{
				object thisLock = base.ThisLock;
				bool flag2;
				lock (thisLock)
				{
					flag2 = !this.doneReceiving;
					this.doneReceiving = true;
				}
				if (flag2)
				{
					DispatchRuntime dispatchRuntime = this.ClientRuntime.DispatchRuntime;
					if (dispatchRuntime != null)
					{
						dispatchRuntime.GetRuntime().InputSessionDoneReceiving(this);
					}
					this.DecrementActivity();
				}
			}
		}

		// Token: 0x0600604B RID: 24651 RVA: 0x00167688 File Offset: 0x00165888
		private void HandleReply(ProxyOperationRuntime operation, ref ProxyRpc rpc)
		{
			try
			{
				if (TraceUtility.MessageFlowTracingOnly && rpc.ActivityId != Guid.Empty)
				{
					DiagnosticTraceBase.ActivityId = rpc.ActivityId;
				}
				if (rpc.Reply != null)
				{
					TraceUtility.MessageFlowAtMessageReceived(rpc.Reply, null, rpc.EventTraceActivity, false);
					if (MessageLogger.LogMessagesAtServiceLevel)
					{
						MessageLogger.LogMessage(ref rpc.Reply, MessageLoggingSource.ServiceLevelReceiveReply | MessageLoggingSource.LastChance);
					}
					operation.Parent.AfterReceiveReply(ref rpc);
					if (operation.ReplyAction != "*" && !rpc.Reply.IsFault && rpc.Reply.Headers.Action != null && string.CompareOrdinal(operation.ReplyAction, rpc.Reply.Headers.Action) != 0)
					{
						Exception exception = new ProtocolException(SR.GetString("SFxReplyActionMismatch3", new object[]
						{
							operation.Name,
							rpc.Reply.Headers.Action,
							operation.ReplyAction
						}));
						this.TerminateIfNecessary(ref rpc);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
					}
					if (operation.DeserializeReply && this.clientRuntime.IsFault(ref rpc.Reply))
					{
						MessageFault messageFault = MessageFault.CreateFault(rpc.Reply, this.clientRuntime.MaxFaultSize);
						string text = rpc.Reply.Headers.Action;
						if (text == rpc.Reply.Version.Addressing.DefaultFaultAction)
						{
							text = null;
						}
						this.ThrowIfFaultUnderstood(rpc.Reply, messageFault, text, rpc.Reply.Version, rpc.Channel.GetProperty<FaultConverter>());
						FaultException exception2 = rpc.Operation.FaultFormatter.Deserialize(messageFault, text);
						this.TerminateIfNecessary(ref rpc);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(exception2);
					}
					operation.AfterReply(ref rpc);
				}
			}
			finally
			{
				if (operation.SerializeRequest)
				{
					rpc.Request.Close();
				}
				OperationContext operationContext = OperationContext.Current;
				bool flag = rpc.Reply != null && rpc.Reply.State > MessageState.Created;
				if (operationContext != null && operationContext.IsUserContext)
				{
					operationContext.SetClientReply(rpc.Reply, flag);
				}
				else if (flag)
				{
					rpc.Reply.Close();
				}
				if (TraceUtility.MessageFlowTracingOnly && rpc.ActivityId != Guid.Empty)
				{
					DiagnosticTraceBase.ActivityId = Guid.Empty;
					rpc.ActivityId = Guid.Empty;
				}
			}
			this.TerminateIfNecessary(ref rpc);
			if (TD.ServiceChannelCallStopIsEnabled())
			{
				string destination = string.Empty;
				if (this.RemoteAddress != null && this.RemoteAddress.Uri != null)
				{
					destination = this.RemoteAddress.Uri.AbsoluteUri;
				}
				TD.ServiceChannelCallStop(rpc.EventTraceActivity, rpc.Action, this.clientRuntime.ContractName, destination);
			}
		}

		// Token: 0x0600604C RID: 24652 RVA: 0x0016796C File Offset: 0x00165B6C
		private void TerminateIfNecessary(ref ProxyRpc rpc)
		{
			if (rpc.Operation.IsTerminating)
			{
				this.terminatingOperationName = rpc.Operation.Name;
				TerminatingOperationBehavior.AfterReply(ref rpc);
			}
		}

		// Token: 0x0600604D RID: 24653 RVA: 0x00167994 File Offset: 0x00165B94
		private void ThrowIfFaultUnderstood(Message reply, MessageFault fault, string action, MessageVersion version, FaultConverter faultConverter)
		{
			Exception exception;
			if (faultConverter != null && faultConverter.TryCreateException(reply, fault, out exception))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(exception);
			}
			bool flag;
			bool flag2;
			FaultCode faultCode;
			if (version.Envelope == EnvelopeVersion.Soap11)
			{
				flag = true;
				flag2 = true;
				faultCode = fault.Code;
			}
			else
			{
				flag = fault.Code.IsSenderFault;
				flag2 = fault.Code.IsReceiverFault;
				faultCode = fault.Code.SubCode;
			}
			if (faultCode == null)
			{
				return;
			}
			if (faultCode.Namespace == null)
			{
				return;
			}
			if (flag)
			{
				if (string.Compare(faultCode.Namespace, "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/dispatcher", StringComparison.Ordinal) == 0)
				{
					if (string.Compare(faultCode.Name, "SessionTerminated", StringComparison.Ordinal) == 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new ChannelTerminatedException(fault.Reason.GetMatchingTranslation(CultureInfo.CurrentCulture).Text));
					}
					if (string.Compare(faultCode.Name, "TransactionAborted", StringComparison.Ordinal) == 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new ProtocolException(fault.Reason.GetMatchingTranslation(CultureInfo.CurrentCulture).Text));
					}
				}
				if (string.Compare(faultCode.Namespace, SecurityVersion.Default.HeaderNamespace.Value, StringComparison.Ordinal) == 0 && string.Compare(faultCode.Name, SecurityVersion.Default.FailedAuthenticationFaultCode.Value, StringComparison.Ordinal) == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new SecurityAccessDeniedException(fault.Reason.GetMatchingTranslation(CultureInfo.CurrentCulture).Text));
				}
			}
			if (flag2 && string.Compare(faultCode.Namespace, "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/dispatcher", StringComparison.Ordinal) == 0)
			{
				if (string.Compare(faultCode.Name, "InternalServiceFault", StringComparison.Ordinal) == 0)
				{
					if (this.HasSession)
					{
						base.Fault();
					}
					if (fault.HasDetail)
					{
						ExceptionDetail detail = fault.GetDetail<ExceptionDetail>();
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new FaultException<ExceptionDetail>(detail, fault.Reason, fault.Code, action));
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new FaultException(fault, action));
				}
				else if (string.Compare(faultCode.Name, "DeserializationFailed", StringComparison.Ordinal) == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new ProtocolException(fault.Reason.GetMatchingTranslation(CultureInfo.CurrentCulture).Text));
				}
			}
		}

		// Token: 0x0600604E RID: 24654 RVA: 0x00167BA8 File Offset: 0x00165DA8
		private void ThrowIfIdleAborted(ProxyOperationRuntime operation)
		{
			if (this.idleManager != null && this.idleManager.DidIdleAbort)
			{
				string @string = SR.GetString("SFxServiceChannelIdleAborted", new object[]
				{
					operation.Name
				});
				Exception exception = new CommunicationObjectAbortedException(@string);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
			}
		}

		// Token: 0x0600604F RID: 24655 RVA: 0x00167BF8 File Offset: 0x00165DF8
		private void ThrowIfIsConnectionOpened(ProxyOperationRuntime operation)
		{
			if (operation.IsSessionOpenNotificationEnabled)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxServiceChannelCannotBeCalledBecauseIsSessionOpenNotificationEnabled", new object[]
				{
					operation.Name,
					"Action",
					"http://schemas.microsoft.com/2011/02/session/onopen",
					"Open"
				})));
			}
		}

		// Token: 0x06006050 RID: 24656 RVA: 0x00167C50 File Offset: 0x00165E50
		private void ThrowIfInitializationUINotCalled()
		{
			if (!this.didInteractiveInitialization && this.ClientRuntime.InteractiveChannelInitializers.Count > 0)
			{
				IInteractiveChannelInitializer interactiveChannelInitializer = this.ClientRuntime.InteractiveChannelInitializers[0];
				string @string = SR.GetString("SFxInitializationUINotCalled", new object[]
				{
					interactiveChannelInitializer.GetType().ToString()
				});
				Exception exception = new InvalidOperationException(@string);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
			}
		}

		// Token: 0x06006051 RID: 24657 RVA: 0x00167CBC File Offset: 0x00165EBC
		private void ThrowIfDisallowedInitializationUI()
		{
			if (!this.allowInitializationUI)
			{
				this.ThrowIfDisallowedInitializationUICore();
			}
		}

		// Token: 0x06006052 RID: 24658 RVA: 0x00167CCC File Offset: 0x00165ECC
		private void ThrowIfDisallowedInitializationUICore()
		{
			if (this.ClientRuntime.InteractiveChannelInitializers.Count > 0)
			{
				IInteractiveChannelInitializer interactiveChannelInitializer = this.ClientRuntime.InteractiveChannelInitializers[0];
				string @string = SR.GetString("SFxInitializationUIDisallowed", new object[]
				{
					interactiveChannelInitializer.GetType().ToString()
				});
				Exception exception = new InvalidOperationException(@string);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
			}
		}

		// Token: 0x06006053 RID: 24659 RVA: 0x00167D30 File Offset: 0x00165F30
		private void ThrowIfOpening()
		{
			if (base.State == CommunicationState.Opening)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxCannotCallAutoOpenWhenExplicitOpenCalled")));
			}
		}

		// Token: 0x06006054 RID: 24660 RVA: 0x00167D55 File Offset: 0x00165F55
		internal void IncrementActivity()
		{
			Interlocked.Increment(ref this.activityCount);
		}

		// Token: 0x06006055 RID: 24661 RVA: 0x00167D64 File Offset: 0x00165F64
		private void OnInnerChannelFaulted(object sender, EventArgs e)
		{
			base.Fault();
			if (this.HasSession)
			{
				DispatchRuntime dispatchRuntime = this.ClientRuntime.DispatchRuntime;
				if (dispatchRuntime != null)
				{
					dispatchRuntime.GetRuntime().InputSessionFaulted(this);
				}
			}
			if (this.autoClose && !this.IsClient)
			{
				base.Abort();
			}
		}

		// Token: 0x06006056 RID: 24662 RVA: 0x00167DB0 File Offset: 0x00165FB0
		private void AddMessageProperties(Message message, OperationContext context)
		{
			if (this.allowOutputBatching)
			{
				message.Properties.AllowOutputBatching = true;
			}
			if (context != null && context.InternalServiceChannel == this)
			{
				if (!context.OutgoingMessageVersion.IsMatch(message.Headers.MessageVersion))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxVersionMismatchInOperationContextAndMessage2", new object[]
					{
						context.OutgoingMessageVersion,
						message.Headers.MessageVersion
					})));
				}
				if (context.HasOutgoingMessageHeaders)
				{
					message.Headers.CopyHeadersFrom(context.OutgoingMessageHeaders);
				}
				if (context.HasOutgoingMessageProperties)
				{
					message.Properties.CopyProperties(context.OutgoingMessageProperties);
				}
			}
		}

		// Token: 0x06006057 RID: 24663 RVA: 0x00167E64 File Offset: 0x00166064
		public void Send(Message message)
		{
			this.Send(message, this.OperationTimeout);
		}

		// Token: 0x06006058 RID: 24664 RVA: 0x00167E74 File Offset: 0x00166074
		public void Send(Message message, TimeSpan timeout)
		{
			ProxyOperationRuntime unhandledProxyOperation = this.UnhandledProxyOperation;
			this.Call(message.Headers.Action, true, unhandledProxyOperation, new object[]
			{
				message
			}, EmptyArray<object>.Instance, timeout);
		}

		// Token: 0x06006059 RID: 24665 RVA: 0x00167EAC File Offset: 0x001660AC
		public IAsyncResult BeginSend(Message message, AsyncCallback callback, object state)
		{
			return this.BeginSend(message, this.OperationTimeout, callback, state);
		}

		// Token: 0x0600605A RID: 24666 RVA: 0x00167EC0 File Offset: 0x001660C0
		public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			ProxyOperationRuntime unhandledProxyOperation = this.UnhandledProxyOperation;
			return this.BeginCall(message.Headers.Action, true, unhandledProxyOperation, new object[]
			{
				message
			}, timeout, callback, state);
		}

		// Token: 0x0600605B RID: 24667 RVA: 0x00167EF5 File Offset: 0x001660F5
		public void EndSend(IAsyncResult result)
		{
			this.EndCall("*", EmptyArray<object>.Instance, result);
		}

		// Token: 0x0600605C RID: 24668 RVA: 0x00167F09 File Offset: 0x00166109
		public Message Request(Message message)
		{
			return this.Request(message, this.OperationTimeout);
		}

		// Token: 0x0600605D RID: 24669 RVA: 0x00167F18 File Offset: 0x00166118
		public Message Request(Message message, TimeSpan timeout)
		{
			ProxyOperationRuntime unhandledProxyOperation = this.UnhandledProxyOperation;
			return (Message)this.Call(message.Headers.Action, false, unhandledProxyOperation, new object[]
			{
				message
			}, EmptyArray<object>.Instance, timeout);
		}

		// Token: 0x0600605E RID: 24670 RVA: 0x00167F54 File Offset: 0x00166154
		public IAsyncResult BeginRequest(Message message, AsyncCallback callback, object state)
		{
			return this.BeginRequest(message, this.OperationTimeout, callback, state);
		}

		// Token: 0x0600605F RID: 24671 RVA: 0x00167F68 File Offset: 0x00166168
		public IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			ProxyOperationRuntime unhandledProxyOperation = this.UnhandledProxyOperation;
			return this.BeginCall(message.Headers.Action, false, unhandledProxyOperation, new object[]
			{
				message
			}, timeout, callback, state);
		}

		// Token: 0x06006060 RID: 24672 RVA: 0x00167F9D File Offset: 0x0016619D
		public Message EndRequest(IAsyncResult result)
		{
			return (Message)this.EndCall("*", EmptyArray<object>.Instance, result);
		}

		// Token: 0x06006061 RID: 24673 RVA: 0x00167FB8 File Offset: 0x001661B8
		protected override void OnAbort()
		{
			if (this.idleManager != null)
			{
				this.idleManager.CancelTimer();
			}
			this.binder.Abort();
			if (this.factory != null)
			{
				this.factory.ChannelDisposed(this);
			}
			if (this.closeFactory && this.factory != null)
			{
				this.factory.Abort();
			}
			this.CleanupChannelCollections();
			ServiceThrottle serviceThrottle = this.serviceThrottle;
			if (serviceThrottle != null)
			{
				serviceThrottle.DeactivateChannel();
			}
			if (this.instanceContext != null && this.HasSession && this.instanceContext.HasTransaction)
			{
				this.instanceContext.Transaction.CompletePendingTransaction(this.instanceContext.Transaction.Attached, new Exception());
			}
			this.DecrementBusyCount();
		}

		// Token: 0x06006062 RID: 24674 RVA: 0x00168070 File Offset: 0x00166270
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (this.idleManager != null)
			{
				this.idleManager.CancelTimer();
			}
			if (this.factory != null)
			{
				this.factory.ChannelDisposed(this);
			}
			if (this.InstanceContext != null && this.InstanceContext.HasTransaction)
			{
				this.InstanceContext.CompleteAttachedTransaction();
			}
			if (this.closeBinder)
			{
				if (this.closeFactory)
				{
					return new ChainedAsyncResult(timeout, callback, state, new ChainedBeginHandler(this.InnerChannel.BeginClose), new ChainedEndHandler(this.InnerChannel.EndClose), new ChainedBeginHandler(this.factory.BeginClose), new ChainedEndHandler(this.factory.EndClose));
				}
				return this.InnerChannel.BeginClose(timeout, callback, state);
			}
			else
			{
				if (this.closeFactory)
				{
					return this.factory.BeginClose(timeout, callback, state);
				}
				return new CompletedAsyncResult(callback, state);
			}
		}

		// Token: 0x06006063 RID: 24675 RVA: 0x00168154 File Offset: 0x00166354
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.ThrowIfDisallowedInitializationUI();
			this.ThrowIfInitializationUINotCalled();
			if (this.autoOpenManager == null)
			{
				this.explicitlyOpened = true;
			}
			if (this.HasSession && !this.IsClient)
			{
				this.IncrementBusyCount();
			}
			this.TraceChannelOpenStarted();
			if (this.openBinder)
			{
				return this.InnerChannel.BeginOpen(timeout, callback, state);
			}
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06006064 RID: 24676 RVA: 0x001681B8 File Offset: 0x001663B8
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.idleManager != null)
			{
				this.idleManager.CancelTimer();
			}
			if (this.factory != null)
			{
				this.factory.ChannelDisposed(this);
			}
			if (this.InstanceContext != null && this.InstanceContext.HasTransaction)
			{
				this.InstanceContext.CompleteAttachedTransaction();
			}
			if (this.closeBinder)
			{
				this.InnerChannel.Close(timeoutHelper.RemainingTime());
			}
			if (this.closeFactory)
			{
				this.factory.Close(timeoutHelper.RemainingTime());
			}
			this.CleanupChannelCollections();
			ServiceThrottle serviceThrottle = this.serviceThrottle;
			if (serviceThrottle != null)
			{
				serviceThrottle.DeactivateChannel();
			}
			this.DecrementBusyCount();
		}

		// Token: 0x06006065 RID: 24677 RVA: 0x00168264 File Offset: 0x00166464
		protected override void OnEndClose(IAsyncResult result)
		{
			if (this.closeBinder)
			{
				if (this.closeFactory)
				{
					ChainedAsyncResult.End(result);
				}
				else
				{
					this.InnerChannel.EndClose(result);
				}
			}
			else if (this.closeFactory)
			{
				this.factory.EndClose(result);
			}
			else
			{
				CompletedAsyncResult.End(result);
			}
			this.CleanupChannelCollections();
			ServiceThrottle serviceThrottle = this.serviceThrottle;
			if (serviceThrottle != null)
			{
				serviceThrottle.DeactivateChannel();
			}
			this.DecrementBusyCount();
		}

		// Token: 0x06006066 RID: 24678 RVA: 0x001682CF File Offset: 0x001664CF
		protected override void OnEndOpen(IAsyncResult result)
		{
			if (this.openBinder)
			{
				this.InnerChannel.EndOpen(result);
			}
			else
			{
				CompletedAsyncResult.End(result);
			}
			this.BindDuplexCallbacks();
			this.CompletedIOOperation();
			this.TraceChannelOpenCompleted();
		}

		// Token: 0x06006067 RID: 24679 RVA: 0x00168300 File Offset: 0x00166500
		protected override void OnOpen(TimeSpan timeout)
		{
			this.ThrowIfDisallowedInitializationUI();
			this.ThrowIfInitializationUINotCalled();
			if (this.autoOpenManager == null)
			{
				this.explicitlyOpened = true;
			}
			if (this.HasSession && !this.IsClient)
			{
				this.IncrementBusyCount();
			}
			this.TraceChannelOpenStarted();
			if (this.openBinder)
			{
				this.InnerChannel.Open(timeout);
			}
			this.BindDuplexCallbacks();
			this.CompletedIOOperation();
			this.TraceChannelOpenCompleted();
		}

		// Token: 0x06006068 RID: 24680 RVA: 0x0016836C File Offset: 0x0016656C
		private void CleanupChannelCollections()
		{
			if (!this.hasCleanedUpChannelCollections)
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (!this.hasCleanedUpChannelCollections)
					{
						if (this.InstanceContext != null && this.InstanceContext.State != CommunicationState.Closed && this.InstanceContext.State != CommunicationState.Faulted)
						{
							try
							{
								this.InstanceContext.OutgoingChannels.Remove((IChannel)this.proxy);
							}
							catch (CommunicationException)
							{
							}
							catch (ObjectDisposedException)
							{
							}
						}
						if (this.WmiInstanceContext != null)
						{
							this.WmiInstanceContext.WmiChannels.Remove((IChannel)this.proxy);
						}
						this.hasCleanedUpChannelCollections = true;
					}
				}
			}
		}

		// Token: 0x06006069 RID: 24681 RVA: 0x00168444 File Offset: 0x00166644
		private void IncrementBusyCount()
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (base.State == CommunicationState.Opening)
				{
					AspNetEnvironment.Current.IncrementBusyCount();
					if (AspNetEnvironment.Current.TraceIncrementBusyCountIsEnabled())
					{
						AspNetEnvironment.Current.TraceIncrementBusyCount(base.GetType().FullName);
					}
					this.hasIncrementedBusyCount = true;
				}
			}
		}

		// Token: 0x0600606A RID: 24682 RVA: 0x001684BC File Offset: 0x001666BC
		private void DecrementBusyCount()
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (this.hasIncrementedBusyCount)
				{
					AspNetEnvironment.Current.DecrementBusyCount();
					if (AspNetEnvironment.Current.TraceDecrementBusyCountIsEnabled())
					{
						AspNetEnvironment.Current.TraceDecrementBusyCount(base.GetType().FullName);
					}
					this.hasIncrementedBusyCount = false;
				}
			}
		}

		// Token: 0x1700172A RID: 5930
		// (get) Token: 0x0600606B RID: 24683 RVA: 0x00168530 File Offset: 0x00166730
		// (set) Token: 0x0600606C RID: 24684 RVA: 0x00168538 File Offset: 0x00166738
		bool IDuplexContextChannel.AutomaticInputSessionShutdown
		{
			get
			{
				return this.autoClose;
			}
			set
			{
				this.autoClose = value;
			}
		}

		// Token: 0x1700172B RID: 5931
		// (get) Token: 0x0600606D RID: 24685 RVA: 0x00168541 File Offset: 0x00166741
		// (set) Token: 0x0600606E RID: 24686 RVA: 0x00168549 File Offset: 0x00166749
		bool IClientChannel.AllowInitializationUI
		{
			get
			{
				return this.allowInitializationUI;
			}
			set
			{
				base.ThrowIfDisposedOrImmutable();
				this.allowInitializationUI = value;
			}
		}

		// Token: 0x1700172C RID: 5932
		// (get) Token: 0x0600606F RID: 24687 RVA: 0x00168558 File Offset: 0x00166758
		// (set) Token: 0x06006070 RID: 24688 RVA: 0x00168560 File Offset: 0x00166760
		bool IContextChannel.AllowOutputBatching
		{
			get
			{
				return this.allowOutputBatching;
			}
			set
			{
				this.allowOutputBatching = value;
			}
		}

		// Token: 0x1700172D RID: 5933
		// (get) Token: 0x06006071 RID: 24689 RVA: 0x00168569 File Offset: 0x00166769
		bool IClientChannel.DidInteractiveInitialization
		{
			get
			{
				return this.didInteractiveInitialization;
			}
		}

		// Token: 0x06006072 RID: 24690 RVA: 0x00168571 File Offset: 0x00166771
		IAsyncResult IDuplexContextChannel.BeginCloseOutputSession(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.GetDuplexSessionOrThrow().BeginCloseOutputSession(timeout, callback, state);
		}

		// Token: 0x06006073 RID: 24691 RVA: 0x00168581 File Offset: 0x00166781
		void IDuplexContextChannel.EndCloseOutputSession(IAsyncResult result)
		{
			this.GetDuplexSessionOrThrow().EndCloseOutputSession(result);
		}

		// Token: 0x06006074 RID: 24692 RVA: 0x0016858F File Offset: 0x0016678F
		void IDuplexContextChannel.CloseOutputSession(TimeSpan timeout)
		{
			this.GetDuplexSessionOrThrow().CloseOutputSession(timeout);
		}

		// Token: 0x06006075 RID: 24693 RVA: 0x001685A0 File Offset: 0x001667A0
		private IDuplexSession GetDuplexSessionOrThrow()
		{
			if (this.InnerChannel == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("channelIsNotAvailable0")));
			}
			ISessionChannel<IDuplexSession> sessionChannel = this.InnerChannel as ISessionChannel<IDuplexSession>;
			if (sessionChannel == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("channelDoesNotHaveADuplexSession0")));
			}
			return sessionChannel.Session;
		}

		// Token: 0x1700172E RID: 5934
		// (get) Token: 0x06006076 RID: 24694 RVA: 0x00168600 File Offset: 0x00166800
		IExtensionCollection<IContextChannel> IExtensibleObject<IContextChannel>.Extensions
		{
			get
			{
				object thisLock = base.ThisLock;
				IExtensionCollection<IContextChannel> result;
				lock (thisLock)
				{
					if (this.extensions == null)
					{
						this.extensions = new ExtensionCollection<IContextChannel>((IContextChannel)this.Proxy, base.ThisLock);
					}
					result = this.extensions;
				}
				return result;
			}
		}

		// Token: 0x1700172F RID: 5935
		// (get) Token: 0x06006077 RID: 24695 RVA: 0x00168668 File Offset: 0x00166868
		// (set) Token: 0x06006078 RID: 24696 RVA: 0x00168670 File Offset: 0x00166870
		InstanceContext IDuplexContextChannel.CallbackInstance
		{
			get
			{
				return this.instanceContext;
			}
			set
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (this.instanceContext != null)
					{
						this.instanceContext.OutgoingChannels.Remove((IChannel)this.proxy);
					}
					this.instanceContext = value;
					if (this.instanceContext != null)
					{
						this.instanceContext.OutgoingChannels.Add((IChannel)this.proxy);
					}
				}
			}
		}

		// Token: 0x17001730 RID: 5936
		// (get) Token: 0x06006079 RID: 24697 RVA: 0x001686F8 File Offset: 0x001668F8
		IInputSession IContextChannel.InputSession
		{
			get
			{
				if (this.InnerChannel != null)
				{
					ISessionChannel<IInputSession> sessionChannel = this.InnerChannel as ISessionChannel<IInputSession>;
					if (sessionChannel != null)
					{
						return sessionChannel.Session;
					}
					ISessionChannel<IDuplexSession> sessionChannel2 = this.InnerChannel as ISessionChannel<IDuplexSession>;
					if (sessionChannel2 != null)
					{
						return sessionChannel2.Session;
					}
				}
				return null;
			}
		}

		// Token: 0x17001731 RID: 5937
		// (get) Token: 0x0600607A RID: 24698 RVA: 0x0016873C File Offset: 0x0016693C
		IOutputSession IContextChannel.OutputSession
		{
			get
			{
				if (this.InnerChannel != null)
				{
					ISessionChannel<IOutputSession> sessionChannel = this.InnerChannel as ISessionChannel<IOutputSession>;
					if (sessionChannel != null)
					{
						return sessionChannel.Session;
					}
					ISessionChannel<IDuplexSession> sessionChannel2 = this.InnerChannel as ISessionChannel<IDuplexSession>;
					if (sessionChannel2 != null)
					{
						return sessionChannel2.Session;
					}
				}
				return null;
			}
		}

		// Token: 0x17001732 RID: 5938
		// (get) Token: 0x0600607B RID: 24699 RVA: 0x00168780 File Offset: 0x00166980
		string IContextChannel.SessionId
		{
			get
			{
				if (this.InnerChannel != null)
				{
					ISessionChannel<IInputSession> sessionChannel = this.InnerChannel as ISessionChannel<IInputSession>;
					if (sessionChannel != null)
					{
						return sessionChannel.Session.Id;
					}
					ISessionChannel<IOutputSession> sessionChannel2 = this.InnerChannel as ISessionChannel<IOutputSession>;
					if (sessionChannel2 != null)
					{
						return sessionChannel2.Session.Id;
					}
					ISessionChannel<IDuplexSession> sessionChannel3 = this.InnerChannel as ISessionChannel<IDuplexSession>;
					if (sessionChannel3 != null)
					{
						return sessionChannel3.Session.Id;
					}
				}
				return null;
			}
		}

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x0600607C RID: 24700 RVA: 0x001687E8 File Offset: 0x001669E8
		// (remove) Token: 0x0600607D RID: 24701 RVA: 0x0016883C File Offset: 0x00166A3C
		event EventHandler<UnknownMessageReceivedEventArgs> IClientChannel.UnknownMessageReceived
		{
			add
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					this.unknownMessageReceived = (EventHandler<UnknownMessageReceivedEventArgs>)Delegate.Combine(this.unknownMessageReceived, value);
				}
			}
			remove
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					this.unknownMessageReceived = (EventHandler<UnknownMessageReceivedEventArgs>)Delegate.Remove(this.unknownMessageReceived, value);
				}
			}
		}

		// Token: 0x0600607E RID: 24702 RVA: 0x00168890 File Offset: 0x00166A90
		public void DisplayInitializationUI()
		{
			this.ThrowIfDisallowedInitializationUI();
			if (this.autoDisplayUIManager == null)
			{
				this.explicitlyOpened = true;
			}
			this.ClientRuntime.GetRuntime().DisplayInitializationUI(this);
			this.didInteractiveInitialization = true;
		}

		// Token: 0x0600607F RID: 24703 RVA: 0x001688BF File Offset: 0x00166ABF
		public IAsyncResult BeginDisplayInitializationUI(AsyncCallback callback, object state)
		{
			this.ThrowIfDisallowedInitializationUI();
			if (this.autoDisplayUIManager == null)
			{
				this.explicitlyOpened = true;
			}
			return this.ClientRuntime.GetRuntime().BeginDisplayInitializationUI(this, callback, state);
		}

		// Token: 0x06006080 RID: 24704 RVA: 0x001688E9 File Offset: 0x00166AE9
		public void EndDisplayInitializationUI(IAsyncResult result)
		{
			this.ClientRuntime.GetRuntime().EndDisplayInitializationUI(result);
			this.didInteractiveInitialization = true;
		}

		// Token: 0x06006081 RID: 24705 RVA: 0x00168903 File Offset: 0x00166B03
		void IDisposable.Dispose()
		{
			base.Close();
		}

		// Token: 0x06006082 RID: 24706 RVA: 0x0016890C File Offset: 0x00166B0C
		private void TraceChannelOpenStarted()
		{
			if (TD.ClientChannelOpenStartIsEnabled() && this.endpointDispatcher == null)
			{
				TD.ClientChannelOpenStart(this.EventActivity);
			}
			else if (TD.ServiceChannelOpenStartIsEnabled())
			{
				TD.ServiceChannelOpenStart(this.EventActivity);
			}
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>(4);
				bool flag = false;
				DispatchRuntime dispatchRuntime = this.DispatchRuntime;
				if (dispatchRuntime != null)
				{
					if (dispatchRuntime.Type != null)
					{
						dictionary["ServiceType"] = dispatchRuntime.Type.AssemblyQualifiedName;
					}
					dictionary["ContractNamespace"] = this.clientRuntime.ContractNamespace;
					dictionary["ContractName"] = this.clientRuntime.ContractName;
					flag = true;
				}
				if (this.endpointDispatcher != null && this.endpointDispatcher.ListenUri != null)
				{
					dictionary["Uri"] = this.endpointDispatcher.ListenUri.ToString();
					flag = true;
				}
				if (flag)
				{
					TraceUtility.TraceEvent(TraceEventType.Information, 524331, SR.GetString("TraceCodeServiceChannelLifetime"), new DictionaryTraceRecord(dictionary), this, null);
				}
			}
		}

		// Token: 0x06006083 RID: 24707 RVA: 0x00168A0E File Offset: 0x00166C0E
		private void TraceChannelOpenCompleted()
		{
			if (this.endpointDispatcher == null && TD.ClientChannelOpenStopIsEnabled())
			{
				TD.ClientChannelOpenStop(this.EventActivity);
				return;
			}
			if (TD.ServiceChannelOpenStopIsEnabled())
			{
				TD.ServiceChannelOpenStop(this.EventActivity);
			}
		}

		// Token: 0x06006084 RID: 24708 RVA: 0x00168A3D File Offset: 0x00166C3D
		private static void TraceServiceChannelCallStart(EventTraceActivity eventTraceActivity, bool isSynchronous)
		{
			if (TD.ServiceChannelCallStartIsEnabled())
			{
				if (isSynchronous)
				{
					TD.ServiceChannelCallStart(eventTraceActivity);
					return;
				}
				TD.ServiceChannelBeginCallStart(eventTraceActivity);
			}
		}

		// Token: 0x0400386A RID: 14442
		private int activityCount;

		// Token: 0x0400386B RID: 14443
		private bool allowInitializationUI = true;

		// Token: 0x0400386C RID: 14444
		private bool allowOutputBatching;

		// Token: 0x0400386D RID: 14445
		private bool autoClose = true;

		// Token: 0x0400386E RID: 14446
		private ServiceChannel.CallOnceManager autoDisplayUIManager;

		// Token: 0x0400386F RID: 14447
		private ServiceChannel.CallOnceManager autoOpenManager;

		// Token: 0x04003870 RID: 14448
		private readonly IChannelBinder binder;

		// Token: 0x04003871 RID: 14449
		private readonly ChannelDispatcher channelDispatcher;

		// Token: 0x04003872 RID: 14450
		private ClientRuntime clientRuntime;

		// Token: 0x04003873 RID: 14451
		private readonly bool closeBinder = true;

		// Token: 0x04003874 RID: 14452
		private bool closeFactory;

		// Token: 0x04003875 RID: 14453
		private bool didInteractiveInitialization;

		// Token: 0x04003876 RID: 14454
		private bool doneReceiving;

		// Token: 0x04003877 RID: 14455
		private EndpointDispatcher endpointDispatcher;

		// Token: 0x04003878 RID: 14456
		private bool explicitlyOpened;

		// Token: 0x04003879 RID: 14457
		private ExtensionCollection<IContextChannel> extensions;

		// Token: 0x0400387A RID: 14458
		private readonly ServiceChannelFactory factory;

		// Token: 0x0400387B RID: 14459
		private readonly bool hasSession;

		// Token: 0x0400387C RID: 14460
		private readonly ServiceChannel.SessionIdleManager idleManager;

		// Token: 0x0400387D RID: 14461
		private InstanceContext instanceContext;

		// Token: 0x0400387E RID: 14462
		private ServiceThrottle instanceContextServiceThrottle;

		// Token: 0x0400387F RID: 14463
		private bool isPending;

		// Token: 0x04003880 RID: 14464
		private readonly bool isReplyChannel;

		// Token: 0x04003881 RID: 14465
		private EndpointAddress localAddress;

		// Token: 0x04003882 RID: 14466
		private readonly MessageVersion messageVersion;

		// Token: 0x04003883 RID: 14467
		private readonly bool openBinder;

		// Token: 0x04003884 RID: 14468
		private TimeSpan operationTimeout;

		// Token: 0x04003885 RID: 14469
		private object proxy;

		// Token: 0x04003886 RID: 14470
		private ServiceThrottle serviceThrottle;

		// Token: 0x04003887 RID: 14471
		private string terminatingOperationName;

		// Token: 0x04003888 RID: 14472
		private InstanceContext wmiInstanceContext;

		// Token: 0x04003889 RID: 14473
		private bool hasChannelStartedAutoClosing;

		// Token: 0x0400388A RID: 14474
		private bool hasIncrementedBusyCount;

		// Token: 0x0400388B RID: 14475
		private bool hasCleanedUpChannelCollections;

		// Token: 0x0400388C RID: 14476
		private EventTraceActivity eventActivity;

		// Token: 0x0400388D RID: 14477
		private EventHandler<UnknownMessageReceivedEventArgs> unknownMessageReceived;

		// Token: 0x02000E1E RID: 3614
		private class SendAsyncResult : TraceAsyncResult
		{
			// Token: 0x06008211 RID: 33297 RVA: 0x001E1ABA File Offset: 0x001DFCBA
			internal SendAsyncResult(ServiceChannel channel, ProxyOperationRuntime operation, string action, object[] inputParameters, bool isOneWay, TimeSpan timeout, AsyncCallback userCallback, object userState) : base(userCallback, userState)
			{
				this.Rpc = new ProxyRpc(channel, operation, action, inputParameters, timeout);
				this.isOneWay = isOneWay;
				this.operation = operation;
				this.operationContext = OperationContext.Current;
			}

			// Token: 0x06008212 RID: 33298 RVA: 0x001E1AF4 File Offset: 0x001DFCF4
			internal void Begin()
			{
				this.Rpc.Channel.PrepareCall(this.operation, this.isOneWay, ref this.Rpc);
				if (this.Rpc.Channel.explicitlyOpened)
				{
					this.Rpc.Channel.ThrowIfOpening();
					this.Rpc.Channel.ThrowIfDisposedOrNotOpen();
					this.StartSend(true);
					return;
				}
				this.StartEnsureInteractiveInit();
			}

			// Token: 0x06008213 RID: 33299 RVA: 0x001E1B64 File Offset: 0x001DFD64
			private void StartEnsureInteractiveInit()
			{
				IAsyncResult asyncResult = this.Rpc.Channel.BeginEnsureDisplayUI(ServiceChannel.SendAsyncResult.ensureInteractiveInitCallback, this);
				if (asyncResult.CompletedSynchronously)
				{
					this.FinishEnsureInteractiveInit(asyncResult, true);
				}
			}

			// Token: 0x06008214 RID: 33300 RVA: 0x001E1B98 File Offset: 0x001DFD98
			private static void EnsureInteractiveInitCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					((ServiceChannel.SendAsyncResult)result.AsyncState).FinishEnsureInteractiveInit(result, false);
				}
			}

			// Token: 0x06008215 RID: 33301 RVA: 0x001E1BB4 File Offset: 0x001DFDB4
			private void FinishEnsureInteractiveInit(IAsyncResult result, bool completedSynchronously)
			{
				Exception ex = null;
				try
				{
					this.Rpc.Channel.EndEnsureDisplayUI(result);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2) || completedSynchronously)
					{
						throw;
					}
					ex = ex2;
				}
				if (ex != null)
				{
					this.CallComplete(completedSynchronously, ex);
					return;
				}
				this.StartEnsureOpen(completedSynchronously);
			}

			// Token: 0x06008216 RID: 33302 RVA: 0x001E1C0C File Offset: 0x001DFE0C
			private void StartEnsureOpen(bool completedSynchronously)
			{
				TimeSpan timeout = this.Rpc.TimeoutHelper.RemainingTime();
				IAsyncResult asyncResult = null;
				Exception ex = null;
				try
				{
					asyncResult = this.Rpc.Channel.BeginEnsureOpened(timeout, ServiceChannel.SendAsyncResult.ensureOpenCallback, this);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2) || completedSynchronously)
					{
						throw;
					}
					ex = ex2;
				}
				if (ex != null)
				{
					this.CallComplete(completedSynchronously, ex);
					return;
				}
				if (asyncResult.CompletedSynchronously)
				{
					this.FinishEnsureOpen(asyncResult, completedSynchronously);
				}
			}

			// Token: 0x06008217 RID: 33303 RVA: 0x001E1C8C File Offset: 0x001DFE8C
			private static void EnsureOpenCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					((ServiceChannel.SendAsyncResult)result.AsyncState).FinishEnsureOpen(result, false);
				}
			}

			// Token: 0x06008218 RID: 33304 RVA: 0x001E1CA8 File Offset: 0x001DFEA8
			private void FinishEnsureOpen(IAsyncResult result, bool completedSynchronously)
			{
				Exception ex = null;
				using (ServiceModelActivity.BoundOperation(this.Rpc.Activity))
				{
					try
					{
						this.Rpc.Channel.EndEnsureOpened(result);
					}
					catch (Exception ex2)
					{
						if (Fx.IsFatal(ex2) || completedSynchronously)
						{
							throw;
						}
						ex = ex2;
					}
					if (ex != null)
					{
						this.CallComplete(completedSynchronously, ex);
					}
					else
					{
						this.StartSend(completedSynchronously);
					}
				}
			}

			// Token: 0x06008219 RID: 33305 RVA: 0x001E1D28 File Offset: 0x001DFF28
			private void StartSend(bool completedSynchronously)
			{
				TimeSpan timeout = this.Rpc.TimeoutHelper.RemainingTime();
				IAsyncResult asyncResult = null;
				Exception ex = null;
				try
				{
					ConcurrencyBehavior.UnlockInstanceBeforeCallout(this.operationContext);
					if (this.isOneWay)
					{
						asyncResult = this.Rpc.Channel.binder.BeginSend(this.Rpc.Request, timeout, ServiceChannel.SendAsyncResult.sendCallback, this);
					}
					else
					{
						asyncResult = this.Rpc.Channel.binder.BeginRequest(this.Rpc.Request, timeout, ServiceChannel.SendAsyncResult.sendCallback, this);
					}
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					if (completedSynchronously)
					{
						ConcurrencyBehavior.LockInstanceAfterCallout(this.operationContext);
						throw;
					}
					ex = ex2;
				}
				finally
				{
					ServiceChannel.CallOnceManager.SignalNextIfNonNull(this.Rpc.Channel.autoOpenManager);
				}
				if (ex != null)
				{
					this.CallComplete(completedSynchronously, ex);
					return;
				}
				if (asyncResult.CompletedSynchronously)
				{
					this.FinishSend(asyncResult, completedSynchronously);
				}
			}

			// Token: 0x0600821A RID: 33306 RVA: 0x001E1E24 File Offset: 0x001E0024
			private static void SendCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					((ServiceChannel.SendAsyncResult)result.AsyncState).FinishSend(result, false);
				}
			}

			// Token: 0x0600821B RID: 33307 RVA: 0x001E1E40 File Offset: 0x001E0040
			private void FinishSend(IAsyncResult result, bool completedSynchronously)
			{
				Exception exception = null;
				try
				{
					if (this.isOneWay)
					{
						this.Rpc.Channel.binder.EndSend(result);
					}
					else
					{
						this.Rpc.Reply = this.Rpc.Channel.binder.EndRequest(result);
						if (this.Rpc.Reply == null)
						{
							this.Rpc.Channel.ThrowIfFaulted();
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SFxServerDidNotReply")));
						}
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					if (completedSynchronously)
					{
						ConcurrencyBehavior.LockInstanceAfterCallout(this.operationContext);
						throw;
					}
					exception = ex;
				}
				this.CallComplete(completedSynchronously, exception);
			}

			// Token: 0x0600821C RID: 33308 RVA: 0x001E1F00 File Offset: 0x001E0100
			private void CallComplete(bool completedSynchronously, Exception exception)
			{
				this.Rpc.Channel.CompletedIOOperation();
				base.Complete(completedSynchronously, exception);
			}

			// Token: 0x0600821D RID: 33309 RVA: 0x001E1F1C File Offset: 0x001E011C
			public static void End(ServiceChannel.SendAsyncResult result)
			{
				try
				{
					AsyncResult.End<ServiceChannel.SendAsyncResult>(result);
				}
				finally
				{
					ConcurrencyBehavior.LockInstanceAfterCallout(result.operationContext);
				}
			}

			// Token: 0x040049E7 RID: 18919
			private readonly bool isOneWay;

			// Token: 0x040049E8 RID: 18920
			private readonly ProxyOperationRuntime operation;

			// Token: 0x040049E9 RID: 18921
			internal ProxyRpc Rpc;

			// Token: 0x040049EA RID: 18922
			private OperationContext operationContext;

			// Token: 0x040049EB RID: 18923
			private static AsyncCallback ensureInteractiveInitCallback = Fx.ThunkCallback(new AsyncCallback(ServiceChannel.SendAsyncResult.EnsureInteractiveInitCallback));

			// Token: 0x040049EC RID: 18924
			private static AsyncCallback ensureOpenCallback = Fx.ThunkCallback(new AsyncCallback(ServiceChannel.SendAsyncResult.EnsureOpenCallback));

			// Token: 0x040049ED RID: 18925
			private static AsyncCallback sendCallback = Fx.ThunkCallback(new AsyncCallback(ServiceChannel.SendAsyncResult.SendCallback));
		}

		// Token: 0x02000E1F RID: 3615
		private interface ICallOnce
		{
			// Token: 0x0600821F RID: 33311
			void Call(ServiceChannel channel, TimeSpan timeout);

			// Token: 0x06008220 RID: 33312
			IAsyncResult BeginCall(ServiceChannel channel, TimeSpan timeout, AsyncCallback callback, object state);

			// Token: 0x06008221 RID: 33313
			void EndCall(ServiceChannel channel, IAsyncResult result);
		}

		// Token: 0x02000E20 RID: 3616
		private class CallDisplayUIOnce : ServiceChannel.ICallOnce
		{
			// Token: 0x17001CAE RID: 7342
			// (get) Token: 0x06008222 RID: 33314 RVA: 0x001E1F9F File Offset: 0x001E019F
			internal static ServiceChannel.CallDisplayUIOnce Instance
			{
				get
				{
					if (ServiceChannel.CallDisplayUIOnce.instance == null)
					{
						ServiceChannel.CallDisplayUIOnce.instance = new ServiceChannel.CallDisplayUIOnce();
					}
					return ServiceChannel.CallDisplayUIOnce.instance;
				}
			}

			// Token: 0x06008223 RID: 33315 RVA: 0x001E1FB7 File Offset: 0x001E01B7
			[Conditional("DEBUG")]
			private void ValidateTimeoutIsMaxValue(TimeSpan timeout)
			{
				timeout != TimeSpan.MaxValue;
			}

			// Token: 0x06008224 RID: 33316 RVA: 0x001E1FC5 File Offset: 0x001E01C5
			void ServiceChannel.ICallOnce.Call(ServiceChannel channel, TimeSpan timeout)
			{
				channel.DisplayInitializationUI();
			}

			// Token: 0x06008225 RID: 33317 RVA: 0x001E1FCD File Offset: 0x001E01CD
			IAsyncResult ServiceChannel.ICallOnce.BeginCall(ServiceChannel channel, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return channel.BeginDisplayInitializationUI(callback, state);
			}

			// Token: 0x06008226 RID: 33318 RVA: 0x001E1FD8 File Offset: 0x001E01D8
			void ServiceChannel.ICallOnce.EndCall(ServiceChannel channel, IAsyncResult result)
			{
				channel.EndDisplayInitializationUI(result);
			}

			// Token: 0x040049EE RID: 18926
			private static ServiceChannel.CallDisplayUIOnce instance;
		}

		// Token: 0x02000E21 RID: 3617
		private class CallOpenOnce : ServiceChannel.ICallOnce
		{
			// Token: 0x17001CAF RID: 7343
			// (get) Token: 0x06008228 RID: 33320 RVA: 0x001E1FE9 File Offset: 0x001E01E9
			internal static ServiceChannel.CallOpenOnce Instance
			{
				get
				{
					if (ServiceChannel.CallOpenOnce.instance == null)
					{
						ServiceChannel.CallOpenOnce.instance = new ServiceChannel.CallOpenOnce();
					}
					return ServiceChannel.CallOpenOnce.instance;
				}
			}

			// Token: 0x06008229 RID: 33321 RVA: 0x001E2001 File Offset: 0x001E0201
			void ServiceChannel.ICallOnce.Call(ServiceChannel channel, TimeSpan timeout)
			{
				channel.Open(timeout);
			}

			// Token: 0x0600822A RID: 33322 RVA: 0x001E200A File Offset: 0x001E020A
			IAsyncResult ServiceChannel.ICallOnce.BeginCall(ServiceChannel channel, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return channel.BeginOpen(timeout, callback, state);
			}

			// Token: 0x0600822B RID: 33323 RVA: 0x001E2016 File Offset: 0x001E0216
			void ServiceChannel.ICallOnce.EndCall(ServiceChannel channel, IAsyncResult result)
			{
				channel.EndOpen(result);
			}

			// Token: 0x040049EF RID: 18927
			private static ServiceChannel.CallOpenOnce instance;
		}

		// Token: 0x02000E22 RID: 3618
		private class CallOnceManager
		{
			// Token: 0x0600822D RID: 33325 RVA: 0x001E2027 File Offset: 0x001E0227
			internal CallOnceManager(ServiceChannel channel, ServiceChannel.ICallOnce callOnce)
			{
				this.callOnce = callOnce;
				this.channel = channel;
				this.queue = new Queue<ServiceChannel.CallOnceManager.IWaiter>();
			}

			// Token: 0x17001CB0 RID: 7344
			// (get) Token: 0x0600822E RID: 33326 RVA: 0x001E204F File Offset: 0x001E024F
			private object ThisLock
			{
				get
				{
					return this;
				}
			}

			// Token: 0x0600822F RID: 33327 RVA: 0x001E2054 File Offset: 0x001E0254
			internal void CallOnce(TimeSpan timeout, ServiceChannel.CallOnceManager cascade)
			{
				ServiceChannel.CallOnceManager.SyncWaiter syncWaiter = null;
				bool flag = false;
				if (this.queue != null)
				{
					object thisLock = this.ThisLock;
					lock (thisLock)
					{
						if (this.queue != null)
						{
							if (this.isFirst)
							{
								flag = true;
								this.isFirst = false;
							}
							else
							{
								syncWaiter = new ServiceChannel.CallOnceManager.SyncWaiter(this);
								this.queue.Enqueue(syncWaiter);
							}
						}
					}
				}
				ServiceChannel.CallOnceManager.SignalNextIfNonNull(cascade);
				if (flag)
				{
					bool flag3 = true;
					try
					{
						this.callOnce.Call(this.channel, timeout);
						flag3 = false;
						return;
					}
					finally
					{
						if (flag3)
						{
							this.SignalNext();
						}
					}
				}
				if (syncWaiter != null)
				{
					syncWaiter.Wait(timeout);
				}
			}

			// Token: 0x06008230 RID: 33328 RVA: 0x001E2110 File Offset: 0x001E0310
			internal IAsyncResult BeginCallOnce(TimeSpan timeout, ServiceChannel.CallOnceManager cascade, AsyncCallback callback, object state)
			{
				ServiceChannel.CallOnceManager.AsyncWaiter asyncWaiter = null;
				bool flag = false;
				if (this.queue != null)
				{
					object thisLock = this.ThisLock;
					lock (thisLock)
					{
						if (this.queue != null)
						{
							if (this.isFirst)
							{
								flag = true;
								this.isFirst = false;
							}
							else
							{
								asyncWaiter = new ServiceChannel.CallOnceManager.AsyncWaiter(this, timeout, callback, state);
								this.queue.Enqueue(asyncWaiter);
							}
						}
					}
				}
				ServiceChannel.CallOnceManager.SignalNextIfNonNull(cascade);
				if (flag)
				{
					bool flag3 = true;
					try
					{
						IAsyncResult result = this.callOnce.BeginCall(this.channel, timeout, callback, state);
						flag3 = false;
						return result;
					}
					finally
					{
						if (flag3)
						{
							this.SignalNext();
						}
					}
				}
				if (asyncWaiter != null)
				{
					return asyncWaiter;
				}
				return new ServiceChannel.CallOnceCompletedAsyncResult(callback, state);
			}

			// Token: 0x06008231 RID: 33329 RVA: 0x001E21DC File Offset: 0x001E03DC
			internal void EndCallOnce(IAsyncResult result)
			{
				if (result is ServiceChannel.CallOnceCompletedAsyncResult)
				{
					ServiceChannel.CallOnceCompletedAsyncResult.End(result);
					return;
				}
				if (result is ServiceChannel.CallOnceManager.AsyncWaiter)
				{
					ServiceChannel.CallOnceManager.AsyncWaiter.End(result);
					return;
				}
				bool flag = true;
				try
				{
					this.callOnce.EndCall(this.channel, result);
					flag = false;
				}
				finally
				{
					if (flag)
					{
						this.SignalNext();
					}
				}
			}

			// Token: 0x06008232 RID: 33330 RVA: 0x001E223C File Offset: 0x001E043C
			internal static void SignalNextIfNonNull(ServiceChannel.CallOnceManager manager)
			{
				if (manager != null)
				{
					manager.SignalNext();
				}
			}

			// Token: 0x06008233 RID: 33331 RVA: 0x001E2248 File Offset: 0x001E0448
			internal void SignalNext()
			{
				if (this.queue == null)
				{
					return;
				}
				ServiceChannel.CallOnceManager.IWaiter waiter = null;
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (this.queue != null)
					{
						if (this.queue.Count > 0)
						{
							waiter = this.queue.Dequeue();
						}
						else
						{
							this.queue = null;
						}
					}
				}
				if (waiter != null)
				{
					ActionItem.Schedule(ServiceChannel.CallOnceManager.signalWaiter, waiter);
				}
			}

			// Token: 0x06008234 RID: 33332 RVA: 0x001E22C8 File Offset: 0x001E04C8
			private static void SignalWaiter(object state)
			{
				((ServiceChannel.CallOnceManager.IWaiter)state).Signal();
			}

			// Token: 0x040049F0 RID: 18928
			private readonly ServiceChannel.ICallOnce callOnce;

			// Token: 0x040049F1 RID: 18929
			private readonly ServiceChannel channel;

			// Token: 0x040049F2 RID: 18930
			private bool isFirst = true;

			// Token: 0x040049F3 RID: 18931
			private Queue<ServiceChannel.CallOnceManager.IWaiter> queue;

			// Token: 0x040049F4 RID: 18932
			private static Action<object> signalWaiter = new Action<object>(ServiceChannel.CallOnceManager.SignalWaiter);

			// Token: 0x02000F85 RID: 3973
			private interface IWaiter
			{
				// Token: 0x06008827 RID: 34855
				void Signal();
			}

			// Token: 0x02000F86 RID: 3974
			private class SyncWaiter : ServiceChannel.CallOnceManager.IWaiter
			{
				// Token: 0x06008828 RID: 34856 RVA: 0x001FA692 File Offset: 0x001F8892
				internal SyncWaiter(ServiceChannel.CallOnceManager manager)
				{
					this.manager = manager;
				}

				// Token: 0x17001D9F RID: 7583
				// (get) Token: 0x06008829 RID: 34857 RVA: 0x001FA6AD File Offset: 0x001F88AD
				private bool ShouldSignalNext
				{
					get
					{
						return this.isTimedOut && this.isSignaled;
					}
				}

				// Token: 0x0600882A RID: 34858 RVA: 0x001FA6C0 File Offset: 0x001F88C0
				void ServiceChannel.CallOnceManager.IWaiter.Signal()
				{
					this.wait.Set();
					this.CloseWaitHandle();
					object thisLock = this.manager.ThisLock;
					bool shouldSignalNext;
					lock (thisLock)
					{
						this.isSignaled = true;
						shouldSignalNext = this.ShouldSignalNext;
					}
					if (shouldSignalNext)
					{
						this.manager.SignalNext();
					}
				}

				// Token: 0x0600882B RID: 34859 RVA: 0x001FA730 File Offset: 0x001F8930
				internal bool Wait(TimeSpan timeout)
				{
					try
					{
						if (!TimeoutHelper.WaitOne(this.wait, timeout))
						{
							object thisLock = this.manager.ThisLock;
							bool shouldSignalNext;
							lock (thisLock)
							{
								this.isTimedOut = true;
								shouldSignalNext = this.ShouldSignalNext;
							}
							if (shouldSignalNext)
							{
								this.manager.SignalNext();
							}
						}
					}
					finally
					{
						this.CloseWaitHandle();
					}
					return !this.isTimedOut;
				}

				// Token: 0x0600882C RID: 34860 RVA: 0x001FA7B8 File Offset: 0x001F89B8
				private void CloseWaitHandle()
				{
					if (Interlocked.Increment(ref this.waitCount) == 2)
					{
						this.wait.Close();
					}
				}

				// Token: 0x04004F6F RID: 20335
				private ManualResetEvent wait = new ManualResetEvent(false);

				// Token: 0x04004F70 RID: 20336
				private ServiceChannel.CallOnceManager manager;

				// Token: 0x04004F71 RID: 20337
				private bool isTimedOut;

				// Token: 0x04004F72 RID: 20338
				private bool isSignaled;

				// Token: 0x04004F73 RID: 20339
				private int waitCount;
			}

			// Token: 0x02000F87 RID: 3975
			private class AsyncWaiter : AsyncResult, ServiceChannel.CallOnceManager.IWaiter
			{
				// Token: 0x0600882D RID: 34861 RVA: 0x001FA7D4 File Offset: 0x001F89D4
				internal AsyncWaiter(ServiceChannel.CallOnceManager manager, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					this.manager = manager;
					this.timeout = timeout;
					if (timeout != TimeSpan.MaxValue)
					{
						this.timer = new IOThreadTimer(ServiceChannel.CallOnceManager.AsyncWaiter.timerCallback, this, false);
						this.timer.Set(timeout);
					}
				}

				// Token: 0x0600882E RID: 34862 RVA: 0x001FA823 File Offset: 0x001F8A23
				internal static void End(IAsyncResult result)
				{
					AsyncResult.End<ServiceChannel.CallOnceManager.AsyncWaiter>(result);
				}

				// Token: 0x0600882F RID: 34863 RVA: 0x001FA82C File Offset: 0x001F8A2C
				void ServiceChannel.CallOnceManager.IWaiter.Signal()
				{
					if (this.timer == null || this.timer.Cancel())
					{
						base.Complete(false);
						this.manager.channel.Closed -= this.OnClosed;
						return;
					}
					this.manager.SignalNext();
				}

				// Token: 0x06008830 RID: 34864 RVA: 0x001FA87D File Offset: 0x001F8A7D
				private void OnClosed(object sender, EventArgs e)
				{
					if (this.timer == null || this.timer.Cancel())
					{
						base.Complete(false, this.manager.channel.CreateClosedException());
					}
				}

				// Token: 0x06008831 RID: 34865 RVA: 0x001FA8AC File Offset: 0x001F8AAC
				private static void TimerCallback(object state)
				{
					ServiceChannel.CallOnceManager.AsyncWaiter asyncWaiter = (ServiceChannel.CallOnceManager.AsyncWaiter)state;
					asyncWaiter.Complete(false, asyncWaiter.manager.channel.GetOpenTimeoutException(asyncWaiter.timeout));
				}

				// Token: 0x04004F74 RID: 20340
				private static Action<object> timerCallback = new Action<object>(ServiceChannel.CallOnceManager.AsyncWaiter.TimerCallback);

				// Token: 0x04004F75 RID: 20341
				private ServiceChannel.CallOnceManager manager;

				// Token: 0x04004F76 RID: 20342
				private TimeSpan timeout;

				// Token: 0x04004F77 RID: 20343
				private IOThreadTimer timer;
			}
		}

		// Token: 0x02000E23 RID: 3619
		private class CallOnceCompletedAsyncResult : AsyncResult
		{
			// Token: 0x06008236 RID: 33334 RVA: 0x001E22E8 File Offset: 0x001E04E8
			internal CallOnceCompletedAsyncResult(AsyncCallback callback, object state) : base(callback, state)
			{
				base.Complete(true);
			}

			// Token: 0x06008237 RID: 33335 RVA: 0x001E22F9 File Offset: 0x001E04F9
			internal static void End(IAsyncResult result)
			{
				AsyncResult.End<ServiceChannel.CallOnceCompletedAsyncResult>(result);
			}
		}

		// Token: 0x02000E24 RID: 3620
		internal class SessionIdleManager
		{
			// Token: 0x06008238 RID: 33336 RVA: 0x001E2304 File Offset: 0x001E0504
			private SessionIdleManager(IChannelBinder binder, TimeSpan idle)
			{
				this.binder = binder;
				this.timer = new IOThreadTimer(ServiceChannel.SessionIdleManager.GetTimerCallback(), this, false);
				this.idleTicks = Ticks.FromTimeSpan(idle);
				this.timer.SetAt(Ticks.Now + this.idleTicks);
				this.thisLock = new object();
			}

			// Token: 0x06008239 RID: 33337 RVA: 0x001E235E File Offset: 0x001E055E
			internal static ServiceChannel.SessionIdleManager CreateIfNeeded(IChannelBinder binder, TimeSpan idle)
			{
				if (binder.HasSession && idle != TimeSpan.MaxValue)
				{
					return new ServiceChannel.SessionIdleManager(binder, idle);
				}
				return null;
			}

			// Token: 0x17001CB1 RID: 7345
			// (get) Token: 0x0600823A RID: 33338 RVA: 0x001E2380 File Offset: 0x001E0580
			internal bool DidIdleAbort
			{
				get
				{
					object obj = this.thisLock;
					bool result;
					lock (obj)
					{
						result = this.didIdleAbort;
					}
					return result;
				}
			}

			// Token: 0x0600823B RID: 33339 RVA: 0x001E23C4 File Offset: 0x001E05C4
			internal void CancelTimer()
			{
				object obj = this.thisLock;
				lock (obj)
				{
					this.isTimerCancelled = true;
					this.timer.Cancel();
				}
			}

			// Token: 0x0600823C RID: 33340 RVA: 0x001E2414 File Offset: 0x001E0614
			internal void CompletedActivity()
			{
				Interlocked.Exchange(ref this.lastActivity, Ticks.Now);
			}

			// Token: 0x0600823D RID: 33341 RVA: 0x001E2428 File Offset: 0x001E0628
			internal void RegisterChannel(ServiceChannel channel, out bool didIdleAbort)
			{
				object obj = this.thisLock;
				lock (obj)
				{
					this.channel = channel;
					didIdleAbort = this.didIdleAbort;
				}
			}

			// Token: 0x0600823E RID: 33342 RVA: 0x001E2474 File Offset: 0x001E0674
			private static Action<object> GetTimerCallback()
			{
				if (ServiceChannel.SessionIdleManager.timerCallback == null)
				{
					ServiceChannel.SessionIdleManager.timerCallback = new Action<object>(ServiceChannel.SessionIdleManager.TimerCallback);
				}
				return ServiceChannel.SessionIdleManager.timerCallback;
			}

			// Token: 0x0600823F RID: 33343 RVA: 0x001E2493 File Offset: 0x001E0693
			private static void TimerCallback(object state)
			{
				((ServiceChannel.SessionIdleManager)state).TimerCallback();
			}

			// Token: 0x06008240 RID: 33344 RVA: 0x001E24A0 File Offset: 0x001E06A0
			private void TimerCallback()
			{
				long num = Interlocked.CompareExchange(ref this.lastActivity, 0L, 0L);
				long num2 = num + this.idleTicks;
				object obj = this.thisLock;
				lock (obj)
				{
					if (Ticks.Now > num2)
					{
						if (TD.SessionIdleTimeoutIsEnabled())
						{
							string remoteAddress = string.Empty;
							if (this.binder.ListenUri != null)
							{
								remoteAddress = this.binder.ListenUri.AbsoluteUri;
							}
							TD.SessionIdleTimeout(remoteAddress);
						}
						this.didIdleAbort = true;
						if (this.channel != null)
						{
							this.channel.Abort();
						}
						else
						{
							this.binder.Abort();
						}
					}
					else if (!this.isTimerCancelled && this.binder.Channel.State != CommunicationState.Faulted && this.binder.Channel.State != CommunicationState.Closed)
					{
						this.timer.SetAt(num2);
					}
				}
			}

			// Token: 0x040049F5 RID: 18933
			private readonly IChannelBinder binder;

			// Token: 0x040049F6 RID: 18934
			private ServiceChannel channel;

			// Token: 0x040049F7 RID: 18935
			private readonly long idleTicks;

			// Token: 0x040049F8 RID: 18936
			private long lastActivity;

			// Token: 0x040049F9 RID: 18937
			private readonly IOThreadTimer timer;

			// Token: 0x040049FA RID: 18938
			private static Action<object> timerCallback;

			// Token: 0x040049FB RID: 18939
			private bool didIdleAbort;

			// Token: 0x040049FC RID: 18940
			private bool isTimerCancelled;

			// Token: 0x040049FD RID: 18941
			private object thisLock;
		}
	}
}
