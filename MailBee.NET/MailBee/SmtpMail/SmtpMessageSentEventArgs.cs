using System;
using System.Data;
using a;
using a.d;
using a.n;
using MailBee.Mime;

namespace MailBee.SmtpMail
{
	// Token: 0x0200014E RID: 334
	public class SmtpMessageSentEventArgs : CommonEventArgs
	{
		// Token: 0x06000BA1 RID: 2977 RVA: 0x00031208 File Offset: 0x00030208
		internal SmtpMessageSentEventArgs(MailMessage A_0, string A_1, EmailAddressCollection A_2, EmailAddressCollection A_3, EmailAddressCollection A_4, global::a.d.k A_5, string A_6, global::a.n.a A_7, bc A_8) : base(A_8)
		{
			this.a = A_0;
			this.e = A_1;
			this.b = A_2;
			this.c = A_3;
			this.d = A_4;
			this.f = A_5;
			this.g = A_6;
			this.h = A_7;
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0003125A File Offset: 0x0003025A
		public MailMessage MailMessage
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x00031262 File Offset: 0x00030262
		public EmailAddressCollection IntendedRecipients
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x0003126A File Offset: 0x0003026A
		public EmailAddressCollection SuccessfulRecipients
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x00031272 File Offset: 0x00030272
		public EmailAddressCollection FailedRecipients
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x0003127A File Offset: 0x0003027A
		public string ActualSenderEmail
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x00031282 File Offset: 0x00030282
		public DataTable MergeTable
		{
			get
			{
				if (this.f != null)
				{
					return this.f.c();
				}
				return null;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x00031299 File Offset: 0x00030299
		public int MergeRowIndex
		{
			get
			{
				if (this.f != null)
				{
					return this.f.a();
				}
				return 0;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x000312B0 File Offset: 0x000302B0
		public IDataReader MergeDataReader
		{
			get
			{
				if (this.f != null)
				{
					return this.f.e();
				}
				return null;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x000312C7 File Offset: 0x000302C7
		public object[] MergeDataReaderRowValues
		{
			get
			{
				if (this.f != null)
				{
					return this.f.d();
				}
				return null;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x000312DE File Offset: 0x000302DE
		public string[] MergeDataReaderColumnNames
		{
			get
			{
				if (this.f != null)
				{
					return this.f.b();
				}
				return null;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x000312F5 File Offset: 0x000302F5
		internal global::a.d.k Merge
		{
			get
			{
				return this.f;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x000312FD File Offset: 0x000302FD
		public string Tag
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x00031305 File Offset: 0x00030305
		internal global::a.n.a AddrCheck
		{
			get
			{
				return this.h;
			}
		}

		// Token: 0x04000859 RID: 2137
		private MailMessage a;

		// Token: 0x0400085A RID: 2138
		private EmailAddressCollection b;

		// Token: 0x0400085B RID: 2139
		private EmailAddressCollection c;

		// Token: 0x0400085C RID: 2140
		private EmailAddressCollection d;

		// Token: 0x0400085D RID: 2141
		private string e;

		// Token: 0x0400085E RID: 2142
		private global::a.d.k f;

		// Token: 0x0400085F RID: 2143
		private string g;

		// Token: 0x04000860 RID: 2144
		private global::a.n.a h;
	}
}
