using System;
using System.Data;
using a;
using a.d;
using MailBee.Mime;

namespace MailBee.SmtpMail
{
	// Token: 0x0200015A RID: 346
	public class SmtpMessageSubmittedToPickupFolderEventArgs : CommonEventArgs
	{
		// Token: 0x06000C0A RID: 3082 RVA: 0x000317BD File Offset: 0x000307BD
		internal SmtpMessageSubmittedToPickupFolderEventArgs(MailMessage A_0, string A_1, EmailAddressCollection A_2, string A_3, string A_4, global::a.d.k A_5, string A_6, bc A_7) : base(A_7)
		{
			this.a = A_0;
			this.e = A_1;
			this.d = A_2;
			this.b = A_3;
			this.c = A_4;
			this.f = A_5;
			this.g = A_6;
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x000317FC File Offset: 0x000307FC
		public MailMessage MailMessage
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x00031804 File Offset: 0x00030804
		public string PickupFolderName
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x0003180C File Offset: 0x0003080C
		public string Filename
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x00031814 File Offset: 0x00030814
		public EmailAddressCollection ActualRecipients
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x0003181C File Offset: 0x0003081C
		public string ActualSenderEmail
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x00031824 File Offset: 0x00030824
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

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x0003183B File Offset: 0x0003083B
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

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x00031852 File Offset: 0x00030852
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

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x00031869 File Offset: 0x00030869
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

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x00031880 File Offset: 0x00030880
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

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x00031897 File Offset: 0x00030897
		internal global::a.d.k Merge
		{
			get
			{
				return this.f;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x0003189F File Offset: 0x0003089F
		public string Tag
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x04000882 RID: 2178
		private MailMessage a;

		// Token: 0x04000883 RID: 2179
		private string b;

		// Token: 0x04000884 RID: 2180
		private string c;

		// Token: 0x04000885 RID: 2181
		private EmailAddressCollection d;

		// Token: 0x04000886 RID: 2182
		private string e;

		// Token: 0x04000887 RID: 2183
		private global::a.d.k f;

		// Token: 0x04000888 RID: 2184
		private string g;
	}
}
