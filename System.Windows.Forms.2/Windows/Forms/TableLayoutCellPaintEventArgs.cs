using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x0200039A RID: 922
	public class TableLayoutCellPaintEventArgs : PaintEventArgs
	{
		// Token: 0x06003C47 RID: 15431 RVA: 0x0010717B File Offset: 0x0010537B
		public TableLayoutCellPaintEventArgs(Graphics g, Rectangle clipRectangle, Rectangle cellBounds, int column, int row) : base(g, clipRectangle)
		{
			this.bounds = cellBounds;
			this.row = row;
			this.column = column;
		}

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x06003C48 RID: 15432 RVA: 0x0010719C File Offset: 0x0010539C
		public Rectangle CellBounds
		{
			get
			{
				return this.bounds;
			}
		}

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x06003C49 RID: 15433 RVA: 0x001071A4 File Offset: 0x001053A4
		public int Row
		{
			get
			{
				return this.row;
			}
		}

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x06003C4A RID: 15434 RVA: 0x001071AC File Offset: 0x001053AC
		public int Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x040023A1 RID: 9121
		private Rectangle bounds;

		// Token: 0x040023A2 RID: 9122
		private int row;

		// Token: 0x040023A3 RID: 9123
		private int column;
	}
}
