using System;
using System.Web;

namespace Telerik.Web.UI.PersistenceFramework
{
	// Token: 0x02000C1D RID: 3101
	public class CookieStateStorageProvider : BaseStateStorageProvider
	{
		// Token: 0x06007600 RID: 30208 RVA: 0x001B684B File Offset: 0x001B4A4B
		public CookieStateStorageProvider() : this(30)
		{
		}

		// Token: 0x06007601 RID: 30209 RVA: 0x001B6855 File Offset: 0x001B4A55
		public CookieStateStorageProvider(int cookieLifeSpan)
		{
			this.CookieLifeSpan = new int?(cookieLifeSpan);
		}

		// Token: 0x06007602 RID: 30210 RVA: 0x001B686C File Offset: 0x001B4A6C
		public override void SaveStateToStorage(string key, string serializedState)
		{
			HttpCookie cookie = this.CraeteCookie(key, serializedState);
			HttpContext.Current.Response.SetCookie(cookie);
		}

		// Token: 0x06007603 RID: 30211 RVA: 0x001B6894 File Offset: 0x001B4A94
		public override string LoadStateFromStorage(string key)
		{
			HttpCookie httpCookie = HttpContext.Current.Request.Cookies.Get(key);
			if (object.Equals(null, httpCookie))
			{
				return string.Empty;
			}
			return httpCookie.Value;
		}

		// Token: 0x06007604 RID: 30212 RVA: 0x001B68CC File Offset: 0x001B4ACC
		protected virtual HttpCookie CraeteCookie(string key, string value)
		{
			HttpCookie httpCookie = new HttpCookie(key, value);
			if (this.CookieLifeSpan != null)
			{
				httpCookie.Expires = DateTime.Now.AddDays((double)this.CookieLifeSpan.Value);
			}
			return httpCookie;
		}

		// Token: 0x17002664 RID: 9828
		// (get) Token: 0x06007605 RID: 30213 RVA: 0x001B6914 File Offset: 0x001B4B14
		// (set) Token: 0x06007606 RID: 30214 RVA: 0x001B691C File Offset: 0x001B4B1C
		public int? CookieLifeSpan { get; set; }
	}
}
