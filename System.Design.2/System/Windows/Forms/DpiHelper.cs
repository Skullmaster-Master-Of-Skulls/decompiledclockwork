using System;
using System.Configuration;
using System.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x0200028A RID: 650
	internal static class DpiHelper
	{
		// Token: 0x060018C2 RID: 6338 RVA: 0x0008B4A0 File Offset: 0x000896A0
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
				IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
				if (dc != IntPtr.Zero)
				{
					DpiHelper.deviceDpi = (double)UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, dc), 88);
					UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dc));
				}
			}
			DpiHelper.isInitialized = true;
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x0008B554 File Offset: 0x00089754
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

		// Token: 0x060018C4 RID: 6340 RVA: 0x0008B600 File Offset: 0x00089800
		internal static void InitializeDpiHelperForWinforms()
		{
			DpiHelper.Initialize();
			DpiHelper.InitializeDpiHelperQuirks();
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0008B60C File Offset: 0x0008980C
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

		// Token: 0x060018C6 RID: 6342 RVA: 0x0008B774 File Offset: 0x00089974
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

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x0008B798 File Offset: 0x00089998
		internal static bool EnableDpiChangedHighDpiImprovements
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				return DpiHelper.enableDpiChangedHighDpiImprovements;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x0008B7A4 File Offset: 0x000899A4
		internal static bool EnableToolStripHighDpiImprovements
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				return DpiHelper.enableToolStripHighDpiImprovements;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060018C9 RID: 6345 RVA: 0x0008B7B0 File Offset: 0x000899B0
		internal static bool EnableToolStripPerMonitorV2HighDpiImprovements
		{
			get
			{
				return DpiHelper.EnableDpiChangedMessageHandling && DpiHelper.enableToolStripHighDpiImprovements && DpiHelper.enableDpiChangedHighDpiImprovements;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060018CA RID: 6346 RVA: 0x0008B7C8 File Offset: 0x000899C8
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

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x0008B7F1 File Offset: 0x000899F1
		internal static bool EnableCheckedListBoxHighDpiImprovements
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				return DpiHelper.enableCheckedListBoxHighDpiImprovements;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x0008B7FD File Offset: 0x000899FD
		internal static bool EnableSinglePassScalingOfDpiForms
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				return DpiHelper.enableSinglePassScalingOfDpiForms;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x0008B809 File Offset: 0x00089A09
		internal static bool EnableThreadExceptionDialogHighDpiImprovements
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				return DpiHelper.enableThreadExceptionDialogHighDpiImprovements;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x0008B815 File Offset: 0x00089A15
		internal static bool EnableDataGridViewControlHighDpiImprovements
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				return DpiHelper.enableDataGridViewControlHighDpiImprovements;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060018CF RID: 6351 RVA: 0x0008B821 File Offset: 0x00089A21
		internal static bool EnableAnchorLayoutHighDpiImprovements
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				return DpiHelper.enableAnchorLayoutHighDpiImprovements;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x0008B82D File Offset: 0x00089A2D
		internal static bool EnableMonthCalendarHighDpiImprovements
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				return DpiHelper.enableMonthCalendarHighDpiImprovements;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060018D1 RID: 6353 RVA: 0x0008B839 File Offset: 0x00089A39
		internal static bool EnableAnchorLayoutHighDpiImprovementsV2
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				return DpiHelper.enableAnchorLayoutHighDpiImprovementsV2;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x0008B845 File Offset: 0x00089A45
		internal static int DeviceDpi
		{
			get
			{
				DpiHelper.Initialize();
				return (int)DpiHelper.deviceDpi;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060018D3 RID: 6355 RVA: 0x0008B852 File Offset: 0x00089A52
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

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060018D4 RID: 6356 RVA: 0x0008B884 File Offset: 0x00089A84
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

		// Token: 0x060018D5 RID: 6357 RVA: 0x0008B8D8 File Offset: 0x00089AD8
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

		// Token: 0x060018D6 RID: 6358 RVA: 0x0008B99C File Offset: 0x00089B9C
		private static Bitmap CreateScaledBitmap(Bitmap logicalImage, int deviceDpi = 0)
		{
			Size deviceImageSize = DpiHelper.LogicalToDeviceUnits(logicalImage.Size, deviceDpi);
			return DpiHelper.ScaleBitmapToSize(logicalImage, deviceImageSize);
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x0008B9BD File Offset: 0x00089BBD
		public static bool IsScalingRequired
		{
			get
			{
				DpiHelper.Initialize();
				return DpiHelper.deviceDpi != 96.0;
			}
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x0008B9D8 File Offset: 0x00089BD8
		public static int LogicalToDeviceUnits(int value, int devicePixels = 0)
		{
			if (devicePixels == 0)
			{
				return (int)Math.Round(DpiHelper.LogicalToDeviceUnitsScalingFactor * (double)value);
			}
			double num = (double)devicePixels / 96.0;
			return (int)Math.Round(num * (double)value);
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x0008BA10 File Offset: 0x00089C10
		public static double LogicalToDeviceUnits(double value, int devicePixels = 0)
		{
			if (devicePixels == 0)
			{
				return DpiHelper.LogicalToDeviceUnitsScalingFactor * value;
			}
			double num = (double)devicePixels / 96.0;
			return num * value;
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x0008BA38 File Offset: 0x00089C38
		public static int LogicalToDeviceUnitsX(int value)
		{
			return DpiHelper.LogicalToDeviceUnits(value, 0);
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x0008BA38 File Offset: 0x00089C38
		public static int LogicalToDeviceUnitsY(int value)
		{
			return DpiHelper.LogicalToDeviceUnits(value, 0);
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x0008BA41 File Offset: 0x00089C41
		public static Size LogicalToDeviceUnits(Size logicalSize, int deviceDpi = 0)
		{
			return new Size(DpiHelper.LogicalToDeviceUnits(logicalSize.Width, deviceDpi), DpiHelper.LogicalToDeviceUnits(logicalSize.Height, deviceDpi));
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x0008BA62 File Offset: 0x00089C62
		public static Bitmap CreateResizedBitmap(Bitmap logicalImage, Size targetImageSize)
		{
			if (logicalImage == null)
			{
				return null;
			}
			return DpiHelper.ScaleBitmapToSize(logicalImage, targetImageSize);
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0008BA70 File Offset: 0x00089C70
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

		// Token: 0x060018DF RID: 6367 RVA: 0x0008BA98 File Offset: 0x00089C98
		public static int ConvertToGivenDpiPixel(int value, double pixelFactor)
		{
			int num = (int)Math.Round((double)value * pixelFactor);
			if (num != 0)
			{
				return num;
			}
			return 1;
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x0008BAB8 File Offset: 0x00089CB8
		public static void ScaleButtonImageLogicalToDevice(Button button)
		{
			if (button == null)
			{
				return;
			}
			Bitmap bitmap = button.Image as Bitmap;
			if (bitmap == null)
			{
				return;
			}
			Bitmap image = DpiHelper.CreateScaledBitmap(bitmap, 0);
			button.Image.Dispose();
			button.Image = image;
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x0008BAF3 File Offset: 0x00089CF3
		public static IDisposable EnterDpiAwarenessScope(DpiAwarenessContext awareness)
		{
			return new DpiHelper.DpiAwarenessScope(awareness);
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x0008BAFC File Offset: 0x00089CFC
		public static T CreateInstanceInSystemAwareContext<T>(Func<T> createInstance)
		{
			T result;
			using (DpiHelper.EnterDpiAwarenessScope(DpiAwarenessContext.DPI_AWARENESS_CONTEXT_SYSTEM_AWARE))
			{
				result = createInstance();
			}
			return result;
		}

		// Token: 0x04001536 RID: 5430
		internal const double LogicalDpi = 96.0;

		// Token: 0x04001537 RID: 5431
		private static bool isInitialized = false;

		// Token: 0x04001538 RID: 5432
		private static double deviceDpi = 96.0;

		// Token: 0x04001539 RID: 5433
		private static double logicalToDeviceUnitsScalingFactor = 0.0;

		// Token: 0x0400153A RID: 5434
		private static bool enableHighDpi = false;

		// Token: 0x0400153B RID: 5435
		private static string dpiAwarenessValue = null;

		// Token: 0x0400153C RID: 5436
		private static InterpolationMode interpolationMode = InterpolationMode.Invalid;

		// Token: 0x0400153D RID: 5437
		private static bool isDpiHelperQuirksInitialized = false;

		// Token: 0x0400153E RID: 5438
		private static bool enableToolStripHighDpiImprovements = false;

		// Token: 0x0400153F RID: 5439
		private static bool enableDpiChangedMessageHandling = false;

		// Token: 0x04001540 RID: 5440
		private static bool enableCheckedListBoxHighDpiImprovements = false;

		// Token: 0x04001541 RID: 5441
		private static bool enableThreadExceptionDialogHighDpiImprovements = false;

		// Token: 0x04001542 RID: 5442
		private static bool enableDataGridViewControlHighDpiImprovements = false;

		// Token: 0x04001543 RID: 5443
		private static bool enableSinglePassScalingOfDpiForms = false;

		// Token: 0x04001544 RID: 5444
		private static bool enableAnchorLayoutHighDpiImprovements = false;

		// Token: 0x04001545 RID: 5445
		private static bool enableMonthCalendarHighDpiImprovements = false;

		// Token: 0x04001546 RID: 5446
		private static bool enableDpiChangedHighDpiImprovements = false;

		// Token: 0x04001547 RID: 5447
		private static bool enableAnchorLayoutHighDpiImprovementsV2 = false;

		// Token: 0x04001548 RID: 5448
		private static readonly Version dpiChangedMessageHighDpiImprovementsMinimumFrameworkVersion = new Version(4, 8);

		// Token: 0x0200051A RID: 1306
		private class DpiAwarenessScope : IDisposable
		{
			// Token: 0x06002FFA RID: 12282 RVA: 0x001078FC File Offset: 0x00105AFC
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

			// Token: 0x06002FFB RID: 12283 RVA: 0x00107978 File Offset: 0x00105B78
			public void Dispose()
			{
				this.ResetDpiAwarenessContextChanges();
			}

			// Token: 0x06002FFC RID: 12284 RVA: 0x00107980 File Offset: 0x00105B80
			private void ResetDpiAwarenessContextChanges()
			{
				if (this.dpiAwarenessScopeIsSet)
				{
					CommonUnsafeNativeMethods.TrySetThreadDpiAwarenessContext(this.originalAwareness);
					this.dpiAwarenessScopeIsSet = false;
				}
			}

			// Token: 0x04002088 RID: 8328
			private bool dpiAwarenessScopeIsSet;

			// Token: 0x04002089 RID: 8329
			private DpiAwarenessContext originalAwareness;
		}
	}
}
