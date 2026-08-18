using System;
using System.Collections.Specialized;
using a;
using MailBee.Mime;

namespace MailBee.SmtpMail
{
	// Token: 0x0200013C RID: 316
	public class SmtpMessageMXLookupDoneEventArgs : CommonEventArgs
	{
		// Token: 0x060009ED RID: 2541 RVA: 0x0002E319 File Offset: 0x0002D319
		internal SmtpMessageMXLookupDoneEventArgs(MailMessage A_0, StringCollection A_1, StringCollection A_2, StringCollection A_3, bc A_4) : base(A_4)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = A_2;
			this.d = A_3;
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x0002E340 File Offset: 0x0002D340
		public MailMessage MailMessage
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x0002E348 File Offset: 0x0002D348
		public StringCollection IntendedDomains
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x0002E350 File Offset: 0x0002D350
		public StringCollection SuccessfulDomains
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x0002E358 File Offset: 0x0002D358
		public StringCollection FailedDomains
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x040007D7 RID: 2007
		private MailMessage a;

		// Token: 0x040007D8 RID: 2008
		private StringCollection b;

		// Token: 0x040007D9 RID: 2009
		private StringCollection c;

		// Token: 0x040007DA RID: 2010
		private StringCollection d;
	}
}
