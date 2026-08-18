using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A14 RID: 2580
	[Obsolete("PeerChannel feature is obsolete and will be removed in the future.", false)]
	internal sealed class PeerInputChannelListener : PeerChannelListener<IInputChannel, PeerInputChannelAcceptor>
	{
		// Token: 0x06006613 RID: 26131 RVA: 0x0017C176 File Offset: 0x0017A376
		public PeerInputChannelListener(PeerTransportBindingElement bindingElement, BindingContext context, PeerResolver peerResolver) : base(bindingElement, context, peerResolver)
		{
		}

		// Token: 0x1700189A RID: 6298
		// (get) Token: 0x06006614 RID: 26132 RVA: 0x0017C181 File Offset: 0x0017A381
		protected override PeerInputChannelAcceptor ChannelAcceptor
		{
			get
			{
				return this.inputAcceptor;
			}
		}

		// Token: 0x06006615 RID: 26133 RVA: 0x0017C189 File Offset: 0x0017A389
		protected override void CreateAcceptor()
		{
			this.inputAcceptor = new PeerInputChannelAcceptor(base.InnerNode, base.Registration, this, new EndpointAddress(this.Uri, new AddressHeader[0]), this.Uri);
		}

		// Token: 0x04003ADE RID: 15070
		private PeerInputChannelAcceptor inputAcceptor;
	}
}
