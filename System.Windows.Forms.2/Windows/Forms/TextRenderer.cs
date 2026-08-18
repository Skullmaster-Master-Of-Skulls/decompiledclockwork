using System;
using System.Drawing;
using System.Windows.Forms.Internal;

namespace System.Windows.Forms
{
	// Token: 0x0200044F RID: 1103
	public sealed class TextRenderer
	{
		// Token: 0x06004D4A RID: 19786 RVA: 0x00002843 File Offset: 0x00000A43
		private TextRenderer()
		{
		}

		// Token: 0x06004D4B RID: 19787 RVA: 0x0013F448 File Offset: 0x0013D648
		public static void DrawText(IDeviceContext dc, string text, Font font, Point pt, Color foreColor)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			WindowsFontQuality fontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);
			IntPtr hdc = dc.GetHdc();
			try
			{
				using (WindowsGraphics windowsGraphics = WindowsGraphics.FromHdc(hdc))
				{
					using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(font, fontQuality))
					{
						windowsGraphics.DrawText(text, windowsFont, pt, foreColor);
					}
				}
			}
			finally
			{
				dc.ReleaseHdc();
			}
		}

		// Token: 0x06004D4C RID: 19788 RVA: 0x0013F4D8 File Offset: 0x0013D6D8
		public static void DrawText(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, Color backColor)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			WindowsFontQuality fontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);
			IntPtr hdc = dc.GetHdc();
			try
			{
				using (WindowsGraphics windowsGraphics = WindowsGraphics.FromHdc(hdc))
				{
					using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(font, fontQuality))
					{
						windowsGraphics.DrawText(text, windowsFont, pt, foreColor, backColor);
					}
				}
			}
			finally
			{
				dc.ReleaseHdc();
			}
		}

		// Token: 0x06004D4D RID: 19789 RVA: 0x0013F568 File Offset: 0x0013D768
		public static void DrawText(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, TextFormatFlags flags)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			WindowsFontQuality fontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);
			using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, flags))
			{
				using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(font, fontQuality))
				{
					windowsGraphicsWrapper.WindowsGraphics.DrawText(text, windowsFont, pt, foreColor, TextRenderer.GetIntTextFormatFlags(flags));
				}
			}
		}

		// Token: 0x06004D4E RID: 19790 RVA: 0x0013F5EC File Offset: 0x0013D7EC
		public static void DrawText(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, Color backColor, TextFormatFlags flags)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			WindowsFontQuality fontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);
			using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, flags))
			{
				using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(font, fontQuality))
				{
					windowsGraphicsWrapper.WindowsGraphics.DrawText(text, windowsFont, pt, foreColor, backColor, TextRenderer.GetIntTextFormatFlags(flags));
				}
			}
		}

		// Token: 0x06004D4F RID: 19791 RVA: 0x0013F670 File Offset: 0x0013D870
		public static void DrawText(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			WindowsFontQuality fontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);
			IntPtr hdc = dc.GetHdc();
			try
			{
				using (WindowsGraphics windowsGraphics = WindowsGraphics.FromHdc(hdc))
				{
					using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(font, fontQuality))
					{
						windowsGraphics.DrawText(text, windowsFont, bounds, foreColor);
					}
				}
			}
			finally
			{
				dc.ReleaseHdc();
			}
		}

		// Token: 0x06004D50 RID: 19792 RVA: 0x0013F700 File Offset: 0x0013D900
		public static void DrawText(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, Color backColor)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			WindowsFontQuality fontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);
			IntPtr hdc = dc.GetHdc();
			try
			{
				using (WindowsGraphics windowsGraphics = WindowsGraphics.FromHdc(hdc))
				{
					using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(font, fontQuality))
					{
						windowsGraphics.DrawText(text, windowsFont, bounds, foreColor, backColor);
					}
				}
			}
			finally
			{
				dc.ReleaseHdc();
			}
		}

		// Token: 0x06004D51 RID: 19793 RVA: 0x0013F790 File Offset: 0x0013D990
		public static void DrawText(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, TextFormatFlags flags)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			WindowsFontQuality fontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);
			using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, flags))
			{
				using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(font, fontQuality))
				{
					windowsGraphicsWrapper.WindowsGraphics.DrawText(text, windowsFont, bounds, foreColor, TextRenderer.GetIntTextFormatFlags(flags));
				}
			}
		}

		// Token: 0x06004D52 RID: 19794 RVA: 0x0013F814 File Offset: 0x0013DA14
		public static void DrawText(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, Color backColor, TextFormatFlags flags)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			WindowsFontQuality fontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);
			using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, flags))
			{
				using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(font, fontQuality))
				{
					windowsGraphicsWrapper.WindowsGraphics.DrawText(text, windowsFont, bounds, foreColor, backColor, TextRenderer.GetIntTextFormatFlags(flags));
				}
			}
		}

		// Token: 0x06004D53 RID: 19795 RVA: 0x0013F898 File Offset: 0x0013DA98
		private static IntTextFormatFlags GetIntTextFormatFlags(TextFormatFlags flags)
		{
			if (((ulong)flags & 18446744073692774400UL) == 0UL)
			{
				return (IntTextFormatFlags)flags;
			}
			return (IntTextFormatFlags)(flags & (TextFormatFlags)16777215);
		}

		// Token: 0x06004D54 RID: 19796 RVA: 0x0013F8BC File Offset: 0x0013DABC
		public static Size MeasureText(string text, Font font)
		{
			if (string.IsNullOrEmpty(text))
			{
				return Size.Empty;
			}
			Size result;
			using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(font))
			{
				result = WindowsGraphicsCacheManager.MeasurementGraphics.MeasureText(text, windowsFont);
			}
			return result;
		}

		// Token: 0x06004D55 RID: 19797 RVA: 0x0013F908 File Offset: 0x0013DB08
		public static Size MeasureText(string text, Font font, Size proposedSize)
		{
			if (string.IsNullOrEmpty(text))
			{
				return Size.Empty;
			}
			Size result;
			using (WindowsGraphicsCacheManager.GetWindowsFont(font))
			{
				result = WindowsGraphicsCacheManager.MeasurementGraphics.MeasureText(text, WindowsGraphicsCacheManager.GetWindowsFont(font), proposedSize);
			}
			return result;
		}

		// Token: 0x06004D56 RID: 19798 RVA: 0x0013F95C File Offset: 0x0013DB5C
		public static Size MeasureText(string text, Font font, Size proposedSize, TextFormatFlags flags)
		{
			if (string.IsNullOrEmpty(text))
			{
				return Size.Empty;
			}
			Size result;
			using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(font))
			{
				result = WindowsGraphicsCacheManager.MeasurementGraphics.MeasureText(text, windowsFont, proposedSize, TextRenderer.GetIntTextFormatFlags(flags));
			}
			return result;
		}

		// Token: 0x06004D57 RID: 19799 RVA: 0x0013F9B0 File Offset: 0x0013DBB0
		public static Size MeasureText(IDeviceContext dc, string text, Font font)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (string.IsNullOrEmpty(text))
			{
				return Size.Empty;
			}
			WindowsFontQuality fontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);
			IntPtr hdc = dc.GetHdc();
			Size result;
			try
			{
				using (WindowsGraphics windowsGraphics = WindowsGraphics.FromHdc(hdc))
				{
					using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(font, fontQuality))
					{
						result = windowsGraphics.MeasureText(text, windowsFont);
					}
				}
			}
			finally
			{
				dc.ReleaseHdc();
			}
			return result;
		}

		// Token: 0x06004D58 RID: 19800 RVA: 0x0013FA4C File Offset: 0x0013DC4C
		public static Size MeasureText(IDeviceContext dc, string text, Font font, Size proposedSize)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (string.IsNullOrEmpty(text))
			{
				return Size.Empty;
			}
			WindowsFontQuality fontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);
			IntPtr hdc = dc.GetHdc();
			Size result;
			try
			{
				using (WindowsGraphics windowsGraphics = WindowsGraphics.FromHdc(hdc))
				{
					using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(font, fontQuality))
					{
						result = windowsGraphics.MeasureText(text, windowsFont, proposedSize);
					}
				}
			}
			finally
			{
				dc.ReleaseHdc();
			}
			return result;
		}

		// Token: 0x06004D59 RID: 19801 RVA: 0x0013FAEC File Offset: 0x0013DCEC
		public static Size MeasureText(IDeviceContext dc, string text, Font font, Size proposedSize, TextFormatFlags flags)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (string.IsNullOrEmpty(text))
			{
				return Size.Empty;
			}
			WindowsFontQuality fontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);
			Size result;
			using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, flags))
			{
				using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(font, fontQuality))
				{
					result = windowsGraphicsWrapper.WindowsGraphics.MeasureText(text, windowsFont, proposedSize, TextRenderer.GetIntTextFormatFlags(flags));
				}
			}
			return result;
		}

		// Token: 0x06004D5A RID: 19802 RVA: 0x0013FB7C File Offset: 0x0013DD7C
		internal static Color DisabledTextColor(Color backColor)
		{
			if (SystemInformation.HighContrast && AccessibilityImprovements.Level1)
			{
				return SystemColors.GrayText;
			}
			Color result = SystemColors.ControlDark;
			if (ControlPaint.IsDarker(backColor, SystemColors.Control))
			{
				result = ControlPaint.Dark(backColor);
			}
			return result;
		}
	}
}
