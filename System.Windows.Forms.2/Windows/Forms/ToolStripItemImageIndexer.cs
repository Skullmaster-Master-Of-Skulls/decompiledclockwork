using System;

namespace System.Windows.Forms
{
	// Token: 0x020003CC RID: 972
	internal class ToolStripItemImageIndexer : ImageList.Indexer
	{
		// Token: 0x060042FF RID: 17151 RVA: 0x0011C4BF File Offset: 0x0011A6BF
		public ToolStripItemImageIndexer(ToolStripItem item)
		{
			this.item = item;
		}

		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x06004300 RID: 17152 RVA: 0x0011C4CE File Offset: 0x0011A6CE
		// (set) Token: 0x06004301 RID: 17153 RVA: 0x000072B6 File Offset: 0x000054B6
		public override ImageList ImageList
		{
			get
			{
				if (this.item != null && this.item.Owner != null)
				{
					return this.item.Owner.ImageList;
				}
				return null;
			}
			set
			{
			}
		}

		// Token: 0x04002595 RID: 9621
		private ToolStripItem item;
	}
}
