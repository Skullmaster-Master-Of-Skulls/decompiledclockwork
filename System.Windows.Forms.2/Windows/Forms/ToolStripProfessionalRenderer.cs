using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x020003F6 RID: 1014
	public class ToolStripProfessionalRenderer : ToolStripRenderer
	{
		// Token: 0x06004594 RID: 17812 RVA: 0x00124034 File Offset: 0x00122234
		public ToolStripProfessionalRenderer()
		{
		}

		// Token: 0x06004595 RID: 17813 RVA: 0x0012408C File Offset: 0x0012228C
		internal ToolStripProfessionalRenderer(bool isDefault) : base(isDefault)
		{
		}

		// Token: 0x06004596 RID: 17814 RVA: 0x001240E4 File Offset: 0x001222E4
		public ToolStripProfessionalRenderer(ProfessionalColorTable professionalColorTable)
		{
			this.professionalColorTable = professionalColorTable;
		}

		// Token: 0x17001122 RID: 4386
		// (get) Token: 0x06004597 RID: 17815 RVA: 0x00124141 File Offset: 0x00122341
		public ProfessionalColorTable ColorTable
		{
			get
			{
				if (this.professionalColorTable == null)
				{
					return ProfessionalColors.ColorTable;
				}
				return this.professionalColorTable;
			}
		}

		// Token: 0x17001123 RID: 4387
		// (get) Token: 0x06004598 RID: 17816 RVA: 0x00124157 File Offset: 0x00122357
		internal override ToolStripRenderer RendererOverride
		{
			get
			{
				if (DisplayInformation.HighContrast)
				{
					return this.HighContrastRenderer;
				}
				if (DisplayInformation.LowResolution)
				{
					return this.LowResolutionRenderer;
				}
				return null;
			}
		}

		// Token: 0x17001124 RID: 4388
		// (get) Token: 0x06004599 RID: 17817 RVA: 0x00124176 File Offset: 0x00122376
		internal ToolStripRenderer HighContrastRenderer
		{
			get
			{
				if (this.toolStripHighContrastRenderer == null)
				{
					this.toolStripHighContrastRenderer = new ToolStripHighContrastRenderer(false);
				}
				return this.toolStripHighContrastRenderer;
			}
		}

		// Token: 0x17001125 RID: 4389
		// (get) Token: 0x0600459A RID: 17818 RVA: 0x00124192 File Offset: 0x00122392
		internal ToolStripRenderer LowResolutionRenderer
		{
			get
			{
				if (this.toolStripLowResolutionRenderer == null)
				{
					this.toolStripLowResolutionRenderer = new ToolStripProfessionalLowResolutionRenderer();
				}
				return this.toolStripLowResolutionRenderer;
			}
		}

		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x0600459B RID: 17819 RVA: 0x001241AD File Offset: 0x001223AD
		// (set) Token: 0x0600459C RID: 17820 RVA: 0x001241B5 File Offset: 0x001223B5
		public bool RoundedEdges
		{
			get
			{
				return this.roundedEdges;
			}
			set
			{
				this.roundedEdges = value;
			}
		}

		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x0600459D RID: 17821 RVA: 0x001241BE File Offset: 0x001223BE
		private bool UseSystemColors
		{
			get
			{
				return this.ColorTable.UseSystemColors || !ToolStripManager.VisualStylesEnabled;
			}
		}

		// Token: 0x0600459E RID: 17822 RVA: 0x001241D8 File Offset: 0x001223D8
		protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderToolStripBackground(e);
				return;
			}
			ToolStrip toolStrip = e.ToolStrip;
			if (!base.ShouldPaintBackground(toolStrip))
			{
				return;
			}
			if (toolStrip is ToolStripDropDown)
			{
				this.RenderToolStripDropDownBackground(e);
				return;
			}
			if (toolStrip is MenuStrip)
			{
				this.RenderMenuStripBackground(e);
				return;
			}
			if (toolStrip is StatusStrip)
			{
				this.RenderStatusStripBackground(e);
				return;
			}
			this.RenderToolStripBackgroundInternal(e);
		}

		// Token: 0x0600459F RID: 17823 RVA: 0x00124240 File Offset: 0x00122440
		protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
		{
			this.ScaleObjectSizesIfNeeded(e.ToolStrip.DeviceDpi);
			if (this.RendererOverride != null)
			{
				base.OnRenderOverflowButtonBackground(e);
				return;
			}
			ToolStripItem item = e.Item;
			Graphics graphics = e.Graphics;
			bool flag = item.RightToLeft == RightToLeft.Yes;
			this.RenderOverflowBackground(e, flag);
			bool flag2 = e.ToolStrip.Orientation == Orientation.Horizontal;
			Rectangle empty = Rectangle.Empty;
			if (flag)
			{
				empty = new Rectangle(0, item.Height - this.overflowArrowOffsetY, this.overflowArrowWidth, this.overflowArrowHeight);
			}
			else
			{
				empty = new Rectangle(item.Width - this.overflowButtonWidth, item.Height - this.overflowArrowOffsetY, this.overflowArrowWidth, this.overflowArrowHeight);
			}
			ArrowDirection direction = flag2 ? ArrowDirection.Down : ArrowDirection.Right;
			int num = (flag && flag2) ? -1 : 1;
			empty.Offset(num, 1);
			this.RenderArrowInternal(graphics, empty, direction, SystemBrushes.ButtonHighlight);
			empty.Offset(-1 * num, -1);
			Point point = this.RenderArrowInternal(graphics, empty, direction, SystemBrushes.ControlText);
			if (flag2)
			{
				num = (flag ? -2 : 0);
				graphics.DrawLine(SystemPens.ControlText, point.X - ToolStripRenderer.Offset2X, empty.Y - ToolStripRenderer.Offset2Y, point.X + ToolStripRenderer.Offset2X, empty.Y - ToolStripRenderer.Offset2Y);
				graphics.DrawLine(SystemPens.ButtonHighlight, point.X - ToolStripRenderer.Offset2X + 1 + num, empty.Y - ToolStripRenderer.Offset2Y + 1, point.X + ToolStripRenderer.Offset2X + 1 + num, empty.Y - ToolStripRenderer.Offset2Y + 1);
				return;
			}
			graphics.DrawLine(SystemPens.ControlText, empty.X, point.Y - ToolStripRenderer.Offset2Y, empty.X, point.Y + ToolStripRenderer.Offset2Y);
			graphics.DrawLine(SystemPens.ButtonHighlight, empty.X + 1, point.Y - ToolStripRenderer.Offset2Y + 1, empty.X + 1, point.Y + ToolStripRenderer.Offset2Y + 1);
		}

		// Token: 0x060045A0 RID: 17824 RVA: 0x00124454 File Offset: 0x00122654
		protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderDropDownButtonBackground(e);
				return;
			}
			ToolStripDropDownItem toolStripDropDownItem = e.Item as ToolStripDropDownItem;
			if (toolStripDropDownItem != null && toolStripDropDownItem.Pressed && toolStripDropDownItem.HasDropDownItems)
			{
				Rectangle bounds = new Rectangle(Point.Empty, toolStripDropDownItem.Size);
				this.RenderPressedGradient(e.Graphics, bounds);
				return;
			}
			this.RenderItemInternal(e, true);
		}

		// Token: 0x060045A1 RID: 17825 RVA: 0x001244B8 File Offset: 0x001226B8
		protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderSeparator(e);
				return;
			}
			this.RenderSeparatorInternal(e.Graphics, e.Item, new Rectangle(Point.Empty, e.Item.Size), e.Vertical);
		}

		// Token: 0x060045A2 RID: 17826 RVA: 0x001244F8 File Offset: 0x001226F8
		protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderSplitButtonBackground(e);
				return;
			}
			ToolStripSplitButton toolStripSplitButton = e.Item as ToolStripSplitButton;
			Graphics graphics = e.Graphics;
			if (toolStripSplitButton != null)
			{
				Rectangle rectangle = new Rectangle(Point.Empty, toolStripSplitButton.Size);
				if (toolStripSplitButton.BackgroundImage != null)
				{
					Rectangle clipRect = toolStripSplitButton.Selected ? toolStripSplitButton.ContentRectangle : rectangle;
					ControlPaint.DrawBackgroundImage(graphics, toolStripSplitButton.BackgroundImage, toolStripSplitButton.BackColor, toolStripSplitButton.BackgroundImageLayout, rectangle, clipRect);
				}
				bool flag = toolStripSplitButton.Pressed || toolStripSplitButton.ButtonPressed || toolStripSplitButton.Selected || toolStripSplitButton.ButtonSelected;
				if (flag)
				{
					this.RenderItemInternal(e, true);
				}
				if (toolStripSplitButton.ButtonPressed)
				{
					Rectangle rectangle2 = toolStripSplitButton.ButtonBounds;
					Padding padding = (toolStripSplitButton.RightToLeft == RightToLeft.Yes) ? new Padding(0, 1, 1, 1) : new Padding(1, 1, 0, 1);
					rectangle2 = LayoutUtils.DeflateRect(rectangle2, padding);
					this.RenderPressedButtonFill(graphics, rectangle2);
				}
				else if (toolStripSplitButton.Pressed)
				{
					this.RenderPressedGradient(e.Graphics, rectangle);
				}
				Rectangle dropDownButtonBounds = toolStripSplitButton.DropDownButtonBounds;
				if (flag && !toolStripSplitButton.Pressed)
				{
					using (Brush brush = new SolidBrush(this.ColorTable.ButtonSelectedBorder))
					{
						graphics.FillRectangle(brush, toolStripSplitButton.SplitterBounds);
					}
				}
				base.DrawArrow(new ToolStripArrowRenderEventArgs(graphics, toolStripSplitButton, dropDownButtonBounds, SystemColors.ControlText, ArrowDirection.Down));
			}
		}

		// Token: 0x060045A3 RID: 17827 RVA: 0x00124664 File Offset: 0x00122864
		protected override void OnRenderToolStripStatusLabelBackground(ToolStripItemRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderToolStripStatusLabelBackground(e);
				return;
			}
			ToolStripProfessionalRenderer.RenderLabelInternal(e);
			ToolStripStatusLabel toolStripStatusLabel = e.Item as ToolStripStatusLabel;
			ControlPaint.DrawBorder3D(e.Graphics, new Rectangle(0, 0, toolStripStatusLabel.Width, toolStripStatusLabel.Height), toolStripStatusLabel.BorderStyle, (Border3DSide)toolStripStatusLabel.BorderSides);
		}

		// Token: 0x060045A4 RID: 17828 RVA: 0x001246BD File Offset: 0x001228BD
		protected override void OnRenderLabelBackground(ToolStripItemRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderLabelBackground(e);
				return;
			}
			ToolStripProfessionalRenderer.RenderLabelInternal(e);
		}

		// Token: 0x060045A5 RID: 17829 RVA: 0x001246D8 File Offset: 0x001228D8
		protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderButtonBackground(e);
				return;
			}
			ToolStripButton toolStripButton = e.Item as ToolStripButton;
			Graphics graphics = e.Graphics;
			Rectangle rectangle = new Rectangle(Point.Empty, toolStripButton.Size);
			if (toolStripButton.CheckState == CheckState.Unchecked)
			{
				this.RenderItemInternal(e, true);
				return;
			}
			Rectangle clipRect = toolStripButton.Selected ? toolStripButton.ContentRectangle : rectangle;
			if (toolStripButton.BackgroundImage != null)
			{
				ControlPaint.DrawBackgroundImage(graphics, toolStripButton.BackgroundImage, toolStripButton.BackColor, toolStripButton.BackgroundImageLayout, rectangle, clipRect);
			}
			if (this.UseSystemColors)
			{
				if (toolStripButton.Selected)
				{
					this.RenderPressedButtonFill(graphics, rectangle);
				}
				else
				{
					this.RenderCheckedButtonFill(graphics, rectangle);
				}
				using (Pen pen = new Pen(this.ColorTable.ButtonSelectedBorder))
				{
					graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
					return;
				}
			}
			if (toolStripButton.Selected)
			{
				this.RenderPressedButtonFill(graphics, rectangle);
			}
			else
			{
				this.RenderCheckedButtonFill(graphics, rectangle);
			}
			using (Pen pen2 = new Pen(this.ColorTable.ButtonSelectedBorder))
			{
				graphics.DrawRectangle(pen2, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
			}
		}

		// Token: 0x060045A6 RID: 17830 RVA: 0x00124848 File Offset: 0x00122A48
		protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderToolStripBorder(e);
				return;
			}
			ToolStrip toolStrip = e.ToolStrip;
			Graphics graphics = e.Graphics;
			if (toolStrip is ToolStripDropDown)
			{
				this.RenderToolStripDropDownBorder(e);
				return;
			}
			if (!(toolStrip is MenuStrip))
			{
				if (toolStrip is StatusStrip)
				{
					this.RenderStatusStripBorder(e);
					return;
				}
				Rectangle rectangle = new Rectangle(Point.Empty, toolStrip.Size);
				using (Pen pen = new Pen(this.ColorTable.ToolStripBorder))
				{
					if (toolStrip.Orientation == Orientation.Horizontal)
					{
						graphics.DrawLine(pen, rectangle.Left, rectangle.Height - 1, rectangle.Right, rectangle.Height - 1);
						if (this.RoundedEdges)
						{
							graphics.DrawLine(pen, rectangle.Width - 2, rectangle.Height - 2, rectangle.Width - 1, rectangle.Height - 3);
						}
					}
					else
					{
						graphics.DrawLine(pen, rectangle.Width - 1, 0, rectangle.Width - 1, rectangle.Height - 1);
						if (this.RoundedEdges)
						{
							graphics.DrawLine(pen, rectangle.Width - 2, rectangle.Height - 2, rectangle.Width - 1, rectangle.Height - 3);
						}
					}
				}
				if (this.RoundedEdges)
				{
					if (toolStrip.OverflowButton.Visible)
					{
						this.RenderOverflowButtonEffectsOverBorder(e);
						return;
					}
					Rectangle empty = Rectangle.Empty;
					if (toolStrip.Orientation == Orientation.Horizontal)
					{
						empty = new Rectangle(rectangle.Width - 1, 3, 1, rectangle.Height - 3);
					}
					else
					{
						empty = new Rectangle(3, rectangle.Height - 1, rectangle.Width - 3, rectangle.Height - 1);
					}
					this.ScaleObjectSizesIfNeeded(toolStrip.DeviceDpi);
					this.FillWithDoubleGradient(this.ColorTable.OverflowButtonGradientBegin, this.ColorTable.OverflowButtonGradientMiddle, this.ColorTable.OverflowButtonGradientEnd, e.Graphics, empty, this.iconWellGradientWidth, this.iconWellGradientWidth, LinearGradientMode.Vertical, false);
					this.RenderToolStripCurve(e);
				}
			}
		}

		// Token: 0x060045A7 RID: 17831 RVA: 0x00124A5C File Offset: 0x00122C5C
		protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderGrip(e);
				return;
			}
			this.ScaleObjectSizesIfNeeded(e.ToolStrip.DeviceDpi);
			Graphics graphics = e.Graphics;
			Rectangle gripBounds = e.GripBounds;
			ToolStrip toolStrip = e.ToolStrip;
			bool flag = e.ToolStrip.RightToLeft == RightToLeft.Yes;
			int num = (toolStrip.Orientation == Orientation.Horizontal) ? gripBounds.Height : gripBounds.Width;
			int num2 = (toolStrip.Orientation == Orientation.Horizontal) ? gripBounds.Width : gripBounds.Height;
			int num3 = (num - this.gripPadding * 2) / 4;
			if (num3 > 0)
			{
				int num4 = (toolStrip is MenuStrip) ? 2 : 0;
				Rectangle[] array = new Rectangle[num3];
				int num5 = this.gripPadding + 1 + num4;
				int num6 = num2 / 2;
				for (int i = 0; i < num3; i++)
				{
					array[i] = ((toolStrip.Orientation == Orientation.Horizontal) ? new Rectangle(num6, num5, 2, 2) : new Rectangle(num5, num6, 2, 2));
					num5 += 4;
				}
				int num7 = flag ? 1 : -1;
				if (flag)
				{
					for (int j = 0; j < num3; j++)
					{
						array[j].Offset(-num7, 0);
					}
				}
				using (Brush brush = new SolidBrush(this.ColorTable.GripLight))
				{
					graphics.FillRectangles(brush, array);
				}
				for (int k = 0; k < num3; k++)
				{
					array[k].Offset(num7, -1);
				}
				using (Brush brush2 = new SolidBrush(this.ColorTable.GripDark))
				{
					graphics.FillRectangles(brush2, array);
				}
			}
		}

		// Token: 0x060045A8 RID: 17832 RVA: 0x00124C24 File Offset: 0x00122E24
		protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderMenuItemBackground(e);
				return;
			}
			ToolStripItem item = e.Item;
			Graphics graphics = e.Graphics;
			Rectangle rectangle = new Rectangle(Point.Empty, item.Size);
			if (rectangle.Width == 0 || rectangle.Height == 0)
			{
				return;
			}
			if (item is MdiControlStrip.SystemMenuItem)
			{
				return;
			}
			if (item.IsOnDropDown)
			{
				this.ScaleObjectSizesIfNeeded(item.DeviceDpi);
				rectangle = LayoutUtils.DeflateRect(rectangle, this.scaledDropDownMenuItemPaintPadding);
				if (item.Selected)
				{
					Color color = this.ColorTable.MenuItemBorder;
					if (item.Enabled)
					{
						if (this.UseSystemColors)
						{
							color = SystemColors.Highlight;
							this.RenderSelectedButtonFill(graphics, rectangle);
						}
						else
						{
							using (Brush brush = new SolidBrush(this.ColorTable.MenuItemSelected))
							{
								graphics.FillRectangle(brush, rectangle);
							}
						}
					}
					using (Pen pen = new Pen(color))
					{
						graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
						return;
					}
				}
				Rectangle rectangle2 = rectangle;
				if (item.BackgroundImage != null)
				{
					ControlPaint.DrawBackgroundImage(graphics, item.BackgroundImage, item.BackColor, item.BackgroundImageLayout, rectangle, rectangle2);
					return;
				}
				if (item.Owner == null || !(item.BackColor != item.Owner.BackColor))
				{
					return;
				}
				using (Brush brush2 = new SolidBrush(item.BackColor))
				{
					graphics.FillRectangle(brush2, rectangle2);
					return;
				}
			}
			if (item.Pressed)
			{
				this.RenderPressedGradient(graphics, rectangle);
				return;
			}
			if (item.Selected)
			{
				Color color2 = this.ColorTable.MenuItemBorder;
				if (item.Enabled)
				{
					if (this.UseSystemColors)
					{
						color2 = SystemColors.Highlight;
						this.RenderSelectedButtonFill(graphics, rectangle);
					}
					else
					{
						using (Brush brush3 = new LinearGradientBrush(rectangle, this.ColorTable.MenuItemSelectedGradientBegin, this.ColorTable.MenuItemSelectedGradientEnd, LinearGradientMode.Vertical))
						{
							graphics.FillRectangle(brush3, rectangle);
						}
					}
				}
				using (Pen pen2 = new Pen(color2))
				{
					graphics.DrawRectangle(pen2, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
					return;
				}
			}
			Rectangle rectangle3 = rectangle;
			if (item.BackgroundImage != null)
			{
				ControlPaint.DrawBackgroundImage(graphics, item.BackgroundImage, item.BackColor, item.BackgroundImageLayout, rectangle, rectangle3);
				return;
			}
			if (item.Owner != null && item.BackColor != item.Owner.BackColor)
			{
				using (Brush brush4 = new SolidBrush(item.BackColor))
				{
					graphics.FillRectangle(brush4, rectangle3);
				}
			}
		}

		// Token: 0x060045A9 RID: 17833 RVA: 0x00124F30 File Offset: 0x00123130
		protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderArrow(e);
				return;
			}
			ToolStripItem item = e.Item;
			if (item is ToolStripDropDownItem)
			{
				e.DefaultArrowColor = (item.Enabled ? SystemColors.ControlText : SystemColors.ControlDark);
			}
			base.OnRenderArrow(e);
		}

		// Token: 0x060045AA RID: 17834 RVA: 0x00124F80 File Offset: 0x00123180
		protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderImageMargin(e);
				return;
			}
			this.ScaleObjectSizesIfNeeded(e.ToolStrip.DeviceDpi);
			Graphics graphics = e.Graphics;
			Rectangle affectedBounds = e.AffectedBounds;
			affectedBounds.Y += 2;
			affectedBounds.Height -= 4;
			RightToLeft rightToLeft = e.ToolStrip.RightToLeft;
			Color beginColor = (rightToLeft == RightToLeft.No) ? this.ColorTable.ImageMarginGradientBegin : this.ColorTable.ImageMarginGradientEnd;
			Color endColor = (rightToLeft == RightToLeft.No) ? this.ColorTable.ImageMarginGradientEnd : this.ColorTable.ImageMarginGradientBegin;
			this.FillWithDoubleGradient(beginColor, this.ColorTable.ImageMarginGradientMiddle, endColor, e.Graphics, affectedBounds, this.iconWellGradientWidth, this.iconWellGradientWidth, LinearGradientMode.Horizontal, e.ToolStrip.RightToLeft == RightToLeft.Yes);
		}

		// Token: 0x060045AB RID: 17835 RVA: 0x00125058 File Offset: 0x00123258
		protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderItemText(e);
				return;
			}
			if (e.Item is ToolStripMenuItem && (e.Item.Selected || e.Item.Pressed))
			{
				e.DefaultTextColor = e.Item.ForeColor;
			}
			base.OnRenderItemText(e);
		}

		// Token: 0x060045AC RID: 17836 RVA: 0x001250B4 File Offset: 0x001232B4
		protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderItemCheck(e);
				return;
			}
			this.RenderCheckBackground(e);
			base.OnRenderItemCheck(e);
		}

		// Token: 0x060045AD RID: 17837 RVA: 0x001250D4 File Offset: 0x001232D4
		protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderItemImage(e);
				return;
			}
			Rectangle imageRectangle = e.ImageRectangle;
			Image image = e.Image;
			if (e.Item is ToolStripMenuItem)
			{
				ToolStripMenuItem toolStripMenuItem = e.Item as ToolStripMenuItem;
				if (toolStripMenuItem.CheckState != CheckState.Unchecked)
				{
					ToolStripDropDownMenu toolStripDropDownMenu = toolStripMenuItem.ParentInternal as ToolStripDropDownMenu;
					if (toolStripDropDownMenu != null && !toolStripDropDownMenu.ShowCheckMargin && toolStripDropDownMenu.ShowImageMargin)
					{
						this.RenderCheckBackground(e);
					}
				}
			}
			if (imageRectangle != Rectangle.Empty && image != null)
			{
				if (!e.Item.Enabled)
				{
					base.OnRenderItemImage(e);
					return;
				}
				if (e.Item.ImageScaling == ToolStripItemImageScaling.None)
				{
					e.Graphics.DrawImage(image, imageRectangle, new Rectangle(Point.Empty, imageRectangle.Size), GraphicsUnit.Pixel);
					return;
				}
				e.Graphics.DrawImage(image, imageRectangle);
			}
		}

		// Token: 0x060045AE RID: 17838 RVA: 0x001251A8 File Offset: 0x001233A8
		protected override void OnRenderToolStripPanelBackground(ToolStripPanelRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderToolStripPanelBackground(e);
				return;
			}
			ToolStripPanel toolStripPanel = e.ToolStripPanel;
			if (!base.ShouldPaintBackground(toolStripPanel))
			{
				return;
			}
			e.Handled = true;
			this.RenderBackgroundGradient(e.Graphics, toolStripPanel, this.ColorTable.ToolStripPanelGradientBegin, this.ColorTable.ToolStripPanelGradientEnd);
		}

		// Token: 0x060045AF RID: 17839 RVA: 0x00125200 File Offset: 0x00123400
		protected override void OnRenderToolStripContentPanelBackground(ToolStripContentPanelRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderToolStripContentPanelBackground(e);
				return;
			}
			ToolStripContentPanel toolStripContentPanel = e.ToolStripContentPanel;
			if (!base.ShouldPaintBackground(toolStripContentPanel))
			{
				return;
			}
			if (SystemInformation.InLockedTerminalSession())
			{
				return;
			}
			e.Handled = true;
			e.Graphics.Clear(this.ColorTable.ToolStripContentPanelGradientEnd);
		}

		// Token: 0x060045B0 RID: 17840 RVA: 0x00125254 File Offset: 0x00123454
		internal override Region GetTransparentRegion(ToolStrip toolStrip)
		{
			if (toolStrip is ToolStripDropDown || toolStrip is MenuStrip || toolStrip is StatusStrip)
			{
				return null;
			}
			if (!this.RoundedEdges)
			{
				return null;
			}
			Rectangle rectangle = new Rectangle(Point.Empty, toolStrip.Size);
			if (toolStrip.ParentInternal != null)
			{
				Point empty = Point.Empty;
				Point point = new Point(rectangle.Width - 1, 0);
				Point location = new Point(0, rectangle.Height - 1);
				Point point2 = new Point(rectangle.Width - 1, rectangle.Height - 1);
				Rectangle rect = new Rectangle(empty, ToolStripProfessionalRenderer.onePix);
				Rectangle rect2 = new Rectangle(location, new Size(2, 1));
				Rectangle rect3 = new Rectangle(location.X, location.Y - 1, 1, 2);
				Rectangle rect4 = new Rectangle(point2.X - 1, point2.Y, 2, 1);
				Rectangle rect5 = new Rectangle(point2.X, point2.Y - 1, 1, 2);
				Rectangle rect6;
				Rectangle rect7;
				if (toolStrip.OverflowButton.Visible)
				{
					rect6 = new Rectangle(point.X - 1, point.Y, 1, 1);
					rect7 = new Rectangle(point.X, point.Y, 1, 2);
				}
				else
				{
					rect6 = new Rectangle(point.X - 2, point.Y, 2, 1);
					rect7 = new Rectangle(point.X, point.Y, 1, 3);
				}
				Region region = new Region(rect);
				region.Union(rect);
				region.Union(rect2);
				region.Union(rect3);
				region.Union(rect4);
				region.Union(rect5);
				region.Union(rect6);
				region.Union(rect7);
				return region;
			}
			return null;
		}

		// Token: 0x060045B1 RID: 17841 RVA: 0x00125408 File Offset: 0x00123608
		private void RenderOverflowButtonEffectsOverBorder(ToolStripRenderEventArgs e)
		{
			ToolStrip toolStrip = e.ToolStrip;
			ToolStripItem overflowButton = toolStrip.OverflowButton;
			if (!overflowButton.Visible)
			{
				return;
			}
			Graphics graphics = e.Graphics;
			Color color;
			Color color2;
			if (overflowButton.Pressed)
			{
				color = this.ColorTable.ButtonPressedGradientBegin;
				color2 = color;
			}
			else if (overflowButton.Selected)
			{
				color = this.ColorTable.ButtonSelectedGradientMiddle;
				color2 = color;
			}
			else
			{
				color = this.ColorTable.ToolStripBorder;
				color2 = this.ColorTable.ToolStripGradientMiddle;
			}
			using (Brush brush = new SolidBrush(color))
			{
				graphics.FillRectangle(brush, toolStrip.Width - 1, toolStrip.Height - 2, 1, 1);
				graphics.FillRectangle(brush, toolStrip.Width - 2, toolStrip.Height - 1, 1, 1);
			}
			using (Brush brush2 = new SolidBrush(color2))
			{
				graphics.FillRectangle(brush2, toolStrip.Width - 2, 0, 1, 1);
				graphics.FillRectangle(brush2, toolStrip.Width - 1, 1, 1, 1);
			}
		}

		// Token: 0x060045B2 RID: 17842 RVA: 0x00125524 File Offset: 0x00123724
		private void FillWithDoubleGradient(Color beginColor, Color middleColor, Color endColor, Graphics g, Rectangle bounds, int firstGradientWidth, int secondGradientWidth, LinearGradientMode mode, bool flipHorizontal)
		{
			if (bounds.Width == 0 || bounds.Height == 0)
			{
				return;
			}
			Rectangle rect = bounds;
			Rectangle rect2 = bounds;
			bool flag;
			if (mode == LinearGradientMode.Horizontal)
			{
				if (flipHorizontal)
				{
					Color color = endColor;
					endColor = beginColor;
					beginColor = color;
				}
				rect2.Width = firstGradientWidth;
				rect.Width = secondGradientWidth + 1;
				rect.X = bounds.Right - rect.Width;
				flag = (bounds.Width > firstGradientWidth + secondGradientWidth);
			}
			else
			{
				rect2.Height = firstGradientWidth;
				rect.Height = secondGradientWidth + 1;
				rect.Y = bounds.Bottom - rect.Height;
				flag = (bounds.Height > firstGradientWidth + secondGradientWidth);
			}
			if (flag)
			{
				using (Brush brush = new SolidBrush(middleColor))
				{
					g.FillRectangle(brush, bounds);
				}
				using (Brush brush2 = new LinearGradientBrush(rect2, beginColor, middleColor, mode))
				{
					g.FillRectangle(brush2, rect2);
				}
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, middleColor, endColor, mode))
				{
					if (mode == LinearGradientMode.Horizontal)
					{
						rect.X++;
						rect.Width--;
					}
					else
					{
						rect.Y++;
						rect.Height--;
					}
					g.FillRectangle(linearGradientBrush, rect);
					return;
				}
			}
			using (Brush brush3 = new LinearGradientBrush(bounds, beginColor, endColor, mode))
			{
				g.FillRectangle(brush3, bounds);
			}
		}

		// Token: 0x060045B3 RID: 17843 RVA: 0x001256DC File Offset: 0x001238DC
		private void RenderStatusStripBorder(ToolStripRenderEventArgs e)
		{
			e.Graphics.DrawLine(SystemPens.ButtonHighlight, 0, 0, e.ToolStrip.Width, 0);
		}

		// Token: 0x060045B4 RID: 17844 RVA: 0x001256FC File Offset: 0x001238FC
		private void RenderStatusStripBackground(ToolStripRenderEventArgs e)
		{
			StatusStrip statusStrip = e.ToolStrip as StatusStrip;
			this.RenderBackgroundGradient(e.Graphics, statusStrip, this.ColorTable.StatusStripGradientBegin, this.ColorTable.StatusStripGradientEnd, statusStrip.Orientation);
		}

		// Token: 0x060045B5 RID: 17845 RVA: 0x00125740 File Offset: 0x00123940
		private void RenderCheckBackground(ToolStripItemImageRenderEventArgs e)
		{
			Rectangle rectangle = DpiHelper.IsScalingRequired ? new Rectangle(e.ImageRectangle.Left - 2, (e.Item.Height - e.ImageRectangle.Height) / 2 - 1, e.ImageRectangle.Width + 4, e.ImageRectangle.Height + 2) : new Rectangle(e.ImageRectangle.Left - 2, 1, e.ImageRectangle.Width + 4, e.Item.Height - 2);
			Graphics graphics = e.Graphics;
			if (!this.UseSystemColors)
			{
				Color color = e.Item.Selected ? this.ColorTable.CheckSelectedBackground : this.ColorTable.CheckBackground;
				color = (e.Item.Pressed ? this.ColorTable.CheckPressedBackground : color);
				using (Brush brush = new SolidBrush(color))
				{
					graphics.FillRectangle(brush, rectangle);
				}
				using (Pen pen = new Pen(this.ColorTable.ButtonSelectedBorder))
				{
					graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
					return;
				}
			}
			if (e.Item.Pressed)
			{
				this.RenderPressedButtonFill(graphics, rectangle);
			}
			else
			{
				this.RenderSelectedButtonFill(graphics, rectangle);
			}
			graphics.DrawRectangle(SystemPens.Highlight, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
		}

		// Token: 0x060045B6 RID: 17846 RVA: 0x00125900 File Offset: 0x00123B00
		private void RenderPressedGradient(Graphics g, Rectangle bounds)
		{
			if (bounds.Width == 0 || bounds.Height == 0)
			{
				return;
			}
			using (Brush brush = new LinearGradientBrush(bounds, this.ColorTable.MenuItemPressedGradientBegin, this.ColorTable.MenuItemPressedGradientEnd, LinearGradientMode.Vertical))
			{
				g.FillRectangle(brush, bounds);
			}
			using (Pen pen = new Pen(this.ColorTable.MenuBorder))
			{
				g.DrawRectangle(pen, bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
			}
		}

		// Token: 0x060045B7 RID: 17847 RVA: 0x001259B4 File Offset: 0x00123BB4
		private void RenderMenuStripBackground(ToolStripRenderEventArgs e)
		{
			this.RenderBackgroundGradient(e.Graphics, e.ToolStrip, this.ColorTable.MenuStripGradientBegin, this.ColorTable.MenuStripGradientEnd, e.ToolStrip.Orientation);
		}

		// Token: 0x060045B8 RID: 17848 RVA: 0x001259EC File Offset: 0x00123BEC
		private static void RenderLabelInternal(ToolStripItemRenderEventArgs e)
		{
			Graphics graphics = e.Graphics;
			ToolStripItem item = e.Item;
			Rectangle rectangle = new Rectangle(Point.Empty, item.Size);
			Rectangle clipRect = item.Selected ? item.ContentRectangle : rectangle;
			if (item.BackgroundImage != null)
			{
				ControlPaint.DrawBackgroundImage(graphics, item.BackgroundImage, item.BackColor, item.BackgroundImageLayout, rectangle, clipRect);
			}
		}

		// Token: 0x060045B9 RID: 17849 RVA: 0x00125A4D File Offset: 0x00123C4D
		private void RenderBackgroundGradient(Graphics g, Control control, Color beginColor, Color endColor)
		{
			this.RenderBackgroundGradient(g, control, beginColor, endColor, Orientation.Horizontal);
		}

		// Token: 0x060045BA RID: 17850 RVA: 0x00125A5C File Offset: 0x00123C5C
		private void RenderBackgroundGradient(Graphics g, Control control, Color beginColor, Color endColor, Orientation orientation)
		{
			if (control.RightToLeft == RightToLeft.Yes)
			{
				Color color = beginColor;
				beginColor = endColor;
				endColor = color;
			}
			if (orientation == Orientation.Horizontal)
			{
				Control parentInternal = control.ParentInternal;
				if (parentInternal != null)
				{
					Rectangle rectangle = new Rectangle(Point.Empty, parentInternal.Size);
					if (LayoutUtils.IsZeroWidthOrHeight(rectangle))
					{
						return;
					}
					using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, beginColor, endColor, LinearGradientMode.Horizontal))
					{
						linearGradientBrush.TranslateTransform((float)(parentInternal.Width - control.Location.X), (float)(parentInternal.Height - control.Location.Y));
						g.FillRectangle(linearGradientBrush, new Rectangle(Point.Empty, control.Size));
						return;
					}
				}
				Rectangle rectangle2 = new Rectangle(Point.Empty, control.Size);
				if (LayoutUtils.IsZeroWidthOrHeight(rectangle2))
				{
					return;
				}
				using (LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(rectangle2, beginColor, endColor, LinearGradientMode.Horizontal))
				{
					g.FillRectangle(linearGradientBrush2, rectangle2);
					return;
				}
			}
			using (Brush brush = new SolidBrush(beginColor))
			{
				g.FillRectangle(brush, new Rectangle(Point.Empty, control.Size));
			}
		}

		// Token: 0x060045BB RID: 17851 RVA: 0x00125BA4 File Offset: 0x00123DA4
		private void RenderToolStripBackgroundInternal(ToolStripRenderEventArgs e)
		{
			this.ScaleObjectSizesIfNeeded(e.ToolStrip.DeviceDpi);
			ToolStrip toolStrip = e.ToolStrip;
			Graphics graphics = e.Graphics;
			Rectangle bounds = new Rectangle(Point.Empty, e.ToolStrip.Size);
			LinearGradientMode mode = (toolStrip.Orientation == Orientation.Horizontal) ? LinearGradientMode.Vertical : LinearGradientMode.Horizontal;
			this.FillWithDoubleGradient(this.ColorTable.ToolStripGradientBegin, this.ColorTable.ToolStripGradientMiddle, this.ColorTable.ToolStripGradientEnd, e.Graphics, bounds, this.iconWellGradientWidth, this.iconWellGradientWidth, mode, false);
		}

		// Token: 0x060045BC RID: 17852 RVA: 0x00125C30 File Offset: 0x00123E30
		private void RenderToolStripDropDownBackground(ToolStripRenderEventArgs e)
		{
			ToolStrip toolStrip = e.ToolStrip;
			Rectangle rect = new Rectangle(Point.Empty, e.ToolStrip.Size);
			using (Brush brush = new SolidBrush(this.ColorTable.ToolStripDropDownBackground))
			{
				e.Graphics.FillRectangle(brush, rect);
			}
		}

		// Token: 0x060045BD RID: 17853 RVA: 0x00125C98 File Offset: 0x00123E98
		private void RenderToolStripDropDownBorder(ToolStripRenderEventArgs e)
		{
			ToolStripDropDown toolStripDropDown = e.ToolStrip as ToolStripDropDown;
			Graphics graphics = e.Graphics;
			if (toolStripDropDown != null)
			{
				Rectangle rectangle = new Rectangle(Point.Empty, toolStripDropDown.Size);
				using (Pen pen = new Pen(this.ColorTable.MenuBorder))
				{
					graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
				}
				if (!(toolStripDropDown is ToolStripOverflow))
				{
					using (Brush brush = new SolidBrush(this.ColorTable.ToolStripDropDownBackground))
					{
						graphics.FillRectangle(brush, e.ConnectedArea);
					}
				}
			}
		}

		// Token: 0x060045BE RID: 17854 RVA: 0x00125D68 File Offset: 0x00123F68
		private void RenderOverflowBackground(ToolStripItemRenderEventArgs e, bool rightToLeft)
		{
			this.ScaleObjectSizesIfNeeded(e.Item.DeviceDpi);
			Graphics graphics = e.Graphics;
			ToolStripOverflowButton toolStripOverflowButton = e.Item as ToolStripOverflowButton;
			Rectangle rectangle = new Rectangle(Point.Empty, e.Item.Size);
			Rectangle withinBounds = rectangle;
			bool flag = this.RoundedEdges && !(toolStripOverflowButton.GetCurrentParent() is MenuStrip);
			bool flag2 = e.ToolStrip.Orientation == Orientation.Horizontal;
			if (flag2)
			{
				rectangle.X += rectangle.Width - this.overflowButtonWidth + 1;
				rectangle.Width = this.overflowButtonWidth;
				if (rightToLeft)
				{
					rectangle = LayoutUtils.RTLTranslate(rectangle, withinBounds);
				}
			}
			else
			{
				rectangle.Y = rectangle.Height - this.overflowButtonWidth + 1;
				rectangle.Height = this.overflowButtonWidth;
			}
			Color color;
			Color middleColor;
			Color endColor;
			Color color2;
			Color color3;
			if (toolStripOverflowButton.Pressed)
			{
				color = this.ColorTable.ButtonPressedGradientBegin;
				middleColor = this.ColorTable.ButtonPressedGradientMiddle;
				endColor = this.ColorTable.ButtonPressedGradientEnd;
				color2 = this.ColorTable.ButtonPressedGradientBegin;
				color3 = color2;
			}
			else if (toolStripOverflowButton.Selected)
			{
				color = this.ColorTable.ButtonSelectedGradientBegin;
				middleColor = this.ColorTable.ButtonSelectedGradientMiddle;
				endColor = this.ColorTable.ButtonSelectedGradientEnd;
				color2 = this.ColorTable.ButtonSelectedGradientMiddle;
				color3 = color2;
			}
			else
			{
				color = this.ColorTable.OverflowButtonGradientBegin;
				middleColor = this.ColorTable.OverflowButtonGradientMiddle;
				endColor = this.ColorTable.OverflowButtonGradientEnd;
				color2 = this.ColorTable.ToolStripBorder;
				color3 = (flag2 ? this.ColorTable.ToolStripGradientMiddle : this.ColorTable.ToolStripGradientEnd);
			}
			if (flag)
			{
				using (Pen pen = new Pen(color2))
				{
					Point pt = new Point(rectangle.Left - 1, rectangle.Height - 2);
					Point pt2 = new Point(rectangle.Left, rectangle.Height - 2);
					if (rightToLeft)
					{
						pt.X = rectangle.Right + 1;
						pt2.X = rectangle.Right;
					}
					graphics.DrawLine(pen, pt, pt2);
				}
			}
			LinearGradientMode mode = flag2 ? LinearGradientMode.Vertical : LinearGradientMode.Horizontal;
			this.FillWithDoubleGradient(color, middleColor, endColor, graphics, rectangle, this.iconWellGradientWidth, this.iconWellGradientWidth, mode, false);
			if (flag)
			{
				using (Brush brush = new SolidBrush(color3))
				{
					if (flag2)
					{
						Point point = new Point(rectangle.X - 2, 0);
						Point point2 = new Point(rectangle.X - 1, 1);
						if (rightToLeft)
						{
							point.X = rectangle.Right + 1;
							point2.X = rectangle.Right;
						}
						graphics.FillRectangle(brush, point.X, point.Y, 1, 1);
						graphics.FillRectangle(brush, point2.X, point2.Y, 1, 1);
					}
					else
					{
						graphics.FillRectangle(brush, rectangle.Width - 3, rectangle.Top - 1, 1, 1);
						graphics.FillRectangle(brush, rectangle.Width - 2, rectangle.Top - 2, 1, 1);
					}
				}
				using (Brush brush2 = new SolidBrush(color))
				{
					if (flag2)
					{
						Rectangle rect = new Rectangle(rectangle.X - 1, 0, 1, 1);
						if (rightToLeft)
						{
							rect.X = rectangle.Right;
						}
						graphics.FillRectangle(brush2, rect);
					}
					else
					{
						graphics.FillRectangle(brush2, rectangle.X, rectangle.Top - 1, 1, 1);
					}
				}
			}
		}

		// Token: 0x060045BF RID: 17855 RVA: 0x00126114 File Offset: 0x00124314
		private void RenderToolStripCurve(ToolStripRenderEventArgs e)
		{
			Rectangle rectangle = new Rectangle(Point.Empty, e.ToolStrip.Size);
			ToolStrip toolStrip = e.ToolStrip;
			Rectangle displayRectangle = toolStrip.DisplayRectangle;
			Graphics graphics = e.Graphics;
			Point empty = Point.Empty;
			Point location = new Point(rectangle.Width - 1, 0);
			Point point = new Point(0, rectangle.Height - 1);
			using (Brush brush = new SolidBrush(this.ColorTable.ToolStripGradientMiddle))
			{
				Rectangle rectangle2 = new Rectangle(empty, ToolStripProfessionalRenderer.onePix);
				rectangle2.X++;
				Rectangle rectangle3 = new Rectangle(empty, ToolStripProfessionalRenderer.onePix);
				rectangle3.Y++;
				Rectangle rectangle4 = new Rectangle(location, ToolStripProfessionalRenderer.onePix);
				rectangle4.X -= 2;
				Rectangle rectangle5 = rectangle4;
				rectangle5.Y++;
				rectangle5.X++;
				Rectangle[] array = new Rectangle[]
				{
					rectangle2,
					rectangle3,
					rectangle4,
					rectangle5
				};
				for (int i = 0; i < array.Length; i++)
				{
					if (displayRectangle.IntersectsWith(array[i]))
					{
						array[i] = Rectangle.Empty;
					}
				}
				graphics.FillRectangles(brush, array);
			}
			using (Brush brush2 = new SolidBrush(this.ColorTable.ToolStripGradientEnd))
			{
				Point point2 = point;
				point2.Offset(1, -1);
				if (!displayRectangle.Contains(point2))
				{
					graphics.FillRectangle(brush2, new Rectangle(point2, ToolStripProfessionalRenderer.onePix));
				}
				Rectangle rect = new Rectangle(point.X, point.Y - 2, 1, 1);
				if (!displayRectangle.IntersectsWith(rect))
				{
					graphics.FillRectangle(brush2, rect);
				}
			}
		}

		// Token: 0x060045C0 RID: 17856 RVA: 0x00126310 File Offset: 0x00124510
		private void RenderSelectedButtonFill(Graphics g, Rectangle bounds)
		{
			if (bounds.Width == 0 || bounds.Height == 0)
			{
				return;
			}
			if (!this.UseSystemColors)
			{
				using (Brush brush = new LinearGradientBrush(bounds, this.ColorTable.ButtonSelectedGradientBegin, this.ColorTable.ButtonSelectedGradientEnd, LinearGradientMode.Vertical))
				{
					g.FillRectangle(brush, bounds);
					return;
				}
			}
			Color buttonSelectedHighlight = this.ColorTable.ButtonSelectedHighlight;
			using (Brush brush2 = new SolidBrush(buttonSelectedHighlight))
			{
				g.FillRectangle(brush2, bounds);
			}
		}

		// Token: 0x060045C1 RID: 17857 RVA: 0x001263B0 File Offset: 0x001245B0
		private void RenderCheckedButtonFill(Graphics g, Rectangle bounds)
		{
			if (bounds.Width == 0 || bounds.Height == 0)
			{
				return;
			}
			if (!this.UseSystemColors)
			{
				using (Brush brush = new LinearGradientBrush(bounds, this.ColorTable.ButtonCheckedGradientBegin, this.ColorTable.ButtonCheckedGradientEnd, LinearGradientMode.Vertical))
				{
					g.FillRectangle(brush, bounds);
					return;
				}
			}
			Color buttonCheckedHighlight = this.ColorTable.ButtonCheckedHighlight;
			using (Brush brush2 = new SolidBrush(buttonCheckedHighlight))
			{
				g.FillRectangle(brush2, bounds);
			}
		}

		// Token: 0x060045C2 RID: 17858 RVA: 0x00126450 File Offset: 0x00124650
		private void RenderSeparatorInternal(Graphics g, ToolStripItem item, Rectangle bounds, bool vertical)
		{
			Color separatorDark = this.ColorTable.SeparatorDark;
			Color separatorLight = this.ColorTable.SeparatorLight;
			Pen pen = new Pen(separatorDark);
			Pen pen2 = new Pen(separatorLight);
			bool flag = true;
			bool flag2 = true;
			bool flag3 = item is ToolStripSeparator;
			bool flag4 = false;
			if (flag3)
			{
				if (vertical)
				{
					if (!item.IsOnDropDown)
					{
						bounds.Y += 3;
						bounds.Height = Math.Max(0, bounds.Height - 6);
					}
				}
				else
				{
					ToolStripDropDownMenu toolStripDropDownMenu = item.GetCurrentParent() as ToolStripDropDownMenu;
					if (toolStripDropDownMenu != null)
					{
						if (toolStripDropDownMenu.RightToLeft == RightToLeft.No)
						{
							bounds.X += toolStripDropDownMenu.Padding.Left - 2;
							bounds.Width = toolStripDropDownMenu.Width - bounds.X;
						}
						else
						{
							bounds.X += 2;
							bounds.Width = toolStripDropDownMenu.Width - bounds.X - toolStripDropDownMenu.Padding.Right;
						}
					}
					else
					{
						flag4 = true;
					}
				}
			}
			try
			{
				if (vertical)
				{
					if (bounds.Height >= 4)
					{
						bounds.Inflate(0, -2);
					}
					bool flag5 = item.RightToLeft == RightToLeft.Yes;
					Pen pen3 = flag5 ? pen2 : pen;
					Pen pen4 = flag5 ? pen : pen2;
					int num = bounds.Width / 2;
					g.DrawLine(pen3, num, bounds.Top, num, bounds.Bottom - 1);
					num++;
					g.DrawLine(pen4, num, bounds.Top + 1, num, bounds.Bottom);
				}
				else
				{
					if (flag4 && bounds.Width >= 4)
					{
						bounds.Inflate(-2, 0);
					}
					int num2 = bounds.Height / 2;
					g.DrawLine(pen, bounds.Left, num2, bounds.Right - 1, num2);
					if (!flag3 || flag4)
					{
						num2++;
						g.DrawLine(pen2, bounds.Left + 1, num2, bounds.Right - 1, num2);
					}
				}
			}
			finally
			{
				if (flag && pen != null)
				{
					pen.Dispose();
				}
				if (flag2 && pen2 != null)
				{
					pen2.Dispose();
				}
			}
		}

		// Token: 0x060045C3 RID: 17859 RVA: 0x00126688 File Offset: 0x00124888
		private void RenderPressedButtonFill(Graphics g, Rectangle bounds)
		{
			if (bounds.Width == 0 || bounds.Height == 0)
			{
				return;
			}
			if (!this.UseSystemColors)
			{
				using (Brush brush = new LinearGradientBrush(bounds, this.ColorTable.ButtonPressedGradientBegin, this.ColorTable.ButtonPressedGradientEnd, LinearGradientMode.Vertical))
				{
					g.FillRectangle(brush, bounds);
					return;
				}
			}
			Color buttonPressedHighlight = this.ColorTable.ButtonPressedHighlight;
			using (Brush brush2 = new SolidBrush(buttonPressedHighlight))
			{
				g.FillRectangle(brush2, bounds);
			}
		}

		// Token: 0x060045C4 RID: 17860 RVA: 0x00126728 File Offset: 0x00124928
		private void RenderItemInternal(ToolStripItemRenderEventArgs e, bool useHotBorder)
		{
			Graphics graphics = e.Graphics;
			ToolStripItem item = e.Item;
			Rectangle rectangle = new Rectangle(Point.Empty, item.Size);
			bool flag = false;
			Rectangle clipRect = item.Selected ? item.ContentRectangle : rectangle;
			if (item.BackgroundImage != null)
			{
				ControlPaint.DrawBackgroundImage(graphics, item.BackgroundImage, item.BackColor, item.BackgroundImageLayout, rectangle, clipRect);
			}
			if (item.Pressed)
			{
				this.RenderPressedButtonFill(graphics, rectangle);
				flag = useHotBorder;
			}
			else if (item.Selected)
			{
				this.RenderSelectedButtonFill(graphics, rectangle);
				flag = useHotBorder;
			}
			else if (item.Owner != null && item.BackColor != item.Owner.BackColor)
			{
				using (Brush brush = new SolidBrush(item.BackColor))
				{
					graphics.FillRectangle(brush, rectangle);
				}
			}
			if (flag)
			{
				using (Pen pen = new Pen(this.ColorTable.ButtonSelectedBorder))
				{
					graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
				}
			}
		}

		// Token: 0x060045C5 RID: 17861 RVA: 0x00126860 File Offset: 0x00124A60
		private void ScaleObjectSizesIfNeeded(int currentDeviceDpi)
		{
			if (DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements && this.previousDeviceDpi != currentDeviceDpi)
			{
				ToolStripRenderer.ScaleArrowOffsetsIfNeeded(currentDeviceDpi);
				this.overflowButtonWidth = DpiHelper.LogicalToDeviceUnits(12, currentDeviceDpi);
				this.overflowArrowWidth = DpiHelper.LogicalToDeviceUnits(9, currentDeviceDpi);
				this.overflowArrowHeight = DpiHelper.LogicalToDeviceUnits(5, currentDeviceDpi);
				this.overflowArrowOffsetY = DpiHelper.LogicalToDeviceUnits(8, currentDeviceDpi);
				this.gripPadding = DpiHelper.LogicalToDeviceUnits(4, currentDeviceDpi);
				this.iconWellGradientWidth = DpiHelper.LogicalToDeviceUnits(12, currentDeviceDpi);
				int num = DpiHelper.LogicalToDeviceUnits(1, currentDeviceDpi);
				this.scaledDropDownMenuItemPaintPadding = new Padding(num + 1, 0, num, 0);
				this.previousDeviceDpi = currentDeviceDpi;
				this.isScalingInitialized = true;
				return;
			}
			if (this.isScalingInitialized)
			{
				return;
			}
			if (DpiHelper.IsScalingRequired)
			{
				ToolStripRenderer.ScaleArrowOffsetsIfNeeded();
				this.overflowButtonWidth = DpiHelper.LogicalToDeviceUnitsX(12);
				this.overflowArrowWidth = DpiHelper.LogicalToDeviceUnitsX(9);
				this.overflowArrowHeight = DpiHelper.LogicalToDeviceUnitsY(5);
				this.overflowArrowOffsetY = DpiHelper.LogicalToDeviceUnitsY(8);
				if (DpiHelper.EnableToolStripHighDpiImprovements)
				{
					this.gripPadding = DpiHelper.LogicalToDeviceUnitsY(4);
					this.iconWellGradientWidth = DpiHelper.LogicalToDeviceUnitsX(12);
					int num2 = DpiHelper.LogicalToDeviceUnitsX(1);
					this.scaledDropDownMenuItemPaintPadding = new Padding(num2 + 1, 0, num2, 0);
				}
			}
			this.isScalingInitialized = true;
		}

		// Token: 0x060045C6 RID: 17862 RVA: 0x00126988 File Offset: 0x00124B88
		private Point RenderArrowInternal(Graphics g, Rectangle dropDownRect, ArrowDirection direction, Brush brush)
		{
			Point result = new Point(dropDownRect.Left + dropDownRect.Width / 2, dropDownRect.Top + dropDownRect.Height / 2);
			result.X += dropDownRect.Width % 2;
			Point[] points;
			if (direction <= ArrowDirection.Up)
			{
				if (direction == ArrowDirection.Left)
				{
					points = new Point[]
					{
						new Point(result.X + ToolStripRenderer.Offset2X, result.Y - ToolStripRenderer.Offset2Y - 1),
						new Point(result.X + ToolStripRenderer.Offset2X, result.Y + ToolStripRenderer.Offset2Y + 1),
						new Point(result.X - 1, result.Y)
					};
					goto IL_236;
				}
				if (direction == ArrowDirection.Up)
				{
					points = new Point[]
					{
						new Point(result.X - ToolStripRenderer.Offset2X, result.Y + 1),
						new Point(result.X + ToolStripRenderer.Offset2X + 1, result.Y + 1),
						new Point(result.X, result.Y - ToolStripRenderer.Offset2Y)
					};
					goto IL_236;
				}
			}
			else
			{
				if (direction == ArrowDirection.Right)
				{
					points = new Point[]
					{
						new Point(result.X - ToolStripRenderer.Offset2X, result.Y - ToolStripRenderer.Offset2Y - 1),
						new Point(result.X - ToolStripRenderer.Offset2X, result.Y + ToolStripRenderer.Offset2Y + 1),
						new Point(result.X + 1, result.Y)
					};
					goto IL_236;
				}
				if (direction != ArrowDirection.Down)
				{
				}
			}
			points = new Point[]
			{
				new Point(result.X - ToolStripRenderer.Offset2X, result.Y - 1),
				new Point(result.X + ToolStripRenderer.Offset2X + 1, result.Y - 1),
				new Point(result.X, result.Y + ToolStripRenderer.Offset2Y)
			};
			IL_236:
			g.FillPolygon(brush, points);
			return result;
		}

		// Token: 0x0400266B RID: 9835
		private const int GRIP_PADDING = 4;

		// Token: 0x0400266C RID: 9836
		private int gripPadding = 4;

		// Token: 0x0400266D RID: 9837
		private const int ICON_WELL_GRADIENT_WIDTH = 12;

		// Token: 0x0400266E RID: 9838
		private int iconWellGradientWidth = 12;

		// Token: 0x0400266F RID: 9839
		private static readonly Size onePix = new Size(1, 1);

		// Token: 0x04002670 RID: 9840
		private bool isScalingInitialized;

		// Token: 0x04002671 RID: 9841
		private const int OVERFLOW_BUTTON_WIDTH = 12;

		// Token: 0x04002672 RID: 9842
		private const int OVERFLOW_ARROW_WIDTH = 9;

		// Token: 0x04002673 RID: 9843
		private const int OVERFLOW_ARROW_HEIGHT = 5;

		// Token: 0x04002674 RID: 9844
		private const int OVERFLOW_ARROW_OFFSETY = 8;

		// Token: 0x04002675 RID: 9845
		private int overflowButtonWidth = 12;

		// Token: 0x04002676 RID: 9846
		private int overflowArrowWidth = 9;

		// Token: 0x04002677 RID: 9847
		private int overflowArrowHeight = 5;

		// Token: 0x04002678 RID: 9848
		private int overflowArrowOffsetY = 8;

		// Token: 0x04002679 RID: 9849
		private const int DROP_DOWN_MENU_ITEM_PAINT_PADDING_SIZE = 1;

		// Token: 0x0400267A RID: 9850
		private Padding scaledDropDownMenuItemPaintPadding = new Padding(2, 0, 1, 0);

		// Token: 0x0400267B RID: 9851
		private ProfessionalColorTable professionalColorTable;

		// Token: 0x0400267C RID: 9852
		private bool roundedEdges = true;

		// Token: 0x0400267D RID: 9853
		private ToolStripRenderer toolStripHighContrastRenderer;

		// Token: 0x0400267E RID: 9854
		private ToolStripRenderer toolStripLowResolutionRenderer;
	}
}
