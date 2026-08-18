using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000968 RID: 2408
	internal sealed class SequenceTerminatedFault : WsrmHeaderFault
	{
		// Token: 0x06005D67 RID: 23911 RVA: 0x00159575 File Offset: 0x00157775
		private SequenceTerminatedFault(bool isSenderFault, UniqueId sequenceID, string faultReason, string exceptionMessage) : base(isSenderFault, "SequenceTerminated", faultReason, exceptionMessage, sequenceID, true, true)
		{
		}

		// Token: 0x06005D68 RID: 23912 RVA: 0x00159589 File Offset: 0x00157789
		public SequenceTerminatedFault(FaultCode code, FaultReason reason, XmlDictionaryReader detailReader, ReliableMessagingVersion reliableMessagingVersion) : base(code, "SequenceTerminated", reason, detailReader, reliableMessagingVersion, true, true)
		{
		}

		// Token: 0x06005D69 RID: 23913 RVA: 0x0015959D File Offset: 0x0015779D
		public static WsrmFault CreateCommunicationFault(UniqueId sequenceID, string faultReason, string exceptionMessage)
		{
			return new SequenceTerminatedFault(false, sequenceID, faultReason, exceptionMessage);
		}

		// Token: 0x06005D6A RID: 23914 RVA: 0x001595A8 File Offset: 0x001577A8
		public static WsrmFault CreateMaxRetryCountExceededFault(UniqueId sequenceId)
		{
			return SequenceTerminatedFault.CreateCommunicationFault(sequenceId, SR.GetString("SequenceTerminatedMaximumRetryCountExceeded"), null);
		}

		// Token: 0x06005D6B RID: 23915 RVA: 0x001595BB File Offset: 0x001577BB
		public static WsrmFault CreateProtocolFault(UniqueId sequenceID, string faultReason, string exceptionMessage)
		{
			return new SequenceTerminatedFault(true, sequenceID, faultReason, exceptionMessage);
		}

		// Token: 0x06005D6C RID: 23916 RVA: 0x001595C6 File Offset: 0x001577C6
		public static WsrmFault CreateQuotaExceededFault(UniqueId sequenceID)
		{
			return SequenceTerminatedFault.CreateProtocolFault(sequenceID, SR.GetString("SequenceTerminatedQuotaExceededException"), null);
		}
	}
}
