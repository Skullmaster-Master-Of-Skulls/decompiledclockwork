using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x0200002C RID: 44
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpResponseHeadersExtensions
	{
		// Token: 0x06000155 RID: 341 RVA: 0x000064C8 File Offset: 0x000046C8
		public static void AddCookies(this HttpResponseHeaders headers, IEnumerable<CookieHeaderValue> cookies)
		{
			if (headers == null)
			{
				throw Error.ArgumentNull("headers");
			}
			if (cookies == null)
			{
				throw Error.ArgumentNull("cookies");
			}
			foreach (CookieHeaderValue cookieHeaderValue in cookies)
			{
				if (cookieHeaderValue == null)
				{
					throw Error.Argument("cookies", Resources.CookieNull, new object[0]);
				}
				headers.TryAddWithoutValidation("Set-Cookie", cookieHeaderValue.ToString());
			}
		}

		// Token: 0x04000064 RID: 100
		private const string SetCookie = "Set-Cookie";
	}
}
