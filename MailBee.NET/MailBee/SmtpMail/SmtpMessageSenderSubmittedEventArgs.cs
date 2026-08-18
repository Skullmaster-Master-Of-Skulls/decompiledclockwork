using System;
using a;
using MailBee.Mime;

namespace MailBee.SmtpMail
{
	// Token: 0x02000146 RID: 326
	public class SmtpMessageSenderSubmittedEventArgs : CommonEventArgs
	{
		// Token: 0x06000B7C RID: 2940 RVA: 0x000310E1 File Offset: 0x000300E1
		internal SmtpMessageSenderSubmittedEventArgs(MailMessage A_0, string A_1, bc A_2) : base(A_2)
		{
			this.b = A_0;
			this.a = A_1;
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x000310F8 File Offset: 0x000300F8
		public string SenderEmail
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x00031100 File Offset: 0x00030100
		public MailMessage MailMessage
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x04000849 RID: 2121
		private string a;

		// Token: 0x0400084A RID: 2122
		private MailMessage b;
	}
}
