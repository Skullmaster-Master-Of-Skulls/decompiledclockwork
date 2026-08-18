using System;

namespace System.Windows.Forms
{
	// Token: 0x020001AA RID: 426
	public class DataGridViewCellEventArgs : EventArgs
	{
		// Token: 0x06001E39 RID: 7737 RVA: 0x0008F0D1 File Offset: 0x0008D2D1
		internal DataGridViewCellEventArgs(DataGridViewCell dataGridViewCell) : this(dataGridViewCell.ColumnIndex, dataGridViewCell.RowIndex)
		{
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x0008F0E5 File Offset: 0x0008D2E5
		public DataGridViewCellEventArgs(int columnIndex, int rowIndex)
		{
			if (columnIndex < -1)
			{
				throw new ArgumentOutOfRangeException("columnIndex");
			}
			if (rowIndex < -1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			this.columnIndex = columnIndex;
			this.rowIndex = rowIndex;
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001E3B RID: 7739 RVA: 0x0008F119 File Offset: 0x0008D319
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001E3C RID: 7740 RVA: 0x0008F121 File Offset: 0x0008D321
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000CC6 RID: 3270
		private int columnIndex;

		// Token: 0x04000CC7 RID: 3271
		private int rowIndex;
	}
}
