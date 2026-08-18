using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x0200026E RID: 622
	public sealed class GroupBoxRenderer
	{
		// Token: 0x060027ED RID: 10221 RVA: 0x00002843 File Offset: 0x00000A43
		private GroupBoxRenderer()
		{
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x060027EE RID: 10222 RVA: 0x000B9BD8 File Offset: 0x000B7DD8
		// (set) Token: 0x060027EF RID: 10223 RVA: 0x000B9BDF File Offset: 0x000B7DDF
		public static bool RenderMatchingApplicationState
		{
			get
			{
				return GroupBoxRenderer.renderMatchingApplicationState;
			}
			set
			{
				GroupBoxRenderer.renderMatchingApplicationState = value;
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x060027F0 RID: 10224 RVA: 0x000B9BE7 File Offset: 0x000B7DE7
		private static bool RenderWithVisualStyles
		{
			get
			{
				return !GroupBoxRenderer.renderMatchingApplicationState || Application.RenderWithVisualStyles;
			}
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x000B9BF7 File Offset: 0x000B7DF7
		public static bool IsBackgroundPartiallyTransparent(GroupBoxState state)
		{
			if (GroupBoxRenderer.RenderWithVisualStyles)
			{
				GroupBoxRenderer.InitializeRenderer((int)state);
				return GroupBoxRenderer.visualStyleRenderer.IsBackgroundPartiallyTransparent();
			}
			return false;
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x000B9C12 File Offset: 0x000B7E12
		public static void DrawParentBackground(Graphics g, Rectangle bounds, Control childControl)
		{
			if (GroupBoxRenderer.RenderWithVisualStyles)
			{
				GroupBoxRenderer.InitializeRenderer(0);
				GroupBoxRenderer.visualStyleRenderer.DrawParentBackground(g, bounds, childControl);
			}
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x000B9C2E File Offset: 0x000B7E2E
		public static void DrawGroupBox(Graphics g, Rectangle bounds, GroupBoxState state)
		{
			if (GroupBoxRenderer.RenderWithVisualStyles)
			{
				GroupBoxRenderer.DrawThemedGroupBoxNoText(g, bounds, state);
				return;
			}
			GroupBoxRenderer.DrawUnthemedGroupBoxNoText(g, bounds, state);
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x000B9C48 File Offset: 0x000B7E48
		public static void DrawGroupBox(Graphics g, Rectangle bounds, string groupBoxText, Font font, GroupBoxState state)
		{
			GroupBoxRenderer.DrawGroupBox(g, bounds, groupBoxText, font, TextFormatFlags.Default, state);
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x000B9C56 File Offset: 0x000B7E56
		public static void DrawGroupBox(Graphics g, Rectangle bounds, string groupBoxText, Font font, Color textColor, GroupBoxState state)
		{
			GroupBoxRenderer.DrawGroupBox(g, bounds, groupBoxText, font, textColor, TextFormatFlags.Default, state);
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x000B9C66 File Offset: 0x000B7E66
		public static void DrawGroupBox(Graphics g, Rectangle bounds, string groupBoxText, Font font, TextFormatFlags flags, GroupBoxState state)
		{
			if (GroupBoxRenderer.RenderWithVisualStyles)
			{
				GroupBoxRenderer.DrawThemedGroupBoxWithText(g, bounds, groupBoxText, font, GroupBoxRenderer.DefaultTextColor(state), flags, state);
				return;
			}
			GroupBoxRenderer.DrawUnthemedGroupBoxWithText(g, bounds, groupBoxText, font, GroupBoxRenderer.DefaultTextColor(state), flags, state);
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x000B9C98 File Offset: 0x000B7E98
		public static void DrawGroupBox(Graphics g, Rectangle bounds, string groupBoxText, Font font, Color textColor, TextFormatFlags flags, GroupBoxState state)
		{
			if (GroupBoxRenderer.RenderWithVisualStyles)
			{
				GroupBoxRenderer.DrawThemedGroupBoxWithText(g, bounds, groupBoxText, font, textColor, flags, state);
				return;
			}
			GroupBoxRenderer.DrawUnthemedGroupBoxWithText(g, bounds, groupBoxText, font, textColor, flags, state);
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x000B9CC0 File Offset: 0x000B7EC0
		private static void DrawThemedGroupBoxNoText(Graphics g, Rectangle bounds, GroupBoxState state)
		{
			GroupBoxRenderer.InitializeRenderer((int)state);
			GroupBoxRenderer.visualStyleRenderer.DrawBackground(g, bounds);
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x000B9CD4 File Offset: 0x000B7ED4
		private static void DrawThemedGroupBoxWithText(Graphics g, Rectangle bounds, string groupBoxText, Font font, Color textColor, TextFormatFlags flags, GroupBoxState state)
		{
			GroupBoxRenderer.InitializeRenderer((int)state);
			Rectangle bounds2 = bounds;
			bounds2.Width -= 14;
			Size size = TextRenderer.MeasureText(g, groupBoxText, font, new Size(bounds2.Width, bounds2.Height), flags);
			bounds2.Width = size.Width;
			bounds2.Height = size.Height;
			if ((flags & TextFormatFlags.Right) == TextFormatFlags.Right)
			{
				bounds2.X = bounds.Right - bounds2.Width - 7 + 1;
			}
			else
			{
				bounds2.X += 6;
			}
			TextRenderer.DrawText(g, groupBoxText, font, bounds2, textColor, flags);
			Rectangle rectangle = bounds;
			rectangle.Y += font.Height / 2;
			rectangle.Height -= font.Height / 2;
			Rectangle clipRectangle = rectangle;
			Rectangle clipRectangle2 = rectangle;
			Rectangle clipRectangle3 = rectangle;
			clipRectangle.Width = 7;
			clipRectangle2.Width = Math.Max(0, bounds2.Width - 3);
			if ((flags & TextFormatFlags.Right) == TextFormatFlags.Right)
			{
				clipRectangle.X = rectangle.Right - 7;
				clipRectangle2.X = clipRectangle.Left - clipRectangle2.Width;
				clipRectangle3.Width = clipRectangle2.X - rectangle.X;
			}
			else
			{
				clipRectangle2.X = clipRectangle.Right;
				clipRectangle3.X = clipRectangle2.Right;
				clipRectangle3.Width = rectangle.Right - clipRectangle3.X;
			}
			clipRectangle2.Y = bounds2.Bottom;
			clipRectangle2.Height -= bounds2.Bottom - rectangle.Top;
			GroupBoxRenderer.visualStyleRenderer.DrawBackground(g, rectangle, clipRectangle);
			GroupBoxRenderer.visualStyleRenderer.DrawBackground(g, rectangle, clipRectangle2);
			GroupBoxRenderer.visualStyleRenderer.DrawBackground(g, rectangle, clipRectangle3);
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x000B9E94 File Offset: 0x000B8094
		private static void DrawUnthemedGroupBoxNoText(Graphics g, Rectangle bounds, GroupBoxState state)
		{
			Color control = SystemColors.Control;
			Pen pen = new Pen(ControlPaint.Light(control, 1f));
			Pen pen2 = new Pen(ControlPaint.Dark(control, 0f));
			try
			{
				g.DrawLine(pen, bounds.Left + 1, bounds.Top + 1, bounds.Left + 1, bounds.Height - 1);
				g.DrawLine(pen2, bounds.Left, bounds.Top + 1, bounds.Left, bounds.Height - 2);
				g.DrawLine(pen, bounds.Left, bounds.Height - 1, bounds.Width - 1, bounds.Height - 1);
				g.DrawLine(pen2, bounds.Left, bounds.Height - 2, bounds.Width - 1, bounds.Height - 2);
				g.DrawLine(pen, bounds.Left + 1, bounds.Top + 1, bounds.Width - 1, bounds.Top + 1);
				g.DrawLine(pen2, bounds.Left, bounds.Top, bounds.Width - 2, bounds.Top);
				g.DrawLine(pen, bounds.Width - 1, bounds.Top, bounds.Width - 1, bounds.Height - 1);
				g.DrawLine(pen2, bounds.Width - 2, bounds.Top, bounds.Width - 2, bounds.Height - 2);
			}
			finally
			{
				if (pen != null)
				{
					pen.Dispose();
				}
				if (pen2 != null)
				{
					pen2.Dispose();
				}
			}
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x000BA040 File Offset: 0x000B8240
		private static void DrawUnthemedGroupBoxWithText(Graphics g, Rectangle bounds, string groupBoxText, Font font, Color textColor, TextFormatFlags flags, GroupBoxState state)
		{
			Rectangle bounds2 = bounds;
			bounds2.Width -= 8;
			Size size = TextRenderer.MeasureText(g, groupBoxText, font, new Size(bounds2.Width, bounds2.Height), flags);
			bounds2.Width = size.Width;
			bounds2.Height = size.Height;
			if ((flags & TextFormatFlags.Right) == TextFormatFlags.Right)
			{
				bounds2.X = bounds.Right - bounds2.Width - 8;
			}
			else
			{
				bounds2.X += 8;
			}
			TextRenderer.DrawText(g, groupBoxText, font, bounds2, textColor, flags);
			if (bounds2.Width > 0)
			{
				bounds2.Inflate(2, 0);
			}
			Pen pen = new Pen(SystemColors.ControlLight);
			Pen pen2 = new Pen(SystemColors.ControlDark);
			int num = bounds.Top + font.Height / 2;
			g.DrawLine(pen, bounds.Left + 1, num, bounds.Left + 1, bounds.Height - 1);
			g.DrawLine(pen2, bounds.Left, num - 1, bounds.Left, bounds.Height - 2);
			g.DrawLine(pen, bounds.Left, bounds.Height - 1, bounds.Width, bounds.Height - 1);
			g.DrawLine(pen2, bounds.Left, bounds.Height - 2, bounds.Width - 1, bounds.Height - 2);
			g.DrawLine(pen, bounds.Left + 1, num, bounds2.X - 2, num);
			g.DrawLine(pen2, bounds.Left, num - 1, bounds2.X - 3, num - 1);
			g.DrawLine(pen, bounds2.X + bounds2.Width + 1, num, bounds.Width - 1, num);
			g.DrawLine(pen2, bounds2.X + bounds2.Width + 2, num - 1, bounds.Width - 2, num - 1);
			g.DrawLine(pen, bounds.Width - 1, num, bounds.Width - 1, bounds.Height - 1);
			g.DrawLine(pen2, bounds.Width - 2, num - 1, bounds.Width - 2, bounds.Height - 2);
			pen.Dispose();
			pen2.Dispose();
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x000BA286 File Offset: 0x000B8486
		private static Color DefaultTextColor(GroupBoxState state)
		{
			if (GroupBoxRenderer.RenderWithVisualStyles)
			{
				GroupBoxRenderer.InitializeRenderer((int)state);
				return GroupBoxRenderer.visualStyleRenderer.GetColor(ColorProperty.TextColor);
			}
			return SystemColors.ControlText;
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x000BA2AC File Offset: 0x000B84AC
		private static void InitializeRenderer(int state)
		{
			int part = GroupBoxRenderer.GroupBoxElement.Part;
			if (AccessibilityImprovements.Level2 && SystemInformation.HighContrast && state == 2 && VisualStyleRenderer.IsCombinationDefined(GroupBoxRenderer.GroupBoxElement.ClassName, VisualStyleElement.Button.GroupBox.HighContrastDisabledPart))
			{
				part = VisualStyleElement.Button.GroupBox.HighContrastDisabledPart;
			}
			if (GroupBoxRenderer.visualStyleRenderer == null)
			{
				GroupBoxRenderer.visualStyleRenderer = new VisualStyleRenderer(GroupBoxRenderer.GroupBoxElement.ClassName, part, state);
				return;
			}
			GroupBoxRenderer.visualStyleRenderer.SetParameters(GroupBoxRenderer.GroupBoxElement.ClassName, part, state);
		}

		// Token: 0x04001064 RID: 4196
		[ThreadStatic]
		private static VisualStyleRenderer visualStyleRenderer = null;

		// Token: 0x04001065 RID: 4197
		private static readonly VisualStyleElement GroupBoxElement = VisualStyleElement.Button.GroupBox.Normal;

		// Token: 0x04001066 RID: 4198
		private const int textOffset = 8;

		// Token: 0x04001067 RID: 4199
		private const int boxHeaderWidth = 7;

		// Token: 0x04001068 RID: 4200
		private static bool renderMatchingApplicationState = true;
	}
}
