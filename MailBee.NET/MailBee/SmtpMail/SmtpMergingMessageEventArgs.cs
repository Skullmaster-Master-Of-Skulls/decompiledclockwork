using System;
using System.Data;
using a;
using a.d;
using a.n;
using MailBee.Mime;

namespace MailBee.SmtpMail
{
	// Token: 0x02000154 RID: 340
	public class SmtpMergingMessageEventArgs : CommonEventArgs
	{
		// Token: 0x06000BD7 RID: 3031 RVA: 0x00031508 File Offset: 0x00030508
		internal SmtpMergingMessageEventArgs(MailMessage A_0, string A_1, EmailAddressCollection A_2, DeliveryNotificationOptions A_3, global::a.d.k A_4, string A_5, global::a.n.a A_6, bc A_7) : base(A_7)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = A_2;
			this.d = A_3;
			this.e = A_4;
			this.f = A_5;
			this.g = A_6;
			this.h = true;
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x00031559 File Offset: 0x00030559
		public MailMessage TemplateMessage
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x00031561 File Offset: 0x00030561
		public string TemplateSenderEmail
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x00031569 File Offset: 0x00030569
		public EmailAddressCollection TemplateRecipients
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x00031571 File Offset: 0x00030571
		public DeliveryNotificationOptions TemplateDeliveryNotification
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x00031579 File Offset: 0x00030579
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

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x00031590 File Offset: 0x00030590
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

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x000315A7 File Offset: 0x000305A7
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

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x000315BE File Offset: 0x000305BE
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

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x000315D5 File Offset: 0x000305D5
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

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x000315EC File Offset: 0x000305EC
		internal global::a.d.k Merge
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x000315F4 File Offset: 0x000305F4
		public string Tag
		{
			get
			{
				return this.f;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x000315FC File Offset: 0x000305FC
		// (set) Token: 0x06000BE4 RID: 3044 RVA: 0x00031604 File Offset: 0x00030604
		public bool MergeIt
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

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x0003160D File Offset: 0x0003060D
		internal global::a.n.a AddrCheck
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x04000870 RID: 2160
		private MailMessage a;

		// Token: 0x04000871 RID: 2161
		private string b;

		// Token: 0x04000872 RID: 2162
		private EmailAddressCollection c;

		// Token: 0x04000873 RID: 2163
		private DeliveryNotificationOptions d;

		// Token: 0x04000874 RID: 2164
		private global::a.d.k e;

		// Token: 0x04000875 RID: 2165
		private string f;

		// Token: 0x04000876 RID: 2166
		private global::a.n.a g;

		// Token: 0x04000877 RID: 2167
		private bool h;
	}
}
