using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000376 RID: 886
	public class StatusBarDrawItemEventArgs : DrawItemEventArgs
	{
		// Token: 0x06003A28 RID: 14888 RVA: 0x00100E8F File Offset: 0x000FF08F
		public StatusBarDrawItemEventArgs(Graphics g, Font font, Rectangle r, int itemId, DrawItemState itemState, StatusBarPanel panel) : base(g, font, r, itemId, itemState)
		{
			this.panel = panel;
		}

		// Token: 0x06003A29 RID: 14889 RVA: 0x00100EA6 File Offset: 0x000FF0A6
		public StatusBarDrawItemEventArgs(Graphics g, Font font, Rectangle r, int itemId, DrawItemState itemState, StatusBarPanel panel, Color foreColor, Color backColor) : base(g, font, r, itemId, itemState, foreColor, backColor)
		{
			this.panel = panel;
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x06003A2A RID: 14890 RVA: 0x00100EC1 File Offset: 0x000FF0C1
		public StatusBarPanel Panel
		{
			get
			{
				return this.panel;
			}
		}

		// Token: 0x040022EC RID: 8940
		private readonly StatusBarPanel panel;
	}
}
