using System;
using System.Runtime.Serialization;

namespace MailBee.Outlook
{
	// Token: 0x0200058B RID: 1419
	[Serializable]
	public abstract class MailBeeOutlookMsgException : MailBeeLocalException
	{
		// Token: 0x06002F84 RID: 12164 RVA: 0x000E1D2D File Offset: 0x000E0D2D
		internal MailBeeOutlookMsgException(string A_0, int A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x000E1D37 File Offset: 0x000E0D37
		internal MailBeeOutlookMsgException(int A_0) : base(A_0)
		{
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x000E1D40 File Offset: 0x000E0D40
		internal MailBeeOutlookMsgException(int A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x000E1D4A File Offset: 0x000E0D4A
		protected MailBeeOutlookMsgException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
