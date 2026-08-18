using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x0200014D RID: 333
	public sealed class CheckBoxRenderer
	{
		// Token: 0x06000D0E RID: 3342 RVA: 0x00002843 File Offset: 0x00000A43
		private CheckBoxRenderer()
		{
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x00025640 File Offset: 0x00023840
		// (set) Token: 0x06000D10 RID: 3344 RVA: 0x00025647 File Offset: 0x00023847
		public static bool RenderMatchingApplicationState
		{
			get
			{
				return CheckBoxRenderer.renderMatchingApplicationState;
			}
			set
			{
				CheckBoxRenderer.renderMatchingApplicationState = value;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x0002564F File Offset: 0x0002384F
		private static bool RenderWithVisualStyles
		{
			get
			{
				return !CheckBoxRenderer.renderMatchingApplicationState || Application.RenderWithVisualStyles;
			}
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0002565F File Offset: 0x0002385F
		public static bool IsBackgroundPartiallyTransparent(CheckBoxState state)
		{
			if (CheckBoxRenderer.RenderWithVisualStyles)
			{
				CheckBoxRenderer.InitializeRenderer((int)state);
				return CheckBoxRenderer.visualStyleRenderer.IsBackgroundPartiallyTransparent();
			}
			return false;
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0002567A File Offset: 0x0002387A
		public static void DrawParentBackground(Graphics g, Rectangle bounds, Control childControl)
		{
			if (CheckBoxRenderer.RenderWithVisualStyles)
			{
				CheckBoxRenderer.InitializeRenderer(0);
				CheckBoxRenderer.visualStyleRenderer.DrawParentBackground(g, bounds, childControl);
			}
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00025696 File Offset: 0x00023896
		public static void DrawCheckBox(Graphics g, Point glyphLocation, CheckBoxState state)
		{
			CheckBoxRenderer.DrawCheckBox(g, glyphLocation, state, IntPtr.Zero);
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x000256A8 File Offset: 0x000238A8
		internal static void DrawCheckBox(Graphics g, Point glyphLocation, CheckBoxState state, IntPtr hWnd)
		{
			Rectangle rectangle = new Rectangle(glyphLocation, CheckBoxRenderer.GetGlyphSize(g, state, hWnd));
			if (CheckBoxRenderer.RenderWithVisualStyles)
			{
				CheckBoxRenderer.InitializeRenderer((int)state);
				CheckBoxRenderer.visualStyleRenderer.DrawBackground(g, rectangle, hWnd);
				return;
			}
			if (CheckBoxRenderer.IsMixed(state))
			{
				ControlPaint.DrawMixedCheckBox(g, rectangle, CheckBoxRenderer.ConvertToButtonState(state));
				return;
			}
			ControlPaint.DrawCheckBox(g, rectangle, CheckBoxRenderer.ConvertToButtonState(state));
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00025703 File Offset: 0x00023903
		public static void DrawCheckBox(Graphics g, Point glyphLocation, Rectangle textBounds, string checkBoxText, Font font, bool focused, CheckBoxState state)
		{
			CheckBoxRenderer.DrawCheckBox(g, glyphLocation, textBounds, checkBoxText, font, TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter, focused, state);
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00025718 File Offset: 0x00023918
		public static void DrawCheckBox(Graphics g, Point glyphLocation, Rectangle textBounds, string checkBoxText, Font font, TextFormatFlags flags, bool focused, CheckBoxState state)
		{
			CheckBoxRenderer.DrawCheckBox(g, glyphLocation, textBounds, checkBoxText, font, flags, focused, state, IntPtr.Zero);
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0002573C File Offset: 0x0002393C
		internal static void DrawCheckBox(Graphics g, Point glyphLocation, Rectangle textBounds, string checkBoxText, Font font, TextFormatFlags flags, bool focused, CheckBoxState state, IntPtr hWnd)
		{
			Rectangle rectangle = new Rectangle(glyphLocation, CheckBoxRenderer.GetGlyphSize(g, state, hWnd));
			Color foreColor;
			if (CheckBoxRenderer.RenderWithVisualStyles)
			{
				CheckBoxRenderer.InitializeRenderer((int)state);
				CheckBoxRenderer.visualStyleRenderer.DrawBackground(g, rectangle);
				foreColor = CheckBoxRenderer.visualStyleRenderer.GetColor(ColorProperty.TextColor);
			}
			else
			{
				if (CheckBoxRenderer.IsMixed(state))
				{
					ControlPaint.DrawMixedCheckBox(g, rectangle, CheckBoxRenderer.ConvertToButtonState(state));
				}
				else
				{
					ControlPaint.DrawCheckBox(g, rectangle, CheckBoxRenderer.ConvertToButtonState(state));
				}
				foreColor = SystemColors.ControlText;
			}
			TextRenderer.DrawText(g, checkBoxText, font, textBounds, foreColor, flags);
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, textBounds);
			}
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x000257CC File Offset: 0x000239CC
		public static void DrawCheckBox(Graphics g, Point glyphLocation, Rectangle textBounds, string checkBoxText, Font font, Image image, Rectangle imageBounds, bool focused, CheckBoxState state)
		{
			CheckBoxRenderer.DrawCheckBox(g, glyphLocation, textBounds, checkBoxText, font, TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter, image, imageBounds, focused, state);
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x000257F0 File Offset: 0x000239F0
		public static void DrawCheckBox(Graphics g, Point glyphLocation, Rectangle textBounds, string checkBoxText, Font font, TextFormatFlags flags, Image image, Rectangle imageBounds, bool focused, CheckBoxState state)
		{
			Rectangle rectangle = new Rectangle(glyphLocation, CheckBoxRenderer.GetGlyphSize(g, state));
			Color foreColor;
			if (CheckBoxRenderer.RenderWithVisualStyles)
			{
				CheckBoxRenderer.InitializeRenderer((int)state);
				CheckBoxRenderer.visualStyleRenderer.DrawImage(g, imageBounds, image);
				CheckBoxRenderer.visualStyleRenderer.DrawBackground(g, rectangle);
				foreColor = CheckBoxRenderer.visualStyleRenderer.GetColor(ColorProperty.TextColor);
			}
			else
			{
				g.DrawImage(image, imageBounds);
				if (CheckBoxRenderer.IsMixed(state))
				{
					ControlPaint.DrawMixedCheckBox(g, rectangle, CheckBoxRenderer.ConvertToButtonState(state));
				}
				else
				{
					ControlPaint.DrawCheckBox(g, rectangle, CheckBoxRenderer.ConvertToButtonState(state));
				}
				foreColor = SystemColors.ControlText;
			}
			TextRenderer.DrawText(g, checkBoxText, font, textBounds, foreColor, flags);
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, textBounds);
			}
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00025897 File Offset: 0x00023A97
		public static Size GetGlyphSize(Graphics g, CheckBoxState state)
		{
			return CheckBoxRenderer.GetGlyphSize(g, state, IntPtr.Zero);
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x000258A5 File Offset: 0x00023AA5
		internal static Size GetGlyphSize(Graphics g, CheckBoxState state, IntPtr hWnd)
		{
			if (CheckBoxRenderer.RenderWithVisualStyles)
			{
				CheckBoxRenderer.InitializeRenderer((int)state);
				return CheckBoxRenderer.visualStyleRenderer.GetPartSize(g, ThemeSizeType.Draw, hWnd);
			}
			return new Size(13, 13);
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x000258CC File Offset: 0x00023ACC
		internal static ButtonState ConvertToButtonState(CheckBoxState state)
		{
			switch (state)
			{
			case CheckBoxState.UncheckedPressed:
				return ButtonState.Pushed;
			case CheckBoxState.UncheckedDisabled:
				return ButtonState.Inactive;
			case CheckBoxState.CheckedNormal:
			case CheckBoxState.CheckedHot:
				return ButtonState.Checked;
			case CheckBoxState.CheckedPressed:
				return ButtonState.Checked | ButtonState.Pushed;
			case CheckBoxState.CheckedDisabled:
				return ButtonState.Checked | ButtonState.Inactive;
			case CheckBoxState.MixedNormal:
			case CheckBoxState.MixedHot:
				return ButtonState.Checked;
			case CheckBoxState.MixedPressed:
				return ButtonState.Checked | ButtonState.Pushed;
			case CheckBoxState.MixedDisabled:
				return ButtonState.Checked | ButtonState.Inactive;
			default:
				return ButtonState.Normal;
			}
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0002593C File Offset: 0x00023B3C
		internal static CheckBoxState ConvertFromButtonState(ButtonState state, bool isMixed, bool isHot)
		{
			if (isMixed)
			{
				if ((state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					return CheckBoxState.MixedPressed;
				}
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					return CheckBoxState.MixedDisabled;
				}
				if (isHot)
				{
					return CheckBoxState.MixedHot;
				}
				return CheckBoxState.MixedNormal;
			}
			else if ((state & ButtonState.Checked) == ButtonState.Checked)
			{
				if ((state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					return CheckBoxState.CheckedPressed;
				}
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					return CheckBoxState.CheckedDisabled;
				}
				if (isHot)
				{
					return CheckBoxState.CheckedHot;
				}
				return CheckBoxState.CheckedNormal;
			}
			else
			{
				if ((state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					return CheckBoxState.UncheckedPressed;
				}
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					return CheckBoxState.UncheckedDisabled;
				}
				if (isHot)
				{
					return CheckBoxState.UncheckedHot;
				}
				return CheckBoxState.UncheckedNormal;
			}
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x000259D2 File Offset: 0x00023BD2
		private static bool IsMixed(CheckBoxState state)
		{
			return state - CheckBoxState.MixedNormal <= 3;
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x000259DE File Offset: 0x00023BDE
		private static bool IsDisabled(CheckBoxState state)
		{
			return state == CheckBoxState.UncheckedDisabled || state == CheckBoxState.CheckedDisabled || state == CheckBoxState.MixedDisabled;
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x000259F0 File Offset: 0x00023BF0
		private static void InitializeRenderer(int state)
		{
			int part = CheckBoxRenderer.CheckBoxElement.Part;
			if (AccessibilityImprovements.Level2 && SystemInformation.HighContrast && CheckBoxRenderer.IsDisabled((CheckBoxState)state) && VisualStyleRenderer.IsCombinationDefined(CheckBoxRenderer.CheckBoxElement.ClassName, VisualStyleElement.Button.CheckBox.HighContrastDisabledPart))
			{
				part = VisualStyleElement.Button.CheckBox.HighContrastDisabledPart;
			}
			if (CheckBoxRenderer.visualStyleRenderer == null)
			{
				CheckBoxRenderer.visualStyleRenderer = new VisualStyleRenderer(CheckBoxRenderer.CheckBoxElement.ClassName, part, state);
				return;
			}
			CheckBoxRenderer.visualStyleRenderer.SetParameters(CheckBoxRenderer.CheckBoxElement.ClassName, part, state);
		}

		// Token: 0x04000774 RID: 1908
		[ThreadStatic]
		private static VisualStyleRenderer visualStyleRenderer = null;

		// Token: 0x04000775 RID: 1909
		private static readonly VisualStyleElement CheckBoxElement = VisualStyleElement.Button.CheckBox.UncheckedNormal;

		// Token: 0x04000776 RID: 1910
		private static bool renderMatchingApplicationState = true;
	}
}
