using System;
using System.Collections.Generic;
using Telerik.Web.UI;

namespace Telerik.Web
{
	// Token: 0x02000009 RID: 9
	public interface ISkinnableControl : IControl
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003C RID: 60
		// (set) Token: 0x0600003D RID: 61
		string Skin { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003E RID: 62
		bool IsSkinSet { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003F RID: 63
		// (set) Token: 0x06000040 RID: 64
		bool EnableEmbeddedSkins { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000041 RID: 65
		// (set) Token: 0x06000042 RID: 66
		bool EnableEmbeddedScripts { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000043 RID: 67
		// (set) Token: 0x06000044 RID: 68
		bool EnableEmbeddedBaseStylesheet { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000045 RID: 69
		// (set) Token: 0x06000046 RID: 70
		string AjaxCssRegistrations { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000047 RID: 71
		// (set) Token: 0x06000048 RID: 72
		bool EnableAjaxSkinRendering { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000049 RID: 73
		// (set) Token: 0x0600004A RID: 74
		RenderMode RenderMode { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600004B RID: 75
		RenderMode ResolvedRenderMode { get; }

		// Token: 0x0600004C RID: 76
		List<string> GetEmbeddedSkinNames();

		// Token: 0x0600004D RID: 77
		string GetSkinSuffix();

		// Token: 0x0600004E RID: 78
		RenderMode PreferredRenderMode(RenderModeBrowserAdaptor browser);
	}
}
