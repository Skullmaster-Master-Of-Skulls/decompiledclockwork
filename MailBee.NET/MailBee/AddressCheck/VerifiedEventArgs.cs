using System;
using System.Data;
using a;
using a.d;

namespace MailBee.AddressCheck
{
	// Token: 0x02000088 RID: 136
	public class VerifiedEventArgs : CommonEventArgs
	{
		// Token: 0x060004AB RID: 1195 RVA: 0x0000CF2A File Offset: 0x0000BF2A
		internal VerifiedEventArgs(string A_0, global::a.d.k A_1, AddressValidationLevel A_2, MailBeeException A_3, bc A_4) : base(A_4)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = A_2;
			this.d = A_3;
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0000CF51 File Offset: 0x0000BF51
		public string Email
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0000CF59 File Offset: 0x0000BF59
		public DataRow Row
		{
			get
			{
				if (this.b.c() != null)
				{
					return this.b.c().Rows[this.b.a()];
				}
				return null;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0000CF8A File Offset: 0x0000BF8A
		public int RowIndex
		{
			get
			{
				return this.b.a();
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0000CF97 File Offset: 0x0000BF97
		public DataTable Table
		{
			get
			{
				return this.b.c();
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0000CFA4 File Offset: 0x0000BFA4
		public IDataReader DataReader
		{
			get
			{
				return this.b.e();
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0000CFB1 File Offset: 0x0000BFB1
		public object[] DataReaderRowValues
		{
			get
			{
				if (this.b != null)
				{
					return this.b.d();
				}
				return null;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000CFC8 File Offset: 0x0000BFC8
		public string[] DataReaderColumnNames
		{
			get
			{
				if (this.b != null)
				{
					return this.b.b();
				}
				return null;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0000CFDF File Offset: 0x0000BFDF
		public AddressValidationLevel Result
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0000CFE7 File Offset: 0x0000BFE7
		public MailBeeException Reason
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x0400021B RID: 539
		private string a;

		// Token: 0x0400021C RID: 540
		private global::a.d.k b;

		// Token: 0x0400021D RID: 541
		private AddressValidationLevel c;

		// Token: 0x0400021E RID: 542
		private MailBeeException d;
	}
}
