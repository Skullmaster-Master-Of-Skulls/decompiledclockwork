using System;
using System.Drawing;
using System.Windows.Forms.ButtonInternal;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x02000500 RID: 1280
	internal class DropDownButtonAdapter : ButtonStandardAdapter
	{
		// Token: 0x060053CA RID: 21450 RVA: 0x0015EF29 File Offset: 0x0015D129
		internal DropDownButtonAdapter(ButtonBase control) : base(control)
		{
		}

		// Token: 0x060053CB RID: 21451 RVA: 0x0015EF34 File Offset: 0x0015D134
		private void DDB_Draw3DBorder(Graphics g, Rectangle r, bool raised)
		{
			if (base.Control.BackColor != SystemColors.Control && SystemInformation.HighContrast)
			{
				if (raised)
				{
					Color color = ControlPaint.LightLight(base.Control.BackColor);
					ControlPaint.DrawBorder(g, r, color, 1, ButtonBorderStyle.Outset, color, 1, ButtonBorderStyle.Outset, color, 2, ButtonBorderStyle.Inset, color, 2, ButtonBorderStyle.Inset);
					return;
				}
				ControlPaint.DrawBorder(g, r, ControlPaint.Dark(base.Control.BackColor), ButtonBorderStyle.Solid);
				return;
			}
			else
			{
				if (raised)
				{
					Color color2 = ControlPaint.Light(base.Control.BackColor);
					ControlPaint.DrawBorder(g, r, color2, 1, ButtonBorderStyle.Solid, color2, 1, ButtonBorderStyle.Solid, base.Control.BackColor, 2, ButtonBorderStyle.Outset, base.Control.BackColor, 2, ButtonBorderStyle.Outset);
					Rectangle bounds = r;
					bounds.Offset(1, 1);
					bounds.Width -= 3;
					bounds.Height -= 3;
					color2 = ControlPaint.LightLight(base.Control.BackColor);
					ControlPaint.DrawBorder(g, bounds, color2, 1, ButtonBorderStyle.Solid, color2, 1, ButtonBorderStyle.Solid, color2, 1, ButtonBorderStyle.None, color2, 1, ButtonBorderStyle.None);
					return;
				}
				ControlPaint.DrawBorder(g, r, ControlPaint.Dark(base.Control.BackColor), ButtonBorderStyle.Solid);
				return;
			}
		}

		// Token: 0x060053CC RID: 21452 RVA: 0x0015F044 File Offset: 0x0015D244
		internal override void PaintUp(PaintEventArgs pevent, CheckState state)
		{
			base.PaintUp(pevent, state);
			if (!Application.RenderWithVisualStyles)
			{
				this.DDB_Draw3DBorder(pevent.Graphics, base.Control.ClientRectangle, true);
				return;
			}
			Color window = SystemColors.Window;
			Rectangle clientRectangle = base.Control.ClientRectangle;
			clientRectangle.Inflate(0, -1);
			ControlPaint.DrawBorder(pevent.Graphics, clientRectangle, window, 1, ButtonBorderStyle.None, window, 1, ButtonBorderStyle.None, window, 1, ButtonBorderStyle.Solid, window, 1, ButtonBorderStyle.None);
		}

		// Token: 0x060053CD RID: 21453 RVA: 0x0015F0AC File Offset: 0x0015D2AC
		internal override void DrawImageCore(Graphics graphics, Image image, Rectangle imageBounds, Point imageStart, ButtonBaseAdapter.LayoutData layout)
		{
			if (AccessibilityImprovements.Level3 && base.IsFilledWithHighlightColor && (base.Control.MouseIsOver || base.Control.Focused))
			{
				ControlPaint.DrawImageReplaceColor(graphics, image, imageBounds, Color.Black, SystemColors.HighlightText);
				return;
			}
			ControlPaint.DrawImageReplaceColor(graphics, image, imageBounds, Color.Black, base.Control.ForeColor);
		}
	}
}
