using System;
using System.Collections.Generic;
using System.Net.Http;

namespace System.Web.Http.WebHost
{
	// Token: 0x02000010 RID: 16
	internal static class HttpRequestMessageExtensions
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00003618 File Offset: 0x00001818
		public static HttpContextBase GetHttpContext(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			HttpContextBase httpContextBase;
			if (request.IsBatchRequest())
			{
				if (!request.Properties.TryGetValue("MS_HttpBatchContext", out httpContextBase))
				{
					if (request.Properties.TryGetValue("MS_HttpContext", out httpContextBase))
					{
						httpContextBase = new HttpBatchContextWrapper(httpContextBase, request);
						request.Properties["MS_HttpBatchContext"] = httpContextBase;
					}
					else
					{
						httpContextBase = null;
					}
				}
			}
			else if (!request.Properties.TryGetValue("MS_HttpContext", out httpContextBase))
			{
				httpContextBase = null;
			}
			return httpContextBase;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003699 File Offset: 0x00001899
		public static void SetHttpContext(this HttpRequestMessage request, HttpContextBase context)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			request.Properties["MS_HttpContext"] = context;
		}

		// Token: 0x04000019 RID: 25
		private const string HttpContextBaseKey = "MS_HttpContext";

		// Token: 0x0400001A RID: 26
		private const string HttpBatchContextKey = "MS_HttpBatchContext";
	}
}
