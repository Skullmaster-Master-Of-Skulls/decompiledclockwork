using System;
using System.Collections.Generic;
using System.Web.Configuration;

namespace AjaxControlToolkit
{
	// Token: 0x02000019 RID: 25
	public static class ToolkitConfig
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x000041C8 File Offset: 0x000023C8
		private static AjaxControlToolkitConfigSection GetSection()
		{
			AjaxControlToolkitConfigSection ajaxControlToolkitConfigSection = (AjaxControlToolkitConfigSection)WebConfigurationManager.GetSection("ajaxControlToolkit");
			if (ajaxControlToolkitConfigSection == null)
			{
				return new AjaxControlToolkitConfigSection();
			}
			return ajaxControlToolkitConfigSection;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000041EF File Offset: 0x000023EF
		private static AjaxControlToolkitConfigSection ConfigSection
		{
			get
			{
				return ToolkitConfig._configSection.Value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000041FB File Offset: 0x000023FB
		public static bool UseStaticResources
		{
			get
			{
				return ToolkitConfig.ConfigSection.UseStaticResources;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004207 File Offset: 0x00002407
		public static bool RenderStyleLinks
		{
			get
			{
				return ToolkitConfig.ConfigSection.RenderStyleLinks;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00004213 File Offset: 0x00002413
		public static string HtmlSanitizer
		{
			get
			{
				return ToolkitConfig.ConfigSection.HtmlSanitizer;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0000421F File Offset: 0x0000241F
		public static string TempFolder
		{
			get
			{
				return ToolkitConfig.ConfigSection.TempFolder;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000043D0 File Offset: 0x000025D0
		public static IEnumerable<Type> CustomControls
		{
			get
			{
				foreach (object control in ToolkitConfig.ConfigSection.CustomControls)
				{
					yield return Type.GetType(((CustomControlElement)control).Type);
				}
				yield break;
			}
		}

		// Token: 0x0400003F RID: 63
		private static Lazy<AjaxControlToolkitConfigSection> _configSection = new Lazy<AjaxControlToolkitConfigSection>(new Func<AjaxControlToolkitConfigSection>(ToolkitConfig.GetSection), true);
	}
}
