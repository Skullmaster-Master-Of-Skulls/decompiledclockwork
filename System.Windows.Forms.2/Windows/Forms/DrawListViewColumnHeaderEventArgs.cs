using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x0200023D RID: 573
	public class DrawListViewColumnHeaderEventArgs : EventArgs
	{
		// Token: 0x060024BF RID: 9407 RVA: 0x000ACD24 File Offset: 0x000AAF24
		public DrawListViewColumnHeaderEventArgs(Graphics graphics, Rectangle bounds, int columnIndex, ColumnHeader header, ListViewItemStates state, Color foreColor, Color backColor, Font font)
		{
			this.graphics = graphics;
			this.bounds = bounds;
			this.columnIndex = columnIndex;
			this.header = header;
			this.state = state;
			this.foreColor = foreColor;
			this.backColor = backColor;
			this.font = font;
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x060024C0 RID: 9408 RVA: 0x000ACD74 File Offset: 0x000AAF74
		// (set) Token: 0x060024C1 RID: 9409 RVA: 0x000ACD7C File Offset: 0x000AAF7C
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

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x060024C2 RID: 9410 RVA: 0x000ACD85 File Offset: 0x000AAF85
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x060024C3 RID: 9411 RVA: 0x000ACD8D File Offset: 0x000AAF8D
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x060024C4 RID: 9412 RVA: 0x000ACD95 File Offset: 0x000AAF95
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x060024C5 RID: 9413 RVA: 0x000ACD9D File Offset: 0x000AAF9D
		public ColumnHeader Header
		{
			get
			{
				return this.header;
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x060024C6 RID: 9414 RVA: 0x000ACDA5 File Offset: 0x000AAFA5
		public ListViewItemStates State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x060024C7 RID: 9415 RVA: 0x000ACDAD File Offset: 0x000AAFAD
		public Color ForeColor
		{
			get
			{
				return this.foreColor;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x060024C8 RID: 9416 RVA: 0x000ACDB5 File Offset: 0x000AAFB5
		public Color BackColor
		{
			get
			{
				return this.backColor;
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x060024C9 RID: 9417 RVA: 0x000ACDBD File Offset: 0x000AAFBD
		public Font Font
		{
			get
			{
				return this.font;
			}
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x000ACDC8 File Offset: 0x000AAFC8
		public void DrawBackground()
		{
			if (Application.RenderWithVisualStyles)
			{
				VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Header.Item.Normal);
				visualStyleRenderer.DrawBackground(this.graphics, this.bounds);
				return;
			}
			using (Brush brush = new SolidBrush(this.backColor))
			{
				this.graphics.FillRectangle(brush, this.bounds);
			}
			Rectangle rect = this.bounds;
			rect.Width--;
			rect.Height--;
			this.graphics.DrawRectangle(SystemPens.ControlDarkDark, rect);
			rect.Width--;
			rect.Height--;
			this.graphics.DrawLine(SystemPens.ControlLightLight, rect.X, rect.Y, rect.Right, rect.Y);
			this.graphics.DrawLine(SystemPens.ControlLightLight, rect.X, rect.Y, rect.X, rect.Bottom);
			this.graphics.DrawLine(SystemPens.ControlDark, rect.X + 1, rect.Bottom, rect.Right, rect.Bottom);
			this.graphics.DrawLine(SystemPens.ControlDark, rect.Right, rect.Y + 1, rect.Right, rect.Bottom);
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x000ACF3C File Offset: 0x000AB13C
		public void DrawText()
		{
			HorizontalAlignment textAlign = this.header.TextAlign;
			TextFormatFlags textFormatFlags = (textAlign == HorizontalAlignment.Left) ? TextFormatFlags.Default : ((textAlign == HorizontalAlignment.Center) ? TextFormatFlags.HorizontalCenter : TextFormatFlags.Right);
			textFormatFlags |= TextFormatFlags.WordEllipsis;
			this.DrawText(textFormatFlags);
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x000ACF74 File Offset: 0x000AB174
		public void DrawText(TextFormatFlags flags)
		{
			string text = this.header.Text;
			int width = TextRenderer.MeasureText(" ", this.font).Width;
			Rectangle rectangle = Rectangle.Inflate(this.bounds, -width, 0);
			TextRenderer.DrawText(this.graphics, text, this.font, rectangle, this.foreColor, flags);
		}

		// Token: 0x04000F3D RID: 3901
		private readonly Graphics graphics;

		// Token: 0x04000F3E RID: 3902
		private readonly Rectangle bounds;

		// Token: 0x04000F3F RID: 3903
		private readonly int columnIndex;

		// Token: 0x04000F40 RID: 3904
		private readonly ColumnHeader header;

		// Token: 0x04000F41 RID: 3905
		private readonly ListViewItemStates state;

		// Token: 0x04000F42 RID: 3906
		private readonly Color foreColor;

		// Token: 0x04000F43 RID: 3907
		private readonly Color backColor;

		// Token: 0x04000F44 RID: 3908
		private readonly Font font;

		// Token: 0x04000F45 RID: 3909
		private bool drawDefault;
	}
}
