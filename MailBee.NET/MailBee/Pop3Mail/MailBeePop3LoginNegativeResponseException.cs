using System;
using System.Runtime.Serialization;
using a;

namespace MailBee.Pop3Mail
{
	// Token: 0x02000582 RID: 1410
	public abstract class MailBeePop3LoginNegativeResponseException : MailBeePop3NegativeResponseException, IMailBeeLoginNegativeResponseException
	{
		// Token: 0x06002F4B RID: 12107 RVA: 0x000DFF16 File Offset: 0x000DEF16
		internal MailBeePop3LoginNegativeResponseException(int A_0, ai A_1, at A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x06002F4C RID: 12108 RVA: 0x000DFF21 File Offset: 0x000DEF21
		protected MailBeePop3LoginNegativeResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
