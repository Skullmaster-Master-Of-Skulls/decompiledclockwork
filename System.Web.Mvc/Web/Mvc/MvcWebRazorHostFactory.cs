using System;
using System.Web.Mvc.Razor;
using System.Web.WebPages.Razor;

namespace System.Web.Mvc
{
	// Token: 0x020000D1 RID: 209
	public class MvcWebRazorHostFactory : WebRazorHostFactory
	{
		// Token: 0x06000565 RID: 1381 RVA: 0x0000F1C0 File Offset: 0x0000D3C0
		public override WebPageRazorHost CreateHost(string virtualPath, string physicalPath)
		{
			WebPageRazorHost webPageRazorHost = base.CreateHost(virtualPath, physicalPath);
			if (!webPageRazorHost.IsSpecialPage)
			{
				return new MvcWebPageRazorHost(virtualPath, physicalPath);
			}
			return webPageRazorHost;
		}
	}
}
