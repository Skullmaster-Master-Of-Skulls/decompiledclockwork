using System;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004E0 RID: 1248
	internal static class MeasurementDCInfo
	{
		// Token: 0x06005145 RID: 20805 RVA: 0x001532D4 File Offset: 0x001514D4
		internal static bool IsMeasurementDC(DeviceContext dc)
		{
			WindowsGraphics currentMeasurementGraphics = WindowsGraphicsCacheManager.GetCurrentMeasurementGraphics();
			return currentMeasurementGraphics != null && currentMeasurementGraphics.DeviceContext != null && currentMeasurementGraphics.DeviceContext.Hdc == dc.Hdc;
		}

		// Token: 0x170013A2 RID: 5026
		// (get) Token: 0x06005146 RID: 20806 RVA: 0x0015330A File Offset: 0x0015150A
		// (set) Token: 0x06005147 RID: 20807 RVA: 0x0015331F File Offset: 0x0015151F
		internal static WindowsFont LastUsedFont
		{
			get
			{
				if (MeasurementDCInfo.cachedMeasurementDCInfo != null)
				{
					return MeasurementDCInfo.cachedMeasurementDCInfo.LastUsedFont;
				}
				return null;
			}
			set
			{
				if (MeasurementDCInfo.cachedMeasurementDCInfo == null)
				{
					MeasurementDCInfo.cachedMeasurementDCInfo = new MeasurementDCInfo.CachedInfo();
				}
				MeasurementDCInfo.cachedMeasurementDCInfo.UpdateFont(value);
			}
		}

		// Token: 0x06005148 RID: 20808 RVA: 0x00153340 File Offset: 0x00151540
		internal static IntNativeMethods.DRAWTEXTPARAMS GetTextMargins(WindowsGraphics wg, WindowsFont font)
		{
			MeasurementDCInfo.CachedInfo cachedInfo = MeasurementDCInfo.cachedMeasurementDCInfo;
			if (cachedInfo != null && cachedInfo.LeftTextMargin > 0 && cachedInfo.RightTextMargin > 0 && font == cachedInfo.LastUsedFont)
			{
				return new IntNativeMethods.DRAWTEXTPARAMS(cachedInfo.LeftTextMargin, cachedInfo.RightTextMargin);
			}
			if (cachedInfo == null)
			{
				cachedInfo = new MeasurementDCInfo.CachedInfo();
				MeasurementDCInfo.cachedMeasurementDCInfo = cachedInfo;
			}
			IntNativeMethods.DRAWTEXTPARAMS textMargins = wg.GetTextMargins(font);
			cachedInfo.LeftTextMargin = textMargins.iLeftMargin;
			cachedInfo.RightTextMargin = textMargins.iRightMargin;
			return new IntNativeMethods.DRAWTEXTPARAMS(cachedInfo.LeftTextMargin, cachedInfo.RightTextMargin);
		}

		// Token: 0x06005149 RID: 20809 RVA: 0x001533C4 File Offset: 0x001515C4
		internal static void ResetIfIsMeasurementDC(IntPtr hdc)
		{
			WindowsGraphics currentMeasurementGraphics = WindowsGraphicsCacheManager.GetCurrentMeasurementGraphics();
			if (currentMeasurementGraphics != null && currentMeasurementGraphics.DeviceContext != null && currentMeasurementGraphics.DeviceContext.Hdc == hdc)
			{
				MeasurementDCInfo.CachedInfo cachedInfo = MeasurementDCInfo.cachedMeasurementDCInfo;
				if (cachedInfo != null)
				{
					cachedInfo.UpdateFont(null);
				}
			}
		}

		// Token: 0x0600514A RID: 20810 RVA: 0x00153408 File Offset: 0x00151608
		internal static void Reset()
		{
			MeasurementDCInfo.CachedInfo cachedInfo = MeasurementDCInfo.cachedMeasurementDCInfo;
			if (cachedInfo != null)
			{
				cachedInfo.UpdateFont(null);
			}
		}

		// Token: 0x040035BC RID: 13756
		[ThreadStatic]
		private static MeasurementDCInfo.CachedInfo cachedMeasurementDCInfo;

		// Token: 0x0200087E RID: 2174
		private sealed class CachedInfo
		{
			// Token: 0x06007184 RID: 29060 RVA: 0x0019FADC File Offset: 0x0019DCDC
			internal void UpdateFont(WindowsFont font)
			{
				if (this.LastUsedFont != font)
				{
					this.LastUsedFont = font;
					this.LeftTextMargin = -1;
					this.RightTextMargin = -1;
				}
			}

			// Token: 0x04004478 RID: 17528
			public WindowsFont LastUsedFont;

			// Token: 0x04004479 RID: 17529
			public int LeftTextMargin;

			// Token: 0x0400447A RID: 17530
			public int RightTextMargin;
		}
	}
}
