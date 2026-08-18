using System;
using System.Runtime.Serialization;
using a;
using MailBee.Mime;

namespace MailBee.SmtpMail
{
	// Token: 0x0200016C RID: 364
	public abstract class MailBeeSmtpSendNegativeResponseException : MailBeeSmtpNegativeResponseException, IMailBeeSmtpSendNeedsResetException
	{
		// Token: 0x06000C43 RID: 3139 RVA: 0x00031ACA File Offset: 0x00030ACA
		internal MailBeeSmtpSendNegativeResponseException(int A_0, ai A_1, at A_2, MailMessage A_3, string A_4, EmailAddressCollection A_5) : base(A_0, A_1, A_2)
		{
			this.a = A_3;
			this.c = A_4;
			this.b = A_5;
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00031AED File Offset: 0x00030AED
		protected MailBeeSmtpSendNegativeResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x00031AF7 File Offset: 0x00030AF7
		public MailMessage MailMessage
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x00031AFF File Offset: 0x00030AFF
		public EmailAddressCollection ActualRecipients
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x00031B07 File Offset: 0x00030B07
		public string ActualSenderEmail
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x040008A3 RID: 2211
		private new MailMessage a;

		// Token: 0x040008A4 RID: 2212
		private EmailAddressCollection b;

		// Token: 0x040008A5 RID: 2213
		private string c;
	}
}
