using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000400 RID: 1024
	public class ToolStripSeparatorRenderEventArgs : ToolStripItemRenderEventArgs
	{
		// Token: 0x060046C7 RID: 18119 RVA: 0x00128F39 File Offset: 0x00127139
		public ToolStripSeparatorRenderEventArgs(Graphics g, ToolStripSeparator separator, bool vertical) : base(g, separator)
		{
			this.vertical = vertical;
		}

		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x060046C8 RID: 18120 RVA: 0x00128F4A File Offset: 0x0012714A
		public bool Vertical
		{
			get
			{
				return this.vertical;
			}
		}

		// Token: 0x040026BD RID: 9917
		private bool vertical;
	}
}
