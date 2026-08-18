using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000241 RID: 577
	public class DrawListViewSubItemEventArgs : EventArgs
	{
		// Token: 0x060024E2 RID: 9442 RVA: 0x000AD1EC File Offset: 0x000AB3EC
		public DrawListViewSubItemEventArgs(Graphics graphics, Rectangle bounds, ListViewItem item, ListViewItem.ListViewSubItem subItem, int itemIndex, int columnIndex, ColumnHeader header, ListViewItemStates itemState)
		{
			this.graphics = graphics;
			this.bounds = bounds;
			this.item = item;
			this.subItem = subItem;
			this.itemIndex = itemIndex;
			this.columnIndex = columnIndex;
			this.header = header;
			this.itemState = itemState;
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x060024E3 RID: 9443 RVA: 0x000AD23C File Offset: 0x000AB43C
		// (set) Token: 0x060024E4 RID: 9444 RVA: 0x000AD244 File Offset: 0x000AB444
		public bool DrawDefault
		{
			get
			{
				return this.drawDefault;
			}
			set
			{
				this.drawDefault = value;
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x060024E5 RID: 9445 RVA: 0x000AD24D File Offset: 0x000AB44D
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x060024E6 RID: 9446 RVA: 0x000AD255 File Offset: 0x000AB455
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x060024E7 RID: 9447 RVA: 0x000AD25D File Offset: 0x000AB45D
		public ListViewItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x060024E8 RID: 9448 RVA: 0x000AD265 File Offset: 0x000AB465
		public ListViewItem.ListViewSubItem SubItem
		{
			get
			{
				return this.subItem;
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x060024E9 RID: 9449 RVA: 0x000AD26D File Offset: 0x000AB46D
		public int ItemIndex
		{
			get
			{
				return this.itemIndex;
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x060024EA RID: 9450 RVA: 0x000AD275 File Offset: 0x000AB475
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x060024EB RID: 9451 RVA: 0x000AD27D File Offset: 0x000AB47D
		public ColumnHeader Header
		{
			get
			{
				return this.header;
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x060024EC RID: 9452 RVA: 0x000AD285 File Offset: 0x000AB485
		public ListViewItemStates ItemState
		{
			get
			{
				return this.itemState;
			}
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x000AD290 File Offset: 0x000AB490
		public void DrawBackground()
		{
			Color color = (this.itemIndex == -1) ? this.item.BackColor : this.subItem.BackColor;
			using (Brush brush = new SolidBrush(color))
			{
				this.Graphics.FillRectangle(brush, this.bounds);
			}
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x000AD2F4 File Offset: 0x000AB4F4
		public void DrawFocusRectangle(Rectangle bounds)
		{
			if ((this.itemState & ListViewItemStates.Focused) == ListViewItemStates.Focused)
			{
				ControlPaint.DrawFocusRectangle(this.graphics, Rectangle.Inflate(bounds, -1, -1), this.item.ForeColor, this.item.BackColor);
			}
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x000AD32C File Offset: 0x000AB52C
		public void DrawText()
		{
			HorizontalAlignment textAlign = this.header.TextAlign;
			TextFormatFlags textFormatFlags = (textAlign == HorizontalAlignment.Left) ? TextFormatFlags.Default : ((textAlign == HorizontalAlignment.Center) ? TextFormatFlags.HorizontalCenter : TextFormatFlags.Right);
			textFormatFlags |= TextFormatFlags.WordEllipsis;
			this.DrawText(textFormatFlags);
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x000AD364 File Offset: 0x000AB564
		public void DrawText(TextFormatFlags flags)
		{
			string text = (this.itemIndex == -1) ? this.item.Text : this.subItem.Text;
			Font font = (this.itemIndex == -1) ? this.item.Font : this.subItem.Font;
			Color foreColor = (this.itemIndex == -1) ? this.item.ForeColor : this.subItem.ForeColor;
			int width = TextRenderer.MeasureText(" ", font).Width;
			Rectangle rectangle = Rectangle.Inflate(this.bounds, -width, 0);
			TextRenderer.DrawText(this.graphics, text, font, rectangle, foreColor, flags);
		}

		// Token: 0x04000F4C RID: 3916
		private readonly Graphics graphics;

		// Token: 0x04000F4D RID: 3917
		private readonly Rectangle bounds;

		// Token: 0x04000F4E RID: 3918
		private readonly ListViewItem item;

		// Token: 0x04000F4F RID: 3919
		private readonly ListViewItem.ListViewSubItem subItem;

		// Token: 0x04000F50 RID: 3920
		private readonly int itemIndex;

		// Token: 0x04000F51 RID: 3921
		private readonly int columnIndex;

		// Token: 0x04000F52 RID: 3922
		private readonly ColumnHeader header;

		// Token: 0x04000F53 RID: 3923
		private readonly ListViewItemStates itemState;

		// Token: 0x04000F54 RID: 3924
		private bool drawDefault;
	}
}
