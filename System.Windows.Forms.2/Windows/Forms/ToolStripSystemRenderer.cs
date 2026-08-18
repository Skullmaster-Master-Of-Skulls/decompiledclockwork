using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x02000408 RID: 1032
	public class ToolStripSystemRenderer : ToolStripRenderer
	{
		// Token: 0x06004750 RID: 18256 RVA: 0x0012B39E File Offset: 0x0012959E
		public ToolStripSystemRenderer()
		{
		}

		// Token: 0x06004751 RID: 18257 RVA: 0x0012B3A6 File Offset: 0x001295A6
		internal ToolStripSystemRenderer(bool isDefault) : base(isDefault)
		{
		}

		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x06004752 RID: 18258 RVA: 0x0012B3AF File Offset: 0x001295AF
		internal override ToolStripRenderer RendererOverride
		{
			get
			{
				if (DisplayInformation.HighContrast)
				{
					return this.HighContrastRenderer;
				}
				return null;
			}
		}

		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x06004753 RID: 18259 RVA: 0x0012B3C0 File Offset: 0x001295C0
		internal ToolStripRenderer HighContrastRenderer
		{
			get
			{
				if (this.toolStripHighContrastRenderer == null)
				{
					this.toolStripHighContrastRenderer = new ToolStripHighContrastRenderer(!AccessibilityImprovements.Level5);
				}
				return this.toolStripHighContrastRenderer;
			}
		}

		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x06004754 RID: 18260 RVA: 0x0012B3E3 File Offset: 0x001295E3
		private static VisualStyleRenderer VisualStyleRenderer
		{
			get
			{
				if (Application.RenderWithVisualStyles)
				{
					if (ToolStripSystemRenderer.renderer == null && VisualStyleRenderer.IsElementDefined(VisualStyleElement.ToolBar.Button.Normal))
					{
						ToolStripSystemRenderer.renderer = new VisualStyleRenderer(VisualStyleElement.ToolBar.Button.Normal);
					}
				}
				else
				{
					ToolStripSystemRenderer.renderer = null;
				}
				return ToolStripSystemRenderer.renderer;
			}
		}

		// Token: 0x06004755 RID: 18261 RVA: 0x0012B41C File Offset: 0x0012961C
		private static void FillBackground(Graphics g, Rectangle bounds, Color backColor)
		{
			if (backColor.IsSystemColor)
			{
				g.FillRectangle(SystemBrushes.FromSystemColor(backColor), bounds);
				return;
			}
			using (Brush brush = new SolidBrush(backColor))
			{
				g.FillRectangle(brush, bounds);
			}
		}

		// Token: 0x06004756 RID: 18262 RVA: 0x0012B46C File Offset: 0x0012966C
		private static bool GetPen(Color color, ref Pen pen)
		{
			if (color.IsSystemColor)
			{
				pen = SystemPens.FromSystemColor(color);
				return false;
			}
			pen = new Pen(color);
			return true;
		}

		// Token: 0x06004757 RID: 18263 RVA: 0x0012B48A File Offset: 0x0012968A
		private static int GetItemState(ToolStripItem item)
		{
			return (int)ToolStripSystemRenderer.GetToolBarState(item);
		}

		// Token: 0x06004758 RID: 18264 RVA: 0x0012B492 File Offset: 0x00129692
		private static int GetSplitButtonDropDownItemState(ToolStripSplitButton item)
		{
			return (int)ToolStripSystemRenderer.GetSplitButtonToolBarState(item, true);
		}

		// Token: 0x06004759 RID: 18265 RVA: 0x0012B49B File Offset: 0x0012969B
		private static int GetSplitButtonItemState(ToolStripSplitButton item)
		{
			return (int)ToolStripSystemRenderer.GetSplitButtonToolBarState(item, false);
		}

		// Token: 0x0600475A RID: 18266 RVA: 0x0012B4A4 File Offset: 0x001296A4
		private static ToolBarState GetSplitButtonToolBarState(ToolStripSplitButton button, bool dropDownButton)
		{
			ToolBarState result = ToolBarState.Normal;
			if (button != null)
			{
				if (!button.Enabled)
				{
					result = ToolBarState.Disabled;
				}
				else if (dropDownButton)
				{
					if (button.DropDownButtonPressed || button.ButtonPressed)
					{
						result = ToolBarState.Pressed;
					}
					else if (button.DropDownButtonSelected || button.ButtonSelected)
					{
						result = ToolBarState.Hot;
					}
				}
				else if (button.ButtonPressed)
				{
					result = ToolBarState.Pressed;
				}
				else if (button.ButtonSelected)
				{
					result = ToolBarState.Hot;
				}
			}
			return result;
		}

		// Token: 0x0600475B RID: 18267 RVA: 0x0012B504 File Offset: 0x00129704
		private static ToolBarState GetToolBarState(ToolStripItem item)
		{
			ToolBarState result = ToolBarState.Normal;
			if (item != null)
			{
				if (!item.Enabled)
				{
					result = ToolBarState.Disabled;
				}
				if (item is ToolStripButton && ((ToolStripButton)item).Checked)
				{
					if (((ToolStripButton)item).Selected && AccessibilityImprovements.Level1)
					{
						result = ToolBarState.Hot;
					}
					else
					{
						result = ToolBarState.Checked;
					}
				}
				else if (item.Pressed)
				{
					result = ToolBarState.Pressed;
				}
				else if (item.Selected)
				{
					result = ToolBarState.Hot;
				}
			}
			return result;
		}

		// Token: 0x0600475C RID: 18268 RVA: 0x0012B568 File Offset: 0x00129768
		protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{
			ToolStrip toolStrip = e.ToolStrip;
			Graphics graphics = e.Graphics;
			Rectangle affectedBounds = e.AffectedBounds;
			if (!base.ShouldPaintBackground(toolStrip))
			{
				return;
			}
			if (toolStrip is StatusStrip)
			{
				ToolStripSystemRenderer.RenderStatusStripBackground(e);
				return;
			}
			if (DisplayInformation.HighContrast)
			{
				ToolStripSystemRenderer.FillBackground(graphics, affectedBounds, SystemColors.ButtonFace);
				return;
			}
			if (DisplayInformation.LowResolution)
			{
				ToolStripSystemRenderer.FillBackground(graphics, affectedBounds, (toolStrip is ToolStripDropDown) ? SystemColors.ControlLight : e.BackColor);
				return;
			}
			if (toolStrip.IsDropDown)
			{
				ToolStripSystemRenderer.FillBackground(graphics, affectedBounds, (!ToolStripManager.VisualStylesEnabled) ? e.BackColor : SystemColors.Menu);
				return;
			}
			if (toolStrip is MenuStrip)
			{
				ToolStripSystemRenderer.FillBackground(graphics, affectedBounds, (!ToolStripManager.VisualStylesEnabled) ? e.BackColor : SystemColors.MenuBar);
				return;
			}
			if (ToolStripManager.VisualStylesEnabled && VisualStyleRenderer.IsElementDefined(VisualStyleElement.Rebar.Band.Normal))
			{
				VisualStyleRenderer visualStyleRenderer = ToolStripSystemRenderer.VisualStyleRenderer;
				visualStyleRenderer.SetParameters(VisualStyleElement.ToolBar.Bar.Normal);
				visualStyleRenderer.DrawBackground(graphics, affectedBounds);
				return;
			}
			ToolStripSystemRenderer.FillBackground(graphics, affectedBounds, (!ToolStripManager.VisualStylesEnabled) ? e.BackColor : SystemColors.MenuBar);
		}

		// Token: 0x0600475D RID: 18269 RVA: 0x0012B66C File Offset: 0x0012986C
		protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
			ToolStrip toolStrip = e.ToolStrip;
			Graphics graphics = e.Graphics;
			Rectangle clientRectangle = e.ToolStrip.ClientRectangle;
			if (toolStrip is StatusStrip)
			{
				this.RenderStatusStripBorder(e);
				return;
			}
			if (toolStrip is ToolStripDropDown)
			{
				ToolStripDropDown toolStripDropDown = toolStrip as ToolStripDropDown;
				if (toolStripDropDown.DropShadowEnabled && ToolStripManager.VisualStylesEnabled)
				{
					clientRectangle.Width--;
					clientRectangle.Height--;
					e.Graphics.DrawRectangle(new Pen(SystemColors.ControlDark), clientRectangle);
					return;
				}
				ControlPaint.DrawBorder3D(e.Graphics, clientRectangle, Border3DStyle.Raised);
				return;
			}
			else
			{
				if (ToolStripManager.VisualStylesEnabled)
				{
					e.Graphics.DrawLine(SystemPens.ButtonHighlight, 0, clientRectangle.Bottom - 1, clientRectangle.Width, clientRectangle.Bottom - 1);
					e.Graphics.DrawLine(SystemPens.InactiveBorder, 0, clientRectangle.Bottom - 2, clientRectangle.Width, clientRectangle.Bottom - 2);
					return;
				}
				e.Graphics.DrawLine(SystemPens.ButtonHighlight, 0, clientRectangle.Bottom - 1, clientRectangle.Width, clientRectangle.Bottom - 1);
				e.Graphics.DrawLine(SystemPens.ButtonShadow, 0, clientRectangle.Bottom - 2, clientRectangle.Width, clientRectangle.Bottom - 2);
				return;
			}
		}

		// Token: 0x0600475E RID: 18270 RVA: 0x0012B7B4 File Offset: 0x001299B4
		protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
		{
			Graphics graphics = e.Graphics;
			Rectangle bounds = new Rectangle(Point.Empty, e.GripBounds.Size);
			bool flag = e.GripDisplayStyle == ToolStripGripDisplayStyle.Vertical;
			if (ToolStripManager.VisualStylesEnabled && VisualStyleRenderer.IsElementDefined(VisualStyleElement.Rebar.Gripper.Normal))
			{
				VisualStyleRenderer visualStyleRenderer = ToolStripSystemRenderer.VisualStyleRenderer;
				if (flag)
				{
					visualStyleRenderer.SetParameters(VisualStyleElement.Rebar.Gripper.Normal);
					bounds.Height = (bounds.Height - 2) / 4 * 4;
					bounds.Y = Math.Max(0, (e.GripBounds.Height - bounds.Height - 2) / 2);
				}
				else
				{
					visualStyleRenderer.SetParameters(VisualStyleElement.Rebar.GripperVertical.Normal);
				}
				visualStyleRenderer.DrawBackground(graphics, bounds);
				return;
			}
			Color backColor = e.ToolStrip.BackColor;
			ToolStripSystemRenderer.FillBackground(graphics, bounds, backColor);
			if (flag)
			{
				if (bounds.Height >= 4)
				{
					bounds.Inflate(0, -2);
				}
				bounds.Width = 3;
			}
			else
			{
				if (bounds.Width >= 4)
				{
					bounds.Inflate(-2, 0);
				}
				bounds.Height = 3;
			}
			this.RenderSmall3DBorderInternal(graphics, bounds, ToolBarState.Hot, e.ToolStrip.RightToLeft == RightToLeft.Yes);
		}

		// Token: 0x0600475F RID: 18271 RVA: 0x000072B6 File Offset: 0x000054B6
		protected override void OnRenderItemBackground(ToolStripItemRenderEventArgs e)
		{
		}

		// Token: 0x06004760 RID: 18272 RVA: 0x000072B6 File Offset: 0x000054B6
		protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
		{
		}

		// Token: 0x06004761 RID: 18273 RVA: 0x0012B8D2 File Offset: 0x00129AD2
		protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
		{
			if (AccessibilityImprovements.Level5 && this.RendererOverride != null)
			{
				base.OnRenderButtonBackground(e);
				return;
			}
			this.RenderItemInternal(e);
		}

		// Token: 0x06004762 RID: 18274 RVA: 0x0012B8F2 File Offset: 0x00129AF2
		protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
		{
			if (AccessibilityImprovements.Level5 && this.RendererOverride != null)
			{
				base.OnRenderDropDownButtonBackground(e);
				return;
			}
			this.RenderItemInternal(e);
		}

		// Token: 0x06004763 RID: 18275 RVA: 0x0012B914 File Offset: 0x00129B14
		protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
		{
			ToolStripItem item = e.Item;
			Graphics graphics = e.Graphics;
			if (ToolStripManager.VisualStylesEnabled && VisualStyleRenderer.IsElementDefined(VisualStyleElement.Rebar.Chevron.Normal))
			{
				VisualStyleElement normal = VisualStyleElement.Rebar.Chevron.Normal;
				VisualStyleRenderer visualStyleRenderer = ToolStripSystemRenderer.VisualStyleRenderer;
				visualStyleRenderer.SetParameters(normal.ClassName, normal.Part, ToolStripSystemRenderer.GetItemState(item));
				visualStyleRenderer.DrawBackground(graphics, new Rectangle(Point.Empty, item.Size));
				return;
			}
			this.RenderItemInternal(e);
			Color arrowColor = item.Enabled ? SystemColors.ControlText : SystemColors.ControlDark;
			base.DrawArrow(new ToolStripArrowRenderEventArgs(graphics, item, new Rectangle(Point.Empty, item.Size), arrowColor, ArrowDirection.Down));
		}

		// Token: 0x06004764 RID: 18276 RVA: 0x0012B9BC File Offset: 0x00129BBC
		protected override void OnRenderLabelBackground(ToolStripItemRenderEventArgs e)
		{
			ToolStripSystemRenderer.RenderLabelInternal(e);
		}

		// Token: 0x06004765 RID: 18277 RVA: 0x0012B9C4 File Offset: 0x00129BC4
		protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
		{
			ToolStripMenuItem toolStripMenuItem = e.Item as ToolStripMenuItem;
			Graphics graphics = e.Graphics;
			if (toolStripMenuItem is MdiControlStrip.SystemMenuItem)
			{
				return;
			}
			if (toolStripMenuItem != null)
			{
				Rectangle bounds = new Rectangle(Point.Empty, toolStripMenuItem.Size);
				if (toolStripMenuItem.IsTopLevel && !ToolStripManager.VisualStylesEnabled)
				{
					if (toolStripMenuItem.BackgroundImage != null)
					{
						ControlPaint.DrawBackgroundImage(graphics, toolStripMenuItem.BackgroundImage, toolStripMenuItem.BackColor, toolStripMenuItem.BackgroundImageLayout, toolStripMenuItem.ContentRectangle, toolStripMenuItem.ContentRectangle);
					}
					else if (toolStripMenuItem.RawBackColor != Color.Empty)
					{
						ToolStripSystemRenderer.FillBackground(graphics, toolStripMenuItem.ContentRectangle, toolStripMenuItem.BackColor);
					}
					ToolBarState toolBarState = ToolStripSystemRenderer.GetToolBarState(toolStripMenuItem);
					this.RenderSmall3DBorderInternal(graphics, bounds, toolBarState, toolStripMenuItem.RightToLeft == RightToLeft.Yes);
					return;
				}
				Rectangle rectangle = new Rectangle(Point.Empty, toolStripMenuItem.Size);
				if (toolStripMenuItem.IsOnDropDown)
				{
					rectangle.X += 2;
					rectangle.Width -= 3;
				}
				if (toolStripMenuItem.Selected || toolStripMenuItem.Pressed)
				{
					if (!AccessibilityImprovements.Level1 || toolStripMenuItem.Enabled)
					{
						graphics.FillRectangle(SystemBrushes.Highlight, rectangle);
					}
					if (!AccessibilityImprovements.Level1)
					{
						return;
					}
					Color color = ToolStripManager.VisualStylesEnabled ? SystemColors.Highlight : ProfessionalColors.MenuItemBorder;
					using (Pen pen = new Pen(color))
					{
						graphics.DrawRectangle(pen, bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
						return;
					}
				}
				if (toolStripMenuItem.BackgroundImage != null)
				{
					ControlPaint.DrawBackgroundImage(graphics, toolStripMenuItem.BackgroundImage, toolStripMenuItem.BackColor, toolStripMenuItem.BackgroundImageLayout, toolStripMenuItem.ContentRectangle, rectangle);
					return;
				}
				if (!ToolStripManager.VisualStylesEnabled && toolStripMenuItem.RawBackColor != Color.Empty)
				{
					ToolStripSystemRenderer.FillBackground(graphics, rectangle, toolStripMenuItem.BackColor);
				}
			}
		}

		// Token: 0x06004766 RID: 18278 RVA: 0x0012BBA4 File Offset: 0x00129DA4
		protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
		{
			this.RenderSeparatorInternal(e.Graphics, e.Item, new Rectangle(Point.Empty, e.Item.Size), e.Vertical);
		}

		// Token: 0x06004767 RID: 18279 RVA: 0x0012BBD4 File Offset: 0x00129DD4
		protected override void OnRenderToolStripStatusLabelBackground(ToolStripItemRenderEventArgs e)
		{
			ToolStripSystemRenderer.RenderLabelInternal(e);
			ToolStripStatusLabel toolStripStatusLabel = e.Item as ToolStripStatusLabel;
			ControlPaint.DrawBorder3D(e.Graphics, new Rectangle(0, 0, toolStripStatusLabel.Width - 1, toolStripStatusLabel.Height - 1), toolStripStatusLabel.BorderStyle, (Border3DSide)toolStripStatusLabel.BorderSides);
		}

		// Token: 0x06004768 RID: 18280 RVA: 0x0012BC24 File Offset: 0x00129E24
		protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
		{
			if (AccessibilityImprovements.Level5 && this.RendererOverride != null)
			{
				base.OnRenderSplitButtonBackground(e);
				return;
			}
			ToolStripSplitButton toolStripSplitButton = e.Item as ToolStripSplitButton;
			Graphics graphics = e.Graphics;
			bool flag = toolStripSplitButton.RightToLeft == RightToLeft.Yes;
			Color arrowColor = toolStripSplitButton.Enabled ? SystemColors.ControlText : SystemColors.ControlDark;
			VisualStyleElement visualStyleElement = flag ? VisualStyleElement.ToolBar.SplitButton.Normal : VisualStyleElement.ToolBar.SplitButtonDropDown.Normal;
			VisualStyleElement visualStyleElement2 = flag ? VisualStyleElement.ToolBar.DropDownButton.Normal : VisualStyleElement.ToolBar.SplitButton.Normal;
			Rectangle rectangle = new Rectangle(Point.Empty, toolStripSplitButton.Size);
			if (ToolStripManager.VisualStylesEnabled && VisualStyleRenderer.IsElementDefined(visualStyleElement) && VisualStyleRenderer.IsElementDefined(visualStyleElement2))
			{
				VisualStyleRenderer visualStyleRenderer = ToolStripSystemRenderer.VisualStyleRenderer;
				visualStyleRenderer.SetParameters(visualStyleElement2.ClassName, visualStyleElement2.Part, ToolStripSystemRenderer.GetSplitButtonItemState(toolStripSplitButton));
				Rectangle buttonBounds = toolStripSplitButton.ButtonBounds;
				if (flag)
				{
					buttonBounds.Inflate(2, 0);
				}
				visualStyleRenderer.DrawBackground(graphics, buttonBounds);
				visualStyleRenderer.SetParameters(visualStyleElement.ClassName, visualStyleElement.Part, ToolStripSystemRenderer.GetSplitButtonDropDownItemState(toolStripSplitButton));
				visualStyleRenderer.DrawBackground(graphics, toolStripSplitButton.DropDownButtonBounds);
				Rectangle contentRectangle = toolStripSplitButton.ContentRectangle;
				if (toolStripSplitButton.BackgroundImage != null)
				{
					ControlPaint.DrawBackgroundImage(graphics, toolStripSplitButton.BackgroundImage, toolStripSplitButton.BackColor, toolStripSplitButton.BackgroundImageLayout, contentRectangle, contentRectangle);
				}
				this.RenderSeparatorInternal(graphics, toolStripSplitButton, toolStripSplitButton.SplitterBounds, true);
				if (flag || toolStripSplitButton.BackgroundImage != null)
				{
					base.DrawArrow(new ToolStripArrowRenderEventArgs(graphics, toolStripSplitButton, toolStripSplitButton.DropDownButtonBounds, arrowColor, ArrowDirection.Down));
					return;
				}
			}
			else
			{
				Rectangle buttonBounds2 = toolStripSplitButton.ButtonBounds;
				if (toolStripSplitButton.BackgroundImage != null)
				{
					Rectangle clipRect = toolStripSplitButton.Selected ? toolStripSplitButton.ContentRectangle : rectangle;
					if (toolStripSplitButton.BackgroundImage != null)
					{
						ControlPaint.DrawBackgroundImage(graphics, toolStripSplitButton.BackgroundImage, toolStripSplitButton.BackColor, toolStripSplitButton.BackgroundImageLayout, rectangle, clipRect);
					}
				}
				else
				{
					ToolStripSystemRenderer.FillBackground(graphics, buttonBounds2, toolStripSplitButton.BackColor);
				}
				ToolBarState splitButtonToolBarState = ToolStripSystemRenderer.GetSplitButtonToolBarState(toolStripSplitButton, false);
				this.RenderSmall3DBorderInternal(graphics, buttonBounds2, splitButtonToolBarState, flag);
				Rectangle dropDownButtonBounds = toolStripSplitButton.DropDownButtonBounds;
				if (toolStripSplitButton.BackgroundImage == null)
				{
					ToolStripSystemRenderer.FillBackground(graphics, dropDownButtonBounds, toolStripSplitButton.BackColor);
				}
				splitButtonToolBarState = ToolStripSystemRenderer.GetSplitButtonToolBarState(toolStripSplitButton, true);
				if (splitButtonToolBarState == ToolBarState.Pressed || splitButtonToolBarState == ToolBarState.Hot)
				{
					this.RenderSmall3DBorderInternal(graphics, dropDownButtonBounds, splitButtonToolBarState, flag);
				}
				base.DrawArrow(new ToolStripArrowRenderEventArgs(graphics, toolStripSplitButton, dropDownButtonBounds, arrowColor, ArrowDirection.Down));
			}
		}

		// Token: 0x06004769 RID: 18281 RVA: 0x0012BE58 File Offset: 0x0012A058
		private void RenderItemInternal(ToolStripItemRenderEventArgs e)
		{
			ToolStripItem item = e.Item;
			Graphics graphics = e.Graphics;
			ToolBarState toolBarState = ToolStripSystemRenderer.GetToolBarState(item);
			VisualStyleElement normal = VisualStyleElement.ToolBar.Button.Normal;
			if (ToolStripManager.VisualStylesEnabled && VisualStyleRenderer.IsElementDefined(normal))
			{
				VisualStyleRenderer visualStyleRenderer = ToolStripSystemRenderer.VisualStyleRenderer;
				visualStyleRenderer.SetParameters(normal.ClassName, normal.Part, (int)toolBarState);
				visualStyleRenderer.DrawBackground(graphics, new Rectangle(Point.Empty, item.Size));
				if (AccessibilityImprovements.Level5 && !SystemInformation.HighContrast && (toolBarState == ToolBarState.Hot || toolBarState == ToolBarState.Pressed || toolBarState == ToolBarState.Checked))
				{
					Rectangle clientBounds = item.ClientBounds;
					clientBounds.Height--;
					ControlPaint.DrawBorderSimple(graphics, clientBounds, SystemColors.Highlight, ButtonBorderStyle.Solid);
				}
			}
			else
			{
				this.RenderSmall3DBorderInternal(graphics, new Rectangle(Point.Empty, item.Size), toolBarState, item.RightToLeft == RightToLeft.Yes);
			}
			Rectangle contentRectangle = item.ContentRectangle;
			if (item.BackgroundImage != null)
			{
				ControlPaint.DrawBackgroundImage(graphics, item.BackgroundImage, item.BackColor, item.BackgroundImageLayout, contentRectangle, contentRectangle);
				return;
			}
			ToolStrip currentParent = item.GetCurrentParent();
			if (currentParent != null && toolBarState != ToolBarState.Checked && item.BackColor != currentParent.BackColor)
			{
				ToolStripSystemRenderer.FillBackground(graphics, contentRectangle, item.BackColor);
			}
		}

		// Token: 0x0600476A RID: 18282 RVA: 0x0012BF84 File Offset: 0x0012A184
		private void RenderSeparatorInternal(Graphics g, ToolStripItem item, Rectangle bounds, bool vertical)
		{
			VisualStyleElement visualStyleElement = vertical ? VisualStyleElement.ToolBar.SeparatorHorizontal.Normal : VisualStyleElement.ToolBar.SeparatorVertical.Normal;
			if (ToolStripManager.VisualStylesEnabled && VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				VisualStyleRenderer visualStyleRenderer = ToolStripSystemRenderer.VisualStyleRenderer;
				visualStyleRenderer.SetParameters(visualStyleElement.ClassName, visualStyleElement.Part, ToolStripSystemRenderer.GetItemState(item));
				visualStyleRenderer.DrawBackground(g, bounds);
				return;
			}
			Color foreColor = item.ForeColor;
			Color backColor = item.BackColor;
			Pen controlDark = SystemPens.ControlDark;
			bool pen = ToolStripSystemRenderer.GetPen(foreColor, ref controlDark);
			try
			{
				if (vertical)
				{
					if (bounds.Height >= 4)
					{
						bounds.Inflate(0, -2);
					}
					bool flag = item.RightToLeft == RightToLeft.Yes;
					Pen pen2 = flag ? SystemPens.ButtonHighlight : controlDark;
					Pen pen3 = flag ? controlDark : SystemPens.ButtonHighlight;
					int num = bounds.Width / 2;
					g.DrawLine(pen2, num, bounds.Top, num, bounds.Bottom);
					num++;
					g.DrawLine(pen3, num, bounds.Top, num, bounds.Bottom);
				}
				else
				{
					if (bounds.Width >= 4)
					{
						bounds.Inflate(-2, 0);
					}
					int num2 = bounds.Height / 2;
					g.DrawLine(controlDark, bounds.Left, num2, bounds.Right, num2);
					num2++;
					g.DrawLine(SystemPens.ButtonHighlight, bounds.Left, num2, bounds.Right, num2);
				}
			}
			finally
			{
				if (pen && controlDark != null)
				{
					controlDark.Dispose();
				}
			}
		}

		// Token: 0x0600476B RID: 18283 RVA: 0x0012C100 File Offset: 0x0012A300
		private void RenderSmall3DBorderInternal(Graphics g, Rectangle bounds, ToolBarState state, bool rightToLeft)
		{
			if (state == ToolBarState.Hot || state == ToolBarState.Pressed || state == ToolBarState.Checked)
			{
				Pen pen = (state == ToolBarState.Hot) ? SystemPens.ButtonHighlight : SystemPens.ButtonShadow;
				Pen pen2 = (state == ToolBarState.Hot) ? SystemPens.ButtonShadow : SystemPens.ButtonHighlight;
				Pen pen3 = rightToLeft ? pen2 : pen;
				Pen pen4 = rightToLeft ? pen : pen2;
				g.DrawLine(pen, bounds.Left, bounds.Top, bounds.Right - 1, bounds.Top);
				g.DrawLine(pen3, bounds.Left, bounds.Top, bounds.Left, bounds.Bottom - 1);
				g.DrawLine(pen4, bounds.Right - 1, bounds.Top, bounds.Right - 1, bounds.Bottom - 1);
				g.DrawLine(pen2, bounds.Left, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 1);
			}
		}

		// Token: 0x0600476C RID: 18284 RVA: 0x0012C1EC File Offset: 0x0012A3EC
		private void RenderStatusStripBorder(ToolStripRenderEventArgs e)
		{
			if (!Application.RenderWithVisualStyles)
			{
				e.Graphics.DrawLine(SystemPens.ButtonHighlight, 0, 0, e.ToolStrip.Width, 0);
			}
		}

		// Token: 0x0600476D RID: 18285 RVA: 0x0012C214 File Offset: 0x0012A414
		private static void RenderStatusStripBackground(ToolStripRenderEventArgs e)
		{
			if (Application.RenderWithVisualStyles)
			{
				VisualStyleRenderer visualStyleRenderer = ToolStripSystemRenderer.VisualStyleRenderer;
				visualStyleRenderer.SetParameters(VisualStyleElement.Status.Bar.Normal);
				visualStyleRenderer.DrawBackground(e.Graphics, new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1));
				return;
			}
			if (!SystemInformation.InLockedTerminalSession())
			{
				e.Graphics.Clear(e.BackColor);
			}
		}

		// Token: 0x0600476E RID: 18286 RVA: 0x0012C280 File Offset: 0x0012A480
		private static void RenderLabelInternal(ToolStripItemRenderEventArgs e)
		{
			ToolStripItem item = e.Item;
			Graphics graphics = e.Graphics;
			Rectangle contentRectangle = item.ContentRectangle;
			if (item.BackgroundImage != null)
			{
				ControlPaint.DrawBackgroundImage(graphics, item.BackgroundImage, item.BackColor, item.BackgroundImageLayout, contentRectangle, contentRectangle);
				return;
			}
			VisualStyleRenderer visualStyleRenderer = ToolStripSystemRenderer.VisualStyleRenderer;
			if (visualStyleRenderer == null || item.BackColor != SystemColors.Control)
			{
				ToolStripSystemRenderer.FillBackground(graphics, contentRectangle, item.BackColor);
			}
		}

		// Token: 0x040026E6 RID: 9958
		[ThreadStatic]
		private static VisualStyleRenderer renderer;

		// Token: 0x040026E7 RID: 9959
		private ToolStripRenderer toolStripHighContrastRenderer;
	}
}
