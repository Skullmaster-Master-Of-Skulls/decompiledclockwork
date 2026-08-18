using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200031C RID: 796
	internal class PanelDesigner : ScrollableControlDesigner
	{
		// Token: 0x06001F62 RID: 8034 RVA: 0x000BD4E4 File Offset: 0x000BB6E4
		public PanelDesigner()
		{
			base.AutoResizeHandles = true;
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x000BD4F4 File Offset: 0x000BB6F4
		protected virtual void DrawBorder(Graphics graphics)
		{
			Panel panel = (Panel)base.Component;
			if (panel == null || !panel.Visible)
			{
				return;
			}
			Pen borderPen = this.BorderPen;
			Rectangle clientRectangle = this.Control.ClientRectangle;
			int num = clientRectangle.Width;
			clientRectangle.Width = num - 1;
			num = clientRectangle.Height;
			clientRectangle.Height = num - 1;
			graphics.DrawRectangle(borderPen, clientRectangle);
			borderPen.Dispose();
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x000BD55C File Offset: 0x000BB75C
		protected override void OnPaintAdornments(PaintEventArgs pe)
		{
			Panel panel = (Panel)base.Component;
			if (panel.BorderStyle == BorderStyle.None)
			{
				this.DrawBorder(pe.Graphics);
			}
			base.OnPaintAdornments(pe);
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001F65 RID: 8037 RVA: 0x000BD590 File Offset: 0x000BB790
		protected Pen BorderPen
		{
			get
			{
				Color color = ((double)this.Control.BackColor.GetBrightness() < 0.5) ? ControlPaint.Light(this.Control.BackColor) : ControlPaint.Dark(this.Control.BackColor);
				return new Pen(color)
				{
					DashStyle = DashStyle.Dash
				};
			}
		}
	}
}
