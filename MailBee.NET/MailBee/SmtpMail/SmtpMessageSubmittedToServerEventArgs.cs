using System;
using a;
using MailBee.Mime;

namespace MailBee.SmtpMail
{
	// Token: 0x0200014C RID: 332
	public class SmtpMessageSubmittedToServerEventArgs : CommonEventArgs
	{
		// Token: 0x06000B97 RID: 2967 RVA: 0x000311AF File Offset: 0x000301AF
		internal SmtpMessageSubmittedToServerEventArgs(MailMessage A_0, string A_1, EmailAddressCollection A_2, EmailAddressCollection A_3, EmailAddressCollection A_4, bc A_5) : base(A_5)
		{
			this.a = A_0;
			this.e = A_1;
			this.b = A_2;
			this.c = A_3;
			this.d = A_4;
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x000311DE File Offset: 0x000301DE
		public MailMessage MailMessage
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x000311E6 File Offset: 0x000301E6
		public EmailAddressCollection IntendedRecipients
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x000311EE File Offset: 0x000301EE
		public EmailAddressCollection AcceptedRecipients
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x000311F6 File Offset: 0x000301F6
		public EmailAddressCollection RefusedRecipients
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x000311FE File Offset: 0x000301FE
		public string ActualSenderEmail
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x04000854 RID: 2132
		private MailMessage a;

		// Token: 0x04000855 RID: 2133
		private EmailAddressCollection b;

		// Token: 0x04000856 RID: 2134
		private EmailAddressCollection c;

		// Token: 0x04000857 RID: 2135
		private EmailAddressCollection d;

		// Token: 0x04000858 RID: 2136
		private string e;
	}
}
