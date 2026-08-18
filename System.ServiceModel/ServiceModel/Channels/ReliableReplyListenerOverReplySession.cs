using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000928 RID: 2344
	internal class ReliableReplyListenerOverReplySession : ReliableListenerOverReplySession<IReplySessionChannel, ReliableReplySessionChannel>
	{
		// Token: 0x060059BF RID: 22975 RVA: 0x00147CC9 File Offset: 0x00145EC9
		public ReliableReplyListenerOverReplySession(ReliableSessionBindingElement binding, BindingContext context) : base(binding, context)
		{
		}

		// Token: 0x170015D4 RID: 5588
		// (get) Token: 0x060059C0 RID: 22976 RVA: 0x00147CD3 File Offset: 0x00145ED3
		protected override bool Duplex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060059C1 RID: 22977 RVA: 0x00147CD6 File Offset: 0x00145ED6
		protected override ReliableReplySessionChannel CreateChannel(UniqueId id, CreateSequenceInfo createSequenceInfo, IServerReliableChannelBinder binder)
		{
			binder.Open(base.InternalOpenTimeout);
			return new ReliableReplySessionChannel(this, binder, base.FaultHelper, id, createSequenceInfo.OfferIdentifier);
		}

		// Token: 0x060059C2 RID: 22978 RVA: 0x00147CF8 File Offset: 0x00145EF8
		protected override void ProcessSequencedItem(IReplySessionChannel channel, RequestContext context, ReliableReplySessionChannel reliableChannel, WsrmMessageInfo info, bool newChannel)
		{
			if (!newChannel)
			{
				IServerReliableChannelBinder binder = reliableChannel.Binder;
				if (!binder.UseNewChannel(channel))
				{
					context.RequestMessage.Close();
					context.Abort();
					channel.Abort();
					return;
				}
			}
			reliableChannel.ProcessDemuxedRequest(reliableChannel.Binder.WrapRequestContext(context), info);
		}
	}
}
