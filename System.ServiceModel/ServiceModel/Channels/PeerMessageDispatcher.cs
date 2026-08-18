using System;
using System.Runtime;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A10 RID: 2576
	internal class PeerMessageDispatcher<ChannelInterfaceType, TChannel> : CommunicationObject where ChannelInterfaceType : class, IChannel where TChannel : InputQueueChannel<Message>
	{
		// Token: 0x060065EA RID: 26090 RVA: 0x0017BB2C File Offset: 0x00179D2C
		public PeerMessageDispatcher(PeerMessageDispatcher<ChannelInterfaceType, TChannel>.PeerMessageQueueAdapter queueHandler, PeerNodeImplementation peerNode, ChannelManagerBase channelManager, EndpointAddress to, Uri via)
		{
			PeerNodeImplementation.ValidateVia(via);
			this.queueHandler = queueHandler;
			this.peerNode = peerNode;
			this.to = to;
			this.via = via;
			this.channelManager = channelManager;
			EndpointAddress endpointAddress = null;
			this.securityProtocol = ((IPeerFactory)channelManager).SecurityManager.CreateSecurityProtocol<ChannelInterfaceType>(to, ServiceDefaults.SendTimeout);
			if (typeof(IDuplexChannel).IsAssignableFrom(typeof(ChannelInterfaceType)))
			{
				endpointAddress = to;
			}
			PeerMessageFilter[] filters = new PeerMessageFilter[]
			{
				new PeerMessageFilter(via, endpointAddress)
			};
			peerNode.RegisterMessageFilter(this, this.via, filters, (ITransportFactorySettings)this.channelManager, new PeerNodeImplementation.MessageAvailableCallback(this.OnMessageAvailable), this.securityProtocol);
			this.registered = true;
		}

		// Token: 0x17001897 RID: 6295
		// (get) Token: 0x060065EB RID: 26091 RVA: 0x0017BBFC File Offset: 0x00179DFC
		protected override TimeSpan DefaultCloseTimeout
		{
			get
			{
				return this.channelManager.InternalCloseTimeout;
			}
		}

		// Token: 0x17001898 RID: 6296
		// (get) Token: 0x060065EC RID: 26092 RVA: 0x0017BC09 File Offset: 0x00179E09
		protected override TimeSpan DefaultOpenTimeout
		{
			get
			{
				return this.channelManager.InternalOpenTimeout;
			}
		}

		// Token: 0x17001899 RID: 6297
		// (get) Token: 0x060065ED RID: 26093 RVA: 0x0017BC16 File Offset: 0x00179E16
		public SecurityProtocol SecurityProtocol
		{
			get
			{
				return this.securityProtocol;
			}
		}

		// Token: 0x060065EE RID: 26094 RVA: 0x0017BC1E File Offset: 0x00179E1E
		protected override void OnAbort()
		{
		}

		// Token: 0x060065EF RID: 26095 RVA: 0x0017BC20 File Offset: 0x00179E20
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.OnClose(timeout);
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x060065F0 RID: 26096 RVA: 0x0017BC30 File Offset: 0x00179E30
		protected override void OnEndClose(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x060065F1 RID: 26097 RVA: 0x0017BC38 File Offset: 0x00179E38
		protected override void OnClose(TimeSpan timeout)
		{
			this.Unregister(true);
		}

		// Token: 0x060065F2 RID: 26098 RVA: 0x0017BC41 File Offset: 0x00179E41
		internal void Unregister()
		{
			this.Unregister(false);
		}

		// Token: 0x060065F3 RID: 26099 RVA: 0x0017BC4C File Offset: 0x00179E4C
		internal void Unregister(bool release)
		{
			PeerNodeImplementation peerNodeImplementation = this.peerNode;
			if (peerNodeImplementation != null)
			{
				if (this.registered)
				{
					peerNodeImplementation.UnregisterMessageFilter(this, this.via);
					this.registered = false;
				}
				if (release)
				{
					peerNodeImplementation.Release();
				}
			}
		}

		// Token: 0x060065F4 RID: 26100 RVA: 0x0017BC88 File Offset: 0x00179E88
		protected override void OnOpen(TimeSpan timeout)
		{
		}

		// Token: 0x060065F5 RID: 26101 RVA: 0x0017BC8A File Offset: 0x00179E8A
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x060065F6 RID: 26102 RVA: 0x0017BC93 File Offset: 0x00179E93
		protected override void OnEndOpen(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x060065F7 RID: 26103 RVA: 0x0017BC9B File Offset: 0x00179E9B
		public void OnMessageAvailable(Message message)
		{
			this.quotaHelper.ReadyToEnqueueItem();
			this.queueHandler.EnqueueAndDispatch(message, new Action(this.quotaHelper.ItemDequeued));
		}

		// Token: 0x04003ACB RID: 15051
		private Uri via;

		// Token: 0x04003ACC RID: 15052
		private EndpointAddress to;

		// Token: 0x04003ACD RID: 15053
		private SecurityProtocol securityProtocol;

		// Token: 0x04003ACE RID: 15054
		private PeerNodeImplementation peerNode;

		// Token: 0x04003ACF RID: 15055
		private PeerMessageDispatcher<ChannelInterfaceType, TChannel>.PeerMessageQueueAdapter queueHandler;

		// Token: 0x04003AD0 RID: 15056
		private ChannelManagerBase channelManager;

		// Token: 0x04003AD1 RID: 15057
		private PeerQuotaHelper quotaHelper = new PeerQuotaHelper(int.MaxValue);

		// Token: 0x04003AD2 RID: 15058
		private bool registered;

		// Token: 0x02000E5D RID: 3677
		public class PeerMessageQueueAdapter
		{
			// Token: 0x06008356 RID: 33622 RVA: 0x001E6249 File Offset: 0x001E4449
			public PeerMessageQueueAdapter(SingletonChannelAcceptor<ChannelInterfaceType, TChannel, Message> singletonChannelAcceptor)
			{
				this.singletonChannelAcceptor = singletonChannelAcceptor;
			}

			// Token: 0x06008357 RID: 33623 RVA: 0x001E6258 File Offset: 0x001E4458
			public PeerMessageQueueAdapter(InputQueueChannel<Message> inputQueueChannel)
			{
				this.inputQueueChannel = inputQueueChannel;
			}

			// Token: 0x06008358 RID: 33624 RVA: 0x001E6267 File Offset: 0x001E4467
			public void EnqueueAndDispatch(Message message, Action callback)
			{
				if (this.singletonChannelAcceptor != null)
				{
					this.singletonChannelAcceptor.Enqueue(message, callback);
					return;
				}
				if (this.inputQueueChannel != null)
				{
					this.inputQueueChannel.EnqueueAndDispatch(message, callback);
				}
			}

			// Token: 0x04004AC6 RID: 19142
			private SingletonChannelAcceptor<ChannelInterfaceType, TChannel, Message> singletonChannelAcceptor;

			// Token: 0x04004AC7 RID: 19143
			private InputQueueChannel<Message> inputQueueChannel;
		}
	}
}
