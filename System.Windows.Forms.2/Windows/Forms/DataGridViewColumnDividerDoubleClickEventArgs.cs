using System;

namespace System.Windows.Forms
{
	// Token: 0x020001C3 RID: 451
	public class DataGridViewColumnDividerDoubleClickEventArgs : HandledMouseEventArgs
	{
		// Token: 0x06001FAE RID: 8110 RVA: 0x00095B9C File Offset: 0x00093D9C
		public DataGridViewColumnDividerDoubleClickEventArgs(int columnIndex, HandledMouseEventArgs e) : base(e.Button, e.Clicks, e.X, e.Y, e.Delta, e.Handled)
		{
			if (columnIndex < -1)
			{
				throw new ArgumentOutOfRangeException("columnIndex");
			}
			this.columnIndex = columnIndex;
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001FAF RID: 8111 RVA: 0x00095BE9 File Offset: 0x00093DE9
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		// Token: 0x04000D53 RID: 3411
		private int columnIndex;
	}
}
