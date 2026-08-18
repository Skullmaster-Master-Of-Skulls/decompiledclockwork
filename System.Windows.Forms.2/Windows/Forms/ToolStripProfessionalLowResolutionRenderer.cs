using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms
{
	// Token: 0x020003F7 RID: 1015
	internal class ToolStripProfessionalLowResolutionRenderer : ToolStripProfessionalRenderer
	{
		// Token: 0x17001128 RID: 4392
		// (get) Token: 0x060045C9 RID: 17865 RVA: 0x00015ECC File Offset: 0x000140CC
		internal override ToolStripRenderer RendererOverride
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060045CA RID: 17866 RVA: 0x00126BEB File Offset: 0x00124DEB
		protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{
			if (e.ToolStrip is ToolStripDropDown)
			{
				base.OnRenderToolStripBackground(e);
			}
		}

		// Token: 0x060045CB RID: 17867 RVA: 0x00126C01 File Offset: 0x00124E01
		protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
			if (e.ToolStrip is MenuStrip)
			{
				return;
			}
			if (e.ToolStrip is StatusStrip)
			{
				return;
			}
			if (e.ToolStrip is ToolStripDropDown)
			{
				base.OnRenderToolStripBorder(e);
				return;
			}
			this.RenderToolStripBorderInternal(e);
		}

		// Token: 0x060045CC RID: 17868 RVA: 0x00126C3C File Offset: 0x00124E3C
		private void RenderToolStripBorderInternal(ToolStripRenderEventArgs e)
		{
			Rectangle rectangle = new Rectangle(Point.Empty, e.ToolStrip.Size);
			Graphics graphics = e.Graphics;
			using (Pen pen = new Pen(SystemColors.ButtonShadow))
			{
				pen.DashStyle = DashStyle.Dot;
				bool flag = (rectangle.Width & 1) == 1;
				bool flag2 = (rectangle.Height & 1) == 1;
				int num = 2;
				graphics.DrawLine(pen, rectangle.X + num, rectangle.Y, rectangle.Width - 1, rectangle.Y);
				graphics.DrawLine(pen, rectangle.X + num, rectangle.Height - 1, rectangle.Width - 1, rectangle.Height - 1);
				graphics.DrawLine(pen, rectangle.X, rectangle.Y + num, rectangle.X, rectangle.Height - 1);
				graphics.DrawLine(pen, rectangle.Width - 1, rectangle.Y + num, rectangle.Width - 1, rectangle.Height - 1);
				graphics.FillRectangle(SystemBrushes.ButtonShadow, new Rectangle(1, 1, 1, 1));
				if (flag)
				{
					graphics.FillRectangle(SystemBrushes.ButtonShadow, new Rectangle(rectangle.Width - 2, 1, 1, 1));
				}
				if (flag2)
				{
					graphics.FillRectangle(SystemBrushes.ButtonShadow, new Rectangle(1, rectangle.Height - 2, 1, 1));
				}
				if (flag2 && flag)
				{
					graphics.FillRectangle(SystemBrushes.ButtonShadow, new Rectangle(rectangle.Width - 2, rectangle.Height - 2, 1, 1));
				}
			}
		}
	}
}
