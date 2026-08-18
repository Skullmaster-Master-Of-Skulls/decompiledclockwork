using System;

namespace System.Windows.Forms
{
	// Token: 0x0200020B RID: 523
	public class DataGridViewRowContextMenuStripNeededEventArgs : EventArgs
	{
		// Token: 0x06002273 RID: 8819 RVA: 0x000A5085 File Offset: 0x000A3285
		public DataGridViewRowContextMenuStripNeededEventArgs(int rowIndex)
		{
			if (rowIndex < -1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			this.rowIndex = rowIndex;
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x000A50A3 File Offset: 0x000A32A3
		internal DataGridViewRowContextMenuStripNeededEventArgs(int rowIndex, ContextMenuStrip contextMenuStrip) : this(rowIndex)
		{
			this.contextMenuStrip = contextMenuStrip;
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06002275 RID: 8821 RVA: 0x000A50B3 File Offset: 0x000A32B3
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06002276 RID: 8822 RVA: 0x000A50BB File Offset: 0x000A32BB
		// (set) Token: 0x06002277 RID: 8823 RVA: 0x000A50C3 File Offset: 0x000A32C3
		public ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return this.contextMenuStrip;
			}
			set
			{
				this.contextMenuStrip = value;
			}
		}

		// Token: 0x04000E35 RID: 3637
		private int rowIndex;

		// Token: 0x04000E36 RID: 3638
		private ContextMenuStrip contextMenuStrip;
	}
}
