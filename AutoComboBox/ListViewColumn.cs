using System;

namespace AutoComboBox
{
	// Token: 0x020000B3 RID: 179
	[Serializable]
	public class ListViewColumn
	{
		// Token: 0x060006BE RID: 1726 RVA: 0x00035E84 File Offset: 0x00034E84
		public ListViewColumn(string colHeader, int colWidth, int colOrder)
		{
			this.header = colHeader;
			this.width = colWidth;
			this.order = colOrder;
		}

		// Token: 0x0400054E RID: 1358
		public string header;

		// Token: 0x0400054F RID: 1359
		public int width;

		// Token: 0x04000550 RID: 1360
		public int order;
	}
}
