using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x0200023A RID: 570
	public class DrawItemEventArgs : EventArgs
	{
		// Token: 0x060024B0 RID: 9392 RVA: 0x000ACBCC File Offset: 0x000AADCC
		public DrawItemEventArgs(Graphics graphics, Font font, Rectangle rect, int index, DrawItemState state)
		{
			this.graphics = graphics;
			this.font = font;
			this.rect = rect;
			this.index = index;
			this.state = state;
			this.foreColor = SystemColors.WindowText;
			this.backColor = SystemColors.Window;
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000ACC1A File Offset: 0x000AAE1A
		public DrawItemEventArgs(Graphics graphics, Font font, Rectangle rect, int index, DrawItemState state, Color foreColor, Color backColor)
		{
			this.graphics = graphics;
			this.font = font;
			this.rect = rect;
			this.index = index;
			this.state = state;
			this.foreColor = foreColor;
			this.backColor = backColor;
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x060024B2 RID: 9394 RVA: 0x000ACC57 File Offset: 0x000AAE57
		public Color BackColor
		{
			get
			{
				if ((this.state & DrawItemState.Selected) == DrawItemState.Selected)
				{
					return SystemColors.Highlight;
				}
				return this.backColor;
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x060024B3 RID: 9395 RVA: 0x000ACC70 File Offset: 0x000AAE70
		public Rectangle Bounds
		{
			get
			{
				return this.rect;
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x060024B4 RID: 9396 RVA: 0x000ACC78 File Offset: 0x000AAE78
		public Font Font
		{
			get
			{
				return this.font;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x060024B5 RID: 9397 RVA: 0x000ACC80 File Offset: 0x000AAE80
		public Color ForeColor
		{
			get
			{
				if ((this.state & DrawItemState.Selected) == DrawItemState.Selected)
				{
					return SystemColors.HighlightText;
				}
				return this.foreColor;
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x060024B6 RID: 9398 RVA: 0x000ACC99 File Offset: 0x000AAE99
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x060024B7 RID: 9399 RVA: 0x000ACCA1 File Offset: 0x000AAEA1
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x060024B8 RID: 9400 RVA: 0x000ACCA9 File Offset: 0x000AAEA9
		public DrawItemState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x000ACCB4 File Offset: 0x000AAEB4
		public virtual void DrawBackground()
		{
			Brush brush = new SolidBrush(this.BackColor);
			this.Graphics.FillRectangle(brush, this.rect);
			brush.Dispose();
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x000ACCE5 File Offset: 0x000AAEE5
		public virtual void DrawFocusRectangle()
		{
			if ((this.state & DrawItemState.Focus) == DrawItemState.Focus && (this.state & DrawItemState.NoFocusRect) != DrawItemState.NoFocusRect)
			{
				ControlPaint.DrawFocusRectangle(this.Graphics, this.rect, this.ForeColor, this.BackColor);
			}
		}

		// Token: 0x04000F29 RID: 3881
		private Color backColor;

		// Token: 0x04000F2A RID: 3882
		private Color foreColor;

		// Token: 0x04000F2B RID: 3883
		private Font font;

		// Token: 0x04000F2C RID: 3884
		private readonly Graphics graphics;

		// Token: 0x04000F2D RID: 3885
		private readonly int index;

		// Token: 0x04000F2E RID: 3886
		private readonly Rectangle rect;

		// Token: 0x04000F2F RID: 3887
		private readonly DrawItemState state;
	}
}
