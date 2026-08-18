using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009F8 RID: 2552
	[Obsolete("PeerChannel feature is obsolete and will be removed in the future.", false)]
	internal sealed class PeerDuplexChannelListener : PeerChannelListener<IDuplexChannel, PeerDuplexChannelAcceptor>
	{
		// Token: 0x06006547 RID: 25927 RVA: 0x00179AA6 File Offset: 0x00177CA6
		public PeerDuplexChannelListener(PeerTransportBindingElement bindingElement, BindingContext context, PeerResolver peerResolver) : base(bindingElement, context, peerResolver)
		{
		}

		// Token: 0x17001871 RID: 6257
		// (get) Token: 0x06006548 RID: 25928 RVA: 0x00179AB1 File Offset: 0x00177CB1
		protected override PeerDuplexChannelAcceptor ChannelAcceptor
		{
			get
			{
				return this.duplexAcceptor;
			}
		}

		// Token: 0x06006549 RID: 25929 RVA: 0x00179AB9 File Offset: 0x00177CB9
		protected override void CreateAcceptor()
		{
			this.duplexAcceptor = new PeerDuplexChannelAcceptor(base.InnerNode, base.Registration, this, new EndpointAddress(this.Uri, new AddressHeader[0]), base.BaseUri);
		}

		// Token: 0x04003A19 RID: 14873
		private PeerDuplexChannelAcceptor duplexAcceptor;
	}
}
