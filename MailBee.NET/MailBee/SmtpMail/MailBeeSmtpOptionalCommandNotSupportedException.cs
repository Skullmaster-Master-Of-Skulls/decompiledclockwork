using System;
using System.Runtime.Serialization;
using a;

namespace MailBee.SmtpMail
{
	// Token: 0x0200015D RID: 349
	[Serializable]
	public class MailBeeSmtpOptionalCommandNotSupportedException : MailBeeSmtpNegativeResponseException
	{
		// Token: 0x06000C1D RID: 3101 RVA: 0x000318E0 File Offset: 0x000308E0
		internal MailBeeSmtpOptionalCommandNotSupportedException(int A_0, ai A_1, at A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x000318EB File Offset: 0x000308EB
		protected MailBeeSmtpOptionalCommandNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
