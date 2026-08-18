using System;

namespace System.Windows.Forms
{
	// Token: 0x020001A7 RID: 423
	public class DataGridViewCellContextMenuStripNeededEventArgs : DataGridViewCellEventArgs
	{
		// Token: 0x06001E2F RID: 7727 RVA: 0x0008F010 File Offset: 0x0008D210
		public DataGridViewCellContextMenuStripNeededEventArgs(int columnIndex, int rowIndex) : base(columnIndex, rowIndex)
		{
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x0008F01A File Offset: 0x0008D21A
		internal DataGridViewCellContextMenuStripNeededEventArgs(int columnIndex, int rowIndex, ContextMenuStrip contextMenuStrip) : base(columnIndex, rowIndex)
		{
			this.contextMenuStrip = contextMenuStrip;
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001E31 RID: 7729 RVA: 0x0008F02B File Offset: 0x0008D22B
		// (set) Token: 0x06001E32 RID: 7730 RVA: 0x0008F033 File Offset: 0x0008D233
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

		// Token: 0x04000CC4 RID: 3268
		private ContextMenuStrip contextMenuStrip;
	}
}
