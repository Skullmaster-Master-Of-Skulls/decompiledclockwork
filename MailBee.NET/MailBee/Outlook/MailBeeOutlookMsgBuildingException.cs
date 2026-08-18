using System;
using System.Runtime.Serialization;

namespace MailBee.Outlook
{
	// Token: 0x0200058C RID: 1420
	[Serializable]
	public class MailBeeOutlookMsgBuildingException : MailBeeOutlookMsgException
	{
		// Token: 0x06002F88 RID: 12168 RVA: 0x000E1D54 File Offset: 0x000E0D54
		internal MailBeeOutlookMsgBuildingException(string A_0, int A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x000E1D5E File Offset: 0x000E0D5E
		internal MailBeeOutlookMsgBuildingException(int A_0) : base(A_0)
		{
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x000E1D67 File Offset: 0x000E0D67
		internal MailBeeOutlookMsgBuildingException(int A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x000E1D71 File Offset: 0x000E0D71
		protected MailBeeOutlookMsgBuildingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
