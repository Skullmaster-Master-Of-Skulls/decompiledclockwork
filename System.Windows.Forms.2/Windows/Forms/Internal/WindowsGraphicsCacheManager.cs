using System;
using System.Collections.Generic;
using System.Drawing;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004E8 RID: 1256
	internal class WindowsGraphicsCacheManager
	{
		// Token: 0x060051E9 RID: 20969 RVA: 0x00002843 File Offset: 0x00000A43
		private WindowsGraphicsCacheManager()
		{
		}

		// Token: 0x170013B1 RID: 5041
		// (get) Token: 0x060051EA RID: 20970 RVA: 0x001543FA File Offset: 0x001525FA
		private static List<KeyValuePair<Font, WindowsFont>> WindowsFontCache
		{
			get
			{
				if (WindowsGraphicsCacheManager.windowsFontCache == null)
				{
					WindowsGraphicsCacheManager.currentIndex = -1;
					WindowsGraphicsCacheManager.windowsFontCache = new List<KeyValuePair<Font, WindowsFont>>(10);
				}
				return WindowsGraphicsCacheManager.windowsFontCache;
			}
		}

		// Token: 0x170013B2 RID: 5042
		// (get) Token: 0x060051EB RID: 20971 RVA: 0x0015441A File Offset: 0x0015261A
		public static WindowsGraphics MeasurementGraphics
		{
			get
			{
				if (WindowsGraphicsCacheManager.measurementGraphics == null || WindowsGraphicsCacheManager.measurementGraphics.DeviceContext == null)
				{
					WindowsGraphicsCacheManager.measurementGraphics = WindowsGraphics.CreateMeasurementWindowsGraphics();
				}
				return WindowsGraphicsCacheManager.measurementGraphics;
			}
		}

		// Token: 0x060051EC RID: 20972 RVA: 0x0015443E File Offset: 0x0015263E
		internal static WindowsGraphics GetCurrentMeasurementGraphics()
		{
			return WindowsGraphicsCacheManager.measurementGraphics;
		}

		// Token: 0x060051ED RID: 20973 RVA: 0x00154445 File Offset: 0x00152645
		public static WindowsFont GetWindowsFont(Font font)
		{
			return WindowsGraphicsCacheManager.GetWindowsFont(font, WindowsFontQuality.Default);
		}

		// Token: 0x060051EE RID: 20974 RVA: 0x00154450 File Offset: 0x00152650
		public static WindowsFont GetWindowsFont(Font font, WindowsFontQuality fontQuality)
		{
			if (font == null)
			{
				return null;
			}
			int i = 0;
			int num = WindowsGraphicsCacheManager.currentIndex;
			while (i < WindowsGraphicsCacheManager.WindowsFontCache.Count)
			{
				if (WindowsGraphicsCacheManager.WindowsFontCache[num].Key.Equals(font))
				{
					WindowsFont value = WindowsGraphicsCacheManager.WindowsFontCache[num].Value;
					if (value.Quality == fontQuality)
					{
						return value;
					}
				}
				num--;
				i++;
				if (num < 0)
				{
					num = 9;
				}
			}
			WindowsFont windowsFont = WindowsFont.FromFont(font, fontQuality);
			KeyValuePair<Font, WindowsFont> keyValuePair = new KeyValuePair<Font, WindowsFont>(font, windowsFont);
			WindowsGraphicsCacheManager.currentIndex++;
			if (WindowsGraphicsCacheManager.currentIndex == 10)
			{
				WindowsGraphicsCacheManager.currentIndex = 0;
			}
			if (WindowsGraphicsCacheManager.WindowsFontCache.Count == 10)
			{
				WindowsFont windowsFont2 = null;
				bool flag = false;
				int num2 = WindowsGraphicsCacheManager.currentIndex;
				int num3 = num2 + 1;
				while (!flag)
				{
					if (num3 >= 10)
					{
						num3 = 0;
					}
					if (num3 == num2)
					{
						flag = true;
					}
					windowsFont2 = WindowsGraphicsCacheManager.WindowsFontCache[num3].Value;
					if (!DeviceContexts.IsFontInUse(windowsFont2))
					{
						WindowsGraphicsCacheManager.currentIndex = num3;
						break;
					}
					num3++;
					windowsFont2 = null;
				}
				if (windowsFont2 != null)
				{
					WindowsGraphicsCacheManager.WindowsFontCache[WindowsGraphicsCacheManager.currentIndex] = keyValuePair;
					windowsFont.OwnedByCacheManager = true;
					windowsFont2.OwnedByCacheManager = false;
					windowsFont2.Dispose();
				}
				else
				{
					windowsFont.OwnedByCacheManager = false;
				}
			}
			else
			{
				windowsFont.OwnedByCacheManager = true;
				WindowsGraphicsCacheManager.WindowsFontCache.Add(keyValuePair);
			}
			return windowsFont;
		}

		// Token: 0x040035F2 RID: 13810
		[ThreadStatic]
		private static WindowsGraphics measurementGraphics;

		// Token: 0x040035F3 RID: 13811
		private const int CacheSize = 10;

		// Token: 0x040035F4 RID: 13812
		[ThreadStatic]
		private static int currentIndex;

		// Token: 0x040035F5 RID: 13813
		[ThreadStatic]
		private static List<KeyValuePair<Font, WindowsFont>> windowsFontCache;
	}
}
