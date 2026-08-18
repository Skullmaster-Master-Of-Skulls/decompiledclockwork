using System;
using System.Data;
using a;
using a.d;

namespace MailBee.AddressCheck
{
	// Token: 0x02000086 RID: 134
	public class VerifyingEventArgs : CommonEventArgs
	{
		// Token: 0x0600049D RID: 1181 RVA: 0x0000CE6D File Offset: 0x0000BE6D
		internal VerifyingEventArgs(string A_0, global::a.d.k A_1, bc A_2) : base(A_2)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = true;
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000CE8B File Offset: 0x0000BE8B
		public string Email
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0000CE93 File Offset: 0x0000BE93
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

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0000CEC4 File Offset: 0x0000BEC4
		public int RowIndex
		{
			get
			{
				return this.b.a();
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0000CED1 File Offset: 0x0000BED1
		public DataTable Table
		{
			get
			{
				return this.b.c();
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0000CEDE File Offset: 0x0000BEDE
		public IDataReader DataReader
		{
			get
			{
				return this.b.e();
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0000CEEB File Offset: 0x0000BEEB
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

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0000CF02 File Offset: 0x0000BF02
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

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x0000CF19 File Offset: 0x0000BF19
		// (set) Token: 0x060004A6 RID: 1190 RVA: 0x0000CF21 File Offset: 0x0000BF21
		public bool VerifyIt
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

		// Token: 0x04000218 RID: 536
		private string a;

		// Token: 0x04000219 RID: 537
		private global::a.d.k b;

		// Token: 0x0400021A RID: 538
		private bool c;
	}
}
