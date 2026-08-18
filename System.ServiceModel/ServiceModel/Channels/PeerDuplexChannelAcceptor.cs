using System;
using System.Diagnostics;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009F7 RID: 2551
	[Obsolete("PeerChannel feature is obsolete and will be removed in the future.", false)]
	internal sealed class PeerDuplexChannelAcceptor : SingletonChannelAcceptor<IDuplexChannel, PeerDuplexChannel, Message>
	{
		// Token: 0x06006540 RID: 25920 RVA: 0x001799D0 File Offset: 0x00177BD0
		public PeerDuplexChannelAcceptor(PeerNodeImplementation peerNode, PeerNodeImplementation.Registration registration, ChannelManagerBase channelManager, EndpointAddress localAddress, Uri via) : base(channelManager)
		{
			this.registration = registration;
			this.peerNode = peerNode;
			this.localAddress = localAddress;
			this.via = via;
			PeerMessageDispatcher<IDuplexChannel, PeerDuplexChannel>.PeerMessageQueueAdapter queueHandler = new PeerMessageDispatcher<IDuplexChannel, PeerDuplexChannel>.PeerMessageQueueAdapter(this);
			this.dispatcher = new PeerMessageDispatcher<IDuplexChannel, PeerDuplexChannel>(queueHandler, peerNode, base.ChannelManager, localAddress, via);
		}

		// Token: 0x06006541 RID: 25921 RVA: 0x00179A20 File Offset: 0x00177C20
		protected override void OnClose(TimeSpan timeout)
		{
		}

		// Token: 0x06006542 RID: 25922 RVA: 0x00179A22 File Offset: 0x00177C22
		protected override void OnClosing()
		{
			this.CloseDispatcher();
			base.OnClosing();
		}

		// Token: 0x06006543 RID: 25923 RVA: 0x00179A30 File Offset: 0x00177C30
		protected override void OnFaulted()
		{
			this.CloseDispatcher();
			base.OnFaulted();
		}

		// Token: 0x06006544 RID: 25924 RVA: 0x00179A3E File Offset: 0x00177C3E
		private void CloseDispatcher()
		{
			if (this.dispatcher != null)
			{
				this.dispatcher.Unregister(true);
				this.dispatcher = null;
			}
		}

		// Token: 0x06006545 RID: 25925 RVA: 0x00179A5B File Offset: 0x00177C5B
		protected override PeerDuplexChannel OnCreateChannel()
		{
			return new PeerDuplexChannel(this.peerNode, this.registration, base.ChannelManager, this.localAddress, this.via);
		}

		// Token: 0x06006546 RID: 25926 RVA: 0x00179A80 File Offset: 0x00177C80
		protected override void OnTraceMessageReceived(Message message)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 262163, SR.GetString("TraceCodeMessageReceived"), MessageTransmitTraceRecord.CreateReceiveTraceRecord(message), this, null);
			}
		}

		// Token: 0x04003A14 RID: 14868
		private PeerNodeImplementation peerNode;

		// Token: 0x04003A15 RID: 14869
		private PeerNodeImplementation.Registration registration;

		// Token: 0x04003A16 RID: 14870
		private EndpointAddress localAddress;

		// Token: 0x04003A17 RID: 14871
		private Uri via;

		// Token: 0x04003A18 RID: 14872
		private PeerMessageDispatcher<IDuplexChannel, PeerDuplexChannel> dispatcher;
	}
}
