using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x0200033F RID: 831
	public sealed class RadioButtonRenderer
	{
		// Token: 0x060035B7 RID: 13751 RVA: 0x00002843 File Offset: 0x00000A43
		private RadioButtonRenderer()
		{
		}

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x060035B8 RID: 13752 RVA: 0x000F30A9 File Offset: 0x000F12A9
		// (set) Token: 0x060035B9 RID: 13753 RVA: 0x000F30B0 File Offset: 0x000F12B0
		public static bool RenderMatchingApplicationState
		{
			get
			{
				return RadioButtonRenderer.renderMatchingApplicationState;
			}
			set
			{
				RadioButtonRenderer.renderMatchingApplicationState = value;
			}
		}

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x060035BA RID: 13754 RVA: 0x000F30B8 File Offset: 0x000F12B8
		private static bool RenderWithVisualStyles
		{
			get
			{
				return !RadioButtonRenderer.renderMatchingApplicationState || Application.RenderWithVisualStyles;
			}
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x000F30C8 File Offset: 0x000F12C8
		public static bool IsBackgroundPartiallyTransparent(RadioButtonState state)
		{
			if (RadioButtonRenderer.RenderWithVisualStyles)
			{
				RadioButtonRenderer.InitializeRenderer((int)state);
				return RadioButtonRenderer.visualStyleRenderer.IsBackgroundPartiallyTransparent();
			}
			return false;
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x000F30E3 File Offset: 0x000F12E3
		public static void DrawParentBackground(Graphics g, Rectangle bounds, Control childControl)
		{
			if (RadioButtonRenderer.RenderWithVisualStyles)
			{
				RadioButtonRenderer.InitializeRenderer(0);
				RadioButtonRenderer.visualStyleRenderer.DrawParentBackground(g, bounds, childControl);
			}
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x000F30FF File Offset: 0x000F12FF
		public static void DrawRadioButton(Graphics g, Point glyphLocation, RadioButtonState state)
		{
			RadioButtonRenderer.DrawRadioButton(g, glyphLocation, state, IntPtr.Zero);
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x000F3110 File Offset: 0x000F1310
		internal static void DrawRadioButton(Graphics g, Point glyphLocation, RadioButtonState state, IntPtr hWnd)
		{
			Rectangle rectangle = new Rectangle(glyphLocation, RadioButtonRenderer.GetGlyphSize(g, state, hWnd));
			if (RadioButtonRenderer.RenderWithVisualStyles)
			{
				RadioButtonRenderer.InitializeRenderer((int)state);
				RadioButtonRenderer.visualStyleRenderer.DrawBackground(g, rectangle, hWnd);
				return;
			}
			ControlPaint.DrawRadioButton(g, rectangle, RadioButtonRenderer.ConvertToButtonState(state));
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x000F3155 File Offset: 0x000F1355
		public static void DrawRadioButton(Graphics g, Point glyphLocation, Rectangle textBounds, string radioButtonText, Font font, bool focused, RadioButtonState state)
		{
			RadioButtonRenderer.DrawRadioButton(g, glyphLocation, textBounds, radioButtonText, font, TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter, focused, state);
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x000F3168 File Offset: 0x000F1368
		public static void DrawRadioButton(Graphics g, Point glyphLocation, Rectangle textBounds, string radioButtonText, Font font, TextFormatFlags flags, bool focused, RadioButtonState state)
		{
			RadioButtonRenderer.DrawRadioButton(g, glyphLocation, textBounds, radioButtonText, font, flags, focused, state, IntPtr.Zero);
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x000F318C File Offset: 0x000F138C
		internal static void DrawRadioButton(Graphics g, Point glyphLocation, Rectangle textBounds, string radioButtonText, Font font, TextFormatFlags flags, bool focused, RadioButtonState state, IntPtr hWnd)
		{
			Rectangle rectangle = new Rectangle(glyphLocation, RadioButtonRenderer.GetGlyphSize(g, state, hWnd));
			Color foreColor;
			if (RadioButtonRenderer.RenderWithVisualStyles)
			{
				RadioButtonRenderer.InitializeRenderer((int)state);
				RadioButtonRenderer.visualStyleRenderer.DrawBackground(g, rectangle);
				foreColor = RadioButtonRenderer.visualStyleRenderer.GetColor(ColorProperty.TextColor);
			}
			else
			{
				ControlPaint.DrawRadioButton(g, rectangle, RadioButtonRenderer.ConvertToButtonState(state));
				foreColor = SystemColors.ControlText;
			}
			TextRenderer.DrawText(g, radioButtonText, font, textBounds, foreColor, flags);
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, textBounds);
			}
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x000F3204 File Offset: 0x000F1404
		public static void DrawRadioButton(Graphics g, Point glyphLocation, Rectangle textBounds, string radioButtonText, Font font, Image image, Rectangle imageBounds, bool focused, RadioButtonState state)
		{
			RadioButtonRenderer.DrawRadioButton(g, glyphLocation, textBounds, radioButtonText, font, TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter, image, imageBounds, focused, state);
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x000F3228 File Offset: 0x000F1428
		public static void DrawRadioButton(Graphics g, Point glyphLocation, Rectangle textBounds, string radioButtonText, Font font, TextFormatFlags flags, Image image, Rectangle imageBounds, bool focused, RadioButtonState state)
		{
			RadioButtonRenderer.DrawRadioButton(g, glyphLocation, textBounds, radioButtonText, font, flags, image, imageBounds, focused, state, IntPtr.Zero);
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x000F3250 File Offset: 0x000F1450
		internal static void DrawRadioButton(Graphics g, Point glyphLocation, Rectangle textBounds, string radioButtonText, Font font, TextFormatFlags flags, Image image, Rectangle imageBounds, bool focused, RadioButtonState state, IntPtr hWnd)
		{
			Rectangle rectangle = new Rectangle(glyphLocation, RadioButtonRenderer.GetGlyphSize(g, state, hWnd));
			Color foreColor;
			if (RadioButtonRenderer.RenderWithVisualStyles)
			{
				RadioButtonRenderer.InitializeRenderer((int)state);
				RadioButtonRenderer.visualStyleRenderer.DrawImage(g, imageBounds, image);
				RadioButtonRenderer.visualStyleRenderer.DrawBackground(g, rectangle);
				foreColor = RadioButtonRenderer.visualStyleRenderer.GetColor(ColorProperty.TextColor);
			}
			else
			{
				g.DrawImage(image, imageBounds);
				ControlPaint.DrawRadioButton(g, rectangle, RadioButtonRenderer.ConvertToButtonState(state));
				foreColor = SystemColors.ControlText;
			}
			TextRenderer.DrawText(g, radioButtonText, font, textBounds, foreColor, flags);
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, textBounds);
			}
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x000F32E0 File Offset: 0x000F14E0
		public static Size GetGlyphSize(Graphics g, RadioButtonState state)
		{
			return RadioButtonRenderer.GetGlyphSize(g, state, IntPtr.Zero);
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x000F32EE File Offset: 0x000F14EE
		internal static Size GetGlyphSize(Graphics g, RadioButtonState state, IntPtr hWnd)
		{
			if (RadioButtonRenderer.RenderWithVisualStyles)
			{
				RadioButtonRenderer.InitializeRenderer((int)state);
				return RadioButtonRenderer.visualStyleRenderer.GetPartSize(g, ThemeSizeType.Draw, hWnd);
			}
			return new Size(13, 13);
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x000F3314 File Offset: 0x000F1514
		internal static ButtonState ConvertToButtonState(RadioButtonState state)
		{
			switch (state)
			{
			case RadioButtonState.UncheckedPressed:
				return ButtonState.Pushed;
			case RadioButtonState.UncheckedDisabled:
				return ButtonState.Inactive;
			case RadioButtonState.CheckedNormal:
			case RadioButtonState.CheckedHot:
				return ButtonState.Checked;
			case RadioButtonState.CheckedPressed:
				return ButtonState.Checked | ButtonState.Pushed;
			case RadioButtonState.CheckedDisabled:
				return ButtonState.Checked | ButtonState.Inactive;
			default:
				return ButtonState.Normal;
			}
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x000F3364 File Offset: 0x000F1564
		internal static RadioButtonState ConvertFromButtonState(ButtonState state, bool isHot)
		{
			if ((state & ButtonState.Checked) == ButtonState.Checked)
			{
				if ((state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					return RadioButtonState.CheckedPressed;
				}
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					return RadioButtonState.CheckedDisabled;
				}
				if (isHot)
				{
					return RadioButtonState.CheckedHot;
				}
				return RadioButtonState.CheckedNormal;
			}
			else
			{
				if ((state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					return RadioButtonState.UncheckedPressed;
				}
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					return RadioButtonState.UncheckedDisabled;
				}
				if (isHot)
				{
					return RadioButtonState.UncheckedHot;
				}
				return RadioButtonState.UncheckedNormal;
			}
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x000F33CC File Offset: 0x000F15CC
		private static void InitializeRenderer(int state)
		{
			int part = RadioButtonRenderer.RadioElement.Part;
			if (AccessibilityImprovements.Level2 && SystemInformation.HighContrast && (state == 8 || state == 4) && VisualStyleRenderer.IsCombinationDefined(RadioButtonRenderer.RadioElement.ClassName, VisualStyleElement.Button.RadioButton.HighContrastDisabledPart))
			{
				part = VisualStyleElement.Button.RadioButton.HighContrastDisabledPart;
			}
			if (RadioButtonRenderer.visualStyleRenderer == null)
			{
				RadioButtonRenderer.visualStyleRenderer = new VisualStyleRenderer(RadioButtonRenderer.RadioElement.ClassName, part, state);
				return;
			}
			RadioButtonRenderer.visualStyleRenderer.SetParameters(RadioButtonRenderer.RadioElement.ClassName, part, state);
		}

		// Token: 0x04001F6A RID: 8042
		[ThreadStatic]
		private static VisualStyleRenderer visualStyleRenderer = null;

		// Token: 0x04001F6B RID: 8043
		private static readonly VisualStyleElement RadioElement = VisualStyleElement.Button.RadioButton.UncheckedNormal;

		// Token: 0x04001F6C RID: 8044
		private static bool renderMatchingApplicationState = true;
	}
}
