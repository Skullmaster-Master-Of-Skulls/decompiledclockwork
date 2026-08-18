using System;

namespace System.Web.Helpers.AntiXsrf
{
	// Token: 0x02000021 RID: 33
	internal sealed class AntiForgeryTokenStore : ITokenStore
	{
		// Token: 0x060000FD RID: 253 RVA: 0x0000440D File Offset: 0x0000260D
		internal AntiForgeryTokenStore(IAntiForgeryConfig config, IAntiForgeryTokenSerializer serializer)
		{
			this._config = config;
			this._serializer = serializer;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004424 File Offset: 0x00002624
		public AntiForgeryToken GetCookieToken(HttpContextBase httpContext)
		{
			HttpCookie httpCookie = httpContext.Request.Cookies[this._config.CookieName];
			if (httpCookie == null || string.IsNullOrEmpty(httpCookie.Value))
			{
				return null;
			}
			return this._serializer.Deserialize(httpCookie.Value);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004470 File Offset: 0x00002670
		public AntiForgeryToken GetFormToken(HttpContextBase httpContext)
		{
			string text = httpContext.Request.Form[this._config.FormFieldName];
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return this._serializer.Deserialize(text);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000044B0 File Offset: 0x000026B0
		public void SaveCookieToken(HttpContextBase httpContext, AntiForgeryToken token)
		{
			string value = this._serializer.Serialize(token);
			HttpCookie httpCookie = new HttpCookie(this._config.CookieName, value)
			{
				HttpOnly = true
			};
			if (this._config.RequireSSL)
			{
				httpCookie.Secure = true;
			}
			httpContext.Response.Cookies.Set(httpCookie);
		}

		// Token: 0x04000053 RID: 83
		private readonly IAntiForgeryConfig _config;

		// Token: 0x04000054 RID: 84
		private readonly IAntiForgeryTokenSerializer _serializer;
	}
}
