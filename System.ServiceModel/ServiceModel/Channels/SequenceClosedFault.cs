using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000967 RID: 2407
	internal sealed class SequenceClosedFault : WsrmHeaderFault
	{
		// Token: 0x06005D65 RID: 23909 RVA: 0x00159545 File Offset: 0x00157745
		public SequenceClosedFault(UniqueId sequenceID) : base(true, "SequenceClosed", SR.GetString("SequenceClosedFaultString"), null, sequenceID, false, true)
		{
		}

		// Token: 0x06005D66 RID: 23910 RVA: 0x00159561 File Offset: 0x00157761
		public SequenceClosedFault(FaultCode code, FaultReason reason, XmlDictionaryReader detailReader, ReliableMessagingVersion reliableMessagingVersion) : base(code, "SequenceClosed", reason, detailReader, reliableMessagingVersion, false, true)
		{
		}
	}
}
