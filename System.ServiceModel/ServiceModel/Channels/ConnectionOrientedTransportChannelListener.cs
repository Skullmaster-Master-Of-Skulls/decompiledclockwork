using System;
using System.Collections.ObjectModel;
using System.Runtime;
using System.ServiceModel.Description;
using System.Text;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007DD RID: 2013
	internal abstract class ConnectionOrientedTransportChannelListener : TransportChannelListener, IConnectionOrientedTransportFactorySettings, ITransportFactorySettings, IDefaultCommunicationTimeouts, IConnectionOrientedConnectionSettings, IConnectionOrientedListenerSettings
	{
		// Token: 0x06004C07 RID: 19463 RVA: 0x00115B4C File Offset: 0x00113D4C
		protected ConnectionOrientedTransportChannelListener(ConnectionOrientedTransportBindingElement bindingElement, BindingContext context) : base(bindingElement, context, bindingElement.HostNameComparisonMode)
		{
			if (bindingElement.TransferMode == TransferMode.Buffered)
			{
				if (bindingElement.MaxReceivedMessageSize > 2147483647L)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("bindingElement.MaxReceivedMessageSize", SR.GetString("MaxReceivedMessageSizeMustBeInIntegerRange")));
				}
				if ((long)bindingElement.MaxBufferSize != bindingElement.MaxReceivedMessageSize)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("bindingElement", SR.GetString("MaxBufferSizeMustMatchMaxReceivedMessageSize"));
				}
			}
			else if ((long)bindingElement.MaxBufferSize > bindingElement.MaxReceivedMessageSize)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("bindingElement", SR.GetString("MaxBufferSizeMustNotExceedMaxReceivedMessageSize"));
			}
			this.connectionBufferSize = bindingElement.ConnectionBufferSize;
			this.exposeConnectionProperty = bindingElement.ExposeConnectionProperty;
			base.InheritBaseAddressSettings = bindingElement.InheritBaseAddressSettings;
			this.channelInitializationTimeout = bindingElement.ChannelInitializationTimeout;
			this.maxBufferSize = bindingElement.MaxBufferSize;
			this.maxPendingConnections = bindingElement.MaxPendingConnections;
			this.maxOutputDelay = bindingElement.MaxOutputDelay;
			this.maxPendingAccepts = bindingElement.MaxPendingAccepts;
			this.transferMode = bindingElement.TransferMode;
			Collection<StreamUpgradeBindingElement> collection = context.BindingParameters.FindAll<StreamUpgradeBindingElement>();
			if (collection.Count > 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MultipleStreamUpgradeProvidersInParameters")));
			}
			if (collection.Count == 1 && this.SupportsUpgrade(collection[0]))
			{
				this.upgrade = collection[0].BuildServerStreamUpgradeProvider(context);
				this.ownUpgrade = true;
				context.BindingParameters.Remove<StreamUpgradeBindingElement>();
				this.securityCapabilities = collection[0].GetProperty<ISecurityCapabilities>(context);
			}
		}

		// Token: 0x17001313 RID: 4883
		// (get) Token: 0x06004C08 RID: 19464 RVA: 0x00115CDA File Offset: 0x00113EDA
		public int ConnectionBufferSize
		{
			get
			{
				return this.connectionBufferSize;
			}
		}

		// Token: 0x17001314 RID: 4884
		// (get) Token: 0x06004C09 RID: 19465 RVA: 0x00115CE2 File Offset: 0x00113EE2
		public TimeSpan IdleTimeout
		{
			get
			{
				return this.idleTimeout;
			}
		}

		// Token: 0x17001315 RID: 4885
		// (get) Token: 0x06004C0A RID: 19466 RVA: 0x00115CEA File Offset: 0x00113EEA
		public int MaxPooledConnections
		{
			get
			{
				return this.maxPooledConnections;
			}
		}

		// Token: 0x06004C0B RID: 19467 RVA: 0x00115CF2 File Offset: 0x00113EF2
		internal void SetIdleTimeout(TimeSpan idleTimeout)
		{
			this.idleTimeout = idleTimeout;
		}

		// Token: 0x06004C0C RID: 19468 RVA: 0x00115CFB File Offset: 0x00113EFB
		internal void InitializeMaxPooledConnections(int maxOutboundConnectionsPerEndpoint)
		{
			if (maxOutboundConnectionsPerEndpoint == 10)
			{
				this.maxPooledConnections = ConnectionOrientedTransportDefaults.GetMaxConnections();
				return;
			}
			this.maxPooledConnections = maxOutboundConnectionsPerEndpoint;
		}

		// Token: 0x17001316 RID: 4886
		// (get) Token: 0x06004C0D RID: 19469 RVA: 0x00115D15 File Offset: 0x00113F15
		internal bool ExposeConnectionProperty
		{
			get
			{
				return this.exposeConnectionProperty;
			}
		}

		// Token: 0x17001317 RID: 4887
		// (get) Token: 0x06004C0E RID: 19470 RVA: 0x00115D1D File Offset: 0x00113F1D
		public HostNameComparisonMode HostNameComparisonMode
		{
			get
			{
				return base.HostNameComparisonModeInternal;
			}
		}

		// Token: 0x06004C0F RID: 19471 RVA: 0x00115D28 File Offset: 0x00113F28
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(EndpointIdentity))
			{
				return (T)((object)this.identity);
			}
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

		// Token: 0x17001318 RID: 4888
		// (get) Token: 0x06004C10 RID: 19472 RVA: 0x00115DA7 File Offset: 0x00113FA7
		public TimeSpan ChannelInitializationTimeout
		{
			get
			{
				return this.channelInitializationTimeout;
			}
		}

		// Token: 0x17001319 RID: 4889
		// (get) Token: 0x06004C11 RID: 19473 RVA: 0x00115DAF File Offset: 0x00113FAF
		public int MaxBufferSize
		{
			get
			{
				return this.maxBufferSize;
			}
		}

		// Token: 0x1700131A RID: 4890
		// (get) Token: 0x06004C12 RID: 19474 RVA: 0x00115DB7 File Offset: 0x00113FB7
		public int MaxPendingConnections
		{
			get
			{
				return this.maxPendingConnections;
			}
		}

		// Token: 0x1700131B RID: 4891
		// (get) Token: 0x06004C13 RID: 19475 RVA: 0x00115DBF File Offset: 0x00113FBF
		public TimeSpan MaxOutputDelay
		{
			get
			{
				return this.maxOutputDelay;
			}
		}

		// Token: 0x1700131C RID: 4892
		// (get) Token: 0x06004C14 RID: 19476 RVA: 0x00115DC7 File Offset: 0x00113FC7
		public int MaxPendingAccepts
		{
			get
			{
				return this.maxPendingAccepts;
			}
		}

		// Token: 0x1700131D RID: 4893
		// (get) Token: 0x06004C15 RID: 19477 RVA: 0x00115DCF File Offset: 0x00113FCF
		public StreamUpgradeProvider Upgrade
		{
			get
			{
				return this.upgrade;
			}
		}

		// Token: 0x1700131E RID: 4894
		// (get) Token: 0x06004C16 RID: 19478 RVA: 0x00115DD7 File Offset: 0x00113FD7
		public TransferMode TransferMode
		{
			get
			{
				return this.transferMode;
			}
		}

		// Token: 0x1700131F RID: 4895
		// (get) Token: 0x06004C17 RID: 19479 RVA: 0x00115DDF File Offset: 0x00113FDF
		int IConnectionOrientedTransportFactorySettings.MaxBufferSize
		{
			get
			{
				return this.MaxBufferSize;
			}
		}

		// Token: 0x17001320 RID: 4896
		// (get) Token: 0x06004C18 RID: 19480 RVA: 0x00115DE7 File Offset: 0x00113FE7
		TransferMode IConnectionOrientedTransportFactorySettings.TransferMode
		{
			get
			{
				return this.TransferMode;
			}
		}

		// Token: 0x17001321 RID: 4897
		// (get) Token: 0x06004C19 RID: 19481 RVA: 0x00115DEF File Offset: 0x00113FEF
		StreamUpgradeProvider IConnectionOrientedTransportFactorySettings.Upgrade
		{
			get
			{
				return this.Upgrade;
			}
		}

		// Token: 0x17001322 RID: 4898
		// (get) Token: 0x06004C1A RID: 19482 RVA: 0x00115DF7 File Offset: 0x00113FF7
		ServiceSecurityAuditBehavior IConnectionOrientedTransportFactorySettings.AuditBehavior
		{
			get
			{
				return base.AuditBehavior;
			}
		}

		// Token: 0x06004C1B RID: 19483 RVA: 0x00115DFF File Offset: 0x00113FFF
		internal override int GetMaxBufferSize()
		{
			return this.MaxBufferSize;
		}

		// Token: 0x06004C1C RID: 19484 RVA: 0x00115E08 File Offset: 0x00114008
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			StreamUpgradeProvider streamUpgradeProvider = this.Upgrade;
			if (streamUpgradeProvider != null)
			{
				return new ChainedOpenAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginOpen), new ChainedEndHandler(base.OnEndOpen), new ICommunicationObject[]
				{
					streamUpgradeProvider
				});
			}
			return base.OnBeginOpen(timeout, callback, state);
		}

		// Token: 0x06004C1D RID: 19485 RVA: 0x00115E53 File Offset: 0x00114053
		protected override void OnEndOpen(IAsyncResult result)
		{
			if (result is ChainedOpenAsyncResult)
			{
				ChainedAsyncResult.End(result);
				return;
			}
			base.OnEndOpen(result);
		}

		// Token: 0x06004C1E RID: 19486 RVA: 0x00115E6C File Offset: 0x0011406C
		protected override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnOpen(timeout);
			StreamUpgradeProvider streamUpgradeProvider = this.Upgrade;
			if (streamUpgradeProvider != null)
			{
				streamUpgradeProvider.Open(timeoutHelper.RemainingTime());
			}
		}

		// Token: 0x06004C1F RID: 19487 RVA: 0x00115EA0 File Offset: 0x001140A0
		protected override void OnOpened()
		{
			base.OnOpened();
			StreamSecurityUpgradeProvider streamSecurityUpgradeProvider = this.Upgrade as StreamSecurityUpgradeProvider;
			if (streamSecurityUpgradeProvider != null)
			{
				this.identity = streamSecurityUpgradeProvider.Identity;
			}
		}

		// Token: 0x06004C20 RID: 19488 RVA: 0x00115ED0 File Offset: 0x001140D0
		protected override void OnAbort()
		{
			StreamUpgradeProvider streamUpgradeProvider = this.GetUpgrade();
			if (streamUpgradeProvider != null)
			{
				streamUpgradeProvider.Abort();
			}
			base.OnAbort();
		}

		// Token: 0x06004C21 RID: 19489 RVA: 0x00115EF4 File Offset: 0x001140F4
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			StreamUpgradeProvider streamUpgradeProvider = this.GetUpgrade();
			if (streamUpgradeProvider != null)
			{
				return new ChainedCloseAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose), new ICommunicationObject[]
				{
					streamUpgradeProvider
				});
			}
			return new ChainedCloseAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose), new ICommunicationObject[0]);
		}

		// Token: 0x06004C22 RID: 19490 RVA: 0x00115F5C File Offset: 0x0011415C
		protected override void OnEndClose(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x06004C23 RID: 19491 RVA: 0x00115F64 File Offset: 0x00114164
		protected override void OnClose(TimeSpan timeout)
		{
			StreamUpgradeProvider streamUpgradeProvider = this.GetUpgrade();
			if (streamUpgradeProvider != null)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				streamUpgradeProvider.Close(timeoutHelper.RemainingTime());
				base.OnClose(timeoutHelper.RemainingTime());
				return;
			}
			base.OnClose(timeout);
		}

		// Token: 0x06004C24 RID: 19492 RVA: 0x00115FA8 File Offset: 0x001141A8
		private StreamUpgradeProvider GetUpgrade()
		{
			StreamUpgradeProvider result = null;
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (this.ownUpgrade)
				{
					result = this.upgrade;
					this.ownUpgrade = false;
				}
			}
			return result;
		}

		// Token: 0x06004C25 RID: 19493 RVA: 0x00115FFC File Offset: 0x001141FC
		protected override void ValidateUri(Uri uri)
		{
			base.ValidateUri(uri);
			int num = 2048;
			int byteCount = Encoding.UTF8.GetByteCount(uri.AbsoluteUri);
			if (byteCount > num)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QuotaExceededException(SR.GetString("UriLengthExceedsMaxSupportedSize", new object[]
				{
					uri,
					byteCount,
					num
				})));
			}
		}

		// Token: 0x06004C26 RID: 19494 RVA: 0x00116061 File Offset: 0x00114261
		protected virtual bool SupportsUpgrade(StreamUpgradeBindingElement upgradeBindingElement)
		{
			return true;
		}

		// Token: 0x04002F87 RID: 12167
		private int connectionBufferSize;

		// Token: 0x04002F88 RID: 12168
		private bool exposeConnectionProperty;

		// Token: 0x04002F89 RID: 12169
		private TimeSpan channelInitializationTimeout;

		// Token: 0x04002F8A RID: 12170
		private int maxBufferSize;

		// Token: 0x04002F8B RID: 12171
		private int maxPendingConnections;

		// Token: 0x04002F8C RID: 12172
		private TimeSpan maxOutputDelay;

		// Token: 0x04002F8D RID: 12173
		private int maxPendingAccepts;

		// Token: 0x04002F8E RID: 12174
		private TimeSpan idleTimeout;

		// Token: 0x04002F8F RID: 12175
		private int maxPooledConnections;

		// Token: 0x04002F90 RID: 12176
		private TransferMode transferMode;

		// Token: 0x04002F91 RID: 12177
		private ISecurityCapabilities securityCapabilities;

		// Token: 0x04002F92 RID: 12178
		private StreamUpgradeProvider upgrade;

		// Token: 0x04002F93 RID: 12179
		private bool ownUpgrade;

		// Token: 0x04002F94 RID: 12180
		private EndpointIdentity identity;

		// Token: 0x02000D03 RID: 3331
		protected class ConnectionOrientedTransportReplyChannelAcceptor : TransportReplyChannelAcceptor
		{
			// Token: 0x06007ACE RID: 31438 RVA: 0x001C9621 File Offset: 0x001C7821
			public ConnectionOrientedTransportReplyChannelAcceptor(ConnectionOrientedTransportChannelListener listener) : base(listener)
			{
				this.upgrade = listener.GetUpgrade();
			}

			// Token: 0x06007ACF RID: 31439 RVA: 0x001C9636 File Offset: 0x001C7836
			protected override ReplyChannel OnCreateChannel()
			{
				return new ConnectionOrientedTransportChannelListener.ConnectionOrientedTransportReplyChannelAcceptor.ConnectionOrientedTransportReplyChannel(base.ChannelManager, null);
			}

			// Token: 0x06007AD0 RID: 31440 RVA: 0x001C9644 File Offset: 0x001C7844
			protected override void OnAbort()
			{
				base.OnAbort();
				if (this.upgrade != null && !this.TransferUpgrade())
				{
					this.upgrade.Abort();
				}
			}

			// Token: 0x06007AD1 RID: 31441 RVA: 0x001C9667 File Offset: 0x001C7867
			private IAsyncResult DummyBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new CompletedAsyncResult(callback, state);
			}

			// Token: 0x06007AD2 RID: 31442 RVA: 0x001C9670 File Offset: 0x001C7870
			private void DummyEndClose(IAsyncResult result)
			{
				CompletedAsyncResult.End(result);
			}

			// Token: 0x06007AD3 RID: 31443 RVA: 0x001C9678 File Offset: 0x001C7878
			protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				ChainedBeginHandler begin = new ChainedBeginHandler(this.DummyBeginClose);
				ChainedEndHandler end = new ChainedEndHandler(this.DummyEndClose);
				if (this.upgrade != null && !this.TransferUpgrade())
				{
					begin = new ChainedBeginHandler(this.upgrade.BeginClose);
					end = new ChainedEndHandler(this.upgrade.EndClose);
				}
				return new ChainedAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose), begin, end);
			}

			// Token: 0x06007AD4 RID: 31444 RVA: 0x001C96F7 File Offset: 0x001C78F7
			protected override void OnEndClose(IAsyncResult result)
			{
				ChainedAsyncResult.End(result);
			}

			// Token: 0x06007AD5 RID: 31445 RVA: 0x001C9700 File Offset: 0x001C7900
			protected override void OnClose(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				base.OnClose(timeoutHelper.RemainingTime());
				if (this.upgrade != null && !this.TransferUpgrade())
				{
					this.upgrade.Close(timeoutHelper.RemainingTime());
				}
			}

			// Token: 0x06007AD6 RID: 31446 RVA: 0x001C9744 File Offset: 0x001C7944
			private bool TransferUpgrade()
			{
				ConnectionOrientedTransportChannelListener.ConnectionOrientedTransportReplyChannelAcceptor.ConnectionOrientedTransportReplyChannel connectionOrientedTransportReplyChannel = (ConnectionOrientedTransportChannelListener.ConnectionOrientedTransportReplyChannelAcceptor.ConnectionOrientedTransportReplyChannel)base.GetCurrentChannel();
				return connectionOrientedTransportReplyChannel != null && connectionOrientedTransportReplyChannel.TransferUpgrade(this.upgrade);
			}

			// Token: 0x0400463B RID: 17979
			private StreamUpgradeProvider upgrade;

			// Token: 0x02000F42 RID: 3906
			private class ConnectionOrientedTransportReplyChannel : TransportReplyChannelAcceptor.TransportReplyChannel
			{
				// Token: 0x060086B3 RID: 34483 RVA: 0x001F2FBF File Offset: 0x001F11BF
				public ConnectionOrientedTransportReplyChannel(ChannelManagerBase channelManager, EndpointAddress localAddress) : base(channelManager, localAddress)
				{
				}

				// Token: 0x060086B4 RID: 34484 RVA: 0x001F2FCC File Offset: 0x001F11CC
				public bool TransferUpgrade(StreamUpgradeProvider upgrade)
				{
					object thisLock = base.ThisLock;
					bool result;
					lock (thisLock)
					{
						if (base.State != CommunicationState.Opened)
						{
							result = false;
						}
						else
						{
							this.upgrade = upgrade;
							result = true;
						}
					}
					return result;
				}

				// Token: 0x060086B5 RID: 34485 RVA: 0x001F3020 File Offset: 0x001F1220
				protected override void OnAbort()
				{
					if (this.upgrade != null)
					{
						this.upgrade.Abort();
					}
					base.OnAbort();
				}

				// Token: 0x060086B6 RID: 34486 RVA: 0x001F303C File Offset: 0x001F123C
				protected override void OnClose(TimeSpan timeout)
				{
					TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
					if (this.upgrade != null)
					{
						this.upgrade.Close(timeoutHelper.RemainingTime());
					}
					base.OnClose(timeoutHelper.RemainingTime());
				}

				// Token: 0x060086B7 RID: 34487 RVA: 0x001F3078 File Offset: 0x001F1278
				private IAsyncResult DummyBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
				{
					return new CompletedAsyncResult(callback, state);
				}

				// Token: 0x060086B8 RID: 34488 RVA: 0x001F3081 File Offset: 0x001F1281
				private void DummyEndClose(IAsyncResult result)
				{
					CompletedAsyncResult.End(result);
				}

				// Token: 0x060086B9 RID: 34489 RVA: 0x001F308C File Offset: 0x001F128C
				protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
				{
					ChainedBeginHandler begin = new ChainedBeginHandler(this.DummyBeginClose);
					ChainedEndHandler end = new ChainedEndHandler(this.DummyEndClose);
					if (this.upgrade != null)
					{
						begin = new ChainedBeginHandler(this.upgrade.BeginClose);
						end = new ChainedEndHandler(this.upgrade.EndClose);
					}
					return new ChainedAsyncResult(timeout, callback, state, begin, end, new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose));
				}

				// Token: 0x060086BA RID: 34490 RVA: 0x001F3103 File Offset: 0x001F1303
				protected override void OnEndClose(IAsyncResult result)
				{
					ChainedAsyncResult.End(result);
				}

				// Token: 0x04004E48 RID: 20040
				private StreamUpgradeProvider upgrade;
			}
		}
	}
}
