using System;
using System.Text;

namespace System.Web.UI
{
	// Token: 0x020002E2 RID: 738
	internal class DesignTimePageThemeParser : PageThemeParser
	{
		// Token: 0x0600225C RID: 8796 RVA: 0x00070470 File Offset: 0x0006E670
		internal DesignTimePageThemeParser(string virtualDirPath) : base(null, null, null)
		{
			this._themePhysicalPath = virtualDirPath;
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x0600225D RID: 8797 RVA: 0x00070482 File Offset: 0x0006E682
		internal string ThemePhysicalPath
		{
			get
			{
				return this._themePhysicalPath;
			}
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x0007048A File Offset: 0x0006E68A
		internal override void ParseInternal()
		{
			if (base.Text != null)
			{
				base.ParseString(base.Text, base.CurrentVirtualPath, Encoding.UTF8);
			}
		}

		// Token: 0x04001C39 RID: 7225
		private string _themePhysicalPath;
	}
}
