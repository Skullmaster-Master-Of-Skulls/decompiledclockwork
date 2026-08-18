using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.MsmqIntegration
{
	// Token: 0x020003B1 RID: 945
	internal sealed class MsmqIntegrationInputChannel : MsmqInputChannelBase
	{
		// Token: 0x0600235E RID: 9054 RVA: 0x0008178B File Offset: 0x0007F98B
		public MsmqIntegrationInputChannel(MsmqIntegrationChannelListener listener) : base(listener, new MsmqIntegrationMessagePool(8))
		{
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x0008179C File Offset: 0x0007F99C
		protected override Message DecodeMsmqMessage(MsmqInputMessage msmqMessage, MsmqMessageProperty property)
		{
			MsmqIntegrationChannelListener listener = base.Manager as MsmqIntegrationChannelListener;
			return MsmqDecodeHelper.DecodeIntegrationDatagram(listener, base.MsmqReceiveHelper, msmqMessage as MsmqIntegrationInputMessage, property);
		}
	}
}
