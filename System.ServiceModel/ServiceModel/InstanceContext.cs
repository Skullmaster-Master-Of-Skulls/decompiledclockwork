using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Dispatcher;
using System.Threading;

namespace System.ServiceModel
{
	// Token: 0x02000114 RID: 276
	[__DynamicallyInvokable]
	public sealed class InstanceContext : CommunicationObject, IExtensibleObject<InstanceContext>
	{
		// Token: 0x060006CA RID: 1738 RVA: 0x0001D381 File Offset: 0x0001B581
		[__DynamicallyInvokable]
		public InstanceContext(object implementation) : this(null, implementation)
		{
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001D38B File Offset: 0x0001B58B
		public InstanceContext(ServiceHostBase host, object implementation) : this(host, implementation, true)
		{
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001D396 File Offset: 0x0001B596
		internal InstanceContext(ServiceHostBase host, object implementation, bool isUserCreated) : this(host, implementation, true, isUserCreated)
		{
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001D3A4 File Offset: 0x0001B5A4
		internal InstanceContext(ServiceHostBase host, object implementation, bool wellKnown, bool isUserCreated)
		{
			this.serviceInstanceLock = new object();
			base..ctor();
			this.host = host;
			if (implementation != null)
			{
				this.userObject = implementation;
				this.wellKnown = wellKnown;
			}
			this.autoClose = false;
			this.channels = new ServiceChannelManager(this);
			this.isUserCreated = isUserCreated;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0001D3F5 File Offset: 0x0001B5F5
		public InstanceContext(ServiceHostBase host) : this(host, true)
		{
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001D400 File Offset: 0x0001B600
		internal InstanceContext(ServiceHostBase host, bool isUserCreated)
		{
			this.serviceInstanceLock = new object();
			base..ctor();
			if (host == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("host"));
			}
			this.host = host;
			this.autoClose = true;
			this.channels = new ServiceChannelManager(this, InstanceContext.NotifyEmptyCallback);
			this.isUserCreated = isUserCreated;
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0001D45C File Offset: 0x0001B65C
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x0001D464 File Offset: 0x0001B664
		internal bool IsUserCreated
		{
			get
			{
				return this.isUserCreated;
			}
			set
			{
				this.isUserCreated = value;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0001D46D File Offset: 0x0001B66D
		internal bool IsWellKnown
		{
			get
			{
				return this.wellKnown;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x0001D475 File Offset: 0x0001B675
		// (set) Token: 0x060006D4 RID: 1748 RVA: 0x0001D47D File Offset: 0x0001B67D
		internal bool AutoClose
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

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x0001D486 File Offset: 0x0001B686
		// (set) Token: 0x060006D6 RID: 1750 RVA: 0x0001D48E File Offset: 0x0001B68E
		internal InstanceBehavior Behavior
		{
			get
			{
				return this.behavior;
			}
			set
			{
				if (this.behavior == null)
				{
					this.behavior = value;
				}
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0001D4A0 File Offset: 0x0001B6A0
		internal ConcurrencyInstanceContextFacet Concurrency
		{
			get
			{
				if (this.concurrency == null)
				{
					object thisLock = this.ThisLock;
					lock (thisLock)
					{
						if (this.concurrency == null)
						{
							this.concurrency = new ConcurrencyInstanceContextFacet();
						}
					}
				}
				return this.concurrency;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0001D4FC File Offset: 0x0001B6FC
		internal static InstanceContext Current
		{
			get
			{
				if (OperationContext.Current == null)
				{
					return null;
				}
				return OperationContext.Current.InstanceContext;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0001D511 File Offset: 0x0001B711
		protected override TimeSpan DefaultCloseTimeout
		{
			get
			{
				if (this.host != null)
				{
					return this.host.CloseTimeout;
				}
				return ServiceDefaults.CloseTimeout;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0001D52C File Offset: 0x0001B72C
		protected override TimeSpan DefaultOpenTimeout
		{
			get
			{
				if (this.host != null)
				{
					return this.host.OpenTimeout;
				}
				return ServiceDefaults.OpenTimeout;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0001D548 File Offset: 0x0001B748
		public IExtensionCollection<InstanceContext> Extensions
		{
			get
			{
				base.ThrowIfClosed();
				object thisLock = this.ThisLock;
				IExtensionCollection<InstanceContext> result;
				lock (thisLock)
				{
					if (this.extensions == null)
					{
						this.extensions = new ExtensionCollection<InstanceContext>(this, this.ThisLock);
					}
					result = this.extensions;
				}
				return result;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0001D5AC File Offset: 0x0001B7AC
		internal bool HasTransaction
		{
			get
			{
				return this.transaction != null && !object.Equals(this.transaction.Attached, null);
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x0001D5CC File Offset: 0x0001B7CC
		public ICollection<IChannel> IncomingChannels
		{
			get
			{
				base.ThrowIfClosed();
				return this.channels.IncomingChannels;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0001D5DF File Offset: 0x0001B7DF
		private bool IsBusy
		{
			get
			{
				return base.State != CommunicationState.Closed && this.channels.IsBusy;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x0001D5F7 File Offset: 0x0001B7F7
		private bool IsSingleton
		{
			get
			{
				return this.behavior != null && InstanceContextProviderBase.IsProviderSingleton(this.behavior.InstanceContextProvider);
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0001D613 File Offset: 0x0001B813
		public ICollection<IChannel> OutgoingChannels
		{
			get
			{
				base.ThrowIfClosed();
				return this.channels.OutgoingChannels;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0001D626 File Offset: 0x0001B826
		public ServiceHostBase Host
		{
			get
			{
				base.ThrowIfClosed();
				return this.host;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x0001D634 File Offset: 0x0001B834
		// (set) Token: 0x060006E3 RID: 1763 RVA: 0x0001D641 File Offset: 0x0001B841
		public int ManualFlowControlLimit
		{
			get
			{
				return this.EnsureQuotaThrottle().Limit;
			}
			set
			{
				this.EnsureQuotaThrottle().SetLimit(value);
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0001D64F File Offset: 0x0001B84F
		internal QuotaThrottle QuotaThrottle
		{
			get
			{
				return this.quotaThrottle;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0001D657 File Offset: 0x0001B857
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x0001D65F File Offset: 0x0001B85F
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

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0001D66E File Offset: 0x0001B86E
		// (set) Token: 0x060006E8 RID: 1768 RVA: 0x0001D676 File Offset: 0x0001B876
		internal int InstanceContextManagerIndex
		{
			get
			{
				return this.instanceContextManagerIndex;
			}
			set
			{
				this.instanceContextManagerIndex = value;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x0001D67F File Offset: 0x0001B87F
		// (set) Token: 0x060006EA RID: 1770 RVA: 0x0001D687 File Offset: 0x0001B887
		[__DynamicallyInvokable]
		public SynchronizationContext SynchronizationContext
		{
			[__DynamicallyInvokable]
			get
			{
				return this.synchronizationContext;
			}
			[__DynamicallyInvokable]
			set
			{
				base.ThrowIfClosedOrOpened();
				this.synchronizationContext = value;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x0001D696 File Offset: 0x0001B896
		internal new object ThisLock
		{
			get
			{
				return base.ThisLock;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x0001D6A0 File Offset: 0x0001B8A0
		internal TransactionInstanceContextFacet Transaction
		{
			get
			{
				if (this.transaction == null)
				{
					object thisLock = this.ThisLock;
					lock (thisLock)
					{
						if (this.transaction == null)
						{
							this.transaction = new TransactionInstanceContextFacet(this);
						}
					}
				}
				return this.transaction;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x0001D6FC File Offset: 0x0001B8FC
		internal object UserObject
		{
			get
			{
				return this.userObject;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x0001D704 File Offset: 0x0001B904
		internal ICollection<IChannel> WmiChannels
		{
			get
			{
				if (this.wmiChannels == null)
				{
					object thisLock = this.ThisLock;
					lock (thisLock)
					{
						if (this.wmiChannels == null)
						{
							this.wmiChannels = new SynchronizedCollection<IChannel>();
						}
					}
				}
				return this.wmiChannels;
			}
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001D760 File Offset: 0x0001B960
		protected override void OnAbort()
		{
			this.channels.Abort();
			this.Unload();
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001D773 File Offset: 0x0001B973
		internal IAsyncResult BeginCloseInput(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.channels.BeginCloseInput(timeout, callback, state);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001D783 File Offset: 0x0001B983
		internal void BindRpc(ref MessageRpc rpc)
		{
			base.ThrowIfClosed();
			this.channels.IncrementActivityCount();
			rpc.SuccessfullyBoundInstance = true;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001D7A0 File Offset: 0x0001B9A0
		internal void BindIncomingChannel(ServiceChannel channel)
		{
			base.ThrowIfDisposed();
			channel.InstanceContext = this;
			IChannel channel2 = (IChannel)channel.Proxy;
			this.channels.AddIncomingChannel(channel2);
			if (channel2 != null)
			{
				CommunicationState state = channel.State;
				if (state == CommunicationState.Closing || state == CommunicationState.Closed || state == CommunicationState.Faulted)
				{
					this.channels.RemoveChannel(channel2);
				}
			}
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001D7F8 File Offset: 0x0001B9F8
		private void CloseIfNotBusy()
		{
			if (base.State != CommunicationState.Created)
			{
				CommunicationState state = base.State;
			}
			if (base.State != CommunicationState.Opened)
			{
				return;
			}
			if (this.IsBusy)
			{
				return;
			}
			if (!this.behavior.CanUnload(this))
			{
				return;
			}
			try
			{
				if (base.State == CommunicationState.Opened)
				{
					base.Close();
				}
			}
			catch (ObjectDisposedException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
			}
			catch (InvalidOperationException exception2)
			{
				DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
			}
			catch (CommunicationException exception3)
			{
				DiagnosticUtility.TraceHandledException(exception3, TraceEventType.Information);
			}
			catch (TimeoutException ex)
			{
				if (TD.CloseTimeoutIsEnabled())
				{
					TD.CloseTimeout(ex.Message);
				}
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
			}
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001D8B8 File Offset: 0x0001BAB8
		internal void CloseInput(TimeSpan timeout)
		{
			this.channels.CloseInput(timeout);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001D8C6 File Offset: 0x0001BAC6
		internal void EndCloseInput(IAsyncResult result)
		{
			this.channels.EndCloseInput(result);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001D8D4 File Offset: 0x0001BAD4
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal void CompleteAttachedTransaction()
		{
			Exception error = null;
			if (!this.behavior.TransactionAutoCompleteOnSessionClose)
			{
				error = new Exception();
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					TraceUtility.TraceEvent(TraceEventType.Information, 917515, SR.GetString("TraceCodeTxCompletionStatusAbortedOnSessionClose", new object[]
					{
						this.transaction.Attached.TransactionInformation.LocalIdentifier
					}));
				}
			}
			else if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 917512, SR.GetString("TraceCodeTxCompletionStatusCompletedForTACOSC", new object[]
				{
					this.transaction.Attached.TransactionInformation.LocalIdentifier
				}));
			}
			this.transaction.CompletePendingTransaction(this.transaction.Attached, error);
			this.transaction.Attached = null;
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001D990 File Offset: 0x0001BB90
		private QuotaThrottle EnsureQuotaThrottle()
		{
			object thisLock = this.ThisLock;
			QuotaThrottle result;
			lock (thisLock)
			{
				if (this.quotaThrottle == null)
				{
					this.quotaThrottle = new QuotaThrottle(new WaitCallback(ImmutableDispatchRuntime.GotDynamicInstanceContext), this.ThisLock);
					this.quotaThrottle.Owner = "InstanceContext";
				}
				result = this.quotaThrottle;
			}
			return result;
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001DA08 File Offset: 0x0001BC08
		internal void FaultInternal()
		{
			base.Fault();
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001DA10 File Offset: 0x0001BC10
		public object GetServiceInstance()
		{
			return this.GetServiceInstance(null);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001DA1C File Offset: 0x0001BC1C
		[__DynamicallyInvokable]
		public object GetServiceInstance(Message message)
		{
			object obj = this.serviceInstanceLock;
			object result;
			lock (obj)
			{
				base.ThrowIfClosedOrNotOpen();
				object obj2 = this.userObject;
				if (obj2 != null)
				{
					result = obj2;
				}
				else if (this.behavior == null)
				{
					Exception exception = new InvalidOperationException(SR.GetString("SFxInstanceNotInitialized"));
					if (message != null)
					{
						throw TraceUtility.ThrowHelperError(exception, message);
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
				}
				else
				{
					object instance;
					if (message != null)
					{
						instance = this.behavior.GetInstance(this, message);
					}
					else
					{
						instance = this.behavior.GetInstance(this);
					}
					if (instance != null)
					{
						this.SetUserObject(instance);
					}
					result = instance;
				}
			}
			return result;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001DACC File Offset: 0x0001BCCC
		public int IncrementManualFlowControlLimit(int incrementBy)
		{
			return this.EnsureQuotaThrottle().IncrementLimit(incrementBy);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001DADA File Offset: 0x0001BCDA
		private void Load()
		{
			if (this.behavior != null)
			{
				this.behavior.Initialize(this);
			}
			if (this.host != null)
			{
				this.host.BindInstance(this);
			}
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001DB04 File Offset: 0x0001BD04
		private static void NotifyEmpty(InstanceContext instanceContext)
		{
			if (instanceContext.autoClose)
			{
				instanceContext.CloseIfNotBusy();
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001DB14 File Offset: 0x0001BD14
		private static void NotifyIdle(InstanceContext instanceContext)
		{
			instanceContext.CloseIfNotBusy();
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001DB1C File Offset: 0x0001BD1C
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new InstanceContext.CloseAsyncResult(timeout, callback, state, this);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001DB27 File Offset: 0x0001BD27
		protected override void OnEndClose(IAsyncResult result)
		{
			InstanceContext.CloseAsyncResult.End(result);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001DB2F File Offset: 0x0001BD2F
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001DB38 File Offset: 0x0001BD38
		protected override void OnEndOpen(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001DB40 File Offset: 0x0001BD40
		protected override void OnClose(TimeSpan timeout)
		{
			this.channels.Close(timeout);
			this.Unload();
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001DB54 File Offset: 0x0001BD54
		protected override void OnClosed()
		{
			base.OnClosed();
			ServiceThrottle serviceThrottle = this.serviceThrottle;
			if (serviceThrottle != null)
			{
				serviceThrottle.DeactivateInstanceContext();
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0001DB77 File Offset: 0x0001BD77
		protected override void OnFaulted()
		{
			base.OnFaulted();
			if (this.IsSingleton && this.host != null)
			{
				this.host.FaultInternal();
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001DB9C File Offset: 0x0001BD9C
		protected override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001DBB1 File Offset: 0x0001BDB1
		protected override void OnOpened()
		{
			base.OnOpened();
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001DBB9 File Offset: 0x0001BDB9
		protected override void OnOpening()
		{
			this.Load();
			base.OnOpening();
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001DBC7 File Offset: 0x0001BDC7
		public void ReleaseServiceInstance()
		{
			base.ThrowIfDisposedOrNotOpen();
			this.SetUserObject(null);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001DBD8 File Offset: 0x0001BDD8
		private void SetUserObject(object newUserObject)
		{
			if (this.behavior != null && !this.wellKnown)
			{
				object obj = Interlocked.Exchange(ref this.userObject, newUserObject);
				if (obj != null && this.host != null && !object.Equals(obj, this.host.DisposableInstance))
				{
					this.behavior.ReleaseInstance(this, obj);
				}
			}
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001DC2D File Offset: 0x0001BE2D
		internal void UnbindRpc(ref MessageRpc rpc)
		{
			if (rpc.InstanceContext == this && rpc.SuccessfullyBoundInstance)
			{
				this.channels.DecrementActivityCount();
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001DC4B File Offset: 0x0001BE4B
		internal void UnbindIncomingChannel(ServiceChannel channel)
		{
			this.channels.RemoveChannel((IChannel)channel.Proxy);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001DC64 File Offset: 0x0001BE64
		private void Unload()
		{
			this.SetUserObject(null);
			if (this.host != null)
			{
				this.host.UnbindInstance(this);
			}
		}

		// Token: 0x04000AA0 RID: 2720
		internal static InstanceContextEmptyCallback NotifyEmptyCallback = new InstanceContextEmptyCallback(InstanceContext.NotifyEmpty);

		// Token: 0x04000AA1 RID: 2721
		internal static InstanceContextIdleCallback NotifyIdleCallback = new InstanceContextIdleCallback(InstanceContext.NotifyIdle);

		// Token: 0x04000AA2 RID: 2722
		private bool autoClose;

		// Token: 0x04000AA3 RID: 2723
		private InstanceBehavior behavior;

		// Token: 0x04000AA4 RID: 2724
		private ServiceChannelManager channels;

		// Token: 0x04000AA5 RID: 2725
		private ConcurrencyInstanceContextFacet concurrency;

		// Token: 0x04000AA6 RID: 2726
		private ExtensionCollection<InstanceContext> extensions;

		// Token: 0x04000AA7 RID: 2727
		private readonly ServiceHostBase host;

		// Token: 0x04000AA8 RID: 2728
		private QuotaThrottle quotaThrottle;

		// Token: 0x04000AA9 RID: 2729
		private ServiceThrottle serviceThrottle;

		// Token: 0x04000AAA RID: 2730
		private int instanceContextManagerIndex;

		// Token: 0x04000AAB RID: 2731
		private object serviceInstanceLock;

		// Token: 0x04000AAC RID: 2732
		private SynchronizationContext synchronizationContext;

		// Token: 0x04000AAD RID: 2733
		private TransactionInstanceContextFacet transaction;

		// Token: 0x04000AAE RID: 2734
		private object userObject;

		// Token: 0x04000AAF RID: 2735
		private bool wellKnown;

		// Token: 0x04000AB0 RID: 2736
		private SynchronizedCollection<IChannel> wmiChannels;

		// Token: 0x04000AB1 RID: 2737
		private bool isUserCreated;

		// Token: 0x02000AEB RID: 2795
		private class CloseAsyncResult : AsyncResult
		{
			// Token: 0x06006F0D RID: 28429 RVA: 0x0019CE88 File Offset: 0x0019B088
			public CloseAsyncResult(TimeSpan timeout, AsyncCallback callback, object state, InstanceContext instanceContext) : base(callback, state)
			{
				this.timeoutHelper = new TimeoutHelper(timeout);
				this.instanceContext = instanceContext;
				IAsyncResult asyncResult = this.instanceContext.channels.BeginClose(this.timeoutHelper.RemainingTime(), base.PrepareAsyncCompletion(new AsyncResult.AsyncCompletion(this.CloseChannelsCallback)), this);
				if (asyncResult.CompletedSynchronously && this.CloseChannelsCallback(asyncResult))
				{
					base.Complete(true);
				}
			}

			// Token: 0x06006F0E RID: 28430 RVA: 0x0019CEF8 File Offset: 0x0019B0F8
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<InstanceContext.CloseAsyncResult>(result);
			}

			// Token: 0x06006F0F RID: 28431 RVA: 0x0019CF01 File Offset: 0x0019B101
			private bool CloseChannelsCallback(IAsyncResult result)
			{
				this.instanceContext.channels.EndClose(result);
				this.instanceContext.Unload();
				return true;
			}

			// Token: 0x04003F33 RID: 16179
			private InstanceContext instanceContext;

			// Token: 0x04003F34 RID: 16180
			private TimeoutHelper timeoutHelper;
		}
	}
}
