using System;
using System.Data;
using a;
using a.d;
using a.n;
using MailBee.Mime;

namespace MailBee.SmtpMail
{
	// Token: 0x02000150 RID: 336
	public class SmtpMessageNotSentEventArgs : CommonEventArgs
	{
		// Token: 0x06000BB3 RID: 2995 RVA: 0x0003130D File Offset: 0x0003030D
		internal SmtpMessageNotSentEventArgs(MailMessage A_0, string A_1, EmailAddressCollection A_2, MailBeeException A_3, global::a.d.k A_4, string A_5, global::a.n.a A_6, bc A_7) : base(A_7)
		{
			this.a = A_0;
			this.c = A_1;
			this.b = A_2;
			this.d = A_3;
			this.e = A_4;
			this.f = A_5;
			this.g = A_6;
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x0003134C File Offset: 0x0003034C
		public MailMessage MailMessage
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x00031354 File Offset: 0x00030354
		public EmailAddressCollection IntendedRecipients
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x0003135C File Offset: 0x0003035C
		public string ActualSenderEmail
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x00031364 File Offset: 0x00030364
		public MailBeeException Reason
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x0003136C File Offset: 0x0003036C
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

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x00031383 File Offset: 0x00030383
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

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x0003139A File Offset: 0x0003039A
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

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x000313B1 File Offset: 0x000303B1
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

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x000313C8 File Offset: 0x000303C8
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

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x000313DF File Offset: 0x000303DF
		internal global::a.d.k Merge
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x000313E7 File Offset: 0x000303E7
		public string Tag
		{
			get
			{
				return this.f;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x000313EF File Offset: 0x000303EF
		internal global::a.n.a AddrCheck
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x04000861 RID: 2145
		private MailMessage a;

		// Token: 0x04000862 RID: 2146
		private EmailAddressCollection b;

		// Token: 0x04000863 RID: 2147
		private string c;

		// Token: 0x04000864 RID: 2148
		private MailBeeException d;

		// Token: 0x04000865 RID: 2149
		private global::a.d.k e;

		// Token: 0x04000866 RID: 2150
		private string f;

		// Token: 0x04000867 RID: 2151
		private global::a.n.a g;
	}
}
