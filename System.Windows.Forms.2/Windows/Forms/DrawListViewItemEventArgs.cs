using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x0200023F RID: 575
	public class DrawListViewItemEventArgs : EventArgs
	{
		// Token: 0x060024D1 RID: 9425 RVA: 0x000ACFCF File Offset: 0x000AB1CF
		public DrawListViewItemEventArgs(Graphics graphics, ListViewItem item, Rectangle bounds, int itemIndex, ListViewItemStates state)
		{
			this.graphics = graphics;
			this.item = item;
			this.bounds = bounds;
			this.itemIndex = itemIndex;
			this.state = state;
			this.drawDefault = false;
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x060024D2 RID: 9426 RVA: 0x000AD003 File Offset: 0x000AB203
		// (set) Token: 0x060024D3 RID: 9427 RVA: 0x000AD00B File Offset: 0x000AB20B
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

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x060024D4 RID: 9428 RVA: 0x000AD014 File Offset: 0x000AB214
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x060024D5 RID: 9429 RVA: 0x000AD01C File Offset: 0x000AB21C
		public ListViewItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x060024D6 RID: 9430 RVA: 0x000AD024 File Offset: 0x000AB224
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x060024D7 RID: 9431 RVA: 0x000AD02C File Offset: 0x000AB22C
		public int ItemIndex
		{
			get
			{
				return this.itemIndex;
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x060024D8 RID: 9432 RVA: 0x000AD034 File Offset: 0x000AB234
		public ListViewItemStates State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x000AD03C File Offset: 0x000AB23C
		public void DrawBackground()
		{
			Brush brush = new SolidBrush(this.item.BackColor);
			this.Graphics.FillRectangle(brush, this.bounds);
			brush.Dispose();
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x000AD074 File Offset: 0x000AB274
		public void DrawFocusRectangle()
		{
			if ((this.state & ListViewItemStates.Focused) == ListViewItemStates.Focused)
			{
				Rectangle originalBounds = this.bounds;
				ControlPaint.DrawFocusRectangle(this.graphics, this.UpdateBounds(originalBounds, false), this.item.ForeColor, this.item.BackColor);
			}
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x000AD0BE File Offset: 0x000AB2BE
		public void DrawText()
		{
			this.DrawText(TextFormatFlags.Default);
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x000AD0C7 File Offset: 0x000AB2C7
		public void DrawText(TextFormatFlags flags)
		{
			TextRenderer.DrawText(this.graphics, this.item.Text, this.item.Font, this.UpdateBounds(this.bounds, true), this.item.ForeColor, flags);
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000AD104 File Offset: 0x000AB304
		private Rectangle UpdateBounds(Rectangle originalBounds, bool drawText)
		{
			Rectangle result = originalBounds;
			if (this.item.ListView.View == View.Details)
			{
				if (!this.item.ListView.FullRowSelect && this.item.SubItems.Count > 0)
				{
					ListViewItem.ListViewSubItem listViewSubItem = this.item.SubItems[0];
					Size size = TextRenderer.MeasureText(listViewSubItem.Text, listViewSubItem.Font);
					result = new Rectangle(originalBounds.X, originalBounds.Y, size.Width, size.Height);
					result.X += 4;
					int num = result.Width;
					result.Width = num + 1;
				}
				else
				{
					result.X += 4;
					result.Width -= 4;
				}
				if (drawText)
				{
					int num = result.X;
					result.X = num - 1;
				}
			}
			return result;
		}

		// Token: 0x04000F46 RID: 3910
		private readonly Graphics graphics;

		// Token: 0x04000F47 RID: 3911
		private readonly ListViewItem item;

		// Token: 0x04000F48 RID: 3912
		private readonly Rectangle bounds;

		// Token: 0x04000F49 RID: 3913
		private readonly int itemIndex;

		// Token: 0x04000F4A RID: 3914
		private readonly ListViewItemStates state;

		// Token: 0x04000F4B RID: 3915
		private bool drawDefault;
	}
}
