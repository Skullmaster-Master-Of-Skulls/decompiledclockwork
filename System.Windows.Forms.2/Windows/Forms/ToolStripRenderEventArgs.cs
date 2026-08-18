using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x020003FB RID: 1019
	public class ToolStripRenderEventArgs : EventArgs
	{
		// Token: 0x06004673 RID: 18035 RVA: 0x00128804 File Offset: 0x00126A04
		public ToolStripRenderEventArgs(Graphics g, ToolStrip toolStrip)
		{
			this.toolStrip = toolStrip;
			this.graphics = g;
			this.affectedBounds = new Rectangle(Point.Empty, toolStrip.Size);
		}

		// Token: 0x06004674 RID: 18036 RVA: 0x00128851 File Offset: 0x00126A51
		public ToolStripRenderEventArgs(Graphics g, ToolStrip toolStrip, Rectangle affectedBounds, Color backColor)
		{
			this.toolStrip = toolStrip;
			this.affectedBounds = affectedBounds;
			this.graphics = g;
			this.backColor = backColor;
		}

		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x06004675 RID: 18037 RVA: 0x0012888C File Offset: 0x00126A8C
		public Rectangle AffectedBounds
		{
			get
			{
				return this.affectedBounds;
			}
		}

		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x06004676 RID: 18038 RVA: 0x00128894 File Offset: 0x00126A94
		public Color BackColor
		{
			get
			{
				if (this.backColor == Color.Empty)
				{
					this.backColor = this.toolStrip.RawBackColor;
					if (this.backColor == Color.Empty)
					{
						if (this.toolStrip is ToolStripDropDown)
						{
							this.backColor = SystemColors.Menu;
						}
						else if (this.toolStrip is MenuStrip)
						{
							this.backColor = SystemColors.MenuBar;
						}
						else
						{
							this.backColor = SystemColors.Control;
						}
					}
				}
				return this.backColor;
			}
		}

		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x06004677 RID: 18039 RVA: 0x0012891B File Offset: 0x00126B1B
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x06004678 RID: 18040 RVA: 0x00128923 File Offset: 0x00126B23
		public ToolStrip ToolStrip
		{
			get
			{
				return this.toolStrip;
			}
		}

		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x06004679 RID: 18041 RVA: 0x0012892C File Offset: 0x00126B2C
		public Rectangle ConnectedArea
		{
			get
			{
				ToolStripDropDown toolStripDropDown = this.toolStrip as ToolStripDropDown;
				if (toolStripDropDown != null)
				{
					ToolStripDropDownItem toolStripDropDownItem = toolStripDropDown.OwnerItem as ToolStripDropDownItem;
					if (toolStripDropDownItem is MdiControlStrip.SystemMenuItem)
					{
						return Rectangle.Empty;
					}
					if (toolStripDropDownItem != null && toolStripDropDownItem.ParentInternal != null && !toolStripDropDownItem.IsOnDropDown)
					{
						Rectangle rect = new Rectangle(this.toolStrip.PointToClient(toolStripDropDownItem.TranslatePoint(Point.Empty, ToolStripPointType.ToolStripItemCoords, ToolStripPointType.ScreenCoords)), toolStripDropDownItem.Size);
						Rectangle bounds = this.ToolStrip.Bounds;
						Rectangle clientRectangle = this.ToolStrip.ClientRectangle;
						clientRectangle.Inflate(1, 1);
						if (clientRectangle.IntersectsWith(rect))
						{
							switch (toolStripDropDownItem.DropDownDirection)
							{
							case ToolStripDropDownDirection.AboveLeft:
							case ToolStripDropDownDirection.AboveRight:
								return Rectangle.Empty;
							case ToolStripDropDownDirection.BelowLeft:
							case ToolStripDropDownDirection.BelowRight:
								clientRectangle.Intersect(rect);
								if (clientRectangle.Height == 2)
								{
									return new Rectangle(rect.X + 1, 0, rect.Width - 2, 2);
								}
								return Rectangle.Empty;
							case ToolStripDropDownDirection.Left:
							case ToolStripDropDownDirection.Right:
								return Rectangle.Empty;
							}
						}
					}
				}
				return Rectangle.Empty;
			}
		}

		// Token: 0x040026AC RID: 9900
		private ToolStrip toolStrip;

		// Token: 0x040026AD RID: 9901
		private Graphics graphics;

		// Token: 0x040026AE RID: 9902
		private Rectangle affectedBounds = Rectangle.Empty;

		// Token: 0x040026AF RID: 9903
		private Color backColor = Color.Empty;
	}
}
