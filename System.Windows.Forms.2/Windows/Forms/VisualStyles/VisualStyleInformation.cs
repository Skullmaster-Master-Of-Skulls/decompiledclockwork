using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms.VisualStyles
{
	// Token: 0x02000455 RID: 1109
	public static class VisualStyleInformation
	{
		// Token: 0x170012FF RID: 4863
		// (get) Token: 0x06004D72 RID: 19826 RVA: 0x0013FEBD File Offset: 0x0013E0BD
		public static bool IsSupportedByOS
		{
			get
			{
				return OSFeature.Feature.IsPresent(OSFeature.Themes);
			}
		}

		// Token: 0x17001300 RID: 4864
		// (get) Token: 0x06004D73 RID: 19827 RVA: 0x0013FECE File Offset: 0x0013E0CE
		public static bool IsEnabledByUser
		{
			get
			{
				return VisualStyleInformation.IsSupportedByOS && SafeNativeMethods.IsAppThemed();
			}
		}

		// Token: 0x17001301 RID: 4865
		// (get) Token: 0x06004D74 RID: 19828 RVA: 0x0013FEE0 File Offset: 0x0013E0E0
		internal static string ThemeFilename
		{
			get
			{
				if (VisualStyleInformation.IsEnabledByUser)
				{
					StringBuilder stringBuilder = new StringBuilder(512);
					SafeNativeMethods.GetCurrentThemeName(stringBuilder, stringBuilder.Capacity, null, 0, null, 0);
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x17001302 RID: 4866
		// (get) Token: 0x06004D75 RID: 19829 RVA: 0x0013FF1C File Offset: 0x0013E11C
		public static string ColorScheme
		{
			get
			{
				if (VisualStyleInformation.IsEnabledByUser)
				{
					StringBuilder stringBuilder = new StringBuilder(512);
					SafeNativeMethods.GetCurrentThemeName(null, 0, stringBuilder, stringBuilder.Capacity, null, 0);
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x17001303 RID: 4867
		// (get) Token: 0x06004D76 RID: 19830 RVA: 0x0013FF58 File Offset: 0x0013E158
		public static string Size
		{
			get
			{
				if (VisualStyleInformation.IsEnabledByUser)
				{
					StringBuilder stringBuilder = new StringBuilder(512);
					SafeNativeMethods.GetCurrentThemeName(null, 0, null, 0, stringBuilder, stringBuilder.Capacity);
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x17001304 RID: 4868
		// (get) Token: 0x06004D77 RID: 19831 RVA: 0x0013FF94 File Offset: 0x0013E194
		public static string DisplayName
		{
			get
			{
				if (VisualStyleInformation.IsEnabledByUser)
				{
					StringBuilder stringBuilder = new StringBuilder(512);
					SafeNativeMethods.GetThemeDocumentationProperty(VisualStyleInformation.ThemeFilename, VisualStyleDocProperty.DisplayName, stringBuilder, stringBuilder.Capacity);
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x17001305 RID: 4869
		// (get) Token: 0x06004D78 RID: 19832 RVA: 0x0013FFD8 File Offset: 0x0013E1D8
		public static string Company
		{
			get
			{
				if (VisualStyleInformation.IsEnabledByUser)
				{
					StringBuilder stringBuilder = new StringBuilder(512);
					SafeNativeMethods.GetThemeDocumentationProperty(VisualStyleInformation.ThemeFilename, VisualStyleDocProperty.Company, stringBuilder, stringBuilder.Capacity);
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x17001306 RID: 4870
		// (get) Token: 0x06004D79 RID: 19833 RVA: 0x0014001C File Offset: 0x0013E21C
		public static string Author
		{
			get
			{
				if (VisualStyleInformation.IsEnabledByUser)
				{
					StringBuilder stringBuilder = new StringBuilder(512);
					SafeNativeMethods.GetThemeDocumentationProperty(VisualStyleInformation.ThemeFilename, VisualStyleDocProperty.Author, stringBuilder, stringBuilder.Capacity);
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x17001307 RID: 4871
		// (get) Token: 0x06004D7A RID: 19834 RVA: 0x00140060 File Offset: 0x0013E260
		public static string Copyright
		{
			get
			{
				if (VisualStyleInformation.IsEnabledByUser)
				{
					StringBuilder stringBuilder = new StringBuilder(512);
					SafeNativeMethods.GetThemeDocumentationProperty(VisualStyleInformation.ThemeFilename, VisualStyleDocProperty.Copyright, stringBuilder, stringBuilder.Capacity);
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x17001308 RID: 4872
		// (get) Token: 0x06004D7B RID: 19835 RVA: 0x001400A4 File Offset: 0x0013E2A4
		public static string Url
		{
			get
			{
				if (VisualStyleInformation.IsEnabledByUser)
				{
					StringBuilder stringBuilder = new StringBuilder(512);
					SafeNativeMethods.GetThemeDocumentationProperty(VisualStyleInformation.ThemeFilename, VisualStyleDocProperty.Url, stringBuilder, stringBuilder.Capacity);
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x17001309 RID: 4873
		// (get) Token: 0x06004D7C RID: 19836 RVA: 0x001400E8 File Offset: 0x0013E2E8
		public static string Version
		{
			get
			{
				if (VisualStyleInformation.IsEnabledByUser)
				{
					StringBuilder stringBuilder = new StringBuilder(512);
					SafeNativeMethods.GetThemeDocumentationProperty(VisualStyleInformation.ThemeFilename, VisualStyleDocProperty.Version, stringBuilder, stringBuilder.Capacity);
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x1700130A RID: 4874
		// (get) Token: 0x06004D7D RID: 19837 RVA: 0x0014012C File Offset: 0x0013E32C
		public static string Description
		{
			get
			{
				if (VisualStyleInformation.IsEnabledByUser)
				{
					StringBuilder stringBuilder = new StringBuilder(512);
					SafeNativeMethods.GetThemeDocumentationProperty(VisualStyleInformation.ThemeFilename, VisualStyleDocProperty.Description, stringBuilder, stringBuilder.Capacity);
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x1700130B RID: 4875
		// (get) Token: 0x06004D7E RID: 19838 RVA: 0x00140170 File Offset: 0x0013E370
		public static bool SupportsFlatMenus
		{
			get
			{
				if (Application.RenderWithVisualStyles)
				{
					if (VisualStyleInformation.visualStyleRenderer == null)
					{
						VisualStyleInformation.visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Window.Caption.Active);
					}
					else
					{
						VisualStyleInformation.visualStyleRenderer.SetParameters(VisualStyleElement.Window.Caption.Active);
					}
					return SafeNativeMethods.GetThemeSysBool(new HandleRef(null, VisualStyleInformation.visualStyleRenderer.Handle), VisualStyleSystemProperty.SupportsFlatMenus);
				}
				return false;
			}
		}

		// Token: 0x1700130C RID: 4876
		// (get) Token: 0x06004D7F RID: 19839 RVA: 0x001401C8 File Offset: 0x0013E3C8
		public static int MinimumColorDepth
		{
			get
			{
				if (Application.RenderWithVisualStyles)
				{
					if (VisualStyleInformation.visualStyleRenderer == null)
					{
						VisualStyleInformation.visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Window.Caption.Active);
					}
					else
					{
						VisualStyleInformation.visualStyleRenderer.SetParameters(VisualStyleElement.Window.Caption.Active);
					}
					int result = 0;
					SafeNativeMethods.GetThemeSysInt(new HandleRef(null, VisualStyleInformation.visualStyleRenderer.Handle), VisualStyleSystemProperty.MinimumColorDepth, ref result);
					return result;
				}
				return 0;
			}
		}

		// Token: 0x1700130D RID: 4877
		// (get) Token: 0x06004D80 RID: 19840 RVA: 0x00140228 File Offset: 0x0013E428
		public static Color TextControlBorder
		{
			get
			{
				if (Application.RenderWithVisualStyles)
				{
					if (VisualStyleInformation.visualStyleRenderer == null)
					{
						VisualStyleInformation.visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Normal);
					}
					else
					{
						VisualStyleInformation.visualStyleRenderer.SetParameters(VisualStyleElement.TextBox.TextEdit.Normal);
					}
					return VisualStyleInformation.visualStyleRenderer.GetColor(ColorProperty.BorderColor);
				}
				return SystemColors.WindowFrame;
			}
		}

		// Token: 0x1700130E RID: 4878
		// (get) Token: 0x06004D81 RID: 19841 RVA: 0x0014027C File Offset: 0x0013E47C
		public static Color ControlHighlightHot
		{
			get
			{
				if (Application.RenderWithVisualStyles)
				{
					if (VisualStyleInformation.visualStyleRenderer == null)
					{
						VisualStyleInformation.visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Normal);
					}
					else
					{
						VisualStyleInformation.visualStyleRenderer.SetParameters(VisualStyleElement.Button.PushButton.Normal);
					}
					return VisualStyleInformation.visualStyleRenderer.GetColor(ColorProperty.AccentColorHint);
				}
				return SystemColors.ButtonHighlight;
			}
		}

		// Token: 0x0400325E RID: 12894
		[ThreadStatic]
		private static VisualStyleRenderer visualStyleRenderer;
	}
}
