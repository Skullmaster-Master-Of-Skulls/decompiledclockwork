using System;
using System.Runtime.Serialization;

namespace MailBee.Outlook
{
	// Token: 0x0200058D RID: 1421
	[Serializable]
	public class MailBeeOutlookMsgNotFoundException : MailBeeOutlookMsgException
	{
		// Token: 0x06002F8C RID: 12172 RVA: 0x000E1D7B File Offset: 0x000E0D7B
		internal MailBeeOutlookMsgNotFoundException(string A_0, int A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x000E1D85 File Offset: 0x000E0D85
		internal MailBeeOutlookMsgNotFoundException(int A_0) : base(A_0)
		{
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x000E1D8E File Offset: 0x000E0D8E
		internal MailBeeOutlookMsgNotFoundException(int A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x000E1D98 File Offset: 0x000E0D98
		protected MailBeeOutlookMsgNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
