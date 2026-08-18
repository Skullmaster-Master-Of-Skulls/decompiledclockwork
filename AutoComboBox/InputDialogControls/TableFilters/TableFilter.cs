using System;

namespace AutoComboBox.InputDialogControls.TableFilters
{
	// Token: 0x02000098 RID: 152
	public class TableFilter
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x0003056C File Offset: 0x0002F56C
		// (set) Token: 0x060005CF RID: 1487 RVA: 0x00030584 File Offset: 0x0002F584
		public TableFilterComparerType ComparerType
		{
			get
			{
				return this.comparerType;
			}
			set
			{
				this.comparerType = value;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x00030590 File Offset: 0x0002F590
		// (set) Token: 0x060005D1 RID: 1489 RVA: 0x000305A8 File Offset: 0x0002F5A8
		public string Val
		{
			get
			{
				return this.val;
			}
			set
			{
				this.val = value;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x000305B4 File Offset: 0x0002F5B4
		// (set) Token: 0x060005D3 RID: 1491 RVA: 0x000305CC File Offset: 0x0002F5CC
		public string ColName
		{
			get
			{
				return this.colName;
			}
			set
			{
				this.colName = value;
			}
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x000305D6 File Offset: 0x0002F5D6
		public TableFilter(string colName, TableFilterComparerType comparerType, string val)
		{
			this.colName = colName;
			this.comparerType = comparerType;
			this.val = val;
		}

		// Token: 0x040004C2 RID: 1218
		private TableFilterComparerType comparerType;

		// Token: 0x040004C3 RID: 1219
		private string val;

		// Token: 0x040004C4 RID: 1220
		private string colName;
	}
}
