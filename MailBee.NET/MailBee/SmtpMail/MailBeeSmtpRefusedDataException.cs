using System;
using System.Runtime.Serialization;
using a;
using MailBee.Mime;

namespace MailBee.SmtpMail
{
	// Token: 0x0200016F RID: 367
	[Serializable]
	public class MailBeeSmtpRefusedDataException : MailBeeSmtpSendNegativeResponseException
	{
		// Token: 0x06000C4E RID: 3150 RVA: 0x00031B71 File Offset: 0x00030B71
		internal MailBeeSmtpRefusedDataException(int A_0, ai A_1, at A_2, MailMessage A_3, string A_4, EmailAddressCollection A_5) : base(A_0, A_1, A_2, A_3, A_4, A_5)
		{
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00031B82 File Offset: 0x00030B82
		protected MailBeeSmtpRefusedDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
