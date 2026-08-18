using System;
using System.Runtime.Serialization;

namespace MailBee
{
	// Token: 0x0200002D RID: 45
	[Serializable]
	public class MailBeeDataParsingException : MailBeeLocalException
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00007A0A File Offset: 0x00006A0A
		internal MailBeeDataParsingException(int A_0) : base(A_0)
		{
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00007A13 File Offset: 0x00006A13
		internal MailBeeDataParsingException(int A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00007A1D File Offset: 0x00006A1D
		protected MailBeeDataParsingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
