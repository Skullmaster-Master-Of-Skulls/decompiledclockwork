using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000926 RID: 2342
	internal class ReliableReplyListenerOverReply : ReliableListenerOverReply<IReplySessionChannel, ReliableReplySessionChannel>
	{
		// Token: 0x060059B7 RID: 22967 RVA: 0x00147C0C File Offset: 0x00145E0C
		public ReliableReplyListenerOverReply(ReliableSessionBindingElement binding, BindingContext context) : base(binding, context)
		{
		}

		// Token: 0x170015D2 RID: 5586
		// (get) Token: 0x060059B8 RID: 22968 RVA: 0x00147C16 File Offset: 0x00145E16
		protected override bool Duplex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060059B9 RID: 22969 RVA: 0x00147C19 File Offset: 0x00145E19
		protected override ReliableReplySessionChannel CreateChannel(UniqueId id, CreateSequenceInfo createSequenceInfo, IServerReliableChannelBinder binder)
		{
			binder.Open(base.InternalOpenTimeout);
			return new ReliableReplySessionChannel(this, binder, base.FaultHelper, id, createSequenceInfo.OfferIdentifier);
		}

		// Token: 0x060059BA RID: 22970 RVA: 0x00147C3B File Offset: 0x00145E3B
		protected override void ProcessSequencedItem(ReliableReplySessionChannel reliableChannel, RequestContext context, WsrmMessageInfo info)
		{
			reliableChannel.ProcessDemuxedRequest(reliableChannel.Binder.WrapRequestContext(context), info);
		}
	}
}
