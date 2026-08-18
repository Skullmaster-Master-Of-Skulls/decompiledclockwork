using System;
using System.Data;
using a;
using a.d;
using MailBee.Mime;

namespace MailBee.SmtpMail
{
	// Token: 0x02000158 RID: 344
	public class SmtpSubmittingMessageToPickupFolderEventArgs : CommonEventArgs
	{
		// Token: 0x06000BF3 RID: 3059 RVA: 0x0003165C File Offset: 0x0003065C
		internal SmtpSubmittingMessageToPickupFolderEventArgs(MailMessage A_0, string A_1, EmailAddressCollection A_2, string A_3, string A_4, global::a.d.k A_5, string A_6, bc A_7) : base(A_7)
		{
			this.a = A_0;
			this.e = A_1;
			this.d = A_2;
			this.b = A_3;
			this.c = A_4;
			this.f = A_5;
			this.g = A_6;
			this.h = true;
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x000316AD File Offset: 0x000306AD
		public MailMessage MailMessage
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x000316B5 File Offset: 0x000306B5
		// (set) Token: 0x06000BF6 RID: 3062 RVA: 0x000316BD File Offset: 0x000306BD
		public string PickupFolderName
		{
			get
			{
				return this.b;
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					throw new MailBeeInvalidArgumentException(22);
				}
				this.b = value;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x000316DE File Offset: 0x000306DE
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x000316E6 File Offset: 0x000306E6
		public string Filename
		{
			get
			{
				return this.c;
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					throw new MailBeeInvalidArgumentException(22);
				}
				this.c = value;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x00031707 File Offset: 0x00030707
		// (set) Token: 0x06000BFA RID: 3066 RVA: 0x0003170F File Offset: 0x0003070F
		public EmailAddressCollection ActualRecipients
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

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x00031718 File Offset: 0x00030718
		// (set) Token: 0x06000BFC RID: 3068 RVA: 0x00031720 File Offset: 0x00030720
		public string ActualSenderEmail
		{
			get
			{
				return this.e;
			}
			set
			{
				this.e = value;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x00031729 File Offset: 0x00030729
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

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x00031740 File Offset: 0x00030740
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

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x00031757 File Offset: 0x00030757
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

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0003176E File Offset: 0x0003076E
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

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x00031785 File Offset: 0x00030785
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

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0003179C File Offset: 0x0003079C
		internal global::a.d.k Merge
		{
			get
			{
				return this.f;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x000317A4 File Offset: 0x000307A4
		public string Tag
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x000317AC File Offset: 0x000307AC
		// (set) Token: 0x06000C05 RID: 3077 RVA: 0x000317B4 File Offset: 0x000307B4
		public bool SubmitIt
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

		// Token: 0x0400087A RID: 2170
		private MailMessage a;

		// Token: 0x0400087B RID: 2171
		private string b;

		// Token: 0x0400087C RID: 2172
		private string c;

		// Token: 0x0400087D RID: 2173
		private EmailAddressCollection d;

		// Token: 0x0400087E RID: 2174
		private string e;

		// Token: 0x0400087F RID: 2175
		private global::a.d.k f;

		// Token: 0x04000880 RID: 2176
		private string g;

		// Token: 0x04000881 RID: 2177
		private bool h;
	}
}
