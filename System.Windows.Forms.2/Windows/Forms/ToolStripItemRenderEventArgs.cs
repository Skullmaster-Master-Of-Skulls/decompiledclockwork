using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x020003DB RID: 987
	public class ToolStripItemRenderEventArgs : EventArgs
	{
		// Token: 0x06004353 RID: 17235 RVA: 0x0011D1F2 File Offset: 0x0011B3F2
		public ToolStripItemRenderEventArgs(Graphics g, ToolStripItem item)
		{
			this.item = item;
			this.graphics = g;
		}

		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x06004354 RID: 17236 RVA: 0x0011D208 File Offset: 0x0011B408
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x06004355 RID: 17237 RVA: 0x0011D210 File Offset: 0x0011B410
		public ToolStripItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x06004356 RID: 17238 RVA: 0x0011D218 File Offset: 0x0011B418
		public ToolStrip ToolStrip
		{
			get
			{
				return this.item.ParentInternal;
			}
		}

		// Token: 0x040025C6 RID: 9670
		private ToolStripItem item;

		// Token: 0x040025C7 RID: 9671
		private Graphics graphics;
	}
}
