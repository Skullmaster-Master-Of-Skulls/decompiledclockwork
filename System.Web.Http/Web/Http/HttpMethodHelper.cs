using System;
using System.Net.Http;

namespace System.Web.Http
{
	// Token: 0x02000006 RID: 6
	internal static class HttpMethodHelper
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000029A0 File Offset: 0x00000BA0
		internal static HttpMethod GetHttpMethod(string method)
		{
			if (string.IsNullOrEmpty(method))
			{
				return null;
			}
			if (string.Equals("GET", method, StringComparison.OrdinalIgnoreCase))
			{
				return HttpMethod.Get;
			}
			if (string.Equals("POST", method, StringComparison.OrdinalIgnoreCase))
			{
				return HttpMethod.Post;
			}
			if (string.Equals("PUT", method, StringComparison.OrdinalIgnoreCase))
			{
				return HttpMethod.Put;
			}
			if (string.Equals("DELETE", method, StringComparison.OrdinalIgnoreCase))
			{
				return HttpMethod.Delete;
			}
			if (string.Equals("HEAD", method, StringComparison.OrdinalIgnoreCase))
			{
				return HttpMethod.Head;
			}
			if (string.Equals("OPTIONS", method, StringComparison.OrdinalIgnoreCase))
			{
				return HttpMethod.Options;
			}
			if (string.Equals("TRACE", method, StringComparison.OrdinalIgnoreCase))
			{
				return HttpMethod.Trace;
			}
			return new HttpMethod(method);
		}
	}
}
