using System;

namespace System.Windows.Forms
{
	// Token: 0x0200020D RID: 525
	public class DataGridViewRowDividerDoubleClickEventArgs : HandledMouseEventArgs
	{
		// Token: 0x0600227B RID: 8827 RVA: 0x000A5140 File Offset: 0x000A3340
		public DataGridViewRowDividerDoubleClickEventArgs(int rowIndex, HandledMouseEventArgs e) : base(e.Button, e.Clicks, e.X, e.Y, e.Delta, e.Handled)
		{
			if (rowIndex < -1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			this.rowIndex = rowIndex;
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x0600227C RID: 8828 RVA: 0x000A518D File Offset: 0x000A338D
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000E37 RID: 3639
		private int rowIndex;
	}
}
