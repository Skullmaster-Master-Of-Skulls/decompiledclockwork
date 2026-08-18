using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000923 RID: 2339
	internal class ReliableDuplexListenerOverDuplexSession : ReliableListenerOverDuplexSession<IDuplexSessionChannel, ServerReliableDuplexSessionChannel>
	{
		// Token: 0x060059AB RID: 22955 RVA: 0x00147B03 File Offset: 0x00145D03
		public ReliableDuplexListenerOverDuplexSession(ReliableSessionBindingElement binding, BindingContext context) : base(binding, context)
		{
		}

		// Token: 0x170015CF RID: 5583
		// (get) Token: 0x060059AC RID: 22956 RVA: 0x00147B0D File Offset: 0x00145D0D
		protected override bool Duplex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060059AD RID: 22957 RVA: 0x00147B10 File Offset: 0x00145D10
		protected override ServerReliableDuplexSessionChannel CreateChannel(UniqueId id, CreateSequenceInfo createSequenceInfo, IServerReliableChannelBinder binder)
		{
			binder.Open(base.InternalOpenTimeout);
			return new ServerReliableDuplexSessionChannel(this, binder, base.FaultHelper, id, createSequenceInfo.OfferIdentifier);
		}

		// Token: 0x060059AE RID: 22958 RVA: 0x00147B34 File Offset: 0x00145D34
		protected override void ProcessSequencedItem(IDuplexSessionChannel channel, Message message, ServerReliableDuplexSessionChannel reliableChannel, WsrmMessageInfo info, bool newChannel)
		{
			if (!newChannel)
			{
				IServerReliableChannelBinder serverReliableChannelBinder = (IServerReliableChannelBinder)reliableChannel.Binder;
				if (!serverReliableChannelBinder.UseNewChannel(channel))
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
