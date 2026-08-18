using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x0200021C RID: 540
	public class DataGridViewSortCompareEventArgs : HandledEventArgs
	{
		// Token: 0x06002324 RID: 8996 RVA: 0x000A7492 File Offset: 0x000A5692
		public DataGridViewSortCompareEventArgs(DataGridViewColumn dataGridViewColumn, object cellValue1, object cellValue2, int rowIndex1, int rowIndex2)
		{
			this.dataGridViewColumn = dataGridViewColumn;
			this.cellValue1 = cellValue1;
			this.cellValue2 = cellValue2;
			this.rowIndex1 = rowIndex1;
			this.rowIndex2 = rowIndex2;
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06002325 RID: 8997 RVA: 0x000A74BF File Offset: 0x000A56BF
		public object CellValue1
		{
			get
			{
				return this.cellValue1;
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06002326 RID: 8998 RVA: 0x000A74C7 File Offset: 0x000A56C7
		public object CellValue2
		{
			get
			{
				return this.cellValue2;
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06002327 RID: 8999 RVA: 0x000A74CF File Offset: 0x000A56CF
		public DataGridViewColumn Column
		{
			get
			{
				return this.dataGridViewColumn;
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06002328 RID: 9000 RVA: 0x000A74D7 File Offset: 0x000A56D7
		public int RowIndex1
		{
			get
			{
				return this.rowIndex1;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06002329 RID: 9001 RVA: 0x000A74DF File Offset: 0x000A56DF
		public int RowIndex2
		{
			get
			{
				return this.rowIndex2;
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x0600232A RID: 9002 RVA: 0x000A74E7 File Offset: 0x000A56E7
		// (set) Token: 0x0600232B RID: 9003 RVA: 0x000A74EF File Offset: 0x000A56EF
		public int SortResult
		{
			get
			{
				return this.sortResult;
			}
			set
			{
				this.sortResult = value;
			}
		}

		// Token: 0x04000E75 RID: 3701
		private DataGridViewColumn dataGridViewColumn;

		// Token: 0x04000E76 RID: 3702
		private object cellValue1;

		// Token: 0x04000E77 RID: 3703
		private object cellValue2;

		// Token: 0x04000E78 RID: 3704
		private int sortResult;

		// Token: 0x04000E79 RID: 3705
		private int rowIndex1;

		// Token: 0x04000E7A RID: 3706
		private int rowIndex2;
	}
}
