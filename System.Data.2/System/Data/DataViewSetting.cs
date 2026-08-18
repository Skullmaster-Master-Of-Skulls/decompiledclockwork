using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x020000DD RID: 221
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class DataViewSetting
	{
		// Token: 0x06000EEB RID: 3819 RVA: 0x000789A8 File Offset: 0x00077DA8
		internal DataViewSetting()
		{
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x000789DC File Offset: 0x00077DDC
		internal DataViewSetting(string sort, string rowFilter, DataViewRowState rowStateFilter)
		{
			this.sort = sort;
			this.rowFilter = rowFilter;
			this.rowStateFilter = rowStateFilter;
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x00078A24 File Offset: 0x00077E24
		// (set) Token: 0x06000EEE RID: 3822 RVA: 0x00078A38 File Offset: 0x00077E38
		public bool ApplyDefaultSort
		{
			get
			{
				return this.applyDefaultSort;
			}
			set
			{
				if (this.applyDefaultSort != value)
				{
					this.applyDefaultSort = value;
				}
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x00078A58 File Offset: 0x00077E58
		[Browsable(false)]
		public DataViewManager DataViewManager
		{
			get
			{
				return this.dataViewManager;
			}
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x00078A6C File Offset: 0x00077E6C
		internal void SetDataViewManager(DataViewManager dataViewManager)
		{
			if (this.dataViewManager != dataViewManager)
			{
				DataViewManager dataViewManager2 = this.dataViewManager;
				this.dataViewManager = dataViewManager;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x00078A90 File Offset: 0x00077E90
		[Browsable(false)]
		public DataTable Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x00078AA4 File Offset: 0x00077EA4
		internal void SetDataTable(DataTable table)
		{
			if (this.table != table)
			{
				DataTable dataTable = this.table;
				this.table = table;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x00078AC8 File Offset: 0x00077EC8
		// (set) Token: 0x06000EF4 RID: 3828 RVA: 0x00078ADC File Offset: 0x00077EDC
		public string RowFilter
		{
			get
			{
				return this.rowFilter;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (this.rowFilter != value)
				{
					this.rowFilter = value;
				}
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x00078B08 File Offset: 0x00077F08
		// (set) Token: 0x06000EF6 RID: 3830 RVA: 0x00078B1C File Offset: 0x00077F1C
		public DataViewRowState RowStateFilter
		{
			get
			{
				return this.rowStateFilter;
			}
			set
			{
				if (this.rowStateFilter != value)
				{
					this.rowStateFilter = value;
				}
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x00078B3C File Offset: 0x00077F3C
		// (set) Token: 0x06000EF8 RID: 3832 RVA: 0x00078B50 File Offset: 0x00077F50
		public string Sort
		{
			get
			{
				return this.sort;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (this.sort != value)
				{
					this.sort = value;
				}
			}
		}

		// Token: 0x0400044D RID: 1101
		private DataViewManager dataViewManager;

		// Token: 0x0400044E RID: 1102
		private DataTable table;

		// Token: 0x0400044F RID: 1103
		private string sort = "";

		// Token: 0x04000450 RID: 1104
		private string rowFilter = "";

		// Token: 0x04000451 RID: 1105
		private DataViewRowState rowStateFilter = DataViewRowState.CurrentRows;

		// Token: 0x04000452 RID: 1106
		private bool applyDefaultSort;
	}
}
