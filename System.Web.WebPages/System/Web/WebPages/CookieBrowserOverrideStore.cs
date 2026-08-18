using System;

namespace System.Web.WebPages
{
	// Token: 0x02000019 RID: 25
	public class CookieBrowserOverrideStore : BrowserOverrideStore
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x00003B58 File Offset: 0x00001D58
		public CookieBrowserOverrideStore() : this(7)
		{
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003B61 File Offset: 0x00001D61
		public CookieBrowserOverrideStore(int daysToExpire)
		{
			this._daysToExpire = daysToExpire;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003B70 File Offset: 0x00001D70
		public override string GetOverriddenUserAgent(HttpContextBase httpContext)
		{
			HttpCookieCollection cookies = httpContext.Response.Cookies;
			string[] allKeys = cookies.AllKeys;
			int i = 0;
			while (i < allKeys.Length)
			{
				if (string.Equals(allKeys[i], CookieBrowserOverrideStore.BrowserOverrideCookieName, StringComparison.OrdinalIgnoreCase))
				{
					HttpCookie httpCookie = cookies[CookieBrowserOverrideStore.BrowserOverrideCookieName];
					if (httpCookie.Value != null)
					{
						return httpCookie.Value;
					}
					return null;
				}
				else
				{
					i++;
				}
			}
			HttpCookie httpCookie2 = httpContext.Request.Cookies[CookieBrowserOverrideStore.BrowserOverrideCookieName];
			if (httpCookie2 != null)
			{
				return httpCookie2.Value;
			}
			return null;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003BF0 File Offset: 0x00001DF0
		public override void SetOverriddenUserAgent(HttpContextBase httpContext, string userAgent)
		{
			HttpCookie httpCookie = new HttpCookie(CookieBrowserOverrideStore.BrowserOverrideCookieName, HttpUtility.UrlEncode(userAgent));
			if (userAgent == null)
			{
				httpCookie.Expires = DateTime.Now.AddDays(-1.0);
			}
			else if (this._daysToExpire > 0)
			{
				httpCookie.Expires = DateTime.Now.AddDays((double)this._daysToExpire);
			}
			httpContext.Response.Cookies.Remove(CookieBrowserOverrideStore.BrowserOverrideCookieName);
			httpContext.Response.Cookies.Add(httpCookie);
		}

		// Token: 0x0400003D RID: 61
		internal static readonly string BrowserOverrideCookieName = ".ASPXBrowserOverride";

		// Token: 0x0400003E RID: 62
		private readonly int _daysToExpire;
	}
}
