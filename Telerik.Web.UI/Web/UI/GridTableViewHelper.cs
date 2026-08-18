using System;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x0200117C RID: 4476
	internal static class GridTableViewHelper
	{
		// Token: 0x0600B680 RID: 46720 RVA: 0x00283845 File Offset: 0x00281A45
		public static bool IsBrowser(string browser)
		{
			return HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.Browser.Browser.IndexOf(browser) > -1;
		}

		// Token: 0x0600B681 RID: 46721 RVA: 0x00283879 File Offset: 0x00281A79
		public static bool IsBrowser(string browser, int version)
		{
			return GridTableViewHelper.IsBrowser(browser) && HttpContext.Current.Request.Browser.MajorVersion == version;
		}

		// Token: 0x0600B682 RID: 46722 RVA: 0x0028389C File Offset: 0x00281A9C
		public static bool IsBrowserVersionNewer(string browser, int version)
		{
			return GridTableViewHelper.IsBrowser(browser) && HttpContext.Current.Request.Browser.MajorVersion > version;
		}
	}
}
