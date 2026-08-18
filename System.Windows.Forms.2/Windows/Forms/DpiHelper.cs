using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000112 RID: 274
	internal static class DpiHelper
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x00014F14 File Offset: 0x00013114
		private static void Initialize()
		{
			if (DpiHelper.isInitialized)
			{
				return;
			}
			if (DpiHelper.IsDpiAwarenessValueSet())
			{
				DpiHelper.enableHighDpi = true;
			}
			else
			{
				try
				{
					string text = ConfigurationManager.AppSettings.Get("EnableWindowsFormsHighDpiAutoResizing");
					if (!string.IsNullOrEmpty(text) && string.Equals(text, "true", StringComparison.InvariantCultureIgnoreCase))
					{
						DpiHelper.enableHighDpi = true;
					}
				}
				catch
				{
				}
			}
			if (DpiHelper.enableHighDpi)
			{
				try
				{
					DpiHelper.SetWinformsApplicationDpiAwareness();
				}
				catch (Exception ex)
				{
				}
				IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
				if (dc != IntPtr.Zero)
				{
					DpiHelper.deviceDpi = (double)UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, dc), 88);
					UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dc));
				}
			}
			DpiHelper.isInitialized = true;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00014FDC File Offset: 0x000131DC
		internal static bool IsDpiAwarenessValueSet()
		{
			bool result = false;
			try
			{
				if (string.IsNullOrEmpty(DpiHelper.dpiAwarenessValue))
				{
					DpiHelper.dpiAwarenessValue = ConfigurationOptions.GetConfigSettingValue("DpiAwareness");
				}
			}
			catch
			{
			}
			if (!string.IsNullOrEmpty(DpiHelper.dpiAwarenessValue))
			{
				string a = DpiHelper.dpiAwarenessValue.ToLowerInvariant();
				if (!(a == "true") && !(a == "system") && !(a == "true/pm") && !(a == "permonitor") && !(a == "permonitorv2"))
				{
					if (!(a == "false"))
					{
					}
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00015088 File Offset: 0x00013288
		internal static void InitializeDpiHelperForWinforms()
		{
			DpiHelper.Initialize();
			DpiHelper.InitializeDpiHelperQuirks();
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00015094 File Offset: 0x00013294
		internal static void InitializeDpiHelperQuirks()
		{
			if (DpiHelper.isDpiHelperQuirksInitialized)
			{
				return;
			}
			try
			{
				if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.CompareTo(ConfigurationOptions.RS2Version) >= 0 && DpiHelper.IsExpectedConfigValue("DisableDpiChangedMessageHandling", false) && DpiHelper.IsDpiAwarenessValueSet() && Application.RenderWithVisualStyles)
				{
					DpiHelper.enableDpiChangedMessageHandling = true;
				}
				if ((DpiHelper.IsScalingRequired || DpiHelper.enableDpiChangedMessageHandling) && DpiHelper.IsDpiAwarenessValueSet())
				{
					if (DpiHelper.IsExpectedConfigValue("CheckedListBox.DisableHighDpiImprovements", false))
					{
						DpiHelper.enableCheckedListBoxHighDpiImprovements = true;
					}
					if (DpiHelper.IsExpectedConfigValue("ToolStrip.DisableHighDpiImprovements", false))
					{
						DpiHelper.enableToolStripHighDpiImprovements = true;
					}
					if (DpiHelper.IsExpectedConfigValue("Form.DisableSinglePassScalingOfDpiForms", false))
					{
						DpiHelper.enableSinglePassScalingOfDpiForms = true;
					}
					if (DpiHelper.IsExpectedConfigValue("DataGridView.DisableHighDpiImprovements", false))
					{
						DpiHelper.enableDataGridViewControlHighDpiImprovements = true;
					}
					if (DpiHelper.IsExpectedConfigValue("AnchorLayout.DisableHighDpiImprovements", false))
					{
						DpiHelper.enableAnchorLayoutHighDpiImprovements = true;
					}
					if (DpiHelper.IsExpectedConfigValue("MonthCalendar.DisableHighDpiImprovements", false))
					{
						DpiHelper.enableMonthCalendarHighDpiImprovements = true;
					}
					if (DpiHelper.enableAnchorLayoutHighDpiImprovements && DpiHelper.IsExpectedConfigValue("AnchorLayout.EnableHighDpiImprovementsV2", true))
					{
						DpiHelper.enableAnchorLayoutHighDpiImprovementsV2 = true;
					}
					if (ConfigurationOptions.GetConfigSettingValue("DisableDpiChangedHighDpiImprovements") == null)
					{
						if (ConfigurationOptions.NetFrameworkVersion.CompareTo(DpiHelper.dpiChangedMessageHighDpiImprovementsMinimumFrameworkVersion) >= 0)
						{
							DpiHelper.enableDpiChangedHighDpiImprovements = true;
						}
					}
					else if (DpiHelper.IsExpectedConfigValue("DisableDpiChangedHighDpiImprovements", false))
					{
						DpiHelper.enableDpiChangedHighDpiImprovements = true;
					}
					DpiHelper.enableThreadExceptionDialogHighDpiImprovements = true;
				}
			}
			catch
			{
			}
			DpiHelper.isDpiHelperQuirksInitialized = true;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x000151FC File Offset: 0x000133FC
		internal static bool IsExpectedConfigValue(string configurationSettingName, bool expectedValue)
		{
			string configSettingValue = ConfigurationOptions.GetConfigSettingValue(configurationSettingName);
			bool flag;
			if (!bool.TryParse(configSettingValue, out flag))
			{
				flag = false;
			}
			return flag == expectedValue;
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x00015220 File Offset: 0x00013420
		internal static bool EnableDpiChangedHighDpiImprovements
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				return DpiHelper.enableDpiChangedHighDpiImprovements;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x0001522C File Offset: 0x0001342C
		internal static bool EnableToolStripHighDpiImprovements
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				return DpiHelper.enableToolStripHighDpiImprovements;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x00015238 File Offset: 0x00013438
		internal static bool EnableToolStripPerMonitorV2HighDpiImprovements
		{
			get
			{
				return DpiHelper.EnableDpiChangedMessageHandling && DpiHelper.enableToolStripHighDpiImprovements && DpiHelper.enableDpiChangedHighDpiImprovements;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x00015250 File Offset: 0x00013450
		internal static bool EnableDpiChangedMessageHandling
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				if (DpiHelper.enableDpiChangedMessageHandling)
				{
					DpiAwarenessContext threadDpiAwarenessContext = CommonUnsafeNativeMethods.GetThreadDpiAwarenessContext();
					return CommonUnsafeNativeMethods.TryFindDpiAwarenessContextsEqual(threadDpiAwarenessContext, DpiAwarenessContext.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
				}
				return false;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x00015279 File Offset: 0x00013479
		internal static bool EnableCheckedListBoxHighDpiImprovements
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				return DpiHelper.enableCheckedListBoxHighDpiImprovements;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x00015285 File Offset: 0x00013485
		internal static bool EnableSinglePassScalingOfDpiForms
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				return DpiHelper.enableSinglePassScalingOfDpiForms;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x00015291 File Offset: 0x00013491
		internal static bool EnableThreadExceptionDialogHighDpiImprovements
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				return DpiHelper.enableThreadExceptionDialogHighDpiImprovements;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0001529D File Offset: 0x0001349D
		internal static bool EnableDataGridViewControlHighDpiImprovements
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				return DpiHelper.enableDataGridViewControlHighDpiImprovements;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x000152A9 File Offset: 0x000134A9
		internal static bool EnableAnchorLayoutHighDpiImprovements
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				return DpiHelper.enableAnchorLayoutHighDpiImprovements;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x000152B5 File Offset: 0x000134B5
		internal static bool EnableMonthCalendarHighDpiImprovements
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				return DpiHelper.enableMonthCalendarHighDpiImprovements;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x000152C1 File Offset: 0x000134C1
		internal static bool EnableAnchorLayoutHighDpiImprovementsV2
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				return DpiHelper.enableAnchorLayoutHighDpiImprovementsV2;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x000152CD File Offset: 0x000134CD
		internal static int DeviceDpi
		{
			get
			{
				DpiHelper.Initialize();
				return (int)DpiHelper.deviceDpi;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x000152DA File Offset: 0x000134DA
		private static double LogicalToDeviceUnitsScalingFactor
		{
			get
			{
				if (DpiHelper.logicalToDeviceUnitsScalingFactor == 0.0)
				{
					DpiHelper.Initialize();
					DpiHelper.logicalToDeviceUnitsScalingFactor = DpiHelper.deviceDpi / 96.0;
				}
				return DpiHelper.logicalToDeviceUnitsScalingFactor;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x0001530C File Offset: 0x0001350C
		private static InterpolationMode InterpolationMode
		{
			get
			{
				if (DpiHelper.interpolationMode == InterpolationMode.Invalid)
				{
					int num = (int)Math.Round(DpiHelper.LogicalToDeviceUnitsScalingFactor * 100.0);
					if (num % 100 == 0)
					{
						DpiHelper.interpolationMode = InterpolationMode.NearestNeighbor;
					}
					else if (num < 100)
					{
						DpiHelper.interpolationMode = InterpolationMode.HighQualityBilinear;
					}
					else
					{
						DpiHelper.interpolationMode = InterpolationMode.HighQualityBicubic;
					}
				}
				return DpiHelper.interpolationMode;
			}
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00015360 File Offset: 0x00013560
		private static Bitmap ScaleBitmapToSize(Bitmap logicalImage, Size deviceImageSize)
		{
			Bitmap bitmap = new Bitmap(deviceImageSize.Width, deviceImageSize.Height, logicalImage.PixelFormat);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.InterpolationMode = DpiHelper.InterpolationMode;
				RectangleF srcRect = new RectangleF(0f, 0f, (float)logicalImage.Size.Width, (float)logicalImage.Size.Height);
				RectangleF destRect = new RectangleF(0f, 0f, (float)deviceImageSize.Width, (float)deviceImageSize.Height);
				srcRect.Offset(-0.5f, -0.5f);
				graphics.DrawImage(logicalImage, destRect, srcRect, GraphicsUnit.Pixel);
			}
			return bitmap;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00015424 File Offset: 0x00013624
		private static Bitmap CreateScaledBitmap(Bitmap logicalImage, int deviceDpi = 0)
		{
			Size deviceImageSize = DpiHelper.LogicalToDeviceUnits(logicalImage.Size, deviceDpi);
			return DpiHelper.ScaleBitmapToSize(logicalImage, deviceImageSize);
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x00015445 File Offset: 0x00013645
		public static bool IsScalingRequired
		{
			get
			{
				DpiHelper.Initialize();
				return DpiHelper.deviceDpi != 96.0;
			}
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00015460 File Offset: 0x00013660
		public static int LogicalToDeviceUnits(int value, int devicePixels = 0)
		{
			if (devicePixels == 0)
			{
				return (int)Math.Round(DpiHelper.LogicalToDeviceUnitsScalingFactor * (double)value);
			}
			double num = (double)devicePixels / 96.0;
			return (int)Math.Round(num * (double)value);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00015498 File Offset: 0x00013698
		public static double LogicalToDeviceUnits(double value, int devicePixels = 0)
		{
			if (devicePixels == 0)
			{
				return DpiHelper.LogicalToDeviceUnitsScalingFactor * value;
			}
			double num = (double)devicePixels / 96.0;
			return num * value;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x000154C0 File Offset: 0x000136C0
		public static int LogicalToDeviceUnitsX(int value)
		{
			return DpiHelper.LogicalToDeviceUnits(value, 0);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x000154C0 File Offset: 0x000136C0
		public static int LogicalToDeviceUnitsY(int value)
		{
			return DpiHelper.LogicalToDeviceUnits(value, 0);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x000154C9 File Offset: 0x000136C9
		public static Size LogicalToDeviceUnits(Size logicalSize, int deviceDpi = 0)
		{
			return new Size(DpiHelper.LogicalToDeviceUnits(logicalSize.Width, deviceDpi), DpiHelper.LogicalToDeviceUnits(logicalSize.Height, deviceDpi));
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x000154EA File Offset: 0x000136EA
		public static Bitmap CreateResizedBitmap(Bitmap logicalImage, Size targetImageSize)
		{
			if (logicalImage == null)
			{
				return null;
			}
			return DpiHelper.ScaleBitmapToSize(logicalImage, targetImageSize);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x000154F8 File Offset: 0x000136F8
		public static void ScaleBitmapLogicalToDevice(ref Bitmap logicalBitmap, int deviceDpi = 0)
		{
			if (logicalBitmap == null)
			{
				return;
			}
			Bitmap bitmap = DpiHelper.CreateScaledBitmap(logicalBitmap, deviceDpi);
			if (bitmap != null)
			{
				logicalBitmap.Dispose();
				logicalBitmap = bitmap;
			}
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00015520 File Offset: 0x00013720
		public static int ConvertToGivenDpiPixel(int value, double pixelFactor)
		{
			int num = (int)Math.Round((double)value * pixelFactor);
			if (num != 0)
			{
				return num;
			}
			return 1;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0001553E File Offset: 0x0001373E
		public static IDisposable EnterDpiAwarenessScope(DpiAwarenessContext awareness)
		{
			return new DpiHelper.DpiAwarenessScope(awareness);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00015548 File Offset: 0x00013748
		public static T CreateInstanceInSystemAwareContext<T>(Func<T> createInstance)
		{
			T result;
			using (DpiHelper.EnterDpiAwarenessScope(DpiAwarenessContext.DPI_AWARENESS_CONTEXT_SYSTEM_AWARE))
			{
				result = createInstance();
			}
			return result;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00015584 File Offset: 0x00013784
		public static bool SetWinformsApplicationDpiAwareness()
		{
			Version version = Environment.OSVersion.Version;
			if (!DpiHelper.IsDpiAwarenessValueSet() || Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				return false;
			}
			string a = (DpiHelper.dpiAwarenessValue ?? string.Empty).ToLowerInvariant();
			if (version.CompareTo(ConfigurationOptions.RS2Version) >= 0)
			{
				int processDpiAwarenessContext;
				if (!(a == "true") && !(a == "system"))
				{
					if (!(a == "true/pm") && !(a == "permonitor"))
					{
						if (!(a == "permonitorv2"))
						{
							if (!(a == "false"))
							{
							}
							processDpiAwarenessContext = -1;
						}
						else
						{
							processDpiAwarenessContext = -4;
						}
					}
					else
					{
						processDpiAwarenessContext = -3;
					}
				}
				else
				{
					processDpiAwarenessContext = -2;
				}
				if (!SafeNativeMethods.SetProcessDpiAwarenessContext(processDpiAwarenessContext))
				{
					return false;
				}
			}
			else if (version.CompareTo(new Version(6, 3, 0, 0)) >= 0 && version.CompareTo(ConfigurationOptions.RS2Version) < 0)
			{
				NativeMethods.PROCESS_DPI_AWARENESS process_DPI_AWARENESS;
				if (!(a == "false"))
				{
					if (!(a == "true") && !(a == "system"))
					{
						if (!(a == "true/pm") && !(a == "permonitor") && !(a == "permonitorv2"))
						{
							process_DPI_AWARENESS = NativeMethods.PROCESS_DPI_AWARENESS.PROCESS_DPI_UNINITIALIZED;
						}
						else
						{
							process_DPI_AWARENESS = NativeMethods.PROCESS_DPI_AWARENESS.PROCESS_PER_MONITOR_DPI_AWARE;
						}
					}
					else
					{
						process_DPI_AWARENESS = NativeMethods.PROCESS_DPI_AWARENESS.PROCESS_SYSTEM_DPI_AWARE;
					}
				}
				else
				{
					process_DPI_AWARENESS = NativeMethods.PROCESS_DPI_AWARENESS.PROCESS_DPI_UNAWARE;
				}
				if (SafeNativeMethods.SetProcessDpiAwareness(process_DPI_AWARENESS) != 0)
				{
					return false;
				}
			}
			else
			{
				if (version.CompareTo(new Version(6, 1, 0, 0)) < 0 || version.CompareTo(new Version(6, 3, 0, 0)) >= 0)
				{
					return false;
				}
				NativeMethods.PROCESS_DPI_AWARENESS process_DPI_AWARENESS;
				if (!(a == "false"))
				{
					if (!(a == "true") && !(a == "system") && !(a == "true/pm") && !(a == "permonitor") && !(a == "permonitorv2"))
					{
						process_DPI_AWARENESS = NativeMethods.PROCESS_DPI_AWARENESS.PROCESS_DPI_UNINITIALIZED;
					}
					else
					{
						process_DPI_AWARENESS = NativeMethods.PROCESS_DPI_AWARENESS.PROCESS_SYSTEM_DPI_AWARE;
					}
				}
				else
				{
					process_DPI_AWARENESS = NativeMethods.PROCESS_DPI_AWARENESS.PROCESS_DPI_UNAWARE;
				}
				if (process_DPI_AWARENESS == NativeMethods.PROCESS_DPI_AWARENESS.PROCESS_SYSTEM_DPI_AWARE && !SafeNativeMethods.SetProcessDPIAware())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001575D File Offset: 0x0001395D
		public static Padding LogicalToDeviceUnits(Padding logicalPadding, int deviceDpi = 0)
		{
			return new Padding(DpiHelper.LogicalToDeviceUnits(logicalPadding.Left, deviceDpi), DpiHelper.LogicalToDeviceUnits(logicalPadding.Top, deviceDpi), DpiHelper.LogicalToDeviceUnits(logicalPadding.Right, deviceDpi), DpiHelper.LogicalToDeviceUnits(logicalPadding.Bottom, deviceDpi));
		}

		// Token: 0x040004EF RID: 1263
		internal const double LogicalDpi = 96.0;

		// Token: 0x040004F0 RID: 1264
		private static bool isInitialized = false;

		// Token: 0x040004F1 RID: 1265
		private static double deviceDpi = 96.0;

		// Token: 0x040004F2 RID: 1266
		private static double logicalToDeviceUnitsScalingFactor = 0.0;

		// Token: 0x040004F3 RID: 1267
		private static bool enableHighDpi = false;

		// Token: 0x040004F4 RID: 1268
		private static string dpiAwarenessValue = null;

		// Token: 0x040004F5 RID: 1269
		private static InterpolationMode interpolationMode = InterpolationMode.Invalid;

		// Token: 0x040004F6 RID: 1270
		private static bool isDpiHelperQuirksInitialized = false;

		// Token: 0x040004F7 RID: 1271
		private static bool enableToolStripHighDpiImprovements = false;

		// Token: 0x040004F8 RID: 1272
		private static bool enableDpiChangedMessageHandling = false;

		// Token: 0x040004F9 RID: 1273
		private static bool enableCheckedListBoxHighDpiImprovements = false;

		// Token: 0x040004FA RID: 1274
		private static bool enableThreadExceptionDialogHighDpiImprovements = false;

		// Token: 0x040004FB RID: 1275
		private static bool enableDataGridViewControlHighDpiImprovements = false;

		// Token: 0x040004FC RID: 1276
		private static bool enableSinglePassScalingOfDpiForms = false;

		// Token: 0x040004FD RID: 1277
		private static bool enableAnchorLayoutHighDpiImprovements = false;

		// Token: 0x040004FE RID: 1278
		private static bool enableMonthCalendarHighDpiImprovements = false;

		// Token: 0x040004FF RID: 1279
		private static bool enableDpiChangedHighDpiImprovements = false;

		// Token: 0x04000500 RID: 1280
		private static bool enableAnchorLayoutHighDpiImprovementsV2 = false;

		// Token: 0x04000501 RID: 1281
		private static readonly Version dpiChangedMessageHighDpiImprovementsMinimumFrameworkVersion = new Version(4, 8);

		// Token: 0x020005FD RID: 1533
		private class DpiAwarenessScope : IDisposable
		{
			// Token: 0x060061C0 RID: 25024 RVA: 0x00169308 File Offset: 0x00167508
			public DpiAwarenessScope(DpiAwarenessContext awareness)
			{
				if (DpiHelper.EnableDpiChangedHighDpiImprovements)
				{
					try
					{
						if (!CommonUnsafeNativeMethods.TryFindDpiAwarenessContextsEqual(awareness, DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED))
						{
							this.originalAwareness = CommonUnsafeNativeMethods.GetThreadDpiAwarenessContext();
							if (!CommonUnsafeNativeMethods.TryFindDpiAwarenessContextsEqual(this.originalAwareness, awareness) && !CommonUnsafeNativeMethods.TryFindDpiAwarenessContextsEqual(this.originalAwareness, DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNAWARE))
							{
								this.originalAwareness = CommonUnsafeNativeMethods.SetThreadDpiAwarenessContext(awareness);
								this.dpiAwarenessScopeIsSet = true;
							}
						}
					}
					catch (EntryPointNotFoundException)
					{
						this.dpiAwarenessScopeIsSet = false;
					}
				}
			}

			// Token: 0x060061C1 RID: 25025 RVA: 0x00169384 File Offset: 0x00167584
			public void Dispose()
			{
				this.ResetDpiAwarenessContextChanges();
			}

			// Token: 0x060061C2 RID: 25026 RVA: 0x0016938C File Offset: 0x0016758C
			private void ResetDpiAwarenessContextChanges()
			{
				if (this.dpiAwarenessScopeIsSet)
				{
					CommonUnsafeNativeMethods.TrySetThreadDpiAwarenessContext(this.originalAwareness);
					this.dpiAwarenessScopeIsSet = false;
				}
			}

			// Token: 0x040038A0 RID: 14496
			private bool dpiAwarenessScopeIsSet;

			// Token: 0x040038A1 RID: 14497
			private DpiAwarenessContext originalAwareness;
		}
	}
}
