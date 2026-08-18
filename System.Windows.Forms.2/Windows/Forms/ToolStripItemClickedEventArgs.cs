using System;

namespace System.Windows.Forms
{
	// Token: 0x020003CF RID: 975
	public class ToolStripItemClickedEventArgs : EventArgs
	{
		// Token: 0x06004312 RID: 17170 RVA: 0x0011C879 File Offset: 0x0011AA79
		public ToolStripItemClickedEventArgs(ToolStripItem clickedItem)
		{
			this.clickedItem = clickedItem;
		}

		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x06004313 RID: 17171 RVA: 0x0011C888 File Offset: 0x0011AA88
		public ToolStripItem ClickedItem
		{
			get
			{
				return this.clickedItem;
			}
		}

		// Token: 0x040025A1 RID: 9633
		private ToolStripItem clickedItem;
	}
}
