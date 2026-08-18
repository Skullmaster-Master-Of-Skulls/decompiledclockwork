using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200031F RID: 799
	internal class PictureBoxDesigner : ControlDesigner
	{
		// Token: 0x06001FC0 RID: 8128 RVA: 0x00093E53 File Offset: 0x00092053
		public PictureBoxDesigner()
		{
			base.AutoResizeHandles = true;
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x000C0B70 File Offset: 0x000BED70
		private void DrawBorder(Graphics graphics)
		{
			Control control = this.Control;
			Rectangle clientRectangle = control.ClientRectangle;
			Color color;
			if ((double)control.BackColor.GetBrightness() < 0.5)
			{
				color = ControlPaint.Light(control.BackColor);
			}
			else
			{
				color = ControlPaint.Dark(control.BackColor);
			}
			Pen pen = new Pen(color);
			pen.DashStyle = DashStyle.Dash;
			int num = clientRectangle.Width;
			clientRectangle.Width = num - 1;
			num = clientRectangle.Height;
			clientRectangle.Height = num - 1;
			graphics.DrawRectangle(pen, clientRectangle);
			pen.Dispose();
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x000C0C04 File Offset: 0x000BEE04
		protected override void OnPaintAdornments(PaintEventArgs pe)
		{
			PictureBox pictureBox = (PictureBox)base.Component;
			if (pictureBox.BorderStyle == BorderStyle.None)
			{
				this.DrawBorder(pe.Graphics);
			}
			base.OnPaintAdornments(pe);
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001FC3 RID: 8131 RVA: 0x000C0C38 File Offset: 0x000BEE38
		public override SelectionRules SelectionRules
		{
			get
			{
				SelectionRules selectionRules = base.SelectionRules;
				object component = base.Component;
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["SizeMode"];
				if (propertyDescriptor != null)
				{
					PictureBoxSizeMode pictureBoxSizeMode = (PictureBoxSizeMode)propertyDescriptor.GetValue(component);
					if (pictureBoxSizeMode == PictureBoxSizeMode.AutoSize)
					{
						selectionRules &= ~(SelectionRules.TopSizeable | SelectionRules.BottomSizeable | SelectionRules.LeftSizeable | SelectionRules.RightSizeable);
					}
				}
				return selectionRules;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001FC4 RID: 8132 RVA: 0x000C0C83 File Offset: 0x000BEE83
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				if (this._actionLists == null)
				{
					this._actionLists = new DesignerActionListCollection();
					this._actionLists.Add(new PictureBoxActionList(this));
				}
				return this._actionLists;
			}
		}

		// Token: 0x04001890 RID: 6288
		private DesignerActionListCollection _actionLists;
	}
}
