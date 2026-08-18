using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000925 RID: 2341
	internal class ReliableInputListenerOverReply : ReliableListenerOverReply<IInputSessionChannel, ReliableInputSessionChannelOverReply>
	{
		// Token: 0x060059B3 RID: 22963 RVA: 0x00147BCE File Offset: 0x00145DCE
		public ReliableInputListenerOverReply(ReliableSessionBindingElement binding, BindingContext context) : base(binding, context)
		{
		}

		// Token: 0x170015D1 RID: 5585
		// (get) Token: 0x060059B4 RID: 22964 RVA: 0x00147BD8 File Offset: 0x00145DD8
		protected override bool Duplex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060059B5 RID: 22965 RVA: 0x00147BDB File Offset: 0x00145DDB
		protected override ReliableInputSessionChannelOverReply CreateChannel(UniqueId id, CreateSequenceInfo createSequenceInfo, IServerReliableChannelBinder binder)
		{
			binder.Open(base.InternalOpenTimeout);
			return new ReliableInputSessionChannelOverReply(this, binder, base.FaultHelper, id);
		}

		// Token: 0x060059B6 RID: 22966 RVA: 0x00147BF7 File Offset: 0x00145DF7
		protected override void ProcessSequencedItem(ReliableInputSessionChannelOverReply reliableChannel, RequestContext context, WsrmMessageInfo info)
		{
			reliableChannel.ProcessDemuxedRequest(reliableChannel.Binder.WrapRequestContext(context), info);
		}
	}
}
