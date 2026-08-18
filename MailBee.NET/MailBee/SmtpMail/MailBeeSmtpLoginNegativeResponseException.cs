using System;
using System.Runtime.Serialization;
using a;

namespace MailBee.SmtpMail
{
	// Token: 0x0200015E RID: 350
	public abstract class MailBeeSmtpLoginNegativeResponseException : MailBeeSmtpNegativeResponseException, IMailBeeLoginNegativeResponseException
	{
		// Token: 0x06000C1F RID: 3103 RVA: 0x000318F5 File Offset: 0x000308F5
		internal MailBeeSmtpLoginNegativeResponseException(int A_0, ai A_1, at A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00031900 File Offset: 0x00030900
		protected MailBeeSmtpLoginNegativeResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
