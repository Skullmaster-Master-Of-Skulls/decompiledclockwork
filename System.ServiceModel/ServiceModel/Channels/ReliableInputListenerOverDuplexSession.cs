using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000924 RID: 2340
	internal class ReliableInputListenerOverDuplexSession : ReliableListenerOverDuplexSession<IInputSessionChannel, ReliableInputSessionChannelOverDuplex>
	{
		// Token: 0x060059AF RID: 22959 RVA: 0x00147B6F File Offset: 0x00145D6F
		public ReliableInputListenerOverDuplexSession(ReliableSessionBindingElement binding, BindingContext context) : base(binding, context)
		{
		}

		// Token: 0x170015D0 RID: 5584
		// (get) Token: 0x060059B0 RID: 22960 RVA: 0x00147B79 File Offset: 0x00145D79
		protected override bool Duplex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060059B1 RID: 22961 RVA: 0x00147B7C File Offset: 0x00145D7C
		protected override ReliableInputSessionChannelOverDuplex CreateChannel(UniqueId id, CreateSequenceInfo createSequenceInfo, IServerReliableChannelBinder binder)
		{
			binder.Open(base.InternalOpenTimeout);
			return new ReliableInputSessionChannelOverDuplex(this, binder, base.FaultHelper, id);
		}

		// Token: 0x060059B2 RID: 22962 RVA: 0x00147B98 File Offset: 0x00145D98
		protected override void ProcessSequencedItem(IDuplexSessionChannel channel, Message message, ReliableInputSessionChannelOverDuplex reliableChannel, WsrmMessageInfo info, bool newChannel)
		{
			if (!newChannel)
			{
				IServerReliableChannelBinder binder = reliableChannel.Binder;
				if (!binder.UseNewChannel(channel))
				{
					message.Close();
					channel.Abort();
					return;
				}
			}
			reliableChannel.ProcessDemuxedMessage(info);
		}
	}
}
