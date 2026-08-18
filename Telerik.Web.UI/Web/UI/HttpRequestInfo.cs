using System;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x0200181A RID: 6170
	public class HttpRequestInfo : IHttpRequestInfo
	{
		// Token: 0x170048AF RID: 18607
		// (get) Token: 0x0600F02F RID: 61487 RVA: 0x0036A343 File Offset: 0x00368543
		// (set) Token: 0x0600F030 RID: 61488 RVA: 0x0036A34B File Offset: 0x0036854B
		public bool IsSecure { get; private set; }

		// Token: 0x170048B0 RID: 18608
		// (get) Token: 0x0600F031 RID: 61489 RVA: 0x0036A354 File Offset: 0x00368554
		// (set) Token: 0x0600F032 RID: 61490 RVA: 0x0036A35C File Offset: 0x0036855C
		public bool SupportsGzip { get; private set; }

		// Token: 0x0600F033 RID: 61491 RVA: 0x0036A368 File Offset: 0x00368568
		public HttpRequestInfo(HttpRequest request)
		{
			HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
			bool flag = browser.IsBrowser("IE") && browser.MajorVersion <= 6;
			bool flag2 = false;
			string text = request.Headers["Accept-Encoding"];
			if (!string.IsNullOrEmpty(text))
			{
				flag2 = text.ToLowerInvariant().Contains("gzip");
			}
			this.SupportsGzip = (!flag && flag2);
			this.IsSecure = request.IsSecureConnection;
		}
	}
}
