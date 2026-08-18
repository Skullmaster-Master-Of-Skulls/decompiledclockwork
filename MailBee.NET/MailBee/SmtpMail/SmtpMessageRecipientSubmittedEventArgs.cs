using System;
using a;
using MailBee.Mime;

namespace MailBee.SmtpMail
{
	// Token: 0x02000148 RID: 328
	public class SmtpMessageRecipientSubmittedEventArgs : CommonEventArgs
	{
		// Token: 0x06000B83 RID: 2947 RVA: 0x00031108 File Offset: 0x00030108
		internal SmtpMessageRecipientSubmittedEventArgs(MailMessage A_0, string A_1, bool A_2, bool A_3, string A_4, bc A_5) : base(A_5)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = A_2;
			this.d = A_3;
			this.e = A_4;
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x00031137 File Offset: 0x00030137
		public MailMessage MailMessage
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x0003113F File Offset: 0x0003013F
		public string RecipientEmail
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x00031147 File Offset: 0x00030147
		public bool Result
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x0003114F File Offset: 0x0003014F
		// (set) Token: 0x06000B88 RID: 2952 RVA: 0x00031157 File Offset: 0x00030157
		public bool AllowRefusedRecipient
		{
			get
			{
				return this.d;
			}
			set
			{
				this.d = value;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x00031160 File Offset: 0x00030160
		public string ServerStatusMessage
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x0400084B RID: 2123
		private MailMessage a;

		// Token: 0x0400084C RID: 2124
		private string b;

		// Token: 0x0400084D RID: 2125
		private bool c;

		// Token: 0x0400084E RID: 2126
		private bool d;

		// Token: 0x0400084F RID: 2127
		private string e;
	}
}
