using System;
using System.Data;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity
{
	// Token: 0x02000020 RID: 32
	public class ReportData
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00028EF4 File Offset: 0x000270F4
		public DataTable DataTable
		{
			get
			{
				return this.dv.Table;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00028F14 File Offset: 0x00027114
		// (set) Token: 0x06000248 RID: 584 RVA: 0x00028F2C File Offset: 0x0002712C
		public DataView DataView
		{
			get
			{
				return this.dv;
			}
			set
			{
				this.dv = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00028F38 File Offset: 0x00027138
		// (set) Token: 0x0600024A RID: 586 RVA: 0x00028F50 File Offset: 0x00027150
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00028F5C File Offset: 0x0002715C
		// (set) Token: 0x0600024C RID: 588 RVA: 0x00028F74 File Offset: 0x00027174
		public int Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00028F7E File Offset: 0x0002717E
		public ReportData(DataView dv, string name, int id)
		{
			this.id = id;
			this.dv = dv;
			this.name = name;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00028F9D File Offset: 0x0002719D
		public ReportData(DataTable t, string name, int id)
		{
			this.id = id;
			this.dv = t.DefaultView;
			this.name = name;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00028FC4 File Offset: 0x000271C4
		public bool Is(string name)
		{
			return this.name.ToLower().CompareTo(name.ToLower()) == 0;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00028FF0 File Offset: 0x000271F0
		public bool Is(string name, int id)
		{
			return this.name.ToLower().CompareTo(name.ToLower()) == 0 && this.id == id;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00029028 File Offset: 0x00027228
		public bool Is(DataView dataView)
		{
			return this.dv != null && this.dv == dataView;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00029050 File Offset: 0x00027250
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.name,
				" (",
				this.id.ToString(),
				") [",
				this.RowCount.ToString(),
				"]"
			});
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000253 RID: 595 RVA: 0x000290B0 File Offset: 0x000272B0
		public int RowCount
		{
			get
			{
				return (this.dv == null) ? 0 : ((this.dv.Table == null) ? 0 : this.dv.Table.Rows.Count);
			}
		}

		// Token: 0x040000E7 RID: 231
		private DataView dv;

		// Token: 0x040000E8 RID: 232
		private string name;

		// Token: 0x040000E9 RID: 233
		private int id;
	}
}
