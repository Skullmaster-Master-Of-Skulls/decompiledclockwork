using System;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x020001D3 RID: 467
	public static class UrlHelpers
	{
		// Token: 0x060010EB RID: 4331 RVA: 0x0003E545 File Offset: 0x0003C745
		public static string ToAbsolute(string virtualPath)
		{
			if (string.IsNullOrEmpty(virtualPath) || !VirtualPathUtility.IsAppRelative(virtualPath))
			{
				return virtualPath;
			}
			return VirtualPathUtility.ToAbsolute(virtualPath, HttpRuntime.AppDomainAppVirtualPath);
		}
	}
}
