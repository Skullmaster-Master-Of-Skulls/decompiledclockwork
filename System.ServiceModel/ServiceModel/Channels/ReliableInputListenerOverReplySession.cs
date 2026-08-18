using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000927 RID: 2343
	internal class ReliableInputListenerOverReplySession : ReliableListenerOverReplySession<IInputSessionChannel, ReliableInputSessionChannelOverReply>
	{
		// Token: 0x060059BB RID: 22971 RVA: 0x00147C50 File Offset: 0x00145E50
		public ReliableInputListenerOverReplySession(ReliableSessionBindingElement binding, BindingContext context) : base(binding, context)
		{
		}

		// Token: 0x170015D3 RID: 5587
		// (get) Token: 0x060059BC RID: 22972 RVA: 0x00147C5A File Offset: 0x00145E5A
		protected override bool Duplex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060059BD RID: 22973 RVA: 0x00147C5D File Offset: 0x00145E5D
		protected override ReliableInputSessionChannelOverReply CreateChannel(UniqueId id, CreateSequenceInfo createSequenceInfo, IServerReliableChannelBinder binder)
		{
			binder.Open(base.InternalOpenTimeout);
			return new ReliableInputSessionChannelOverReply(this, binder, base.FaultHelper, id);
		}

		// Token: 0x060059BE RID: 22974 RVA: 0x00147C7C File Offset: 0x00145E7C
		protected override void ProcessSequencedItem(IReplySessionChannel channel, RequestContext context, ReliableInputSessionChannelOverReply reliableChannel, WsrmMessageInfo info, bool newChannel)
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
