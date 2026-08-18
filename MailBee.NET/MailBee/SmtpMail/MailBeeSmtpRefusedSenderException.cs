using System;
using System.Runtime.Serialization;
using a;
using MailBee.Mime;

namespace MailBee.SmtpMail
{
	// Token: 0x0200016D RID: 365
	[Serializable]
	public class MailBeeSmtpRefusedSenderException : MailBeeSmtpSendNegativeResponseException
	{
		// Token: 0x06000C48 RID: 3144 RVA: 0x00031B0F File Offset: 0x00030B0F
		internal MailBeeSmtpRefusedSenderException(int A_0, ai A_1, at A_2, MailMessage A_3, string A_4, EmailAddressCollection A_5) : base(A_0, A_1, A_2, A_3, A_4, A_5)
		{
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x00031B20 File Offset: 0x00030B20
		protected MailBeeSmtpRefusedSenderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
