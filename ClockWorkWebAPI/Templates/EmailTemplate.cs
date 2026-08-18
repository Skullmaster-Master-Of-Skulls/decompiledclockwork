using System;
using System.Collections.Generic;

namespace ClockWorkWebAPI.Templates
{
	// Token: 0x02000047 RID: 71
	public class EmailTemplate
	{
		// Token: 0x06000383 RID: 899 RVA: 0x0001973C File Offset: 0x0001793C
		public EmailTemplate(string to, string from, string cc, string bcc, string attachments, string subject, string body)
		{
			this.to = ((to == null) ? "" : to);
			this.from = ((from == null) ? "" : from);
			this.cc = ((cc == null) ? "" : cc);
			this.bcc = ((bcc == null) ? "" : bcc);
			this.attachments = ((attachments == null) ? "" : attachments);
			this.body = ((body == null) ? "" : body);
			this.subject = ((subject == null) ? "" : subject);
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000384 RID: 900 RVA: 0x000197D0 File Offset: 0x000179D0
		public string To
		{
			get
			{
				return this.to;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000385 RID: 901 RVA: 0x000197E8 File Offset: 0x000179E8
		public string From
		{
			get
			{
				return this.from;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00019800 File Offset: 0x00017A00
		public string Cc
		{
			get
			{
				return this.cc;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000387 RID: 903 RVA: 0x00019818 File Offset: 0x00017A18
		public string Bcc
		{
			get
			{
				return this.bcc;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000388 RID: 904 RVA: 0x00019830 File Offset: 0x00017A30
		public string Attachments
		{
			get
			{
				return this.attachments;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00019848 File Offset: 0x00017A48
		public string Body
		{
			get
			{
				return this.body;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600038A RID: 906 RVA: 0x00019860 File Offset: 0x00017A60
		public string Subject
		{
			get
			{
				return this.subject;
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00019878 File Offset: 0x00017A78
		public void MailMerge(db conn, NameObjectPairCollection args, out string newSubject, out string newBody)
		{
			Template template = new Template(this.subject, this.body, args, conn);
			template.MergeMail(out newSubject, out newBody);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x000198A4 File Offset: 0x00017AA4
		public void MailMerge(NameObjectPairCollection args, out string newSubject, out string newBody)
		{
			Template template = new Template(this.subject, this.body, args);
			template.MergeMail(out newSubject, out newBody);
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600038D RID: 909 RVA: 0x000198D0 File Offset: 0x00017AD0
		public string[] CcArray
		{
			get
			{
				return this.ParseEmailList(this.cc);
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600038E RID: 910 RVA: 0x000198F0 File Offset: 0x00017AF0
		public string[] BccArray
		{
			get
			{
				return this.ParseEmailList(this.bcc);
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00019910 File Offset: 0x00017B10
		private string[] ParseEmailList(string emails)
		{
			bool flag = emails.IndexOf(',') > 0;
			string[] array;
			if (flag)
			{
				array = emails.Split(new char[]
				{
					','
				});
			}
			else
			{
				array = emails.Split(new char[]
				{
					';'
				});
			}
			List<string> list = new List<string>();
			foreach (string text in array)
			{
				bool flag2 = text.Trim().Length > 0;
				if (flag2)
				{
					list.Add(text.Trim());
				}
			}
			string[] array3 = new string[list.Count];
			for (int j = 0; j < list.Count; j++)
			{
				array3[j] = list[j];
			}
			return array3;
		}

		// Token: 0x040001C5 RID: 453
		private string to;

		// Token: 0x040001C6 RID: 454
		private string from;

		// Token: 0x040001C7 RID: 455
		private string cc;

		// Token: 0x040001C8 RID: 456
		private string bcc;

		// Token: 0x040001C9 RID: 457
		private string attachments;

		// Token: 0x040001CA RID: 458
		private string subject;

		// Token: 0x040001CB RID: 459
		private string body;
	}
}
