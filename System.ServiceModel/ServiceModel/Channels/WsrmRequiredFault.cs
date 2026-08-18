using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000962 RID: 2402
	internal class WsrmRequiredFault : WsrmFault
	{
		// Token: 0x06005D48 RID: 23880 RVA: 0x00158F42 File Offset: 0x00157142
		public WsrmRequiredFault(string faultReason) : base(true, "WsrmRequired", faultReason, null)
		{
		}

		// Token: 0x06005D49 RID: 23881 RVA: 0x00158F52 File Offset: 0x00157152
		protected override FaultCode Get11Code(FaultCode code, string subcode)
		{
			return new FaultCode(subcode, WsrmIndex.GetNamespaceString(base.GetReliableMessagingVersion()));
		}

		// Token: 0x06005D4A RID: 23882 RVA: 0x00158F65 File Offset: 0x00157165
		protected override bool Get12HasDetail()
		{
			return false;
		}

		// Token: 0x06005D4B RID: 23883 RVA: 0x00158F68 File Offset: 0x00157168
		protected override void OnFaultMessageCreated(MessageVersion version, Message message)
		{
		}

		// Token: 0x06005D4C RID: 23884 RVA: 0x00158F6A File Offset: 0x0015716A
		protected override void OnWriteDetailContents(XmlDictionaryWriter writer)
		{
		}
	}
}
