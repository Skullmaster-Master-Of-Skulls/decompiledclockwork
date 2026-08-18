using System;
using System.Runtime.Serialization;

namespace MailBee
{
	// Token: 0x0200002E RID: 46
	[Serializable]
	public class MailBeeDateParsingException : MailBeeDataParsingException
	{
		// Token: 0x0600012D RID: 301 RVA: 0x00007A27 File Offset: 0x00006A27
		internal MailBeeDateParsingException(int A_0) : base(A_0)
		{
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00007A30 File Offset: 0x00006A30
		internal MailBeeDateParsingException(int A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00007A3A File Offset: 0x00006A3A
		protected MailBeeDateParsingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
