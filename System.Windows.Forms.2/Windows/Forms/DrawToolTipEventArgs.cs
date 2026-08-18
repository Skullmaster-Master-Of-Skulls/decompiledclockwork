using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000244 RID: 580
	public class DrawToolTipEventArgs : EventArgs
	{
		// Token: 0x060024F5 RID: 9461 RVA: 0x000AD410 File Offset: 0x000AB610
		public DrawToolTipEventArgs(Graphics graphics, IWin32Window associatedWindow, Control associatedControl, Rectangle bounds, string toolTipText, Color backColor, Color foreColor, Font font)
		{
			this.graphics = graphics;
			this.associatedWindow = associatedWindow;
			this.associatedControl = associatedControl;
			this.bounds = bounds;
			this.toolTipText = toolTipText;
			this.backColor = backColor;
			this.foreColor = foreColor;
			this.font = font;
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x060024F6 RID: 9462 RVA: 0x000AD460 File Offset: 0x000AB660
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x060024F7 RID: 9463 RVA: 0x000AD468 File Offset: 0x000AB668
		public IWin32Window AssociatedWindow
		{
			get
			{
				return this.associatedWindow;
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x060024F8 RID: 9464 RVA: 0x000AD470 File Offset: 0x000AB670
		public Control AssociatedControl
		{
			get
			{
				return this.associatedControl;
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x060024F9 RID: 9465 RVA: 0x000AD478 File Offset: 0x000AB678
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x060024FA RID: 9466 RVA: 0x000AD480 File Offset: 0x000AB680
		public string ToolTipText
		{
			get
			{
				return this.toolTipText;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x060024FB RID: 9467 RVA: 0x000AD488 File Offset: 0x000AB688
		public Font Font
		{
			get
			{
				return this.font;
			}
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x000AD490 File Offset: 0x000AB690
		public void DrawBackground()
		{
			Brush brush = new SolidBrush(this.backColor);
			this.Graphics.FillRectangle(brush, this.bounds);
			brush.Dispose();
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x000AD4C1 File Offset: 0x000AB6C1
		public void DrawText()
		{
			this.DrawText(TextFormatFlags.HidePrefix | TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter);
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x000AD4CE File Offset: 0x000AB6CE
		public void DrawText(TextFormatFlags flags)
		{
			TextRenderer.DrawText(this.graphics, this.toolTipText, this.font, this.bounds, this.foreColor, flags);
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x000AD4F4 File Offset: 0x000AB6F4
		public void DrawBorder()
		{
			ControlPaint.DrawBorder(this.graphics, this.bounds, SystemColors.WindowFrame, ButtonBorderStyle.Solid);
		}

		// Token: 0x04000F59 RID: 3929
		private readonly Graphics graphics;

		// Token: 0x04000F5A RID: 3930
		private readonly IWin32Window associatedWindow;

		// Token: 0x04000F5B RID: 3931
		private readonly Control associatedControl;

		// Token: 0x04000F5C RID: 3932
		private readonly Rectangle bounds;

		// Token: 0x04000F5D RID: 3933
		private readonly string toolTipText;

		// Token: 0x04000F5E RID: 3934
		private readonly Color backColor;

		// Token: 0x04000F5F RID: 3935
		private readonly Color foreColor;

		// Token: 0x04000F60 RID: 3936
		private readonly Font font;
	}
}
