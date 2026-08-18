using System;
using System.Collections.Generic;
using System.Net;

namespace System.Web.WebPages
{
	// Token: 0x0200008C RID: 140
	public static class ResponseExtensions
	{
		// Token: 0x06000459 RID: 1113 RVA: 0x0000D8E5 File Offset: 0x0000BAE5
		public static void SetStatus(this HttpResponseBase response, HttpStatusCode httpStatusCode)
		{
			response.SetStatus((int)httpStatusCode);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000D8EE File Offset: 0x0000BAEE
		public static void SetStatus(this HttpResponseBase response, int httpStatusCode)
		{
			response.StatusCode = httpStatusCode;
			response.End();
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000D8FD File Offset: 0x0000BAFD
		public static void WriteBinary(this HttpResponseBase response, byte[] data, string mimeType)
		{
			response.ContentType = mimeType;
			response.WriteBinary(data);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000D90D File Offset: 0x0000BB0D
		public static void WriteBinary(this HttpResponseBase response, byte[] data)
		{
			response.OutputStream.Write(data, 0, data.Length);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000D91F File Offset: 0x0000BB1F
		public static void OutputCache(this HttpResponseBase response, int numberOfSeconds, bool sliding = false, IEnumerable<string> varyByParams = null, IEnumerable<string> varyByHeaders = null, IEnumerable<string> varyByContentEncodings = null, HttpCacheability cacheability = HttpCacheability.Public)
		{
			ResponseExtensions.OutputCache(new HttpContextWrapper(HttpContext.Current), response.Cache, numberOfSeconds, sliding, varyByParams, varyByHeaders, varyByContentEncodings, cacheability);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000D940 File Offset: 0x0000BB40
		internal static void OutputCache(HttpContextBase httpContext, HttpCachePolicyBase cache, int numberOfSeconds, bool sliding, IEnumerable<string> varyByParams, IEnumerable<string> varyByHeaders, IEnumerable<string> varyByContentEncodings, HttpCacheability cacheability)
		{
			cache.SetCacheability(cacheability);
			cache.SetExpires(httpContext.Timestamp.AddSeconds((double)numberOfSeconds));
			cache.SetMaxAge(new TimeSpan(0, 0, numberOfSeconds));
			cache.SetValidUntilExpires(true);
			cache.SetLastModified(httpContext.Timestamp);
			cache.SetSlidingExpiration(sliding);
			if (varyByParams != null)
			{
				foreach (string header in varyByParams)
				{
					cache.VaryByParams[header] = true;
				}
			}
			if (varyByHeaders != null)
			{
				foreach (string header2 in varyByHeaders)
				{
					cache.VaryByHeaders[header2] = true;
				}
			}
			if (varyByContentEncodings != null)
			{
				foreach (string contentEncoding in varyByContentEncodings)
				{
					cache.VaryByContentEncodings[contentEncoding] = true;
				}
			}
		}
	}
}
