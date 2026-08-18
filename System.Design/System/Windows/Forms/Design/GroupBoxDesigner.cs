using System;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000248 RID: 584
	internal class GroupBoxDesigner : ParentControlDesigner
	{
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x00074600 File Offset: 0x00073600
		protected override Point DefaultControlLocation
		{
			get
			{
				GroupBox groupBox = (GroupBox)this.Control;
				return new Point(groupBox.DisplayRectangle.X, groupBox.DisplayRectangle.Y);
			}
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x0007463C File Offset: 0x0007363C
		protected override void OnPaintAdornments(PaintEventArgs pe)
		{
			if (this.DrawGrid)
			{
				Control control = this.Control;
				Rectangle displayRectangle = this.Control.DisplayRectangle;
				displayRectangle.Width++;
				displayRectangle.Height++;
				ControlPaint.DrawGrid(pe.Graphics, displayRectangle, base.GridSize, control.BackColor);
			}
			if (base.Inherited)
			{
				if (this.inheritanceUI == null)
				{
					this.inheritanceUI = (InheritanceUI)this.GetService(typeof(InheritanceUI));
				}
				if (this.inheritanceUI != null)
				{
					pe.Graphics.DrawImage(this.inheritanceUI.InheritanceGlyph, 0, 0);
				}
			}
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x000746E8 File Offset: 0x000736E8
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg == 132)
			{
				base.WndProc(ref m);
				if ((int)m.Result == -1)
				{
					m.Result = (IntPtr)1;
					return;
				}
			}
			else
			{
				base.WndProc(ref m);
			}
		}

		// Token: 0x040012E8 RID: 4840
		private InheritanceUI inheritanceUI;
	}
}
