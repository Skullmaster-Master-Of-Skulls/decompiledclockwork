using System;
using System.Collections.Specialized;
using System.Configuration.Install;
using System.Data;

namespace EmailClassLibrary
{
	// Token: 0x02000005 RID: 5
	public class EmailTemplate
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002472 File Offset: 0x00001472
		// (set) Token: 0x06000010 RID: 16 RVA: 0x0000247A File Offset: 0x0000147A
		public string From
		{
			get
			{
				return this.from;
			}
			set
			{
				this.from = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002483 File Offset: 0x00001483
		// (set) Token: 0x06000012 RID: 18 RVA: 0x0000248B File Offset: 0x0000148B
		public string To
		{
			get
			{
				return this.to;
			}
			set
			{
				this.to = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002494 File Offset: 0x00001494
		// (set) Token: 0x06000014 RID: 20 RVA: 0x0000249C File Offset: 0x0000149C
		public string Cc
		{
			get
			{
				return this.cc;
			}
			set
			{
				this.cc = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000024A5 File Offset: 0x000014A5
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000024AD File Offset: 0x000014AD
		public string Bcc
		{
			get
			{
				return this.bcc;
			}
			set
			{
				this.bcc = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000024B6 File Offset: 0x000014B6
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000024BE File Offset: 0x000014BE
		public string Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000024C7 File Offset: 0x000014C7
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000024CF File Offset: 0x000014CF
		public string Misc
		{
			get
			{
				return this.misc;
			}
			set
			{
				this.misc = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000024D8 File Offset: 0x000014D8
		public StringDictionary Args
		{
			get
			{
				return this.args;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000024E0 File Offset: 0x000014E0
		public int TemplateId
		{
			get
			{
				return this.templateId;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000024E8 File Offset: 0x000014E8
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000024F0 File Offset: 0x000014F0
		public string Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				this.subject = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000024F9 File Offset: 0x000014F9
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002501 File Offset: 0x00001501
		public string Attachments
		{
			get
			{
				return this.attachments;
			}
			set
			{
				this.attachments = value;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000250C File Offset: 0x0000150C
		public override string ToString()
		{
			return string.Format("From: {0}\nTo: {1}\nCc: {2}\nBcc: {3}\nSubject: {4}\nAttachments: {5}\nBody:\n{6}", new object[]
			{
				this.from,
				this.to,
				this.cc,
				this.bcc,
				this.subject,
				this.attachments,
				this.body
			});
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002568 File Offset: 0x00001568
		public EmailTemplate()
		{
			this.from = "";
			this.to = "";
			this.cc = "";
			this.bcc = "";
			this.attachments = "";
			this.body = "";
			this.misc = "";
			this.templateId = 0;
			this.args = new StringDictionary();
			this.subject = "";
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025E8 File Offset: 0x000015E8
		public EmailTemplate(DataRow emailTemplateRow)
		{
			if (emailTemplateRow != null)
			{
				this.from = ((emailTemplateRow["blankreplacements"] == DBNull.Value) ? "" : ((string)emailTemplateRow["blankreplacements"]));
				this.to = (string)emailTemplateRow["eto"];
				this.cc = (string)emailTemplateRow["ecc"];
				this.bcc = (string)emailTemplateRow["ebcc"];
				this.subject = (string)emailTemplateRow["eattachments"];
				this.attachments = ((emailTemplateRow["warningifmissingcodes"] == DBNull.Value) ? "" : ((string)emailTemplateRow["warningifmissingcodes"]));
				this.body = (string)emailTemplateRow["ebody"];
				this.misc = (string)emailTemplateRow["emisc"];
				this.templateId = (int)emailTemplateRow["templateid"];
				this.args = EmailTemplate.ParseArgs(this.misc);
				return;
			}
			this.from = "";
			this.to = "";
			this.cc = "";
			this.bcc = "";
			this.attachments = "";
			this.body = "";
			this.misc = "";
			this.args = new StringDictionary();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002768 File Offset: 0x00001768
		public static StringDictionary ParseArgs(string args)
		{
			InstallContext installContext = new InstallContext(null, args.Split(new char[]
			{
				' '
			}));
			return installContext.Parameters;
		}

		// Token: 0x0400000D RID: 13
		private string from;

		// Token: 0x0400000E RID: 14
		private string to;

		// Token: 0x0400000F RID: 15
		private string cc;

		// Token: 0x04000010 RID: 16
		private string bcc;

		// Token: 0x04000011 RID: 17
		private string attachments;

		// Token: 0x04000012 RID: 18
		private string body;

		// Token: 0x04000013 RID: 19
		private string misc;

		// Token: 0x04000014 RID: 20
		private int templateId;

		// Token: 0x04000015 RID: 21
		private StringDictionary args;

		// Token: 0x04000016 RID: 22
		private string subject;
	}
}
