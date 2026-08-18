using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000969 RID: 2409
	internal sealed class UnknownSequenceFault : WsrmHeaderFault
	{
		// Token: 0x06005D6D RID: 23917 RVA: 0x001595D9 File Offset: 0x001577D9
		public UnknownSequenceFault(UniqueId sequenceID) : base(true, "UnknownSequence", SR.GetString("UnknownSequenceFaultReason"), SR.GetString("UnknownSequenceMessageReceived"), sequenceID, true, true)
		{
		}

		// Token: 0x06005D6E RID: 23918 RVA: 0x001595FE File Offset: 0x001577FE
		public UnknownSequenceFault(FaultCode code, FaultReason reason, XmlDictionaryReader detailReader, ReliableMessagingVersion reliableMessagingVersion) : base(code, "UnknownSequence", reason, detailReader, reliableMessagingVersion, true, true)
		{
		}

		// Token: 0x06005D6F RID: 23919 RVA: 0x00159614 File Offset: 0x00157814
		public override CommunicationException CreateException()
		{
			string text;
			if (base.IsRemote)
			{
				text = FaultException.GetSafeReasonText(this.Reason);
				text = SR.GetString("UnknownSequenceFaultReceived", new object[]
				{
					text
				});
			}
			else
			{
				text = base.GetExceptionMessage();
			}
			return new CommunicationException(text);
		}
	}
}
