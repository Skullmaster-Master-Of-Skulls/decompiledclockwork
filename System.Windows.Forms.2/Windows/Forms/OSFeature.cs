using System;

namespace System.Windows.Forms
{
	// Token: 0x02000314 RID: 788
	public class OSFeature : FeatureSupport
	{
		// Token: 0x0600321F RID: 12831 RVA: 0x000E189E File Offset: 0x000DFA9E
		protected OSFeature()
		{
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06003220 RID: 12832 RVA: 0x000E18A6 File Offset: 0x000DFAA6
		public static OSFeature Feature
		{
			get
			{
				if (OSFeature.feature == null)
				{
					OSFeature.feature = new OSFeature();
				}
				return OSFeature.feature;
			}
		}

		// Token: 0x06003221 RID: 12833 RVA: 0x000E18C0 File Offset: 0x000DFAC0
		public override Version GetVersionPresent(object feature)
		{
			Version result = null;
			if (feature == OSFeature.LayeredWindows)
			{
				if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.CompareTo(new Version(5, 0, 0, 0)) >= 0)
				{
					result = new Version(0, 0, 0, 0);
				}
			}
			else if (feature == OSFeature.Themes)
			{
				if (!OSFeature.themeSupportTested)
				{
					try
					{
						SafeNativeMethods.IsAppThemed();
						OSFeature.themeSupport = true;
					}
					catch
					{
						OSFeature.themeSupport = false;
					}
					OSFeature.themeSupportTested = true;
				}
				if (OSFeature.themeSupport)
				{
					result = new Version(0, 0, 0, 0);
				}
			}
			return result;
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06003222 RID: 12834 RVA: 0x000E195C File Offset: 0x000DFB5C
		internal bool OnXp
		{
			get
			{
				bool result = false;
				if (Environment.OSVersion.Platform == PlatformID.Win32NT)
				{
					result = (Environment.OSVersion.Version.CompareTo(new Version(5, 1, 0, 0)) >= 0);
				}
				return result;
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06003223 RID: 12835 RVA: 0x000E1998 File Offset: 0x000DFB98
		internal bool OnWin2k
		{
			get
			{
				bool result = false;
				if (Environment.OSVersion.Platform == PlatformID.Win32NT)
				{
					result = (Environment.OSVersion.Version.CompareTo(new Version(5, 0, 0, 0)) >= 0);
				}
				return result;
			}
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x000E19D4 File Offset: 0x000DFBD4
		public static bool IsPresent(SystemParameter enumVal)
		{
			switch (enumVal)
			{
			case SystemParameter.DropShadow:
				return OSFeature.Feature.OnXp;
			case SystemParameter.FlatMenu:
				return OSFeature.Feature.OnXp;
			case SystemParameter.FontSmoothingContrastMetric:
				return OSFeature.Feature.OnXp;
			case SystemParameter.FontSmoothingTypeMetric:
				return OSFeature.Feature.OnXp;
			case SystemParameter.MenuFadeEnabled:
				return OSFeature.Feature.OnWin2k;
			case SystemParameter.SelectionFade:
				return OSFeature.Feature.OnWin2k;
			case SystemParameter.ToolTipAnimationMetric:
				return OSFeature.Feature.OnWin2k;
			case SystemParameter.UIEffects:
				return OSFeature.Feature.OnWin2k;
			case SystemParameter.CaretWidthMetric:
				return OSFeature.Feature.OnWin2k;
			case SystemParameter.VerticalFocusThicknessMetric:
				return OSFeature.Feature.OnXp;
			case SystemParameter.HorizontalFocusThicknessMetric:
				return OSFeature.Feature.OnXp;
			default:
				return false;
			}
		}

		// Token: 0x04001E67 RID: 7783
		public static readonly object LayeredWindows = new object();

		// Token: 0x04001E68 RID: 7784
		public static readonly object Themes = new object();

		// Token: 0x04001E69 RID: 7785
		private static OSFeature feature = null;

		// Token: 0x04001E6A RID: 7786
		private static bool themeSupportTested = false;

		// Token: 0x04001E6B RID: 7787
		private static bool themeSupport = false;
	}
}
