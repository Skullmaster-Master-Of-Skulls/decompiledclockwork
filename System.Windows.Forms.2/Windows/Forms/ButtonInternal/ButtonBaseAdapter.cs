using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms.Internal;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms.ButtonInternal
{
	// Token: 0x020004BA RID: 1210
	internal abstract class ButtonBaseAdapter
	{
		// Token: 0x06004F8B RID: 20363 RVA: 0x001481F5 File Offset: 0x001463F5
		internal ButtonBaseAdapter(ButtonBase control)
		{
			this.control = control;
		}

		// Token: 0x1700137A RID: 4986
		// (get) Token: 0x06004F8C RID: 20364 RVA: 0x00148204 File Offset: 0x00146404
		protected ButtonBase Control
		{
			get
			{
				return this.control;
			}
		}

		// Token: 0x06004F8D RID: 20365 RVA: 0x0014820C File Offset: 0x0014640C
		internal void Paint(PaintEventArgs pevent)
		{
			if (this.Control.MouseIsDown)
			{
				this.PaintDown(pevent, CheckState.Unchecked);
				return;
			}
			if (this.Control.MouseIsOver)
			{
				this.PaintOver(pevent, CheckState.Unchecked);
				return;
			}
			this.PaintUp(pevent, CheckState.Unchecked);
		}

		// Token: 0x06004F8E RID: 20366 RVA: 0x00148244 File Offset: 0x00146444
		internal virtual Size GetPreferredSizeCore(Size proposedSize)
		{
			Size preferredSizeCore;
			using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
			{
				using (PaintEventArgs paintEventArgs = new PaintEventArgs(graphics, default(Rectangle)))
				{
					ButtonBaseAdapter.LayoutOptions layoutOptions = this.Layout(paintEventArgs);
					preferredSizeCore = layoutOptions.GetPreferredSizeCore(proposedSize);
				}
			}
			return preferredSizeCore;
		}

		// Token: 0x06004F8F RID: 20367
		protected abstract ButtonBaseAdapter.LayoutOptions Layout(PaintEventArgs e);

		// Token: 0x06004F90 RID: 20368
		internal abstract void PaintUp(PaintEventArgs e, CheckState state);

		// Token: 0x06004F91 RID: 20369
		internal abstract void PaintDown(PaintEventArgs e, CheckState state);

		// Token: 0x06004F92 RID: 20370
		internal abstract void PaintOver(PaintEventArgs e, CheckState state);

		// Token: 0x06004F93 RID: 20371 RVA: 0x001482AC File Offset: 0x001464AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected bool IsHighContrastHighlighted()
		{
			return AccessibilityImprovements.Level1 && this.IsHighContrastHighlightedInternal();
		}

		// Token: 0x06004F94 RID: 20372 RVA: 0x001482BD File Offset: 0x001464BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected bool IsHighContrastHighlighted2()
		{
			return AccessibilityImprovements.Level2 && this.IsHighContrastHighlightedInternal();
		}

		// Token: 0x06004F95 RID: 20373 RVA: 0x001482D0 File Offset: 0x001464D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool IsHighContrastHighlightedInternal()
		{
			return SystemInformation.HighContrast && Application.RenderWithVisualStyles && (this.Control.Focused || this.Control.MouseIsOver || (this.Control.IsDefault && this.Control.Enabled));
		}

		// Token: 0x06004F96 RID: 20374 RVA: 0x00148324 File Offset: 0x00146524
		internal static Color MixedColor(Color color1, Color color2)
		{
			byte a = color1.A;
			byte r = color1.R;
			byte g = color1.G;
			byte b = color1.B;
			byte a2 = color2.A;
			byte r2 = color2.R;
			byte g2 = color2.G;
			byte b2 = color2.B;
			int alpha = (int)((a + a2) / 2);
			int red = (int)((r + r2) / 2);
			int green = (int)((g + g2) / 2);
			int blue = (int)((b + b2) / 2);
			return Color.FromArgb(alpha, red, green, blue);
		}

		// Token: 0x06004F97 RID: 20375 RVA: 0x001483A4 File Offset: 0x001465A4
		internal static Brush CreateDitherBrush(Color color1, Color color2)
		{
			Brush result;
			using (Bitmap bitmap = new Bitmap(2, 2))
			{
				bitmap.SetPixel(0, 0, color1);
				bitmap.SetPixel(0, 1, color2);
				bitmap.SetPixel(1, 1, color1);
				bitmap.SetPixel(1, 0, color2);
				result = new TextureBrush(bitmap);
			}
			return result;
		}

		// Token: 0x06004F98 RID: 20376 RVA: 0x00148404 File Offset: 0x00146604
		internal virtual StringFormat CreateStringFormat()
		{
			return ControlPaint.CreateStringFormat(this.Control, this.Control.TextAlign, this.Control.ShowToolTip, this.Control.UseMnemonic);
		}

		// Token: 0x06004F99 RID: 20377 RVA: 0x00148432 File Offset: 0x00146632
		internal virtual TextFormatFlags CreateTextFormatFlags()
		{
			return ControlPaint.CreateTextFormatFlags(this.Control, this.Control.TextAlign, this.Control.ShowToolTip, this.Control.UseMnemonic);
		}

		// Token: 0x06004F9A RID: 20378 RVA: 0x00148460 File Offset: 0x00146660
		internal static void DrawDitheredFill(Graphics g, Color color1, Color color2, Rectangle bounds)
		{
			using (Brush brush = ButtonBaseAdapter.CreateDitherBrush(color1, color2))
			{
				g.FillRectangle(brush, bounds);
			}
		}

		// Token: 0x06004F9B RID: 20379 RVA: 0x0014849C File Offset: 0x0014669C
		protected void Draw3DBorder(Graphics g, Rectangle bounds, ButtonBaseAdapter.ColorData colors, bool raised)
		{
			if (this.Control.BackColor != SystemColors.Control && SystemInformation.HighContrast)
			{
				if (raised)
				{
					this.Draw3DBorderHighContrastRaised(g, ref bounds, colors);
					return;
				}
				ControlPaint.DrawBorder(g, bounds, ControlPaint.Dark(this.Control.BackColor), ButtonBorderStyle.Solid);
				return;
			}
			else
			{
				if (raised)
				{
					this.Draw3DBorderRaised(g, ref bounds, colors);
					return;
				}
				this.Draw3DBorderNormal(g, ref bounds, colors);
				return;
			}
		}

		// Token: 0x06004F9C RID: 20380 RVA: 0x00148508 File Offset: 0x00146708
		private void Draw3DBorderHighContrastRaised(Graphics g, ref Rectangle bounds, ButtonBaseAdapter.ColorData colors)
		{
			bool flag = colors.buttonFace.ToKnownColor() == SystemColors.Control.ToKnownColor();
			bool flag2 = !this.Control.Enabled && SystemInformation.HighContrast && AccessibilityImprovements.Level1;
			using (WindowsGraphics windowsGraphics = WindowsGraphics.FromGraphics(g))
			{
				Point point = new Point(bounds.X + bounds.Width - 1, bounds.Y);
				Point point2 = new Point(bounds.X, bounds.Y);
				Point point3 = new Point(bounds.X, bounds.Y + bounds.Height - 1);
				Point point4 = new Point(bounds.X + bounds.Width - 1, bounds.Y + bounds.Height - 1);
				WindowsPen windowsPen = null;
				WindowsPen windowsPen2 = null;
				WindowsPen windowsPen3 = null;
				WindowsPen windowsPen4 = null;
				try
				{
					if (flag2)
					{
						windowsPen = new WindowsPen(windowsGraphics.DeviceContext, colors.windowDisabled);
					}
					else
					{
						windowsPen = (flag ? new WindowsPen(windowsGraphics.DeviceContext, SystemColors.ControlLightLight) : new WindowsPen(windowsGraphics.DeviceContext, colors.highlight));
					}
					windowsGraphics.DrawLine(windowsPen, point, point2);
					windowsGraphics.DrawLine(windowsPen, point2, point3);
					if (flag2)
					{
						windowsPen2 = new WindowsPen(windowsGraphics.DeviceContext, colors.windowDisabled);
					}
					else
					{
						windowsPen2 = (flag ? new WindowsPen(windowsGraphics.DeviceContext, SystemColors.ControlDarkDark) : new WindowsPen(windowsGraphics.DeviceContext, colors.buttonShadowDark));
					}
					point.Offset(0, -1);
					windowsGraphics.DrawLine(windowsPen2, point3, point4);
					windowsGraphics.DrawLine(windowsPen2, point4, point);
					if (flag)
					{
						if (SystemInformation.HighContrast)
						{
							windowsPen3 = new WindowsPen(windowsGraphics.DeviceContext, SystemColors.ControlLight);
						}
						else
						{
							windowsPen3 = new WindowsPen(windowsGraphics.DeviceContext, SystemColors.Control);
						}
					}
					else if (SystemInformation.HighContrast)
					{
						windowsPen3 = new WindowsPen(windowsGraphics.DeviceContext, colors.highlight);
					}
					else
					{
						windowsPen3 = new WindowsPen(windowsGraphics.DeviceContext, colors.buttonFace);
					}
					point.Offset(-1, 2);
					point2.Offset(1, 1);
					point3.Offset(1, -1);
					point4.Offset(-1, -1);
					windowsGraphics.DrawLine(windowsPen3, point, point2);
					windowsGraphics.DrawLine(windowsPen3, point2, point3);
					if (flag2)
					{
						windowsPen4 = new WindowsPen(windowsGraphics.DeviceContext, colors.windowDisabled);
					}
					else
					{
						windowsPen4 = (flag ? new WindowsPen(windowsGraphics.DeviceContext, SystemColors.ControlDark) : new WindowsPen(windowsGraphics.DeviceContext, colors.buttonShadow));
					}
					point.Offset(0, -1);
					windowsGraphics.DrawLine(windowsPen4, point3, point4);
					windowsGraphics.DrawLine(windowsPen4, point4, point);
				}
				finally
				{
					if (windowsPen != null)
					{
						windowsPen.Dispose();
					}
					if (windowsPen2 != null)
					{
						windowsPen2.Dispose();
					}
					if (windowsPen3 != null)
					{
						windowsPen3.Dispose();
					}
					if (windowsPen4 != null)
					{
						windowsPen4.Dispose();
					}
				}
			}
		}

		// Token: 0x06004F9D RID: 20381 RVA: 0x001487F4 File Offset: 0x001469F4
		private void Draw3DBorderNormal(Graphics g, ref Rectangle bounds, ButtonBaseAdapter.ColorData colors)
		{
			using (WindowsGraphics windowsGraphics = WindowsGraphics.FromGraphics(g))
			{
				Point point = new Point(bounds.X + bounds.Width - 1, bounds.Y);
				Point point2 = new Point(bounds.X, bounds.Y);
				Point point3 = new Point(bounds.X, bounds.Y + bounds.Height - 1);
				Point point4 = new Point(bounds.X + bounds.Width - 1, bounds.Y + bounds.Height - 1);
				WindowsPen windowsPen = new WindowsPen(windowsGraphics.DeviceContext, colors.buttonShadowDark);
				try
				{
					windowsGraphics.DrawLine(windowsPen, point, point2);
					windowsGraphics.DrawLine(windowsPen, point2, point3);
				}
				finally
				{
					windowsPen.Dispose();
				}
				windowsPen = new WindowsPen(windowsGraphics.DeviceContext, colors.highlight);
				try
				{
					point.Offset(0, -1);
					windowsGraphics.DrawLine(windowsPen, point3, point4);
					windowsGraphics.DrawLine(windowsPen, point4, point);
				}
				finally
				{
					windowsPen.Dispose();
				}
				windowsPen = new WindowsPen(windowsGraphics.DeviceContext, colors.buttonFace);
				point.Offset(-1, 2);
				point2.Offset(1, 1);
				point3.Offset(1, -1);
				point4.Offset(-1, -1);
				try
				{
					windowsGraphics.DrawLine(windowsPen, point, point2);
					windowsGraphics.DrawLine(windowsPen, point2, point3);
				}
				finally
				{
					windowsPen.Dispose();
				}
				if (colors.buttonFace.ToKnownColor() == SystemColors.Control.ToKnownColor())
				{
					windowsPen = new WindowsPen(windowsGraphics.DeviceContext, SystemColors.ControlLight);
				}
				else
				{
					windowsPen = new WindowsPen(windowsGraphics.DeviceContext, colors.buttonFace);
				}
				try
				{
					point.Offset(0, -1);
					windowsGraphics.DrawLine(windowsPen, point3, point4);
					windowsGraphics.DrawLine(windowsPen, point4, point);
				}
				finally
				{
					windowsPen.Dispose();
				}
			}
		}

		// Token: 0x06004F9E RID: 20382 RVA: 0x00148A2C File Offset: 0x00146C2C
		private void Draw3DBorderRaised(Graphics g, ref Rectangle bounds, ButtonBaseAdapter.ColorData colors)
		{
			bool flag = colors.buttonFace.ToKnownColor() == SystemColors.Control.ToKnownColor();
			bool flag2 = !this.Control.Enabled && SystemInformation.HighContrast && AccessibilityImprovements.Level1;
			using (WindowsGraphics windowsGraphics = WindowsGraphics.FromGraphics(g))
			{
				Point point = new Point(bounds.X + bounds.Width - 1, bounds.Y);
				Point point2 = new Point(bounds.X, bounds.Y);
				Point point3 = new Point(bounds.X, bounds.Y + bounds.Height - 1);
				Point point4 = new Point(bounds.X + bounds.Width - 1, bounds.Y + bounds.Height - 1);
				WindowsPen windowsPen;
				if (flag2)
				{
					windowsPen = new WindowsPen(windowsGraphics.DeviceContext, colors.windowDisabled);
				}
				else if (flag)
				{
					windowsPen = new WindowsPen(windowsGraphics.DeviceContext, SystemColors.ControlLightLight);
				}
				else
				{
					windowsPen = new WindowsPen(windowsGraphics.DeviceContext, colors.highlight);
				}
				try
				{
					windowsGraphics.DrawLine(windowsPen, point, point2);
					windowsGraphics.DrawLine(windowsPen, point2, point3);
				}
				finally
				{
					windowsPen.Dispose();
				}
				if (flag2)
				{
					windowsPen = new WindowsPen(windowsGraphics.DeviceContext, colors.windowDisabled);
				}
				else if (flag)
				{
					windowsPen = new WindowsPen(windowsGraphics.DeviceContext, SystemColors.ControlDarkDark);
				}
				else
				{
					windowsPen = new WindowsPen(windowsGraphics.DeviceContext, colors.buttonShadowDark);
				}
				try
				{
					point.Offset(0, -1);
					windowsGraphics.DrawLine(windowsPen, point3, point4);
					windowsGraphics.DrawLine(windowsPen, point4, point);
				}
				finally
				{
					windowsPen.Dispose();
				}
				point.Offset(-1, 2);
				point2.Offset(1, 1);
				point3.Offset(1, -1);
				point4.Offset(-1, -1);
				if (flag)
				{
					if (SystemInformation.HighContrast)
					{
						windowsPen = new WindowsPen(windowsGraphics.DeviceContext, SystemColors.ControlLight);
					}
					else
					{
						windowsPen = new WindowsPen(windowsGraphics.DeviceContext, SystemColors.Control);
					}
				}
				else
				{
					windowsPen = new WindowsPen(windowsGraphics.DeviceContext, colors.buttonFace);
				}
				try
				{
					windowsGraphics.DrawLine(windowsPen, point, point2);
					windowsGraphics.DrawLine(windowsPen, point2, point3);
				}
				finally
				{
					windowsPen.Dispose();
				}
				if (flag2)
				{
					windowsPen = new WindowsPen(windowsGraphics.DeviceContext, colors.windowDisabled);
				}
				else if (flag)
				{
					windowsPen = new WindowsPen(windowsGraphics.DeviceContext, SystemColors.ControlDark);
				}
				else
				{
					windowsPen = new WindowsPen(windowsGraphics.DeviceContext, colors.buttonShadow);
				}
				try
				{
					point.Offset(0, -1);
					windowsGraphics.DrawLine(windowsPen, point3, point4);
					windowsGraphics.DrawLine(windowsPen, point4, point);
				}
				finally
				{
					windowsPen.Dispose();
				}
			}
		}

		// Token: 0x06004F9F RID: 20383 RVA: 0x00148D3C File Offset: 0x00146F3C
		protected internal static Color GetContrastingBorderColor(Color buttonBorderShadowColor)
		{
			if (!AccessibilityImprovements.Level5)
			{
				return buttonBorderShadowColor;
			}
			return Color.FromArgb((int)buttonBorderShadowColor.A, (int)((float)buttonBorderShadowColor.R * 0.8f), (int)((float)buttonBorderShadowColor.G * 0.8f), (int)((float)buttonBorderShadowColor.B * 0.8f));
		}

		// Token: 0x06004FA0 RID: 20384 RVA: 0x00148D8C File Offset: 0x00146F8C
		protected internal static void Draw3DLiteBorder(Graphics g, Rectangle r, ButtonBaseAdapter.ColorData colors, bool up)
		{
			using (WindowsGraphics windowsGraphics = WindowsGraphics.FromGraphics(g))
			{
				Point point = new Point(r.Right - 1, r.Top);
				Point point2 = new Point(r.Left, r.Top);
				Point point3 = new Point(r.Left, r.Bottom - 1);
				Point point4 = new Point(r.Right - 1, r.Bottom - 1);
				Color contrastingBorderColor = ButtonBaseAdapter.GetContrastingBorderColor(colors.buttonShadow);
				WindowsPen windowsPen = up ? new WindowsPen(windowsGraphics.DeviceContext, colors.highlight) : new WindowsPen(windowsGraphics.DeviceContext, contrastingBorderColor);
				try
				{
					windowsGraphics.DrawLine(windowsPen, point, point2);
					windowsGraphics.DrawLine(windowsPen, point2, point3);
				}
				finally
				{
					windowsPen.Dispose();
				}
				windowsPen = (up ? new WindowsPen(windowsGraphics.DeviceContext, contrastingBorderColor) : new WindowsPen(windowsGraphics.DeviceContext, colors.highlight));
				try
				{
					point.Offset(0, -1);
					windowsGraphics.DrawLine(windowsPen, point3, point4);
					windowsGraphics.DrawLine(windowsPen, point4, point);
				}
				finally
				{
					windowsPen.Dispose();
				}
			}
		}

		// Token: 0x06004FA1 RID: 20385 RVA: 0x00148ECC File Offset: 0x001470CC
		internal static void DrawFlatBorder(Graphics g, Rectangle r, Color c)
		{
			ControlPaint.DrawBorder(g, r, c, ButtonBorderStyle.Solid);
		}

		// Token: 0x06004FA2 RID: 20386 RVA: 0x00148ED8 File Offset: 0x001470D8
		internal static void DrawFlatBorderWithSize(Graphics g, Rectangle r, Color c, int size)
		{
			bool isSystemColor = c.IsSystemColor;
			SolidBrush solidBrush = null;
			if (size > 1)
			{
				solidBrush = new SolidBrush(c);
			}
			else if (isSystemColor)
			{
				solidBrush = (SolidBrush)SystemBrushes.FromSystemColor(c);
			}
			else
			{
				solidBrush = new SolidBrush(c);
			}
			try
			{
				size = Math.Min(size, Math.Min(r.Width, r.Height));
				g.FillRectangle(solidBrush, r.X, r.Y, size, r.Height);
				g.FillRectangle(solidBrush, r.X + r.Width - size, r.Y, size, r.Height);
				g.FillRectangle(solidBrush, r.X + size, r.Y, r.Width - size * 2, size);
				g.FillRectangle(solidBrush, r.X + size, r.Y + r.Height - size, r.Width - size * 2, size);
			}
			finally
			{
				if (!isSystemColor && solidBrush != null)
				{
					solidBrush.Dispose();
				}
			}
		}

		// Token: 0x06004FA3 RID: 20387 RVA: 0x00148FE4 File Offset: 0x001471E4
		internal static void DrawFlatFocus(Graphics g, Rectangle r, Color c)
		{
			using (WindowsGraphics windowsGraphics = WindowsGraphics.FromGraphics(g))
			{
				using (WindowsPen windowsPen = new WindowsPen(windowsGraphics.DeviceContext, c))
				{
					windowsGraphics.DrawRectangle(windowsPen, r);
				}
			}
		}

		// Token: 0x06004FA4 RID: 20388 RVA: 0x00149040 File Offset: 0x00147240
		private void DrawFocus(Graphics g, Rectangle r)
		{
			if (this.Control.Focused && this.Control.ShowFocusCues)
			{
				ControlPaint.DrawFocusRectangle(g, r, this.Control.ForeColor, this.Control.BackColor);
			}
		}

		// Token: 0x06004FA5 RID: 20389 RVA: 0x00149079 File Offset: 0x00147279
		private void DrawImage(Graphics graphics, ButtonBaseAdapter.LayoutData layout)
		{
			if (this.Control.Image != null)
			{
				this.DrawImageCore(graphics, this.Control.Image, layout.imageBounds, layout.imageStart, layout);
			}
		}

		// Token: 0x06004FA6 RID: 20390 RVA: 0x001490A8 File Offset: 0x001472A8
		internal virtual void DrawImageCore(Graphics graphics, Image image, Rectangle imageBounds, Point imageStart, ButtonBaseAdapter.LayoutData layout)
		{
			Region clip = graphics.Clip;
			if (!layout.options.everettButtonCompat)
			{
				Rectangle rect = new Rectangle(ButtonBaseAdapter.buttonBorderSize, ButtonBaseAdapter.buttonBorderSize, this.Control.Width - 2 * ButtonBaseAdapter.buttonBorderSize, this.Control.Height - 2 * ButtonBaseAdapter.buttonBorderSize);
				Region region = clip.Clone();
				region.Intersect(rect);
				region.Intersect(imageBounds);
				graphics.Clip = region;
			}
			else
			{
				imageBounds.Width++;
				imageBounds.Height++;
				imageBounds.X = imageStart.X + 1;
				imageBounds.Y = imageStart.Y + 1;
			}
			try
			{
				if (!this.Control.Enabled)
				{
					ControlPaint.DrawImageDisabled(graphics, image, imageBounds, this.Control.BackColor, true);
				}
				else
				{
					graphics.DrawImage(image, imageBounds.X, imageBounds.Y, image.Width, image.Height);
				}
			}
			finally
			{
				if (!layout.options.everettButtonCompat)
				{
					graphics.Clip = clip;
				}
			}
		}

		// Token: 0x06004FA7 RID: 20391 RVA: 0x001491CC File Offset: 0x001473CC
		internal static void DrawDefaultBorder(Graphics g, Rectangle r, Color c, bool isDefault)
		{
			if (isDefault)
			{
				r.Inflate(1, 1);
				Pen pen;
				if (c.IsSystemColor)
				{
					pen = SystemPens.FromSystemColor(c);
				}
				else
				{
					pen = new Pen(c);
				}
				g.DrawRectangle(pen, r.X, r.Y, r.Width - 1, r.Height - 1);
				if (!c.IsSystemColor)
				{
					pen.Dispose();
				}
			}
		}

		// Token: 0x06004FA8 RID: 20392 RVA: 0x00149234 File Offset: 0x00147434
		private void DrawText(Graphics g, ButtonBaseAdapter.LayoutData layout, Color c, ButtonBaseAdapter.ColorData colors)
		{
			Rectangle textBounds = layout.textBounds;
			bool shadowedText = layout.options.shadowedText;
			if (this.Control.UseCompatibleTextRendering)
			{
				using (StringFormat stringFormat = this.CreateStringFormat())
				{
					if ((this.Control.TextAlign & (ContentAlignment)546) == (ContentAlignment)0)
					{
						textBounds.X--;
					}
					textBounds.Width++;
					if (shadowedText && !this.Control.Enabled && (!AccessibilityImprovements.Level1 || (!colors.options.highContrast && AccessibilityImprovements.Level1)))
					{
						using (SolidBrush solidBrush = new SolidBrush(colors.highlight))
						{
							textBounds.Offset(1, 1);
							g.DrawString(this.Control.Text, this.Control.Font, solidBrush, textBounds, stringFormat);
							textBounds.Offset(-1, -1);
							solidBrush.Color = colors.buttonShadow;
							g.DrawString(this.Control.Text, this.Control.Font, solidBrush, textBounds, stringFormat);
							return;
						}
					}
					Brush brush;
					if (c.IsSystemColor)
					{
						brush = SystemBrushes.FromSystemColor(c);
					}
					else
					{
						brush = new SolidBrush(c);
					}
					g.DrawString(this.Control.Text, this.Control.Font, brush, textBounds, stringFormat);
					if (!c.IsSystemColor)
					{
						brush.Dispose();
					}
					return;
				}
			}
			TextFormatFlags flags = this.CreateTextFormatFlags();
			if (shadowedText && !this.Control.Enabled && (!AccessibilityImprovements.Level1 || (!colors.options.highContrast && AccessibilityImprovements.Level1)))
			{
				if (Application.RenderWithVisualStyles)
				{
					TextRenderer.DrawText(g, this.Control.Text, this.Control.Font, textBounds, colors.buttonShadow, flags);
					return;
				}
				textBounds.Offset(1, 1);
				TextRenderer.DrawText(g, this.Control.Text, this.Control.Font, textBounds, colors.highlight, flags);
				textBounds.Offset(-1, -1);
				TextRenderer.DrawText(g, this.Control.Text, this.Control.Font, textBounds, colors.buttonShadow, flags);
				return;
			}
			else
			{
				TextRenderer.DrawText(g, this.Control.Text, this.Control.Font, textBounds, c, flags);
			}
		}

		// Token: 0x06004FA9 RID: 20393 RVA: 0x001494D8 File Offset: 0x001476D8
		internal static void PaintButtonBackground(WindowsGraphics wg, Rectangle bounds, WindowsBrush background)
		{
			wg.FillRectangle(background, bounds);
		}

		// Token: 0x06004FAA RID: 20394 RVA: 0x001494E2 File Offset: 0x001476E2
		internal void PaintButtonBackground(PaintEventArgs e, Rectangle bounds, Brush background)
		{
			if (background == null)
			{
				this.Control.PaintBackground(e, bounds);
				return;
			}
			e.Graphics.FillRectangle(background, bounds);
		}

		// Token: 0x06004FAB RID: 20395 RVA: 0x00149504 File Offset: 0x00147704
		internal void PaintField(PaintEventArgs e, ButtonBaseAdapter.LayoutData layout, ButtonBaseAdapter.ColorData colors, Color foreColor, bool drawFocus)
		{
			Graphics graphics = e.Graphics;
			Rectangle focus = layout.focus;
			this.DrawText(graphics, layout, foreColor, colors);
			if (drawFocus)
			{
				this.DrawFocus(graphics, focus);
			}
		}

		// Token: 0x06004FAC RID: 20396 RVA: 0x00149538 File Offset: 0x00147738
		internal void PaintImage(PaintEventArgs e, ButtonBaseAdapter.LayoutData layout)
		{
			Graphics graphics = e.Graphics;
			this.DrawImage(graphics, layout);
		}

		// Token: 0x06004FAD RID: 20397 RVA: 0x00149554 File Offset: 0x00147754
		internal static ButtonBaseAdapter.LayoutOptions CommonLayout(Rectangle clientRectangle, Padding padding, bool isDefault, Font font, string text, bool enabled, ContentAlignment textAlign, RightToLeft rtl)
		{
			return new ButtonBaseAdapter.LayoutOptions
			{
				client = LayoutUtils.DeflateRect(clientRectangle, padding),
				padding = padding,
				growBorderBy1PxWhenDefault = true,
				isDefault = isDefault,
				borderSize = 2,
				paddingSize = 0,
				maxFocus = true,
				focusOddEvenFixup = false,
				font = font,
				text = text,
				imageSize = Size.Empty,
				checkSize = 0,
				checkPaddingSize = 0,
				checkAlign = ContentAlignment.TopLeft,
				imageAlign = ContentAlignment.MiddleCenter,
				textAlign = textAlign,
				hintTextUp = false,
				shadowedText = !enabled,
				layoutRTL = (RightToLeft.Yes == rtl),
				textImageRelation = TextImageRelation.Overlay,
				useCompatibleTextRendering = false
			};
		}

		// Token: 0x06004FAE RID: 20398 RVA: 0x00149610 File Offset: 0x00147810
		internal virtual ButtonBaseAdapter.LayoutOptions CommonLayout()
		{
			ButtonBaseAdapter.LayoutOptions layoutOptions = new ButtonBaseAdapter.LayoutOptions();
			layoutOptions.client = LayoutUtils.DeflateRect(this.Control.ClientRectangle, this.Control.Padding);
			layoutOptions.padding = this.Control.Padding;
			layoutOptions.growBorderBy1PxWhenDefault = true;
			layoutOptions.isDefault = this.Control.IsDefault;
			layoutOptions.borderSize = 2;
			layoutOptions.paddingSize = 0;
			layoutOptions.maxFocus = true;
			layoutOptions.focusOddEvenFixup = false;
			layoutOptions.font = this.Control.Font;
			layoutOptions.text = this.Control.Text;
			layoutOptions.imageSize = ((this.Control.Image == null) ? Size.Empty : this.Control.Image.Size);
			layoutOptions.checkSize = 0;
			layoutOptions.checkPaddingSize = 0;
			layoutOptions.checkAlign = ContentAlignment.TopLeft;
			layoutOptions.imageAlign = this.Control.ImageAlign;
			layoutOptions.textAlign = this.Control.TextAlign;
			layoutOptions.hintTextUp = false;
			layoutOptions.shadowedText = !this.Control.Enabled;
			layoutOptions.layoutRTL = (RightToLeft.Yes == this.Control.RightToLeft);
			layoutOptions.textImageRelation = this.Control.TextImageRelation;
			layoutOptions.useCompatibleTextRendering = this.Control.UseCompatibleTextRendering;
			if (this.Control.FlatStyle != FlatStyle.System)
			{
				if (layoutOptions.useCompatibleTextRendering)
				{
					using (StringFormat stringFormat = this.Control.CreateStringFormat())
					{
						layoutOptions.StringFormat = stringFormat;
						return layoutOptions;
					}
				}
				layoutOptions.gdiTextFormatFlags = this.Control.CreateTextFormatFlags();
			}
			return layoutOptions;
		}

		// Token: 0x06004FAF RID: 20399 RVA: 0x001497B4 File Offset: 0x001479B4
		private static ButtonBaseAdapter.ColorOptions CommonRender(Graphics g, Color foreColor, Color backColor, bool enabled)
		{
			return new ButtonBaseAdapter.ColorOptions(g, foreColor, backColor)
			{
				enabled = enabled
			};
		}

		// Token: 0x06004FB0 RID: 20400 RVA: 0x001497D4 File Offset: 0x001479D4
		private ButtonBaseAdapter.ColorOptions CommonRender(Graphics g)
		{
			return new ButtonBaseAdapter.ColorOptions(g, this.Control.ForeColor, this.Control.BackColor)
			{
				enabled = this.Control.Enabled
			};
		}

		// Token: 0x06004FB1 RID: 20401 RVA: 0x00149810 File Offset: 0x00147A10
		protected ButtonBaseAdapter.ColorOptions PaintRender(Graphics g)
		{
			return this.CommonRender(g);
		}

		// Token: 0x06004FB2 RID: 20402 RVA: 0x00149819 File Offset: 0x00147A19
		internal static ButtonBaseAdapter.ColorOptions PaintFlatRender(Graphics g, Color foreColor, Color backColor, bool enabled)
		{
			return ButtonBaseAdapter.CommonRender(g, foreColor, backColor, enabled);
		}

		// Token: 0x06004FB3 RID: 20403 RVA: 0x00149810 File Offset: 0x00147A10
		protected ButtonBaseAdapter.ColorOptions PaintFlatRender(Graphics g)
		{
			return this.CommonRender(g);
		}

		// Token: 0x06004FB4 RID: 20404 RVA: 0x00149819 File Offset: 0x00147A19
		internal static ButtonBaseAdapter.ColorOptions PaintPopupRender(Graphics g, Color foreColor, Color backColor, bool enabled)
		{
			return ButtonBaseAdapter.CommonRender(g, foreColor, backColor, enabled);
		}

		// Token: 0x06004FB5 RID: 20405 RVA: 0x00149810 File Offset: 0x00147A10
		protected ButtonBaseAdapter.ColorOptions PaintPopupRender(Graphics g)
		{
			return this.CommonRender(g);
		}

		// Token: 0x04003470 RID: 13424
		private ButtonBase control;

		// Token: 0x04003471 RID: 13425
		protected static int buttonBorderSize = 4;

		// Token: 0x02000859 RID: 2137
		internal class ColorOptions
		{
			// Token: 0x060070CB RID: 28875 RVA: 0x0019D453 File Offset: 0x0019B653
			internal ColorOptions(Graphics graphics, Color foreColor, Color backColor)
			{
				this.graphics = graphics;
				this.backColor = backColor;
				this.foreColor = foreColor;
				this.highContrast = SystemInformation.HighContrast;
			}

			// Token: 0x060070CC RID: 28876 RVA: 0x0019D47C File Offset: 0x0019B67C
			internal static int Adjust255(float percentage, int value)
			{
				int num = (int)(percentage * (float)value);
				if (num > 255)
				{
					return 255;
				}
				return num;
			}

			// Token: 0x060070CD RID: 28877 RVA: 0x0019D4A0 File Offset: 0x0019B6A0
			internal ButtonBaseAdapter.ColorData Calculate()
			{
				ButtonBaseAdapter.ColorData colorData = new ButtonBaseAdapter.ColorData(this);
				colorData.buttonFace = this.backColor;
				if (this.backColor == SystemColors.Control)
				{
					colorData.buttonShadow = SystemColors.ControlDark;
					colorData.buttonShadowDark = SystemColors.ControlDarkDark;
					colorData.highlight = SystemColors.ControlLightLight;
				}
				else if (!this.highContrast)
				{
					colorData.buttonShadow = ControlPaint.Dark(this.backColor);
					colorData.buttonShadowDark = ControlPaint.DarkDark(this.backColor);
					colorData.highlight = ControlPaint.LightLight(this.backColor);
				}
				else
				{
					colorData.buttonShadow = ControlPaint.Dark(this.backColor);
					colorData.buttonShadowDark = ControlPaint.LightLight(this.backColor);
					colorData.highlight = ControlPaint.LightLight(this.backColor);
				}
				colorData.windowDisabled = ((this.highContrast && AccessibilityImprovements.Level1) ? SystemColors.GrayText : colorData.buttonShadow);
				float percentage = 0.9f;
				if ((double)colorData.buttonFace.GetBrightness() < 0.5)
				{
					percentage = 1.2f;
				}
				colorData.lowButtonFace = Color.FromArgb(ButtonBaseAdapter.ColorOptions.Adjust255(percentage, (int)colorData.buttonFace.R), ButtonBaseAdapter.ColorOptions.Adjust255(percentage, (int)colorData.buttonFace.G), ButtonBaseAdapter.ColorOptions.Adjust255(percentage, (int)colorData.buttonFace.B));
				percentage = 0.9f;
				if ((double)colorData.highlight.GetBrightness() < 0.5)
				{
					percentage = 1.2f;
				}
				colorData.lowHighlight = Color.FromArgb(ButtonBaseAdapter.ColorOptions.Adjust255(percentage, (int)colorData.highlight.R), ButtonBaseAdapter.ColorOptions.Adjust255(percentage, (int)colorData.highlight.G), ButtonBaseAdapter.ColorOptions.Adjust255(percentage, (int)colorData.highlight.B));
				if (this.highContrast && this.backColor != SystemColors.Control)
				{
					colorData.highlight = colorData.lowHighlight;
				}
				colorData.windowFrame = this.foreColor;
				if ((double)colorData.buttonFace.GetBrightness() < 0.5)
				{
					colorData.constrastButtonShadow = colorData.lowHighlight;
				}
				else
				{
					colorData.constrastButtonShadow = colorData.buttonShadow;
				}
				if (!this.enabled)
				{
					colorData.windowText = colorData.windowDisabled;
					if (this.highContrast && AccessibilityImprovements.Level1)
					{
						colorData.windowFrame = colorData.windowDisabled;
						colorData.buttonShadow = colorData.windowDisabled;
					}
				}
				else
				{
					colorData.windowText = colorData.windowFrame;
				}
				IntPtr hdc = this.graphics.GetHdc();
				try
				{
					using (WindowsGraphics windowsGraphics = WindowsGraphics.FromHdc(hdc))
					{
						colorData.buttonFace = windowsGraphics.GetNearestColor(colorData.buttonFace);
						colorData.buttonShadow = windowsGraphics.GetNearestColor(colorData.buttonShadow);
						colorData.buttonShadowDark = windowsGraphics.GetNearestColor(colorData.buttonShadowDark);
						colorData.constrastButtonShadow = windowsGraphics.GetNearestColor(colorData.constrastButtonShadow);
						colorData.windowText = windowsGraphics.GetNearestColor(colorData.windowText);
						colorData.highlight = windowsGraphics.GetNearestColor(colorData.highlight);
						colorData.lowHighlight = windowsGraphics.GetNearestColor(colorData.lowHighlight);
						colorData.lowButtonFace = windowsGraphics.GetNearestColor(colorData.lowButtonFace);
						colorData.windowFrame = windowsGraphics.GetNearestColor(colorData.windowFrame);
						colorData.windowDisabled = windowsGraphics.GetNearestColor(colorData.windowDisabled);
					}
				}
				finally
				{
					this.graphics.ReleaseHdc();
				}
				return colorData;
			}

			// Token: 0x040043AC RID: 17324
			internal Color backColor;

			// Token: 0x040043AD RID: 17325
			internal Color foreColor;

			// Token: 0x040043AE RID: 17326
			internal bool enabled;

			// Token: 0x040043AF RID: 17327
			internal bool highContrast;

			// Token: 0x040043B0 RID: 17328
			internal Graphics graphics;
		}

		// Token: 0x0200085A RID: 2138
		internal class ColorData
		{
			// Token: 0x060070CE RID: 28878 RVA: 0x0019D7F4 File Offset: 0x0019B9F4
			internal ColorData(ButtonBaseAdapter.ColorOptions options)
			{
				this.options = options;
			}

			// Token: 0x040043B1 RID: 17329
			internal Color buttonFace;

			// Token: 0x040043B2 RID: 17330
			internal Color buttonShadow;

			// Token: 0x040043B3 RID: 17331
			internal Color buttonShadowDark;

			// Token: 0x040043B4 RID: 17332
			internal Color constrastButtonShadow;

			// Token: 0x040043B5 RID: 17333
			internal Color windowText;

			// Token: 0x040043B6 RID: 17334
			internal Color windowDisabled;

			// Token: 0x040043B7 RID: 17335
			internal Color highlight;

			// Token: 0x040043B8 RID: 17336
			internal Color lowHighlight;

			// Token: 0x040043B9 RID: 17337
			internal Color lowButtonFace;

			// Token: 0x040043BA RID: 17338
			internal Color windowFrame;

			// Token: 0x040043BB RID: 17339
			internal ButtonBaseAdapter.ColorOptions options;
		}

		// Token: 0x0200085B RID: 2139
		internal class LayoutOptions
		{
			// Token: 0x17001895 RID: 6293
			// (get) Token: 0x060070CF RID: 28879 RVA: 0x0019D804 File Offset: 0x0019BA04
			// (set) Token: 0x060070D0 RID: 28880 RVA: 0x0019D86E File Offset: 0x0019BA6E
			public StringFormat StringFormat
			{
				get
				{
					StringFormat stringFormat = new StringFormat();
					stringFormat.FormatFlags = this.gdipFormatFlags;
					stringFormat.Trimming = this.gdipTrimming;
					stringFormat.HotkeyPrefix = this.gdipHotkeyPrefix;
					stringFormat.Alignment = this.gdipAlignment;
					stringFormat.LineAlignment = this.gdipLineAlignment;
					if (this.disableWordWrapping)
					{
						stringFormat.FormatFlags |= StringFormatFlags.NoWrap;
					}
					return stringFormat;
				}
				set
				{
					this.gdipFormatFlags = value.FormatFlags;
					this.gdipTrimming = value.Trimming;
					this.gdipHotkeyPrefix = value.HotkeyPrefix;
					this.gdipAlignment = value.Alignment;
					this.gdipLineAlignment = value.LineAlignment;
				}
			}

			// Token: 0x17001896 RID: 6294
			// (get) Token: 0x060070D1 RID: 28881 RVA: 0x0019D8AC File Offset: 0x0019BAAC
			public TextFormatFlags TextFormatFlags
			{
				get
				{
					if (this.disableWordWrapping)
					{
						return this.gdiTextFormatFlags & ~TextFormatFlags.WordBreak;
					}
					return this.gdiTextFormatFlags;
				}
			}

			// Token: 0x060070D2 RID: 28882 RVA: 0x0019D8C8 File Offset: 0x0019BAC8
			private Size Compose(Size checkSize, Size imageSize, Size textSize)
			{
				ButtonBaseAdapter.LayoutOptions.Composition horizontalComposition = this.GetHorizontalComposition();
				ButtonBaseAdapter.LayoutOptions.Composition verticalComposition = this.GetVerticalComposition();
				return new Size(this.xCompose(horizontalComposition, checkSize.Width, imageSize.Width, textSize.Width), this.xCompose(verticalComposition, checkSize.Height, imageSize.Height, textSize.Height));
			}

			// Token: 0x060070D3 RID: 28883 RVA: 0x0019D920 File Offset: 0x0019BB20
			private int xCompose(ButtonBaseAdapter.LayoutOptions.Composition composition, int checkSize, int imageSize, int textSize)
			{
				switch (composition)
				{
				case ButtonBaseAdapter.LayoutOptions.Composition.NoneCombined:
					return checkSize + imageSize + textSize;
				case ButtonBaseAdapter.LayoutOptions.Composition.CheckCombined:
					return Math.Max(checkSize, imageSize + textSize);
				case ButtonBaseAdapter.LayoutOptions.Composition.TextImageCombined:
					return Math.Max(imageSize, textSize) + checkSize;
				case ButtonBaseAdapter.LayoutOptions.Composition.AllCombined:
					return Math.Max(Math.Max(checkSize, imageSize), textSize);
				default:
					return -7107;
				}
			}

			// Token: 0x060070D4 RID: 28884 RVA: 0x0019D978 File Offset: 0x0019BB78
			private Size Decompose(Size checkSize, Size imageSize, Size proposedSize)
			{
				ButtonBaseAdapter.LayoutOptions.Composition horizontalComposition = this.GetHorizontalComposition();
				ButtonBaseAdapter.LayoutOptions.Composition verticalComposition = this.GetVerticalComposition();
				return new Size(this.xDecompose(horizontalComposition, checkSize.Width, imageSize.Width, proposedSize.Width), this.xDecompose(verticalComposition, checkSize.Height, imageSize.Height, proposedSize.Height));
			}

			// Token: 0x060070D5 RID: 28885 RVA: 0x0019D9D0 File Offset: 0x0019BBD0
			private int xDecompose(ButtonBaseAdapter.LayoutOptions.Composition composition, int checkSize, int imageSize, int proposedSize)
			{
				switch (composition)
				{
				case ButtonBaseAdapter.LayoutOptions.Composition.NoneCombined:
					return proposedSize - (checkSize + imageSize);
				case ButtonBaseAdapter.LayoutOptions.Composition.CheckCombined:
					return proposedSize - imageSize;
				case ButtonBaseAdapter.LayoutOptions.Composition.TextImageCombined:
					return proposedSize - checkSize;
				case ButtonBaseAdapter.LayoutOptions.Composition.AllCombined:
					return proposedSize;
				default:
					return -7109;
				}
			}

			// Token: 0x060070D6 RID: 28886 RVA: 0x0019DA04 File Offset: 0x0019BC04
			private ButtonBaseAdapter.LayoutOptions.Composition GetHorizontalComposition()
			{
				BitVector32 bitVector = default(BitVector32);
				bitVector[ButtonBaseAdapter.LayoutOptions.combineCheck] = (this.checkAlign == ContentAlignment.MiddleCenter || !LayoutUtils.IsHorizontalAlignment(this.checkAlign));
				bitVector[ButtonBaseAdapter.LayoutOptions.combineImageText] = !LayoutUtils.IsHorizontalRelation(this.textImageRelation);
				return (ButtonBaseAdapter.LayoutOptions.Composition)bitVector.Data;
			}

			// Token: 0x060070D7 RID: 28887 RVA: 0x0019DA64 File Offset: 0x0019BC64
			internal Size GetPreferredSizeCore(Size proposedSize)
			{
				int num = this.borderSize * 2 + this.paddingSize * 2;
				if (this.growBorderBy1PxWhenDefault)
				{
					num += 2;
				}
				Size sz = new Size(num, num);
				proposedSize -= sz;
				int fullCheckSize = this.FullCheckSize;
				Size size = (fullCheckSize > 0) ? new Size(fullCheckSize + 1, fullCheckSize) : Size.Empty;
				Size sz2 = new Size(this.textImageInset * 2, this.textImageInset * 2);
				Size size2 = (this.imageSize != Size.Empty) ? (this.imageSize + sz2) : Size.Empty;
				proposedSize -= sz2;
				proposedSize = this.Decompose(size, size2, proposedSize);
				Size textSize = Size.Empty;
				if (!string.IsNullOrEmpty(this.text))
				{
					try
					{
						this.disableWordWrapping = true;
						textSize = this.GetTextSize(proposedSize) + sz2;
					}
					finally
					{
						this.disableWordWrapping = false;
					}
				}
				Size sz3 = this.Compose(size, this.imageSize, textSize);
				return sz3 + sz;
			}

			// Token: 0x060070D8 RID: 28888 RVA: 0x0019DB74 File Offset: 0x0019BD74
			private ButtonBaseAdapter.LayoutOptions.Composition GetVerticalComposition()
			{
				BitVector32 bitVector = default(BitVector32);
				bitVector[ButtonBaseAdapter.LayoutOptions.combineCheck] = (this.checkAlign == ContentAlignment.MiddleCenter || !LayoutUtils.IsVerticalAlignment(this.checkAlign));
				bitVector[ButtonBaseAdapter.LayoutOptions.combineImageText] = !LayoutUtils.IsVerticalRelation(this.textImageRelation);
				return (ButtonBaseAdapter.LayoutOptions.Composition)bitVector.Data;
			}

			// Token: 0x17001897 RID: 6295
			// (get) Token: 0x060070D9 RID: 28889 RVA: 0x0019DBD4 File Offset: 0x0019BDD4
			private int FullBorderSize
			{
				get
				{
					int num = this.borderSize;
					if (this.OnePixExtraBorder)
					{
						this.borderSize++;
					}
					return this.borderSize;
				}
			}

			// Token: 0x17001898 RID: 6296
			// (get) Token: 0x060070DA RID: 28890 RVA: 0x0019DC04 File Offset: 0x0019BE04
			private bool OnePixExtraBorder
			{
				get
				{
					return this.growBorderBy1PxWhenDefault && this.isDefault;
				}
			}

			// Token: 0x060070DB RID: 28891 RVA: 0x0019DC18 File Offset: 0x0019BE18
			internal ButtonBaseAdapter.LayoutData Layout()
			{
				ButtonBaseAdapter.LayoutData layoutData = new ButtonBaseAdapter.LayoutData(this);
				layoutData.client = this.client;
				int fullBorderSize = this.FullBorderSize;
				layoutData.face = Rectangle.Inflate(layoutData.client, -fullBorderSize, -fullBorderSize);
				this.CalcCheckmarkRectangle(layoutData);
				this.LayoutTextAndImage(layoutData);
				if (this.maxFocus)
				{
					layoutData.focus = layoutData.field;
					layoutData.focus.Inflate(-1, -1);
					layoutData.focus = LayoutUtils.InflateRect(layoutData.focus, this.padding);
				}
				else
				{
					Rectangle rectangle = new Rectangle(layoutData.textBounds.X - 1, layoutData.textBounds.Y - 1, layoutData.textBounds.Width + 2, layoutData.textBounds.Height + 3);
					if (this.imageSize != Size.Empty)
					{
						layoutData.focus = Rectangle.Union(rectangle, layoutData.imageBounds);
					}
					else
					{
						layoutData.focus = rectangle;
					}
				}
				if (this.focusOddEvenFixup)
				{
					if (layoutData.focus.Height % 2 == 0)
					{
						ButtonBaseAdapter.LayoutData layoutData2 = layoutData;
						int num = layoutData2.focus.Y;
						layoutData2.focus.Y = num + 1;
						ButtonBaseAdapter.LayoutData layoutData3 = layoutData;
						num = layoutData3.focus.Height;
						layoutData3.focus.Height = num - 1;
					}
					if (layoutData.focus.Width % 2 == 0)
					{
						ButtonBaseAdapter.LayoutData layoutData4 = layoutData;
						int num = layoutData4.focus.X;
						layoutData4.focus.X = num + 1;
						ButtonBaseAdapter.LayoutData layoutData5 = layoutData;
						num = layoutData5.focus.Width;
						layoutData5.focus.Width = num - 1;
					}
				}
				return layoutData;
			}

			// Token: 0x060070DC RID: 28892 RVA: 0x0019DD7F File Offset: 0x0019BF7F
			private TextImageRelation RtlTranslateRelation(TextImageRelation relation)
			{
				if (this.layoutRTL)
				{
					if (relation == TextImageRelation.ImageBeforeText)
					{
						return TextImageRelation.TextBeforeImage;
					}
					if (relation == TextImageRelation.TextBeforeImage)
					{
						return TextImageRelation.ImageBeforeText;
					}
				}
				return relation;
			}

			// Token: 0x060070DD RID: 28893 RVA: 0x0019DD98 File Offset: 0x0019BF98
			internal ContentAlignment RtlTranslateContent(ContentAlignment align)
			{
				if (this.layoutRTL)
				{
					ContentAlignment[][] array = new ContentAlignment[][]
					{
						new ContentAlignment[]
						{
							ContentAlignment.TopLeft,
							ContentAlignment.TopRight
						},
						new ContentAlignment[]
						{
							ContentAlignment.MiddleLeft,
							ContentAlignment.MiddleRight
						},
						new ContentAlignment[]
						{
							ContentAlignment.BottomLeft,
							ContentAlignment.BottomRight
						}
					};
					for (int i = 0; i < 3; i++)
					{
						if (array[i][0] == align)
						{
							return array[i][1];
						}
						if (array[i][1] == align)
						{
							return array[i][0];
						}
					}
				}
				return align;
			}

			// Token: 0x17001899 RID: 6297
			// (get) Token: 0x060070DE RID: 28894 RVA: 0x0019DE1A File Offset: 0x0019C01A
			private int FullCheckSize
			{
				get
				{
					return this.checkSize + this.checkPaddingSize;
				}
			}

			// Token: 0x060070DF RID: 28895 RVA: 0x0019DE2C File Offset: 0x0019C02C
			private void CalcCheckmarkRectangle(ButtonBaseAdapter.LayoutData layout)
			{
				int fullCheckSize = this.FullCheckSize;
				layout.checkBounds = new Rectangle(this.client.X, this.client.Y, fullCheckSize, fullCheckSize);
				ContentAlignment contentAlignment = this.RtlTranslateContent(this.checkAlign);
				Rectangle field = Rectangle.Inflate(layout.face, -this.paddingSize, -this.paddingSize);
				layout.field = field;
				if (fullCheckSize > 0)
				{
					if ((contentAlignment & (ContentAlignment)1092) != (ContentAlignment)0)
					{
						layout.checkBounds.X = field.X + field.Width - layout.checkBounds.Width;
					}
					else if ((contentAlignment & (ContentAlignment)546) != (ContentAlignment)0)
					{
						layout.checkBounds.X = field.X + (field.Width - layout.checkBounds.Width) / 2;
					}
					if ((contentAlignment & (ContentAlignment)1792) != (ContentAlignment)0)
					{
						layout.checkBounds.Y = field.Y + field.Height - layout.checkBounds.Height;
					}
					else if ((contentAlignment & (ContentAlignment)7) != (ContentAlignment)0)
					{
						layout.checkBounds.Y = field.Y + 2;
					}
					else
					{
						layout.checkBounds.Y = field.Y + (field.Height - layout.checkBounds.Height) / 2;
					}
					if (contentAlignment <= ContentAlignment.MiddleCenter)
					{
						switch (contentAlignment)
						{
						case ContentAlignment.TopLeft:
							break;
						case ContentAlignment.TopCenter:
							layout.checkArea.X = field.X;
							layout.checkArea.Width = field.Width;
							layout.checkArea.Y = field.Y;
							layout.checkArea.Height = fullCheckSize;
							layout.field.Y = layout.field.Y + fullCheckSize;
							layout.field.Height = layout.field.Height - fullCheckSize;
							goto IL_34B;
						case (ContentAlignment)3:
							goto IL_34B;
						case ContentAlignment.TopRight:
							goto IL_20C;
						default:
							if (contentAlignment != ContentAlignment.MiddleLeft)
							{
								if (contentAlignment != ContentAlignment.MiddleCenter)
								{
									goto IL_34B;
								}
								layout.checkArea = layout.checkBounds;
								goto IL_34B;
							}
							break;
						}
					}
					else if (contentAlignment <= ContentAlignment.BottomLeft)
					{
						if (contentAlignment == ContentAlignment.MiddleRight)
						{
							goto IL_20C;
						}
						if (contentAlignment != ContentAlignment.BottomLeft)
						{
							goto IL_34B;
						}
					}
					else
					{
						if (contentAlignment == ContentAlignment.BottomCenter)
						{
							layout.checkArea.X = field.X;
							layout.checkArea.Width = field.Width;
							layout.checkArea.Y = field.Y + field.Height - fullCheckSize;
							layout.checkArea.Height = fullCheckSize;
							layout.field.Height = layout.field.Height - fullCheckSize;
							goto IL_34B;
						}
						if (contentAlignment != ContentAlignment.BottomRight)
						{
							goto IL_34B;
						}
						goto IL_20C;
					}
					layout.checkArea.X = field.X;
					layout.checkArea.Width = fullCheckSize + 1;
					layout.checkArea.Y = field.Y;
					layout.checkArea.Height = field.Height;
					layout.field.X = layout.field.X + (fullCheckSize + 1);
					layout.field.Width = layout.field.Width - (fullCheckSize + 1);
					goto IL_34B;
					IL_20C:
					layout.checkArea.X = field.X + field.Width - fullCheckSize;
					layout.checkArea.Width = fullCheckSize + 1;
					layout.checkArea.Y = field.Y;
					layout.checkArea.Height = field.Height;
					layout.field.Width = layout.field.Width - (fullCheckSize + 1);
					IL_34B:
					layout.checkBounds.Width = layout.checkBounds.Width - this.checkPaddingSize;
					layout.checkBounds.Height = layout.checkBounds.Height - this.checkPaddingSize;
				}
			}

			// Token: 0x060070E0 RID: 28896 RVA: 0x0019E1B4 File Offset: 0x0019C3B4
			private static TextImageRelation ImageAlignToRelation(ContentAlignment alignment)
			{
				return ButtonBaseAdapter.LayoutOptions._imageAlignToRelation[LayoutUtils.ContentAlignmentToIndex(alignment)];
			}

			// Token: 0x060070E1 RID: 28897 RVA: 0x0019E1C2 File Offset: 0x0019C3C2
			private static TextImageRelation TextAlignToRelation(ContentAlignment alignment)
			{
				return LayoutUtils.GetOppositeTextImageRelation(ButtonBaseAdapter.LayoutOptions.ImageAlignToRelation(alignment));
			}

			// Token: 0x060070E2 RID: 28898 RVA: 0x0019E1D0 File Offset: 0x0019C3D0
			internal void LayoutTextAndImage(ButtonBaseAdapter.LayoutData layout)
			{
				ContentAlignment contentAlignment = this.RtlTranslateContent(this.imageAlign);
				ContentAlignment contentAlignment2 = this.RtlTranslateContent(this.textAlign);
				TextImageRelation textImageRelation = this.RtlTranslateRelation(this.textImageRelation);
				Rectangle rectangle = Rectangle.Inflate(layout.field, -this.textImageInset, -this.textImageInset);
				if (this.OnePixExtraBorder)
				{
					rectangle.Inflate(1, 1);
				}
				if (this.imageSize == Size.Empty || this.text == null || this.text.Length == 0 || textImageRelation == TextImageRelation.Overlay)
				{
					Size textSize = this.GetTextSize(rectangle.Size);
					Size alignThis = this.imageSize;
					if (layout.options.everettButtonCompat && this.imageSize != Size.Empty)
					{
						alignThis = new Size(alignThis.Width + 1, alignThis.Height + 1);
					}
					layout.imageBounds = LayoutUtils.Align(alignThis, rectangle, contentAlignment);
					layout.textBounds = LayoutUtils.Align(textSize, rectangle, contentAlignment2);
				}
				else
				{
					Size proposedSize = LayoutUtils.SubAlignedRegion(rectangle.Size, this.imageSize, textImageRelation);
					Size textSize2 = this.GetTextSize(proposedSize);
					Rectangle rectangle2 = rectangle;
					Size size = LayoutUtils.AddAlignedRegion(textSize2, this.imageSize, textImageRelation);
					rectangle2.Size = LayoutUtils.UnionSizes(rectangle2.Size, size);
					Rectangle bounds = LayoutUtils.Align(size, rectangle2, ContentAlignment.MiddleCenter);
					bool flag = (ButtonBaseAdapter.LayoutOptions.ImageAlignToRelation(contentAlignment) & textImageRelation) > TextImageRelation.Overlay;
					bool flag2 = (ButtonBaseAdapter.LayoutOptions.TextAlignToRelation(contentAlignment2) & textImageRelation) > TextImageRelation.Overlay;
					if (flag)
					{
						LayoutUtils.SplitRegion(rectangle2, this.imageSize, (AnchorStyles)textImageRelation, out layout.imageBounds, out layout.textBounds);
					}
					else if (flag2)
					{
						LayoutUtils.SplitRegion(rectangle2, textSize2, (AnchorStyles)LayoutUtils.GetOppositeTextImageRelation(textImageRelation), out layout.textBounds, out layout.imageBounds);
					}
					else
					{
						LayoutUtils.SplitRegion(bounds, this.imageSize, (AnchorStyles)textImageRelation, out layout.imageBounds, out layout.textBounds);
						LayoutUtils.ExpandRegionsToFillBounds(rectangle2, (AnchorStyles)textImageRelation, ref layout.imageBounds, ref layout.textBounds);
					}
					layout.imageBounds = LayoutUtils.Align(this.imageSize, layout.imageBounds, contentAlignment);
					layout.textBounds = LayoutUtils.Align(textSize2, layout.textBounds, contentAlignment2);
				}
				if (textImageRelation == TextImageRelation.TextBeforeImage || textImageRelation == TextImageRelation.ImageBeforeText)
				{
					int num = Math.Min(layout.textBounds.Bottom, layout.field.Bottom);
					layout.textBounds.Y = Math.Max(Math.Min(layout.textBounds.Y, layout.field.Y + (layout.field.Height - layout.textBounds.Height) / 2), layout.field.Y);
					layout.textBounds.Height = num - layout.textBounds.Y;
				}
				if (textImageRelation == TextImageRelation.TextAboveImage || textImageRelation == TextImageRelation.ImageAboveText)
				{
					int num2 = Math.Min(layout.textBounds.Right, layout.field.Right);
					layout.textBounds.X = Math.Max(Math.Min(layout.textBounds.X, layout.field.X + (layout.field.Width - layout.textBounds.Width) / 2), layout.field.X);
					layout.textBounds.Width = num2 - layout.textBounds.X;
				}
				if (textImageRelation == TextImageRelation.ImageBeforeText && layout.imageBounds.Size.Width != 0)
				{
					layout.imageBounds.Width = Math.Max(0, Math.Min(rectangle.Width - layout.textBounds.Width, layout.imageBounds.Width));
					layout.textBounds.X = layout.imageBounds.X + layout.imageBounds.Width;
				}
				if (textImageRelation == TextImageRelation.ImageAboveText && layout.imageBounds.Size.Height != 0)
				{
					layout.imageBounds.Height = Math.Max(0, Math.Min(rectangle.Height - layout.textBounds.Height, layout.imageBounds.Height));
					layout.textBounds.Y = layout.imageBounds.Y + layout.imageBounds.Height;
				}
				layout.textBounds = Rectangle.Intersect(layout.textBounds, layout.field);
				if (this.hintTextUp)
				{
					int num3 = layout.textBounds.Y;
					layout.textBounds.Y = num3 - 1;
				}
				if (this.textOffset)
				{
					layout.textBounds.Offset(1, 1);
				}
				if (layout.options.everettButtonCompat)
				{
					layout.imageStart = layout.imageBounds.Location;
					layout.imageBounds = Rectangle.Intersect(layout.imageBounds, layout.field);
				}
				else if (!Application.RenderWithVisualStyles)
				{
					int num3 = layout.textBounds.X;
					layout.textBounds.X = num3 + 1;
				}
				int num4;
				if (!this.useCompatibleTextRendering)
				{
					num4 = Math.Min(layout.textBounds.Bottom, rectangle.Bottom);
					layout.textBounds.Y = Math.Max(layout.textBounds.Y, rectangle.Y);
				}
				else
				{
					num4 = Math.Min(layout.textBounds.Bottom, layout.field.Bottom);
					layout.textBounds.Y = Math.Max(layout.textBounds.Y, layout.field.Y);
				}
				layout.textBounds.Height = num4 - layout.textBounds.Y;
			}

			// Token: 0x060070E3 RID: 28899 RVA: 0x0019E718 File Offset: 0x0019C918
			protected virtual Size GetTextSize(Size proposedSize)
			{
				proposedSize = LayoutUtils.FlipSizeIf(this.verticalText, proposedSize);
				Size size = Size.Empty;
				if (this.useCompatibleTextRendering)
				{
					using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
					{
						using (StringFormat stringFormat = this.StringFormat)
						{
							size = Size.Ceiling(graphics.MeasureString(this.text, this.font, new SizeF((float)proposedSize.Width, (float)proposedSize.Height), stringFormat));
							goto IL_93;
						}
					}
				}
				if (!string.IsNullOrEmpty(this.text))
				{
					size = TextRenderer.MeasureText(this.text, this.font, proposedSize, this.TextFormatFlags);
				}
				IL_93:
				return LayoutUtils.FlipSizeIf(this.verticalText, size);
			}

			// Token: 0x040043BC RID: 17340
			internal Rectangle client;

			// Token: 0x040043BD RID: 17341
			internal bool growBorderBy1PxWhenDefault;

			// Token: 0x040043BE RID: 17342
			internal bool isDefault;

			// Token: 0x040043BF RID: 17343
			internal int borderSize;

			// Token: 0x040043C0 RID: 17344
			internal int paddingSize;

			// Token: 0x040043C1 RID: 17345
			internal bool maxFocus;

			// Token: 0x040043C2 RID: 17346
			internal bool focusOddEvenFixup;

			// Token: 0x040043C3 RID: 17347
			internal Font font;

			// Token: 0x040043C4 RID: 17348
			internal string text;

			// Token: 0x040043C5 RID: 17349
			internal Size imageSize;

			// Token: 0x040043C6 RID: 17350
			internal int checkSize;

			// Token: 0x040043C7 RID: 17351
			internal int checkPaddingSize;

			// Token: 0x040043C8 RID: 17352
			internal ContentAlignment checkAlign;

			// Token: 0x040043C9 RID: 17353
			internal ContentAlignment imageAlign;

			// Token: 0x040043CA RID: 17354
			internal ContentAlignment textAlign;

			// Token: 0x040043CB RID: 17355
			internal TextImageRelation textImageRelation;

			// Token: 0x040043CC RID: 17356
			internal bool hintTextUp;

			// Token: 0x040043CD RID: 17357
			internal bool textOffset;

			// Token: 0x040043CE RID: 17358
			internal bool shadowedText;

			// Token: 0x040043CF RID: 17359
			internal bool layoutRTL;

			// Token: 0x040043D0 RID: 17360
			internal bool verticalText;

			// Token: 0x040043D1 RID: 17361
			internal bool useCompatibleTextRendering;

			// Token: 0x040043D2 RID: 17362
			internal bool everettButtonCompat = true;

			// Token: 0x040043D3 RID: 17363
			internal TextFormatFlags gdiTextFormatFlags = TextFormatFlags.TextBoxControl | TextFormatFlags.WordBreak;

			// Token: 0x040043D4 RID: 17364
			internal StringFormatFlags gdipFormatFlags;

			// Token: 0x040043D5 RID: 17365
			internal StringTrimming gdipTrimming;

			// Token: 0x040043D6 RID: 17366
			internal HotkeyPrefix gdipHotkeyPrefix;

			// Token: 0x040043D7 RID: 17367
			internal StringAlignment gdipAlignment;

			// Token: 0x040043D8 RID: 17368
			internal StringAlignment gdipLineAlignment;

			// Token: 0x040043D9 RID: 17369
			private bool disableWordWrapping;

			// Token: 0x040043DA RID: 17370
			internal int textImageInset = 2;

			// Token: 0x040043DB RID: 17371
			internal Padding padding;

			// Token: 0x040043DC RID: 17372
			private static readonly int combineCheck = BitVector32.CreateMask();

			// Token: 0x040043DD RID: 17373
			private static readonly int combineImageText = BitVector32.CreateMask(ButtonBaseAdapter.LayoutOptions.combineCheck);

			// Token: 0x040043DE RID: 17374
			private static readonly TextImageRelation[] _imageAlignToRelation = new TextImageRelation[]
			{
				(TextImageRelation)5,
				TextImageRelation.ImageAboveText,
				(TextImageRelation)9,
				TextImageRelation.Overlay,
				TextImageRelation.ImageBeforeText,
				TextImageRelation.Overlay,
				TextImageRelation.TextBeforeImage,
				TextImageRelation.Overlay,
				(TextImageRelation)6,
				TextImageRelation.TextAboveImage,
				(TextImageRelation)10
			};

			// Token: 0x0200097C RID: 2428
			private enum Composition
			{
				// Token: 0x040047CD RID: 18381
				NoneCombined,
				// Token: 0x040047CE RID: 18382
				CheckCombined,
				// Token: 0x040047CF RID: 18383
				TextImageCombined,
				// Token: 0x040047D0 RID: 18384
				AllCombined
			}
		}

		// Token: 0x0200085C RID: 2140
		internal class LayoutData
		{
			// Token: 0x060070E6 RID: 28902 RVA: 0x0019E833 File Offset: 0x0019CA33
			internal LayoutData(ButtonBaseAdapter.LayoutOptions options)
			{
				this.options = options;
			}

			// Token: 0x040043DF RID: 17375
			internal Rectangle client;

			// Token: 0x040043E0 RID: 17376
			internal Rectangle face;

			// Token: 0x040043E1 RID: 17377
			internal Rectangle checkArea;

			// Token: 0x040043E2 RID: 17378
			internal Rectangle checkBounds;

			// Token: 0x040043E3 RID: 17379
			internal Rectangle textBounds;

			// Token: 0x040043E4 RID: 17380
			internal Rectangle field;

			// Token: 0x040043E5 RID: 17381
			internal Rectangle focus;

			// Token: 0x040043E6 RID: 17382
			internal Rectangle imageBounds;

			// Token: 0x040043E7 RID: 17383
			internal Point imageStart;

			// Token: 0x040043E8 RID: 17384
			internal ButtonBaseAdapter.LayoutOptions options;
		}
	}
}
