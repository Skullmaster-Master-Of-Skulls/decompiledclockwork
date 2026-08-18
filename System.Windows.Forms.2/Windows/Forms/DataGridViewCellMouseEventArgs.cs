using System;

namespace System.Windows.Forms
{
	// Token: 0x020001AF RID: 431
	public class DataGridViewCellMouseEventArgs : MouseEventArgs
	{
		// Token: 0x06001E56 RID: 7766 RVA: 0x0008F488 File Offset: 0x0008D688
		public DataGridViewCellMouseEventArgs(int columnIndex, int rowIndex, int localX, int localY, MouseEventArgs e) : base(e.Button, e.Clicks, localX, localY, e.Delta)
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

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001E57 RID: 7767 RVA: 0x0008F4DF File Offset: 0x0008D6DF
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001E58 RID: 7768 RVA: 0x0008F4E7 File Offset: 0x0008D6E7
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000CD5 RID: 3285
		private int rowIndex;

		// Token: 0x04000CD6 RID: 3286
		private int columnIndex;
	}
}
