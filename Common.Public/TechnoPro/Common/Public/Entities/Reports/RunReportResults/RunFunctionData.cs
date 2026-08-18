using System;
using System.Data;

namespace TechnoPro.Common.Public.Entities.Reports.RunReportResults
{
	// Token: 0x02000236 RID: 566
	public class RunFunctionData : BusinessBase<string>
	{
		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x00018008 File Offset: 0x00016208
		// (set) Token: 0x06001139 RID: 4409 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public virtual string Name
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x00018020 File Offset: 0x00016220
		// (set) Token: 0x0600113B RID: 4411 RVA: 0x00018028 File Offset: 0x00016228
		public DataTable Table
		{
			get
			{
				return this._table;
			}
			set
			{
				this._table = value;
				bool flag = this._table == null;
				if (!flag)
				{
					foreach (object obj in this._table.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj;
						bool flag2 = dataColumn.ColumnName.Length > 200;
						if (flag2)
						{
							dataColumn.ColumnName = dataColumn.ColumnName.Substring(0, 200);
						}
					}
				}
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x000180CC File Offset: 0x000162CC
		// (set) Token: 0x0600113D RID: 4413 RVA: 0x000180D4 File Offset: 0x000162D4
		public bool AddToAdditionalData { get; set; }

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x000180DD File Offset: 0x000162DD
		// (set) Token: 0x0600113F RID: 4415 RVA: 0x000180E5 File Offset: 0x000162E5
		public bool IsPrimary { get; set; }

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x000180EE File Offset: 0x000162EE
		// (set) Token: 0x06001141 RID: 4417 RVA: 0x000180F6 File Offset: 0x000162F6
		public string TableSort { get; set; }

		// Token: 0x04000F60 RID: 3936
		private DataTable _table;
	}
}
