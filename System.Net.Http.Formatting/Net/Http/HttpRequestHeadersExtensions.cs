using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x0200002D RID: 45
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpRequestHeadersExtensions
	{
		// Token: 0x06000156 RID: 342 RVA: 0x00006550 File Offset: 0x00004750
		public static Collection<CookieHeaderValue> GetCookies(this HttpRequestHeaders headers)
		{
			if (headers == null)
			{
				throw Error.ArgumentNull("headers");
			}
			Collection<CookieHeaderValue> collection = new Collection<CookieHeaderValue>();
			IEnumerable<string> enumerable;
			if (headers.TryGetValues("Cookie", out enumerable))
			{
				foreach (string input in enumerable)
				{
					CookieHeaderValue item;
					if (CookieHeaderValue.TryParse(input, out item))
					{
						collection.Add(item);
					}
				}
			}
			return collection;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006604 File Offset: 0x00004804
		public static Collection<CookieHeaderValue> GetCookies(this HttpRequestHeaders headers, string name)
		{
			if (name == null)
			{
				throw Error.ArgumentNull("name");
			}
			IEnumerable<CookieHeaderValue> cookies = headers.GetCookies();
			CookieHeaderValue[] list = (from header in cookies
			where header.Cookies.Any((CookieState state) => string.Equals(state.Name, name, StringComparison.OrdinalIgnoreCase))
			select header).ToArray<CookieHeaderValue>();
			return new Collection<CookieHeaderValue>(list);
		}

		// Token: 0x04000065 RID: 101
		private const string Cookie = "Cookie";
	}
}
