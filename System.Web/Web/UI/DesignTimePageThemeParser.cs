using System;
using System.Text;

namespace System.Web.UI
{
	// Token: 0x0200044C RID: 1100
	internal class DesignTimePageThemeParser : PageThemeParser
	{
		// Token: 0x06003460 RID: 13408 RVA: 0x000E34EF File Offset: 0x000E24EF
		internal DesignTimePageThemeParser(string virtualDirPath) : base(null, null, null)
		{
			this._themePhysicalPath = virtualDirPath;
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06003461 RID: 13409 RVA: 0x000E3501 File Offset: 0x000E2501
		internal string ThemePhysicalPath
		{
			get
			{
				return this._themePhysicalPath;
			}
		}

		// Token: 0x06003462 RID: 13410 RVA: 0x000E3509 File Offset: 0x000E2509
		internal override void ParseInternal()
		{
			if (base.Text != null)
			{
				base.ParseString(base.Text, base.CurrentVirtualPath, Encoding.UTF8);
			}
		}

		// Token: 0x040024B6 RID: 9398
		private string _themePhysicalPath;
	}
}
