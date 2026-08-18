using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000965 RID: 2405
	internal sealed class LastMessageNumberExceededFault : WsrmHeaderFault
	{
		// Token: 0x06005D60 RID: 23904 RVA: 0x001593E3 File Offset: 0x001575E3
		public LastMessageNumberExceededFault(UniqueId sequenceID) : base(true, "LastMessageNumberExceeded", SR.GetString("LastMessageNumberExceededFaultReason"), SR.GetString("LastMessageNumberExceeded"), sequenceID, false, true)
		{
		}

		// Token: 0x06005D61 RID: 23905 RVA: 0x00159408 File Offset: 0x00157608
		public LastMessageNumberExceededFault(FaultCode code, FaultReason reason, XmlDictionaryReader detailReader, ReliableMessagingVersion reliableMessagingVersion) : base(code, "LastMessageNumberExceeded", reason, detailReader, reliableMessagingVersion, false, true)
		{
		}
	}
}
