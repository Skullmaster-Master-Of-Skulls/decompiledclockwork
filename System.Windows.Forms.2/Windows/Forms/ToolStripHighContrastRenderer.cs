using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace System.Windows.Forms
{
	// Token: 0x020003C5 RID: 965
	internal class ToolStripHighContrastRenderer : ToolStripSystemRenderer
	{
		// Token: 0x06004166 RID: 16742 RVA: 0x00117B2F File Offset: 0x00115D2F
		public ToolStripHighContrastRenderer(bool systemRenderMode)
		{
			this.options[ToolStripHighContrastRenderer.optionsDottedBorder | ToolStripHighContrastRenderer.optionsDottedGrip | ToolStripHighContrastRenderer.optionsFillWhenSelected] = !systemRenderMode;
		}

		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x06004167 RID: 16743 RVA: 0x00117B57 File Offset: 0x00115D57
		public bool DottedBorder
		{
			get
			{
				return this.options[ToolStripHighContrastRenderer.optionsDottedBorder];
			}
		}

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x06004168 RID: 16744 RVA: 0x00117B69 File Offset: 0x00115D69
		public bool DottedGrip
		{
			get
			{
				return this.options[ToolStripHighContrastRenderer.optionsDottedGrip];
			}
		}

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x06004169 RID: 16745 RVA: 0x00117B7B File Offset: 0x00115D7B
		public bool FillWhenSelected
		{
			get
			{
				return this.options[ToolStripHighContrastRenderer.optionsFillWhenSelected];
			}
		}

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x0600416A RID: 16746 RVA: 0x00015ECC File Offset: 0x000140CC
		internal override ToolStripRenderer RendererOverride
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600416B RID: 16747 RVA: 0x00117B8D File Offset: 0x00115D8D
		protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
		{
			base.OnRenderArrow(e);
		}

		// Token: 0x0600416C RID: 16748 RVA: 0x00117B98 File Offset: 0x00115D98
		protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
		{
			if (this.DottedGrip)
			{
				Graphics graphics = e.Graphics;
				Rectangle gripBounds = e.GripBounds;
				ToolStrip toolStrip = e.ToolStrip;
				int num = (toolStrip.Orientation == Orientation.Horizontal) ? gripBounds.Height : gripBounds.Width;
				int num2 = (toolStrip.Orientation == Orientation.Horizontal) ? gripBounds.Width : gripBounds.Height;
				int num3 = (num - 8) / 4;
				if (num3 > 0)
				{
					Rectangle[] array = new Rectangle[num3];
					int num4 = 4;
					int num5 = num2 / 2;
					for (int i = 0; i < num3; i++)
					{
						array[i] = ((toolStrip.Orientation == Orientation.Horizontal) ? new Rectangle(num5, num4, 2, 2) : new Rectangle(num4, num5, 2, 2));
						num4 += 4;
					}
					graphics.FillRectangles(SystemBrushes.ControlLight, array);
					return;
				}
			}
			else
			{
				base.OnRenderGrip(e);
			}
		}

		// Token: 0x0600416D RID: 16749 RVA: 0x00117C6C File Offset: 0x00115E6C
		protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
		{
			if (this.FillWhenSelected)
			{
				this.RenderItemInternalFilled(e, false);
				return;
			}
			base.OnRenderDropDownButtonBackground(e);
			if (e.Item.Pressed)
			{
				e.Graphics.DrawRectangle(SystemPens.ButtonHighlight, new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1));
			}
		}

		// Token: 0x0600416E RID: 16750 RVA: 0x00117CD0 File Offset: 0x00115ED0
		protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
		{
			if (AccessibilityImprovements.Level1)
			{
				Color oldColor = Color.FromArgb(255, 4, 2, 4);
				ColorMap[] array = new ColorMap[]
				{
					new ColorMap()
				};
				array[0].OldColor = oldColor;
				array[0].NewColor = (((e.Item.Selected || e.Item.Pressed) && e.Item.Enabled) ? SystemColors.HighlightText : SystemColors.MenuText);
				ImageAttributes imageAttributes = e.ImageAttributes ?? new ImageAttributes();
				imageAttributes.SetRemapTable(array, ColorAdjustType.Bitmap);
				e.ImageAttributes = imageAttributes;
			}
			base.OnRenderItemCheck(e);
		}

		// Token: 0x0600416F RID: 16751 RVA: 0x000072B6 File Offset: 0x000054B6
		protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
		{
		}

		// Token: 0x06004170 RID: 16752 RVA: 0x00117D6E File Offset: 0x00115F6E
		protected override void OnRenderItemBackground(ToolStripItemRenderEventArgs e)
		{
			base.OnRenderItemBackground(e);
		}

		// Token: 0x06004171 RID: 16753 RVA: 0x00117D78 File Offset: 0x00115F78
		protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
		{
			ToolStripSplitButton toolStripSplitButton = e.Item as ToolStripSplitButton;
			Rectangle rect = new Rectangle(Point.Empty, e.Item.Size);
			Graphics graphics = e.Graphics;
			if (toolStripSplitButton != null)
			{
				Rectangle dropDownButtonBounds = toolStripSplitButton.DropDownButtonBounds;
				if (toolStripSplitButton.Pressed)
				{
					graphics.DrawRectangle(SystemPens.ButtonHighlight, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
				}
				else if (toolStripSplitButton.Selected)
				{
					graphics.FillRectangle(SystemBrushes.Highlight, rect);
					graphics.DrawRectangle(SystemPens.ButtonHighlight, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
					graphics.DrawRectangle(SystemPens.ButtonHighlight, dropDownButtonBounds);
				}
				Color arrowColor = (AccessibilityImprovements.Level2 && toolStripSplitButton.Selected && !toolStripSplitButton.Pressed) ? SystemColors.HighlightText : SystemColors.ControlText;
				base.DrawArrow(new ToolStripArrowRenderEventArgs(graphics, toolStripSplitButton, dropDownButtonBounds, arrowColor, ArrowDirection.Down));
			}
		}

		// Token: 0x06004172 RID: 16754 RVA: 0x00117E73 File Offset: 0x00116073
		protected override void OnRenderStatusStripSizingGrip(ToolStripRenderEventArgs e)
		{
			base.OnRenderStatusStripSizingGrip(e);
		}

		// Token: 0x06004173 RID: 16755 RVA: 0x00117E7C File Offset: 0x0011607C
		protected override void OnRenderLabelBackground(ToolStripItemRenderEventArgs e)
		{
			if (this.FillWhenSelected)
			{
				this.RenderItemInternalFilled(e);
				return;
			}
			base.OnRenderLabelBackground(e);
		}

		// Token: 0x06004174 RID: 16756 RVA: 0x00117E98 File Offset: 0x00116098
		protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
		{
			base.OnRenderMenuItemBackground(e);
			if (!e.Item.IsOnDropDown && e.Item.Pressed)
			{
				e.Graphics.DrawRectangle(SystemPens.ButtonHighlight, 0, 0, e.Item.Width - 1, e.Item.Height - 1);
			}
		}

		// Token: 0x06004175 RID: 16757 RVA: 0x00117EF4 File Offset: 0x001160F4
		protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
		{
			if (this.FillWhenSelected)
			{
				this.RenderItemInternalFilled(e, false);
				ToolStripItem item = e.Item;
				Graphics graphics = e.Graphics;
				Color arrowColor = item.Enabled ? SystemColors.ControlText : SystemColors.ControlDark;
				base.DrawArrow(new ToolStripArrowRenderEventArgs(graphics, item, new Rectangle(Point.Empty, item.Size), arrowColor, ArrowDirection.Down));
				return;
			}
			base.OnRenderOverflowButtonBackground(e);
		}

		// Token: 0x06004176 RID: 16758 RVA: 0x00117F5C File Offset: 0x0011615C
		protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
		{
			if (AccessibilityImprovements.Level2 && e.Item.Selected && (!e.Item.Pressed || e.Item is ToolStripButton))
			{
				e.DefaultTextColor = SystemColors.HighlightText;
			}
			else if (e.TextColor != SystemColors.HighlightText && e.TextColor != SystemColors.ControlText)
			{
				if (e.Item.Selected || e.Item.Pressed)
				{
					e.DefaultTextColor = SystemColors.HighlightText;
				}
				else
				{
					e.DefaultTextColor = SystemColors.ControlText;
				}
			}
			if (AccessibilityImprovements.Level1 && typeof(ToolStripButton).IsAssignableFrom(e.Item.GetType()) && ((ToolStripButton)e.Item).DisplayStyle != ToolStripItemDisplayStyle.Image && ((ToolStripButton)e.Item).Checked)
			{
				e.TextColor = SystemColors.HighlightText;
			}
			base.OnRenderItemText(e);
		}

		// Token: 0x06004177 RID: 16759 RVA: 0x000072B6 File Offset: 0x000054B6
		protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{
		}

		// Token: 0x06004178 RID: 16760 RVA: 0x00118054 File Offset: 0x00116254
		protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
			Rectangle rectangle = new Rectangle(Point.Empty, e.ToolStrip.Size);
			Graphics graphics = e.Graphics;
			if (e.ToolStrip is ToolStripDropDown)
			{
				graphics.DrawRectangle(SystemPens.ButtonHighlight, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
				if (!(e.ToolStrip is ToolStripOverflow))
				{
					graphics.FillRectangle(SystemBrushes.Control, e.ConnectedArea);
					return;
				}
			}
			else if (!(e.ToolStrip is MenuStrip))
			{
				if (e.ToolStrip is StatusStrip)
				{
					graphics.DrawRectangle(SystemPens.ButtonShadow, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
					return;
				}
				this.RenderToolStripBackgroundInternal(e);
			}
		}

		// Token: 0x06004179 RID: 16761 RVA: 0x00118124 File Offset: 0x00116324
		private void RenderToolStripBackgroundInternal(ToolStripRenderEventArgs e)
		{
			Rectangle rect = new Rectangle(Point.Empty, e.ToolStrip.Size);
			Graphics graphics = e.Graphics;
			if (this.DottedBorder)
			{
				using (Pen pen = new Pen(SystemColors.ButtonShadow))
				{
					pen.DashStyle = DashStyle.Dot;
					bool flag = (rect.Width & 1) == 1;
					bool flag2 = (rect.Height & 1) == 1;
					int num = 2;
					graphics.DrawLine(pen, rect.X + num, rect.Y, rect.Width - 1, rect.Y);
					graphics.DrawLine(pen, rect.X + num, rect.Height - 1, rect.Width - 1, rect.Height - 1);
					graphics.DrawLine(pen, rect.X, rect.Y + num, rect.X, rect.Height - 1);
					graphics.DrawLine(pen, rect.Width - 1, rect.Y + num, rect.Width - 1, rect.Height - 1);
					graphics.FillRectangle(SystemBrushes.ButtonShadow, new Rectangle(1, 1, 1, 1));
					if (flag)
					{
						graphics.FillRectangle(SystemBrushes.ButtonShadow, new Rectangle(rect.Width - 2, 1, 1, 1));
					}
					if (flag2)
					{
						graphics.FillRectangle(SystemBrushes.ButtonShadow, new Rectangle(1, rect.Height - 2, 1, 1));
					}
					if (flag2 && flag)
					{
						graphics.FillRectangle(SystemBrushes.ButtonShadow, new Rectangle(rect.Width - 2, rect.Height - 2, 1, 1));
					}
					return;
				}
			}
			rect.Width--;
			rect.Height--;
			graphics.DrawRectangle(SystemPens.ButtonShadow, rect);
		}

		// Token: 0x0600417A RID: 16762 RVA: 0x00118304 File Offset: 0x00116504
		protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
		{
			Pen buttonShadow = SystemPens.ButtonShadow;
			Graphics graphics = e.Graphics;
			Rectangle rectangle = new Rectangle(Point.Empty, e.Item.Size);
			if (e.Vertical)
			{
				if (rectangle.Height >= 8)
				{
					rectangle.Inflate(0, -4);
				}
				int num = rectangle.Width / 2;
				graphics.DrawLine(buttonShadow, num, rectangle.Top, num, rectangle.Bottom - 1);
				return;
			}
			if (rectangle.Width >= 4)
			{
				rectangle.Inflate(-2, 0);
			}
			int num2 = rectangle.Height / 2;
			graphics.DrawLine(buttonShadow, rectangle.Left, num2, rectangle.Right - 1, num2);
		}

		// Token: 0x0600417B RID: 16763 RVA: 0x001183B0 File Offset: 0x001165B0
		internal static bool IsHighContrastWhiteOnBlack()
		{
			return SystemColors.Control.ToArgb() == Color.Black.ToArgb();
		}

		// Token: 0x0600417C RID: 16764 RVA: 0x001183DC File Offset: 0x001165DC
		protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
		{
			Image image = e.Image;
			if (image != null)
			{
				if (Image.GetPixelFormatSize(image.PixelFormat) > 16)
				{
					base.OnRenderItemImage(e);
					return;
				}
				Graphics graphics = e.Graphics;
				ToolStripItem item = e.Item;
				Rectangle imageRectangle = e.ImageRectangle;
				using (ImageAttributes imageAttributes = new ImageAttributes())
				{
					if (ToolStripHighContrastRenderer.IsHighContrastWhiteOnBlack() && (!this.FillWhenSelected || (!e.Item.Pressed && !e.Item.Selected)))
					{
						ColorMap colorMap = new ColorMap();
						ColorMap colorMap2 = new ColorMap();
						ColorMap colorMap3 = new ColorMap();
						colorMap.OldColor = Color.Black;
						colorMap.NewColor = Color.White;
						colorMap2.OldColor = Color.White;
						colorMap2.NewColor = Color.Black;
						colorMap3.OldColor = Color.FromArgb(0, 0, 128);
						colorMap3.NewColor = Color.White;
						imageAttributes.SetRemapTable(new ColorMap[]
						{
							colorMap,
							colorMap2,
							colorMap3
						}, ColorAdjustType.Bitmap);
					}
					if (item.ImageScaling == ToolStripItemImageScaling.None)
					{
						graphics.DrawImage(image, imageRectangle, 0, 0, imageRectangle.Width, imageRectangle.Height, GraphicsUnit.Pixel, imageAttributes);
					}
					else
					{
						graphics.DrawImage(image, imageRectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
					}
				}
			}
		}

		// Token: 0x0600417D RID: 16765 RVA: 0x00118538 File Offset: 0x00116738
		protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
		{
			if (!this.FillWhenSelected)
			{
				base.OnRenderButtonBackground(e);
				return;
			}
			ToolStripButton toolStripButton = e.Item as ToolStripButton;
			if (toolStripButton == null || !toolStripButton.Checked)
			{
				this.RenderItemInternalFilled(e);
				return;
			}
			Graphics graphics = e.Graphics;
			Rectangle rect = new Rectangle(Point.Empty, e.Item.Size);
			if (toolStripButton.CheckState == CheckState.Checked || AccessibilityImprovements.Level5)
			{
				graphics.FillRectangle(SystemBrushes.Highlight, rect);
			}
			if (toolStripButton.Selected && AccessibilityImprovements.Level1)
			{
				graphics.DrawRectangle(SystemPens.Highlight, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
				return;
			}
			graphics.DrawRectangle(SystemPens.ControlLight, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
		}

		// Token: 0x0600417E RID: 16766 RVA: 0x0011861D File Offset: 0x0011681D
		private void RenderItemInternalFilled(ToolStripItemRenderEventArgs e)
		{
			this.RenderItemInternalFilled(e, true);
		}

		// Token: 0x0600417F RID: 16767 RVA: 0x00118628 File Offset: 0x00116828
		private void RenderItemInternalFilled(ToolStripItemRenderEventArgs e, bool pressFill)
		{
			Graphics graphics = e.Graphics;
			Rectangle rect = new Rectangle(Point.Empty, e.Item.Size);
			if (!e.Item.Pressed)
			{
				if (e.Item.Selected)
				{
					graphics.FillRectangle(SystemBrushes.Highlight, rect);
					graphics.DrawRectangle(SystemPens.ControlLight, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
				}
				return;
			}
			if (pressFill)
			{
				graphics.FillRectangle(SystemBrushes.Highlight, rect);
				return;
			}
			graphics.DrawRectangle(SystemPens.ControlLight, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
		}

		// Token: 0x04002519 RID: 9497
		private const int GRIP_PADDING = 4;

		// Token: 0x0400251A RID: 9498
		private BitVector32 options;

		// Token: 0x0400251B RID: 9499
		private static readonly int optionsDottedBorder = BitVector32.CreateMask();

		// Token: 0x0400251C RID: 9500
		private static readonly int optionsDottedGrip = BitVector32.CreateMask(ToolStripHighContrastRenderer.optionsDottedBorder);

		// Token: 0x0400251D RID: 9501
		private static readonly int optionsFillWhenSelected = BitVector32.CreateMask(ToolStripHighContrastRenderer.optionsDottedGrip);
	}
}
