using System;
using System.Runtime.Serialization;

namespace MailBee.Outlook
{
	// Token: 0x0200058E RID: 1422
	[Serializable]
	public class MailBeeOutlookMsgParsingException : MailBeeOutlookMsgException
	{
		// Token: 0x06002F90 RID: 12176 RVA: 0x000E1DA2 File Offset: 0x000E0DA2
		internal MailBeeOutlookMsgParsingException(string A_0, int A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x000E1DAC File Offset: 0x000E0DAC
		internal MailBeeOutlookMsgParsingException(int A_0) : base(A_0)
		{
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x000E1DB5 File Offset: 0x000E0DB5
		internal MailBeeOutlookMsgParsingException(int A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x000E1DBF File Offset: 0x000E0DBF
		protected MailBeeOutlookMsgParsingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
