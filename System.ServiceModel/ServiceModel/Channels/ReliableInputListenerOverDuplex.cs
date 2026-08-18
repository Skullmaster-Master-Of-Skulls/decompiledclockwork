using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000922 RID: 2338
	internal class ReliableInputListenerOverDuplex : ReliableListenerOverDuplex<IInputSessionChannel, ReliableInputSessionChannelOverDuplex>
	{
		// Token: 0x060059A7 RID: 22951 RVA: 0x00147AD1 File Offset: 0x00145CD1
		public ReliableInputListenerOverDuplex(ReliableSessionBindingElement binding, BindingContext context) : base(binding, context)
		{
		}

		// Token: 0x170015CE RID: 5582
		// (get) Token: 0x060059A8 RID: 22952 RVA: 0x00147ADB File Offset: 0x00145CDB
		protected override bool Duplex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060059A9 RID: 22953 RVA: 0x00147ADE File Offset: 0x00145CDE
		protected override ReliableInputSessionChannelOverDuplex CreateChannel(UniqueId id, CreateSequenceInfo createSequenceInfo, IServerReliableChannelBinder binder)
		{
			binder.Open(base.InternalOpenTimeout);
			return new ReliableInputSessionChannelOverDuplex(this, binder, base.FaultHelper, id);
		}

		// Token: 0x060059AA RID: 22954 RVA: 0x00147AFA File Offset: 0x00145CFA
		protected override void ProcessSequencedItem(ReliableInputSessionChannelOverDuplex channel, Message message, WsrmMessageInfo info)
		{
			channel.ProcessDemuxedMessage(info);
		}
	}
}
