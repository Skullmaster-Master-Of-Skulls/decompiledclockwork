using System;
using System.Diagnostics;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A13 RID: 2579
	internal sealed class PeerInputChannelAcceptor : SingletonChannelAcceptor<IInputChannel, PeerInputChannel, Message>
	{
		// Token: 0x0600660C RID: 26124 RVA: 0x0017C0A0 File Offset: 0x0017A2A0
		public PeerInputChannelAcceptor(PeerNodeImplementation peerNode, PeerNodeImplementation.Registration registration, ChannelManagerBase channelManager, EndpointAddress localAddress, Uri via) : base(channelManager)
		{
			this.registration = registration;
			this.peerNode = peerNode;
			this.localAddress = localAddress;
			this.via = via;
			PeerMessageDispatcher<IInputChannel, PeerInputChannel>.PeerMessageQueueAdapter queueHandler = new PeerMessageDispatcher<IInputChannel, PeerInputChannel>.PeerMessageQueueAdapter(this);
			this.dispatcher = new PeerMessageDispatcher<IInputChannel, PeerInputChannel>(queueHandler, peerNode, base.ChannelManager, localAddress, via);
		}

		// Token: 0x0600660D RID: 26125 RVA: 0x0017C0F0 File Offset: 0x0017A2F0
		protected override PeerInputChannel OnCreateChannel()
		{
			return new PeerInputChannel(this.peerNode, this.registration, base.ChannelManager, this.localAddress, this.via);
		}

		// Token: 0x0600660E RID: 26126 RVA: 0x0017C115 File Offset: 0x0017A315
		protected override void OnTraceMessageReceived(Message message)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 262163, SR.GetString("TraceCodeMessageReceived"), MessageTransmitTraceRecord.CreateReceiveTraceRecord(message), this, null);
			}
		}

		// Token: 0x0600660F RID: 26127 RVA: 0x0017C13B File Offset: 0x0017A33B
		protected override void OnClose(TimeSpan timeout)
		{
		}

		// Token: 0x06006610 RID: 26128 RVA: 0x0017C13D File Offset: 0x0017A33D
		protected override void OnClosing()
		{
			this.CloseDispatcher();
			base.OnClosing();
		}

		// Token: 0x06006611 RID: 26129 RVA: 0x0017C14B File Offset: 0x0017A34B
		protected override void OnFaulted()
		{
			this.CloseDispatcher();
			base.OnFaulted();
		}

		// Token: 0x06006612 RID: 26130 RVA: 0x0017C159 File Offset: 0x0017A359
		private void CloseDispatcher()
		{
			if (this.dispatcher != null)
			{
				this.dispatcher.Unregister(true);
				this.dispatcher = null;
			}
		}

		// Token: 0x04003AD9 RID: 15065
		private PeerNodeImplementation peerNode;

		// Token: 0x04003ADA RID: 15066
		private PeerNodeImplementation.Registration registration;

		// Token: 0x04003ADB RID: 15067
		private EndpointAddress localAddress;

		// Token: 0x04003ADC RID: 15068
		private Uri via;

		// Token: 0x04003ADD RID: 15069
		private PeerMessageDispatcher<IInputChannel, PeerInputChannel> dispatcher;
	}
}
