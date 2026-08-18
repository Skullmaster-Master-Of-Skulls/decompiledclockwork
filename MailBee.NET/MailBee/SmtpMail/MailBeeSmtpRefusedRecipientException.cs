using System;
using System.Runtime.Serialization;
using a;
using MailBee.Mime;

namespace MailBee.SmtpMail
{
	// Token: 0x0200016E RID: 366
	[Serializable]
	public class MailBeeSmtpRefusedRecipientException : MailBeeSmtpSendNegativeResponseException
	{
		// Token: 0x06000C4A RID: 3146 RVA: 0x00031B2A File Offset: 0x00030B2A
		internal MailBeeSmtpRefusedRecipientException(int A_0, ai A_1, at A_2, MailMessage A_3, string A_4, EmailAddressCollection A_5, int A_6) : base(A_0, A_1, A_2, A_3, A_4, A_5)
		{
			this.m_refusedRecipientIndex = A_6;
			this.m_refusedRecipientEmail = A_5[A_6].Email;
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x00031B57 File Offset: 0x00030B57
		protected MailBeeSmtpRefusedRecipientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x00031B61 File Offset: 0x00030B61
		public string RefusedRecipientEmail
		{
			get
			{
				return this.m_refusedRecipientEmail;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x00031B69 File Offset: 0x00030B69
		public int RefusedRecipientIndex
		{
			get
			{
				return this.m_refusedRecipientIndex;
			}
		}

		// Token: 0x040008A6 RID: 2214
		private int m_refusedRecipientIndex;

		// Token: 0x040008A7 RID: 2215
		private string m_refusedRecipientEmail;
	}
}
