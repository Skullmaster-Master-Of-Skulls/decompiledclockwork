using System;
using System.Runtime.Serialization;
using a;
using MailBee.Mime;

namespace MailBee.SmtpMail
{
	// Token: 0x0200016A RID: 362
	[Serializable]
	public class MailBeeSmtpNoAcceptedRecipientsException : MailBeeEmailProtocolException, IMailBeeSmtpSendNeedsResetException
	{
		// Token: 0x06000C39 RID: 3129 RVA: 0x00031A44 File Offset: 0x00030A44
		internal MailBeeSmtpNoAcceptedRecipientsException(int A_0, ai A_1, MailMessage A_2, string A_3, EmailAddressCollection A_4) : base(A_0, A_1)
		{
			this.m_mailMessage = A_2;
			this.m_actualSenderEmail = A_3;
			this.m_actualRecipients = A_4;
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00031A65 File Offset: 0x00030A65
		protected MailBeeSmtpNoAcceptedRecipientsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x00031A6F File Offset: 0x00030A6F
		public MailMessage MailMessage
		{
			get
			{
				return this.m_mailMessage;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x00031A77 File Offset: 0x00030A77
		public EmailAddressCollection ActualRecipients
		{
			get
			{
				return this.m_actualRecipients;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x00031A7F File Offset: 0x00030A7F
		public string ActualSenderEmail
		{
			get
			{
				return this.m_actualSenderEmail;
			}
		}

		// Token: 0x0400089D RID: 2205
		private MailMessage m_mailMessage;

		// Token: 0x0400089E RID: 2206
		private EmailAddressCollection m_actualRecipients;

		// Token: 0x0400089F RID: 2207
		private string m_actualSenderEmail;
	}
}
