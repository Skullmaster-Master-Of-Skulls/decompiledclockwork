using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000857 RID: 2135
	internal class TcpDuplexChannelListener : TcpChannelListener<IDuplexSessionChannel, InputQueueChannelAcceptor<IDuplexSessionChannel>>, ISessionPreambleHandler
	{
		// Token: 0x06005013 RID: 20499 RVA: 0x00125C30 File Offset: 0x00123E30
		public TcpDuplexChannelListener(TcpTransportBindingElement bindingElement, BindingContext context) : base(bindingElement, context)
		{
			this.duplexAcceptor = new InputQueueChannelAcceptor<IDuplexSessionChannel>(this);
		}

		// Token: 0x170013D4 RID: 5076
		// (get) Token: 0x06005014 RID: 20500 RVA: 0x00125C46 File Offset: 0x00123E46
		protected override InputQueueChannelAcceptor<IDuplexSessionChannel> ChannelAcceptor
		{
			get
			{
				return this.duplexAcceptor;
			}
		}

		// Token: 0x06005015 RID: 20501 RVA: 0x00125C50 File Offset: 0x00123E50
		void ISessionPreambleHandler.HandleServerSessionPreamble(ServerSessionPreambleConnectionReader preambleReader, ConnectionDemuxer connectionDemuxer)
		{
			IDuplexSessionChannel channel = preambleReader.CreateDuplexSessionChannel(this, new EndpointAddress(this.Uri, new AddressHeader[0]), base.ExposeConnectionProperty, connectionDemuxer);
			this.duplexAcceptor.EnqueueAndDispatch(channel, preambleReader.ConnectionDequeuedCallback);
		}

		// Token: 0x0400319A RID: 12698
		private InputQueueChannelAcceptor<IDuplexSessionChannel> duplexAcceptor;
	}
}
