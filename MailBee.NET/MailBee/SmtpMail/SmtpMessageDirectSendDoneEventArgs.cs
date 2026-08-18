using System;
using a;
using MailBee.Mime;

namespace MailBee.SmtpMail
{
	// Token: 0x0200013A RID: 314
	public class SmtpMessageDirectSendDoneEventArgs : CommonEventArgs
	{
		// Token: 0x060009E3 RID: 2531 RVA: 0x0002E2C2 File Offset: 0x0002D2C2
		internal SmtpMessageDirectSendDoneEventArgs(MailMessage A_0, string A_1, EmailAddressCollection A_2, EmailAddressCollection A_3, EmailAddressCollection A_4, bc A_5) : base(A_5)
		{
			this.a = A_0;
			this.e = A_1;
			this.b = A_2;
			this.c = A_3;
			this.d = A_4;
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0002E2F1 File Offset: 0x0002D2F1
		public MailMessage MailMessage
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x0002E2F9 File Offset: 0x0002D2F9
		public EmailAddressCollection IntendedRecipients
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0002E301 File Offset: 0x0002D301
		public EmailAddressCollection SuccessfulRecipients
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x0002E309 File Offset: 0x0002D309
		public EmailAddressCollection FailedRecipients
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0002E311 File Offset: 0x0002D311
		public string ActualSenderEmail
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x040007D2 RID: 2002
		private MailMessage a;

		// Token: 0x040007D3 RID: 2003
		private EmailAddressCollection b;

		// Token: 0x040007D4 RID: 2004
		private EmailAddressCollection c;

		// Token: 0x040007D5 RID: 2005
		private EmailAddressCollection d;

		// Token: 0x040007D6 RID: 2006
		private string e;
	}
}
