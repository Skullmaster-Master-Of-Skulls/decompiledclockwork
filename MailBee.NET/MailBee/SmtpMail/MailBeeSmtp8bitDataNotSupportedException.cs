using System;
using System.Runtime.Serialization;
using a;
using MailBee.Mime;

namespace MailBee.SmtpMail
{
	// Token: 0x0200016B RID: 363
	[Serializable]
	public class MailBeeSmtp8bitDataNotSupportedException : MailBeeEmailProtocolException, IMailBeeSmtpSendNeedsResetException
	{
		// Token: 0x06000C3E RID: 3134 RVA: 0x00031A87 File Offset: 0x00030A87
		internal MailBeeSmtp8bitDataNotSupportedException(int A_0, ai A_1, MailMessage A_2, string A_3, EmailAddressCollection A_4) : base(A_0, A_1)
		{
			this.m_mailMessage = A_2;
			this.m_actualSenderEmail = A_3;
			this.m_actualRecipients = A_4;
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x00031AA8 File Offset: 0x00030AA8
		protected MailBeeSmtp8bitDataNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x00031AB2 File Offset: 0x00030AB2
		public MailMessage MailMessage
		{
			get
			{
				return this.m_mailMessage;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x00031ABA File Offset: 0x00030ABA
		public EmailAddressCollection ActualRecipients
		{
			get
			{
				return this.m_actualRecipients;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x00031AC2 File Offset: 0x00030AC2
		public string ActualSenderEmail
		{
			get
			{
				return this.m_actualSenderEmail;
			}
		}

		// Token: 0x040008A0 RID: 2208
		private MailMessage m_mailMessage;

		// Token: 0x040008A1 RID: 2209
		private EmailAddressCollection m_actualRecipients;

		// Token: 0x040008A2 RID: 2210
		private string m_actualSenderEmail;
	}
}
