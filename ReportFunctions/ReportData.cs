using System;
using System.Data;

namespace ReportFunctions
{
	// Token: 0x02000029 RID: 41
	public class ReportData
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0003B2C0 File Offset: 0x0003A2C0
		public DataTable DataTable
		{
			get
			{
				return this.dv.Table;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0003B2E0 File Offset: 0x0003A2E0
		// (set) Token: 0x060002CB RID: 715 RVA: 0x0003B2F8 File Offset: 0x0003A2F8
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

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0003B304 File Offset: 0x0003A304
		// (set) Token: 0x060002CD RID: 717 RVA: 0x0003B31C File Offset: 0x0003A31C
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

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0003B328 File Offset: 0x0003A328
		// (set) Token: 0x060002CF RID: 719 RVA: 0x0003B340 File Offset: 0x0003A340
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

		// Token: 0x060002D0 RID: 720 RVA: 0x0003B34A File Offset: 0x0003A34A
		public ReportData(DataView dv, string name, int id)
		{
			this.id = id;
			this.dv = dv;
			this.name = name;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0003B36A File Offset: 0x0003A36A
		public ReportData(DataTable t, string name, int id)
		{
			this.id = id;
			this.dv = t.DefaultView;
			this.name = name;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0003B390 File Offset: 0x0003A390
		public bool Is(string name)
		{
			return this.name.ToLower().CompareTo(name.ToLower()) == 0;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0003B3BC File Offset: 0x0003A3BC
		public bool Is(string name, int id)
		{
			return this.name.ToLower().CompareTo(name.ToLower()) == 0 && this.id == id;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0003B3F4 File Offset: 0x0003A3F4
		public bool Is(DataView dataView)
		{
			return this.dv != null && this.dv == dataView;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0003B41C File Offset: 0x0003A41C
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

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0003B47C File Offset: 0x0003A47C
		public int RowCount
		{
			get
			{
				return (this.dv == null) ? 0 : ((this.dv.Table == null) ? 0 : this.dv.Table.Rows.Count);
			}
		}

		// Token: 0x0400014F RID: 335
		private DataView dv;

		// Token: 0x04000150 RID: 336
		private string name;

		// Token: 0x04000151 RID: 337
		private int id;
	}
}
