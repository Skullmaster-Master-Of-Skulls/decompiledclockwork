using System;
using System.Runtime.Serialization;
using a;
using MailBee.Mime;

namespace MailBee.SmtpMail
{
	// Token: 0x02000168 RID: 360
	public abstract class MailBeeSmtpMessageNotAllowedException : MailBeeEmailProtocolException, IMailBeeSmtpSendException
	{
		// Token: 0x06000C31 RID: 3121 RVA: 0x000319D8 File Offset: 0x000309D8
		internal MailBeeSmtpMessageNotAllowedException(int A_0, ai A_1, MailMessage A_2, string A_3, EmailAddressCollection A_4) : base(A_0, A_1)
		{
			this.a = A_2;
			this.c = A_3;
			this.b = A_4;
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x000319F9 File Offset: 0x000309F9
		protected MailBeeSmtpMessageNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x00031A03 File Offset: 0x00030A03
		public MailMessage MailMessage
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x00031A0B File Offset: 0x00030A0B
		public EmailAddressCollection ActualRecipients
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x00031A13 File Offset: 0x00030A13
		public string ActualSenderEmail
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x04000899 RID: 2201
		private MailMessage a;

		// Token: 0x0400089A RID: 2202
		private EmailAddressCollection b;

		// Token: 0x0400089B RID: 2203
		private string c;
	}
}
