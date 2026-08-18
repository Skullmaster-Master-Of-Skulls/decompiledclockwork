using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000841 RID: 2113
	internal class NamedPipeDuplexChannelListener : NamedPipeChannelListener<IDuplexSessionChannel, InputQueueChannelAcceptor<IDuplexSessionChannel>>, ISessionPreambleHandler
	{
		// Token: 0x06004EF7 RID: 20215 RVA: 0x0011F984 File Offset: 0x0011DB84
		public NamedPipeDuplexChannelListener(NamedPipeTransportBindingElement bindingElement, BindingContext context) : base(bindingElement, context)
		{
			this.duplexAcceptor = new InputQueueChannelAcceptor<IDuplexSessionChannel>(this);
		}

		// Token: 0x170013A9 RID: 5033
		// (get) Token: 0x06004EF8 RID: 20216 RVA: 0x0011F99A File Offset: 0x0011DB9A
		protected override InputQueueChannelAcceptor<IDuplexSessionChannel> ChannelAcceptor
		{
			get
			{
				return this.duplexAcceptor;
			}
		}

		// Token: 0x06004EF9 RID: 20217 RVA: 0x0011F9A4 File Offset: 0x0011DBA4
		void ISessionPreambleHandler.HandleServerSessionPreamble(ServerSessionPreambleConnectionReader preambleReader, ConnectionDemuxer connectionDemuxer)
		{
			IDuplexSessionChannel channel = preambleReader.CreateDuplexSessionChannel(this, new EndpointAddress(this.Uri, new AddressHeader[0]), base.ExposeConnectionProperty, connectionDemuxer);
			this.duplexAcceptor.EnqueueAndDispatch(channel, preambleReader.ConnectionDequeuedCallback);
		}

		// Token: 0x0400310E RID: 12558
		private InputQueueChannelAcceptor<IDuplexSessionChannel> duplexAcceptor;
	}
}
