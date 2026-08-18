using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008E4 RID: 2276
	internal sealed class MsmqInputChannel : MsmqInputChannelBase
	{
		// Token: 0x060056C4 RID: 22212 RVA: 0x0013E884 File Offset: 0x0013CA84
		public MsmqInputChannel(MsmqInputChannelListener listener) : base(listener, new MsmqInputMessagePool((listener.ReceiveParameters as MsmqTransportReceiveParameters).MaxPoolSize))
		{
		}

		// Token: 0x060056C5 RID: 22213 RVA: 0x0013E8A4 File Offset: 0x0013CAA4
		protected override Message DecodeMsmqMessage(MsmqInputMessage msmqMessage, MsmqMessageProperty messageProperty)
		{
			MsmqInputChannelListener listener = base.Manager as MsmqInputChannelListener;
			return MsmqDecodeHelper.DecodeTransportDatagram(listener, base.MsmqReceiveHelper, msmqMessage, messageProperty);
		}
	}
}
