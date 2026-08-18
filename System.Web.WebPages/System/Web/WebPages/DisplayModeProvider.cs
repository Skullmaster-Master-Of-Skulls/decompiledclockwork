using System;
using System.Collections.Generic;

namespace System.Web.WebPages
{
	// Token: 0x0200001B RID: 27
	public sealed class DisplayModeProvider
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00003CE8 File Offset: 0x00001EE8
		internal DisplayModeProvider()
		{
			List<IDisplayMode> list = new List<IDisplayMode>();
			List<IDisplayMode> list2 = list;
			DefaultDisplayMode defaultDisplayMode = new DefaultDisplayMode(DisplayModeProvider.MobileDisplayModeId);
			defaultDisplayMode.ContextCondition = ((HttpContextBase context) => context.GetOverriddenBrowser().IsMobileDevice);
			list2.Add(defaultDisplayMode);
			list.Add(new DefaultDisplayMode());
			this._displayModes = list;
			base..ctor();
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00003D48 File Offset: 0x00001F48
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00003D50 File Offset: 0x00001F50
		public bool RequireConsistentDisplayMode { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00003D59 File Offset: 0x00001F59
		public static DisplayModeProvider Instance
		{
			get
			{
				return DisplayModeProvider._instance;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00003D60 File Offset: 0x00001F60
		public IList<IDisplayMode> Modes
		{
			get
			{
				return this._displayModes;
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003D68 File Offset: 0x00001F68
		private int FindFirstAvailableDisplayMode(IDisplayMode currentDisplayMode, bool requireConsistentDisplayMode)
		{
			if (!requireConsistentDisplayMode || currentDisplayMode == null)
			{
				return 0;
			}
			int num = this._displayModes.IndexOf(currentDisplayMode);
			if (num < 0)
			{
				return this._displayModes.Count;
			}
			return num;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003D9B File Offset: 0x00001F9B
		public IEnumerable<IDisplayMode> GetAvailableDisplayModesForContext(HttpContextBase httpContext, IDisplayMode currentDisplayMode)
		{
			return this.GetAvailableDisplayModesForContext(httpContext, currentDisplayMode, this.RequireConsistentDisplayMode);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003F1C File Offset: 0x0000211C
		internal IEnumerable<IDisplayMode> GetAvailableDisplayModesForContext(HttpContextBase httpContext, IDisplayMode currentDisplayMode, bool requireConsistentDisplayMode)
		{
			int first = this.FindFirstAvailableDisplayMode(currentDisplayMode, requireConsistentDisplayMode);
			for (int i = first; i < this._displayModes.Count; i++)
			{
				IDisplayMode mode = this._displayModes[i];
				if (mode.CanHandleContext(httpContext))
				{
					yield return mode;
				}
			}
			yield break;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003F4E File Offset: 0x0000214E
		public DisplayInfo GetDisplayInfoForVirtualPath(string virtualPath, HttpContextBase httpContext, Func<string, bool> virtualPathExists, IDisplayMode currentDisplayMode)
		{
			return this.GetDisplayInfoForVirtualPath(virtualPath, httpContext, virtualPathExists, currentDisplayMode, this.RequireConsistentDisplayMode);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003F64 File Offset: 0x00002164
		internal DisplayInfo GetDisplayInfoForVirtualPath(string virtualPath, HttpContextBase httpContext, Func<string, bool> virtualPathExists, IDisplayMode currentDisplayMode, bool requireConsistentDisplayMode)
		{
			int num = this.FindFirstAvailableDisplayMode(currentDisplayMode, requireConsistentDisplayMode);
			for (int i = num; i < this._displayModes.Count; i++)
			{
				IDisplayMode displayMode = this._displayModes[i];
				if (displayMode.CanHandleContext(httpContext))
				{
					DisplayInfo displayInfo = displayMode.GetDisplayInfo(httpContext, virtualPath, virtualPathExists);
					if (displayInfo != null)
					{
						return displayInfo;
					}
				}
			}
			return null;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003FB8 File Offset: 0x000021B8
		internal static IDisplayMode GetDisplayMode(HttpContextBase context)
		{
			if (context == null)
			{
				return null;
			}
			return context.Items[DisplayModeProvider._displayModeKey] as IDisplayMode;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003FD4 File Offset: 0x000021D4
		internal static void SetDisplayMode(HttpContextBase context, IDisplayMode displayMode)
		{
			if (context != null)
			{
				context.Items[DisplayModeProvider._displayModeKey] = displayMode;
			}
		}

		// Token: 0x04000041 RID: 65
		public static readonly string MobileDisplayModeId = "Mobile";

		// Token: 0x04000042 RID: 66
		public static readonly string DefaultDisplayModeId = string.Empty;

		// Token: 0x04000043 RID: 67
		private static readonly object _displayModeKey = new object();

		// Token: 0x04000044 RID: 68
		private static readonly DisplayModeProvider _instance = new DisplayModeProvider();

		// Token: 0x04000045 RID: 69
		private readonly List<IDisplayMode> _displayModes;
	}
}
