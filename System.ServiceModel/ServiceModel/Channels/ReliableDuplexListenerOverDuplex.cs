using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000921 RID: 2337
	internal class ReliableDuplexListenerOverDuplex : ReliableListenerOverDuplex<IDuplexSessionChannel, ServerReliableDuplexSessionChannel>
	{
		// Token: 0x060059A3 RID: 22947 RVA: 0x00147A99 File Offset: 0x00145C99
		public ReliableDuplexListenerOverDuplex(ReliableSessionBindingElement binding, BindingContext context) : base(binding, context)
		{
		}

		// Token: 0x170015CD RID: 5581
		// (get) Token: 0x060059A4 RID: 22948 RVA: 0x00147AA3 File Offset: 0x00145CA3
		protected override bool Duplex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060059A5 RID: 22949 RVA: 0x00147AA6 File Offset: 0x00145CA6
		protected override ServerReliableDuplexSessionChannel CreateChannel(UniqueId id, CreateSequenceInfo createSequenceInfo, IServerReliableChannelBinder binder)
		{
			binder.Open(base.InternalOpenTimeout);
			return new ServerReliableDuplexSessionChannel(this, binder, base.FaultHelper, id, createSequenceInfo.OfferIdentifier);
		}

		// Token: 0x060059A6 RID: 22950 RVA: 0x00147AC8 File Offset: 0x00145CC8
		protected override void ProcessSequencedItem(ServerReliableDuplexSessionChannel channel, Message message, WsrmMessageInfo info)
		{
			channel.ProcessDemuxedMessage(info);
		}
	}
}
