using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x020001A5 RID: 421
	public class DataGridViewCellCancelEventArgs : CancelEventArgs
	{
		// Token: 0x06001E06 RID: 7686 RVA: 0x0008E886 File Offset: 0x0008CA86
		internal DataGridViewCellCancelEventArgs(DataGridViewCell dataGridViewCell) : this(dataGridViewCell.ColumnIndex, dataGridViewCell.RowIndex)
		{
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x0008E89A File Offset: 0x0008CA9A
		public DataGridViewCellCancelEventArgs(int columnIndex, int rowIndex)
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

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001E08 RID: 7688 RVA: 0x0008E8CE File Offset: 0x0008CACE
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001E09 RID: 7689 RVA: 0x0008E8D6 File Offset: 0x0008CAD6
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000CBF RID: 3263
		private int columnIndex;

		// Token: 0x04000CC0 RID: 3264
		private int rowIndex;
	}
}
