using System;
using System.Collections.ObjectModel;
using System.Runtime;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007DC RID: 2012
	internal abstract class ConnectionOrientedTransportChannelFactory<TChannel> : TransportChannelFactory<TChannel>, IConnectionOrientedTransportChannelFactorySettings, IConnectionOrientedTransportFactorySettings, ITransportFactorySettings, IDefaultCommunicationTimeouts, IConnectionOrientedConnectionSettings
	{
		// Token: 0x06004BE9 RID: 19433 RVA: 0x001156F8 File Offset: 0x001138F8
		internal ConnectionOrientedTransportChannelFactory(ConnectionOrientedTransportBindingElement bindingElement, BindingContext context, string connectionPoolGroupName, TimeSpan idleTimeout, int maxOutboundConnectionsPerEndpoint, bool supportsImpersonationDuringAsyncOpen) : base(bindingElement, context)
		{
			if (bindingElement.TransferMode == TransferMode.Buffered && bindingElement.MaxReceivedMessageSize > 2147483647L)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("bindingElement.MaxReceivedMessageSize", SR.GetString("MaxReceivedMessageSizeMustBeInIntegerRange")));
			}
			this.connectionBufferSize = bindingElement.ConnectionBufferSize;
			this.connectionPoolGroupName = connectionPoolGroupName;
			this.exposeConnectionProperty = bindingElement.ExposeConnectionProperty;
			this.idleTimeout = idleTimeout;
			this.maxBufferSize = bindingElement.MaxBufferSize;
			this.maxOutboundConnectionsPerEndpoint = maxOutboundConnectionsPerEndpoint;
			this.maxOutputDelay = bindingElement.MaxOutputDelay;
			this.transferMode = bindingElement.TransferMode;
			Collection<StreamUpgradeBindingElement> collection = context.BindingParameters.FindAll<StreamUpgradeBindingElement>();
			if (collection.Count > 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MultipleStreamUpgradeProvidersInParameters")));
			}
			if (collection.Count == 1 && this.SupportsUpgrade(collection[0]))
			{
				this.upgrade = collection[0].BuildClientStreamUpgradeProvider(context);
				context.BindingParameters.Remove<StreamUpgradeBindingElement>();
				this.securityCapabilities = collection[0].GetProperty<ISecurityCapabilities>(context);
				this.flowIdentity = supportsImpersonationDuringAsyncOpen;
			}
		}

		// Token: 0x17001306 RID: 4870
		// (get) Token: 0x06004BEA RID: 19434 RVA: 0x00115816 File Offset: 0x00113A16
		public int ConnectionBufferSize
		{
			get
			{
				return this.connectionBufferSize;
			}
		}

		// Token: 0x17001307 RID: 4871
		// (get) Token: 0x06004BEB RID: 19435 RVA: 0x00115820 File Offset: 0x00113A20
		internal IConnectionInitiator ConnectionInitiator
		{
			get
			{
				if (this.connectionInitiator == null)
				{
					object thisLock = base.ThisLock;
					lock (thisLock)
					{
						if (this.connectionInitiator == null)
						{
							this.connectionInitiator = this.GetConnectionInitiator();
							if (DiagnosticUtility.ShouldUseActivity)
							{
								this.connectionInitiator = new TracingConnectionInitiator(this.connectionInitiator, ServiceModelActivity.Current != null && ServiceModelActivity.Current.ActivityType == ActivityType.OpenClient);
							}
						}
					}
				}
				return this.connectionInitiator;
			}
		}

		// Token: 0x17001308 RID: 4872
		// (get) Token: 0x06004BEC RID: 19436 RVA: 0x001158AC File Offset: 0x00113AAC
		public string ConnectionPoolGroupName
		{
			get
			{
				return this.connectionPoolGroupName;
			}
		}

		// Token: 0x17001309 RID: 4873
		// (get) Token: 0x06004BED RID: 19437 RVA: 0x001158B4 File Offset: 0x00113AB4
		public TimeSpan IdleTimeout
		{
			get
			{
				return this.idleTimeout;
			}
		}

		// Token: 0x1700130A RID: 4874
		// (get) Token: 0x06004BEE RID: 19438 RVA: 0x001158BC File Offset: 0x00113ABC
		public int MaxBufferSize
		{
			get
			{
				return this.maxBufferSize;
			}
		}

		// Token: 0x1700130B RID: 4875
		// (get) Token: 0x06004BEF RID: 19439 RVA: 0x001158C4 File Offset: 0x00113AC4
		public int MaxOutboundConnectionsPerEndpoint
		{
			get
			{
				return this.maxOutboundConnectionsPerEndpoint;
			}
		}

		// Token: 0x1700130C RID: 4876
		// (get) Token: 0x06004BF0 RID: 19440 RVA: 0x001158CC File Offset: 0x00113ACC
		public TimeSpan MaxOutputDelay
		{
			get
			{
				return this.maxOutputDelay;
			}
		}

		// Token: 0x1700130D RID: 4877
		// (get) Token: 0x06004BF1 RID: 19441 RVA: 0x001158D4 File Offset: 0x00113AD4
		public StreamUpgradeProvider Upgrade
		{
			get
			{
				StreamUpgradeProvider result = this.upgrade;
				base.ThrowIfDisposed();
				return result;
			}
		}

		// Token: 0x1700130E RID: 4878
		// (get) Token: 0x06004BF2 RID: 19442 RVA: 0x001158EF File Offset: 0x00113AEF
		public TransferMode TransferMode
		{
			get
			{
				return this.transferMode;
			}
		}

		// Token: 0x1700130F RID: 4879
		// (get) Token: 0x06004BF3 RID: 19443 RVA: 0x001158F7 File Offset: 0x00113AF7
		int IConnectionOrientedTransportFactorySettings.MaxBufferSize
		{
			get
			{
				return this.MaxBufferSize;
			}
		}

		// Token: 0x17001310 RID: 4880
		// (get) Token: 0x06004BF4 RID: 19444 RVA: 0x001158FF File Offset: 0x00113AFF
		TransferMode IConnectionOrientedTransportFactorySettings.TransferMode
		{
			get
			{
				return this.TransferMode;
			}
		}

		// Token: 0x17001311 RID: 4881
		// (get) Token: 0x06004BF5 RID: 19445 RVA: 0x00115907 File Offset: 0x00113B07
		StreamUpgradeProvider IConnectionOrientedTransportFactorySettings.Upgrade
		{
			get
			{
				return this.Upgrade;
			}
		}

		// Token: 0x17001312 RID: 4882
		// (get) Token: 0x06004BF6 RID: 19446 RVA: 0x0011590F File Offset: 0x00113B0F
		ServiceSecurityAuditBehavior IConnectionOrientedTransportFactorySettings.AuditBehavior
		{
			get
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SecurityAuditNotSupportedOnChannelFactory")));
			}
		}

		// Token: 0x06004BF7 RID: 19447 RVA: 0x0011592C File Offset: 0x00113B2C
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(ISecurityCapabilities))
			{
				return (T)((object)this.securityCapabilities);
			}
			T property = base.GetProperty<T>();
			if (property == null && this.upgrade != null)
			{
				property = this.upgrade.GetProperty<T>();
			}
			return property;
		}

		// Token: 0x06004BF8 RID: 19448 RVA: 0x00115984 File Offset: 0x00113B84
		internal override int GetMaxBufferSize()
		{
			return this.MaxBufferSize;
		}

		// Token: 0x06004BF9 RID: 19449
		internal abstract IConnectionInitiator GetConnectionInitiator();

		// Token: 0x06004BFA RID: 19450
		internal abstract ConnectionPool GetConnectionPool();

		// Token: 0x06004BFB RID: 19451
		internal abstract void ReleaseConnectionPool(ConnectionPool pool, TimeSpan timeout);

		// Token: 0x06004BFC RID: 19452 RVA: 0x0011598C File Offset: 0x00113B8C
		protected override TChannel OnCreateChannel(EndpointAddress address, Uri via)
		{
			base.ValidateScheme(via);
			if (this.TransferMode == TransferMode.Buffered)
			{
				return (TChannel)((object)new ClientFramingDuplexSessionChannel(this, this, address, via, this.ConnectionInitiator, this.connectionPool, this.exposeConnectionProperty, this.flowIdentity));
			}
			return (TChannel)((object)new StreamedFramingRequestChannel(this, this, address, via, this.ConnectionInitiator, this.connectionPool));
		}

		// Token: 0x06004BFD RID: 19453 RVA: 0x001159EC File Offset: 0x00113BEC
		private bool GetUpgradeAndConnectionPool(out StreamUpgradeProvider upgradeCopy, out ConnectionPool poolCopy)
		{
			if (this.upgrade != null || this.connectionPool != null)
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (this.upgrade != null || this.connectionPool != null)
					{
						upgradeCopy = this.upgrade;
						poolCopy = this.connectionPool;
						this.upgrade = null;
						this.connectionPool = null;
						return true;
					}
				}
			}
			upgradeCopy = null;
			poolCopy = null;
			return false;
		}

		// Token: 0x06004BFE RID: 19454 RVA: 0x00115A74 File Offset: 0x00113C74
		protected override void OnAbort()
		{
			StreamUpgradeProvider streamUpgradeProvider;
			ConnectionPool connectionPool;
			if (this.GetUpgradeAndConnectionPool(out streamUpgradeProvider, out connectionPool))
			{
				if (connectionPool != null)
				{
					this.ReleaseConnectionPool(connectionPool, TimeSpan.Zero);
				}
				if (streamUpgradeProvider != null)
				{
					streamUpgradeProvider.Abort();
				}
			}
		}

		// Token: 0x06004BFF RID: 19455 RVA: 0x00115AA5 File Offset: 0x00113CA5
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ConnectionOrientedTransportChannelFactory<TChannel>.CloseAsyncResult(this, timeout, callback, state);
		}

		// Token: 0x06004C00 RID: 19456 RVA: 0x00115AB0 File Offset: 0x00113CB0
		protected override void OnEndClose(IAsyncResult result)
		{
			ConnectionOrientedTransportChannelFactory<TChannel>.CloseAsyncResult.End(result);
		}

		// Token: 0x06004C01 RID: 19457 RVA: 0x00115AB8 File Offset: 0x00113CB8
		protected override void OnClose(TimeSpan timeout)
		{
			StreamUpgradeProvider streamUpgradeProvider;
			ConnectionPool connectionPool;
			if (this.GetUpgradeAndConnectionPool(out streamUpgradeProvider, out connectionPool))
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				if (connectionPool != null)
				{
					this.ReleaseConnectionPool(connectionPool, timeoutHelper.RemainingTime());
				}
				if (streamUpgradeProvider != null)
				{
					streamUpgradeProvider.Close(timeoutHelper.RemainingTime());
				}
			}
		}

		// Token: 0x06004C02 RID: 19458 RVA: 0x00115AFA File Offset: 0x00113CFA
		protected override void OnOpening()
		{
			base.OnOpening();
			this.connectionPool = this.GetConnectionPool();
		}

		// Token: 0x06004C03 RID: 19459 RVA: 0x00115B0E File Offset: 0x00113D0E
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ConnectionOrientedTransportChannelFactory<TChannel>.OpenAsyncResult(this.Upgrade, timeout, callback, state);
		}

		// Token: 0x06004C04 RID: 19460 RVA: 0x00115B1E File Offset: 0x00113D1E
		protected override void OnEndOpen(IAsyncResult result)
		{
			ConnectionOrientedTransportChannelFactory<TChannel>.OpenAsyncResult.End(result);
		}

		// Token: 0x06004C05 RID: 19461 RVA: 0x00115B28 File Offset: 0x00113D28
		protected override void OnOpen(TimeSpan timeout)
		{
			StreamUpgradeProvider streamUpgradeProvider = this.Upgrade;
			if (streamUpgradeProvider != null)
			{
				streamUpgradeProvider.Open(timeout);
			}
		}

		// Token: 0x06004C06 RID: 19462 RVA: 0x00115B46 File Offset: 0x00113D46
		protected virtual bool SupportsUpgrade(StreamUpgradeBindingElement upgradeBindingElement)
		{
			return true;
		}

		// Token: 0x04002F7A RID: 12154
		private int connectionBufferSize;

		// Token: 0x04002F7B RID: 12155
		private IConnectionInitiator connectionInitiator;

		// Token: 0x04002F7C RID: 12156
		private ConnectionPool connectionPool;

		// Token: 0x04002F7D RID: 12157
		private string connectionPoolGroupName;

		// Token: 0x04002F7E RID: 12158
		private bool exposeConnectionProperty;

		// Token: 0x04002F7F RID: 12159
		private TimeSpan idleTimeout;

		// Token: 0x04002F80 RID: 12160
		private int maxBufferSize;

		// Token: 0x04002F81 RID: 12161
		private int maxOutboundConnectionsPerEndpoint;

		// Token: 0x04002F82 RID: 12162
		private TimeSpan maxOutputDelay;

		// Token: 0x04002F83 RID: 12163
		private TransferMode transferMode;

		// Token: 0x04002F84 RID: 12164
		private ISecurityCapabilities securityCapabilities;

		// Token: 0x04002F85 RID: 12165
		private StreamUpgradeProvider upgrade;

		// Token: 0x04002F86 RID: 12166
		private bool flowIdentity;

		// Token: 0x02000D01 RID: 3329
		private class OpenAsyncResult : AsyncResult
		{
			// Token: 0x06007AC3 RID: 31427 RVA: 0x001C9390 File Offset: 0x001C7590
			public OpenAsyncResult(ICommunicationObject communicationObject, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.communicationObject = communicationObject;
				if (this.communicationObject == null)
				{
					base.Complete(true);
					return;
				}
				IAsyncResult asyncResult = this.communicationObject.BeginOpen(timeout, ConnectionOrientedTransportChannelFactory<TChannel>.OpenAsyncResult.onOpenComplete, this);
				if (asyncResult.CompletedSynchronously)
				{
					this.communicationObject.EndOpen(asyncResult);
					base.Complete(true);
				}
			}

			// Token: 0x06007AC4 RID: 31428 RVA: 0x001C93EC File Offset: 0x001C75EC
			private static void OnOpenComplete(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ConnectionOrientedTransportChannelFactory<TChannel>.OpenAsyncResult openAsyncResult = (ConnectionOrientedTransportChannelFactory<TChannel>.OpenAsyncResult)result.AsyncState;
				Exception exception = null;
				try
				{
					openAsyncResult.communicationObject.EndOpen(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				openAsyncResult.Complete(false, exception);
			}

			// Token: 0x06007AC5 RID: 31429 RVA: 0x001C9448 File Offset: 0x001C7648
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<ConnectionOrientedTransportChannelFactory<TChannel>.OpenAsyncResult>(result);
			}

			// Token: 0x04004633 RID: 17971
			private ICommunicationObject communicationObject;

			// Token: 0x04004634 RID: 17972
			private static AsyncCallback onOpenComplete = Fx.ThunkCallback(new AsyncCallback(ConnectionOrientedTransportChannelFactory<TChannel>.OpenAsyncResult.OnOpenComplete));
		}

		// Token: 0x02000D02 RID: 3330
		private class CloseAsyncResult : AsyncResult
		{
			// Token: 0x06007AC7 RID: 31431 RVA: 0x001C946C File Offset: 0x001C766C
			public CloseAsyncResult(ConnectionOrientedTransportChannelFactory<TChannel> parent, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.parent = parent;
				this.timeoutHelper = new TimeoutHelper(timeout);
				this.parent.GetUpgradeAndConnectionPool(out this.upgradeProvider, out this.connectionPool);
				if (this.connectionPool == null)
				{
					if (this.HandleReleaseConnectionPoolComplete())
					{
						base.Complete(true);
						return;
					}
				}
				else
				{
					if (ConnectionOrientedTransportChannelFactory<TChannel>.CloseAsyncResult.onReleaseConnectionPoolScheduled == null)
					{
						ConnectionOrientedTransportChannelFactory<TChannel>.CloseAsyncResult.onReleaseConnectionPoolScheduled = new Action<object>(ConnectionOrientedTransportChannelFactory<TChannel>.CloseAsyncResult.OnReleaseConnectionPoolScheduled);
					}
					ActionItem.Schedule(ConnectionOrientedTransportChannelFactory<TChannel>.CloseAsyncResult.onReleaseConnectionPoolScheduled, this);
				}
			}

			// Token: 0x06007AC8 RID: 31432 RVA: 0x001C94E8 File Offset: 0x001C76E8
			private bool HandleReleaseConnectionPoolComplete()
			{
				if (this.upgradeProvider == null)
				{
					return true;
				}
				IAsyncResult asyncResult = this.upgradeProvider.BeginClose(this.timeoutHelper.RemainingTime(), ConnectionOrientedTransportChannelFactory<TChannel>.CloseAsyncResult.onCloseComplete, this);
				if (asyncResult.CompletedSynchronously)
				{
					this.upgradeProvider.EndClose(asyncResult);
					return true;
				}
				return false;
			}

			// Token: 0x06007AC9 RID: 31433 RVA: 0x001C9533 File Offset: 0x001C7733
			private bool OnReleaseConnectionPoolScheduled()
			{
				this.parent.ReleaseConnectionPool(this.connectionPool, this.timeoutHelper.RemainingTime());
				return this.HandleReleaseConnectionPoolComplete();
			}

			// Token: 0x06007ACA RID: 31434 RVA: 0x001C9558 File Offset: 0x001C7758
			private static void OnReleaseConnectionPoolScheduled(object state)
			{
				ConnectionOrientedTransportChannelFactory<TChannel>.CloseAsyncResult closeAsyncResult = (ConnectionOrientedTransportChannelFactory<TChannel>.CloseAsyncResult)state;
				Exception exception = null;
				bool flag;
				try
				{
					flag = closeAsyncResult.OnReleaseConnectionPoolScheduled();
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = ex;
				}
				if (flag)
				{
					closeAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06007ACB RID: 31435 RVA: 0x001C95A4 File Offset: 0x001C77A4
			private static void OnCloseComplete(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ConnectionOrientedTransportChannelFactory<TChannel>.CloseAsyncResult closeAsyncResult = (ConnectionOrientedTransportChannelFactory<TChannel>.CloseAsyncResult)result.AsyncState;
				Exception exception = null;
				try
				{
					closeAsyncResult.upgradeProvider.EndClose(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				closeAsyncResult.Complete(false, exception);
			}

			// Token: 0x06007ACC RID: 31436 RVA: 0x001C9600 File Offset: 0x001C7800
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<ConnectionOrientedTransportChannelFactory<TChannel>.CloseAsyncResult>(result);
			}

			// Token: 0x04004635 RID: 17973
			private ConnectionOrientedTransportChannelFactory<TChannel> parent;

			// Token: 0x04004636 RID: 17974
			private ConnectionPool connectionPool;

			// Token: 0x04004637 RID: 17975
			private StreamUpgradeProvider upgradeProvider;

			// Token: 0x04004638 RID: 17976
			private TimeoutHelper timeoutHelper;

			// Token: 0x04004639 RID: 17977
			private static AsyncCallback onCloseComplete = Fx.ThunkCallback(new AsyncCallback(ConnectionOrientedTransportChannelFactory<TChannel>.CloseAsyncResult.OnCloseComplete));

			// Token: 0x0400463A RID: 17978
			private static Action<object> onReleaseConnectionPoolScheduled;
		}
	}
}
