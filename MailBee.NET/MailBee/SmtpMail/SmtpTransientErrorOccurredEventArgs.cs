using System;
using System.Data;
using a;
using a.d;
using a.n;
using MailBee.Mime;

namespace MailBee.SmtpMail
{
	// Token: 0x02000152 RID: 338
	public class SmtpTransientErrorOccurredEventArgs : CommonEventArgs
	{
		// Token: 0x06000BC4 RID: 3012 RVA: 0x000313F8 File Offset: 0x000303F8
		internal SmtpTransientErrorOccurredEventArgs(MailMessage A_0, string A_1, EmailAddressCollection A_2, MailBeeSmtpNegativeResponseException A_3, global::a.d.k A_4, string A_5, global::a.n.a A_6, bc A_7) : base(A_7)
		{
			this.a = A_0;
			this.c = A_1;
			this.b = A_2;
			this.d = A_3;
			this.e = A_4;
			this.f = A_5;
			this.g = A_6;
			this.h = true;
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x00031449 File Offset: 0x00030449
		public MailMessage MailMessage
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x00031451 File Offset: 0x00030451
		public EmailAddressCollection IntendedRecipients
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x00031459 File Offset: 0x00030459
		public string ActualSenderEmail
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x00031461 File Offset: 0x00030461
		public MailBeeSmtpNegativeResponseException Reason
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x00031469 File Offset: 0x00030469
		public DataTable MergeTable
		{
			get
			{
				if (this.e != null)
				{
					return this.e.c();
				}
				return null;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x00031480 File Offset: 0x00030480
		public int MergeRowIndex
		{
			get
			{
				if (this.e != null)
				{
					return this.e.a();
				}
				return 0;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x00031497 File Offset: 0x00030497
		public IDataReader MergeDataReader
		{
			get
			{
				if (this.e != null)
				{
					return this.e.e();
				}
				return null;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x000314AE File Offset: 0x000304AE
		public object[] MergeDataReaderRowValues
		{
			get
			{
				if (this.e != null)
				{
					return this.e.d();
				}
				return null;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x000314C5 File Offset: 0x000304C5
		public string[] MergeDataReaderColumnNames
		{
			get
			{
				if (this.e != null)
				{
					return this.e.b();
				}
				return null;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x000314DC File Offset: 0x000304DC
		internal global::a.d.k Merge
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x000314E4 File Offset: 0x000304E4
		public string Tag
		{
			get
			{
				return this.f;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x000314EC File Offset: 0x000304EC
		internal global::a.n.a AddrCheck
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x000314F4 File Offset: 0x000304F4
		// (set) Token: 0x06000BD2 RID: 3026 RVA: 0x000314FC File Offset: 0x000304FC
		public bool Continue
		{
			get
			{
				return this.h;
			}
			set
			{
				this.h = value;
			}
		}

		// Token: 0x04000868 RID: 2152
		private MailMessage a;

		// Token: 0x04000869 RID: 2153
		private EmailAddressCollection b;

		// Token: 0x0400086A RID: 2154
		private string c;

		// Token: 0x0400086B RID: 2155
		private MailBeeSmtpNegativeResponseException d;

		// Token: 0x0400086C RID: 2156
		private global::a.d.k e;

		// Token: 0x0400086D RID: 2157
		private string f;

		// Token: 0x0400086E RID: 2158
		private global::a.n.a g;

		// Token: 0x0400086F RID: 2159
		private bool h;
	}
}
