using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000331 RID: 817
	internal class SplitterDesigner : ControlDesigner
	{
		// Token: 0x0600204F RID: 8271 RVA: 0x00093E53 File Offset: 0x00092053
		public SplitterDesigner()
		{
			base.AutoResizeHandles = true;
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x000C3FA4 File Offset: 0x000C21A4
		private void DrawBorder(Graphics graphics)
		{
			Control control = this.Control;
			Rectangle clientRectangle = control.ClientRectangle;
			Color color;
			if ((double)control.BackColor.GetBrightness() < 0.5)
			{
				color = Color.White;
			}
			else
			{
				color = Color.Black;
			}
			using (Pen pen = new Pen(color))
			{
				pen.DashStyle = DashStyle.Dash;
				int num = clientRectangle.Width;
				clientRectangle.Width = num - 1;
				num = clientRectangle.Height;
				clientRectangle.Height = num - 1;
				graphics.DrawRectangle(pen, clientRectangle);
			}
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x000C4044 File Offset: 0x000C2244
		protected override void OnPaintAdornments(PaintEventArgs pe)
		{
			Splitter splitter = (Splitter)base.Component;
			base.OnPaintAdornments(pe);
			if (splitter.BorderStyle == BorderStyle.None)
			{
				this.DrawBorder(pe.Graphics);
			}
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x000C4078 File Offset: 0x000C2278
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg == 71)
			{
				Control control = this.Control;
				control.Invalidate();
			}
			base.WndProc(ref m);
		}
	}
}
