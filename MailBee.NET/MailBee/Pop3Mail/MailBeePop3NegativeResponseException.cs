using System;
using System.Runtime.Serialization;
using a;

namespace MailBee.Pop3Mail
{
	// Token: 0x02000580 RID: 1408
	[Serializable]
	public class MailBeePop3NegativeResponseException : MailBeeEmailProtocolNegativeResponseException
	{
		// Token: 0x06002F47 RID: 12103 RVA: 0x000DFEEC File Offset: 0x000DEEEC
		internal MailBeePop3NegativeResponseException(int A_0, ai A_1, at A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x000DFEF7 File Offset: 0x000DEEF7
		protected MailBeePop3NegativeResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
