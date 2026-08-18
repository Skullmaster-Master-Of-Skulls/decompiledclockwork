using System;
using System.Data;
using a;
using a.d;
using MailBee.Mime;

namespace MailBee.SmtpMail
{
	// Token: 0x02000144 RID: 324
	public class SmtpSendingMessageEventArgs : CommonEventArgs
	{
		// Token: 0x06000B68 RID: 2920 RVA: 0x00030FDD File Offset: 0x0002FFDD
		internal SmtpSendingMessageEventArgs(MailMessage A_0, string A_1, EmailAddressCollection A_2, DeliveryNotificationOptions A_3, global::a.d.k A_4, string A_5, bc A_6) : base(A_6)
		{
			this.a = A_0;
			this.c = A_1;
			this.b = A_2;
			this.d = A_3;
			this.e = A_4;
			this.f = A_5;
			this.g = true;
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x0003101B File Offset: 0x0003001B
		public MailMessage MailMessage
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x00031023 File Offset: 0x00030023
		// (set) Token: 0x06000B6B RID: 2923 RVA: 0x0003102B File Offset: 0x0003002B
		public EmailAddressCollection ActualRecipients
		{
			get
			{
				return this.b;
			}
			set
			{
				this.b = value;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x00031034 File Offset: 0x00030034
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x0003103C File Offset: 0x0003003C
		public string ActualSenderEmail
		{
			get
			{
				return this.c;
			}
			set
			{
				this.c = value;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x00031045 File Offset: 0x00030045
		public DeliveryNotificationOptions DeliveryNotification
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x0003104D File Offset: 0x0003004D
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

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x00031064 File Offset: 0x00030064
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

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x0003107B File Offset: 0x0003007B
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

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x00031092 File Offset: 0x00030092
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

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x000310A9 File Offset: 0x000300A9
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

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x000310C0 File Offset: 0x000300C0
		internal global::a.d.k Merge
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x000310C8 File Offset: 0x000300C8
		public string Tag
		{
			get
			{
				return this.f;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x000310D0 File Offset: 0x000300D0
		// (set) Token: 0x06000B77 RID: 2935 RVA: 0x000310D8 File Offset: 0x000300D8
		public bool SendIt
		{
			get
			{
				return this.g;
			}
			set
			{
				this.g = value;
			}
		}

		// Token: 0x04000842 RID: 2114
		private MailMessage a;

		// Token: 0x04000843 RID: 2115
		private EmailAddressCollection b;

		// Token: 0x04000844 RID: 2116
		private string c;

		// Token: 0x04000845 RID: 2117
		private DeliveryNotificationOptions d;

		// Token: 0x04000846 RID: 2118
		private global::a.d.k e;

		// Token: 0x04000847 RID: 2119
		private string f;

		// Token: 0x04000848 RID: 2120
		private bool g;
	}
}
