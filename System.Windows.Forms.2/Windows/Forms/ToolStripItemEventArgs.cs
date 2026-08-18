using System;

namespace System.Windows.Forms
{
	// Token: 0x020003D3 RID: 979
	public class ToolStripItemEventArgs : EventArgs
	{
		// Token: 0x06004341 RID: 17217 RVA: 0x0011D139 File Offset: 0x0011B339
		public ToolStripItemEventArgs(ToolStripItem item)
		{
			this.item = item;
		}

		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x06004342 RID: 17218 RVA: 0x0011D148 File Offset: 0x0011B348
		public ToolStripItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x040025AB RID: 9643
		private ToolStripItem item;
	}
}
