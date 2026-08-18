using System;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002E9 RID: 745
	internal class GroupBoxDesigner : ParentControlDesigner
	{
		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001DFA RID: 7674 RVA: 0x000B62F4 File Offset: 0x000B44F4
		protected override Point DefaultControlLocation
		{
			get
			{
				GroupBox groupBox = (GroupBox)this.Control;
				return new Point(groupBox.DisplayRectangle.X, groupBox.DisplayRectangle.Y);
			}
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x000B6330 File Offset: 0x000B4530
		protected override void OnPaintAdornments(PaintEventArgs pe)
		{
			if (this.DrawGrid)
			{
				Control control = this.Control;
				Rectangle displayRectangle = this.Control.DisplayRectangle;
				int num = displayRectangle.Width;
				displayRectangle.Width = num + 1;
				num = displayRectangle.Height;
				displayRectangle.Height = num + 1;
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

		// Token: 0x06001DFC RID: 7676 RVA: 0x000B63E0 File Offset: 0x000B45E0
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg == 132)
			{
				base.WndProc(ref m);
				if ((int)((long)m.Result) == -1)
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

		// Token: 0x040017B3 RID: 6067
		private InheritanceUI inheritanceUI;
	}
}
