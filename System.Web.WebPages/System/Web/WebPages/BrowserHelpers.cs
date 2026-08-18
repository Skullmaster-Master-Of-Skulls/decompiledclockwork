using System;
using System.Web.Hosting;

namespace System.Web.WebPages
{
	// Token: 0x0200001C RID: 28
	public static class BrowserHelpers
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x00004014 File Offset: 0x00002214
		public static void ClearOverriddenBrowser(this HttpContextBase httpContext)
		{
			httpContext.SetOverriddenBrowser(null);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004020 File Offset: 0x00002220
		private static HttpBrowserCapabilitiesBase CreateOverriddenBrowser(string userAgent)
		{
			HttpBrowserCapabilities browser = new HttpContext(new BrowserHelpers.UserAgentWorkerRequest(userAgent)).Request.Browser;
			return new HttpBrowserCapabilitiesWrapper(browser);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004049 File Offset: 0x00002249
		public static HttpBrowserCapabilitiesBase GetOverriddenBrowser(this HttpContextBase httpContext)
		{
			return httpContext.GetOverriddenBrowser(null);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004054 File Offset: 0x00002254
		internal static HttpBrowserCapabilitiesBase GetOverriddenBrowser(this HttpContextBase httpContext, Func<string, HttpBrowserCapabilitiesBase> createBrowser)
		{
			HttpBrowserCapabilitiesBase httpBrowserCapabilitiesBase = (HttpBrowserCapabilitiesBase)httpContext.Items[BrowserHelpers._browserOverrideKey];
			if (httpBrowserCapabilitiesBase == null)
			{
				string overriddenUserAgent = httpContext.GetOverriddenUserAgent();
				if (!string.Equals(overriddenUserAgent, httpContext.Request.UserAgent, StringComparison.OrdinalIgnoreCase))
				{
					if (createBrowser != null)
					{
						httpBrowserCapabilitiesBase = createBrowser(overriddenUserAgent);
					}
					else
					{
						httpBrowserCapabilitiesBase = BrowserHelpers.CreateOverriddenBrowser(overriddenUserAgent);
					}
				}
				else
				{
					httpBrowserCapabilitiesBase = httpContext.Request.Browser;
				}
				httpContext.Items[BrowserHelpers._browserOverrideKey] = httpBrowserCapabilitiesBase;
			}
			return httpBrowserCapabilitiesBase;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000040C9 File Offset: 0x000022C9
		public static string GetOverriddenUserAgent(this HttpContextBase httpContext)
		{
			string result;
			if ((result = (string)httpContext.Items[BrowserHelpers._userAgentKey]) == null)
			{
				result = (BrowserOverrideStores.Current.GetOverriddenUserAgent(httpContext) ?? httpContext.Request.UserAgent);
			}
			return result;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000040FE File Offset: 0x000022FE
		public static string GetVaryByCustomStringForOverriddenBrowser(this HttpContext httpContext)
		{
			return new HttpContextWrapper(httpContext).GetVaryByCustomStringForOverriddenBrowser();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000410B File Offset: 0x0000230B
		public static string GetVaryByCustomStringForOverriddenBrowser(this HttpContextBase httpContext)
		{
			return httpContext.GetOverriddenBrowser(null).Type;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000411C File Offset: 0x0000231C
		public static void SetOverriddenBrowser(this HttpContextBase httpContext, BrowserOverride browserOverride)
		{
			string text = null;
			switch (browserOverride)
			{
			case BrowserOverride.Desktop:
				if (httpContext.Request.Browser == null || httpContext.Request.Browser.IsMobileDevice)
				{
					text = "Mozilla/4.0 (compatible; MSIE 6.1; Windows XP)";
				}
				break;
			case BrowserOverride.Mobile:
				if (httpContext.Request.Browser == null || !httpContext.Request.Browser.IsMobileDevice)
				{
					text = "Mozilla/4.0 (compatible; MSIE 6.0; Windows CE; IEMobile 8.12; MSIEMobile 6.0)";
				}
				break;
			}
			if (text != null)
			{
				httpContext.SetOverriddenBrowser(text);
				return;
			}
			httpContext.ClearOverriddenBrowser();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000419A File Offset: 0x0000239A
		public static void SetOverriddenBrowser(this HttpContextBase httpContext, string userAgent)
		{
			httpContext.Items[BrowserHelpers._userAgentKey] = userAgent;
			httpContext.Items[BrowserHelpers._browserOverrideKey] = null;
			BrowserOverrideStores.Current.SetOverriddenUserAgent(httpContext, userAgent);
		}

		// Token: 0x04000048 RID: 72
		private const string DesktopUserAgent = "Mozilla/4.0 (compatible; MSIE 6.1; Windows XP)";

		// Token: 0x04000049 RID: 73
		private const string MobileUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows CE; IEMobile 8.12; MSIEMobile 6.0)";

		// Token: 0x0400004A RID: 74
		private static readonly object _browserOverrideKey = new object();

		// Token: 0x0400004B RID: 75
		private static readonly object _userAgentKey = new object();

		// Token: 0x0200001D RID: 29
		private sealed class UserAgentWorkerRequest : SimpleWorkerRequest
		{
			// Token: 0x060000F2 RID: 242 RVA: 0x000041E0 File Offset: 0x000023E0
			public UserAgentWorkerRequest(string userAgent) : base(string.Empty, string.Empty, null)
			{
				this._userAgent = userAgent;
			}

			// Token: 0x060000F3 RID: 243 RVA: 0x000041FA File Offset: 0x000023FA
			public override string GetKnownRequestHeader(int index)
			{
				if (index != 39)
				{
					return null;
				}
				return this._userAgent;
			}

			// Token: 0x0400004C RID: 76
			private readonly string _userAgent;
		}
	}
}
