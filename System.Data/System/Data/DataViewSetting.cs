using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x020000AE RID: 174
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class DataViewSetting
	{
		// Token: 0x06000BE4 RID: 3044 RVA: 0x0020F0E8 File Offset: 0x0020E4E8
		internal DataViewSetting()
		{
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0020F128 File Offset: 0x0020E528
		internal DataViewSetting(string sort, string rowFilter, DataViewRowState rowStateFilter)
		{
			this.sort = sort;
			this.rowFilter = rowFilter;
			this.rowStateFilter = rowStateFilter;
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0020F178 File Offset: 0x0020E578
		// (set) Token: 0x06000BE7 RID: 3047 RVA: 0x0020F198 File Offset: 0x0020E598
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

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x0020F1B8 File Offset: 0x0020E5B8
		[Browsable(false)]
		public DataViewManager DataViewManager
		{
			get
			{
				return this.dataViewManager;
			}
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0020F1D8 File Offset: 0x0020E5D8
		internal void SetDataViewManager(DataViewManager dataViewManager)
		{
			if (this.dataViewManager != dataViewManager)
			{
				DataViewManager dataViewManager2 = this.dataViewManager;
				this.dataViewManager = dataViewManager;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x0020F208 File Offset: 0x0020E608
		[Browsable(false)]
		public DataTable Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0020F228 File Offset: 0x0020E628
		internal void SetDataTable(DataTable table)
		{
			if (this.table != table)
			{
				DataTable dataTable = this.table;
				this.table = table;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x0020F258 File Offset: 0x0020E658
		// (set) Token: 0x06000BED RID: 3053 RVA: 0x0020F278 File Offset: 0x0020E678
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

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0020F2A8 File Offset: 0x0020E6A8
		// (set) Token: 0x06000BEF RID: 3055 RVA: 0x0020F2C8 File Offset: 0x0020E6C8
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

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x0020F2E8 File Offset: 0x0020E6E8
		// (set) Token: 0x06000BF1 RID: 3057 RVA: 0x0020F308 File Offset: 0x0020E708
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

		// Token: 0x04000871 RID: 2161
		private DataViewManager dataViewManager;

		// Token: 0x04000872 RID: 2162
		private DataTable table;

		// Token: 0x04000873 RID: 2163
		private string sort = "";

		// Token: 0x04000874 RID: 2164
		private string rowFilter = "";

		// Token: 0x04000875 RID: 2165
		private DataViewRowState rowStateFilter = DataViewRowState.CurrentRows;

		// Token: 0x04000876 RID: 2166
		private bool applyDefaultSort;
	}
}
