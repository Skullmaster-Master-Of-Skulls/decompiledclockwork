using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Properties;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000024 RID: 36
	public static class HttpClientFactory
	{
		// Token: 0x06000120 RID: 288 RVA: 0x00005498 File Offset: 0x00003698
		public static HttpClient Create(params DelegatingHandler[] handlers)
		{
			return HttpClientFactory.Create(new HttpClientHandler(), handlers);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000054A8 File Offset: 0x000036A8
		public static HttpClient Create(HttpMessageHandler innerHandler, params DelegatingHandler[] handlers)
		{
			HttpMessageHandler handler = HttpClientFactory.CreatePipeline(innerHandler, handlers);
			return new HttpClient(handler);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000054C4 File Offset: 0x000036C4
		public static HttpMessageHandler CreatePipeline(HttpMessageHandler innerHandler, IEnumerable<DelegatingHandler> handlers)
		{
			if (innerHandler == null)
			{
				throw Error.ArgumentNull("innerHandler");
			}
			if (handlers == null)
			{
				return innerHandler;
			}
			HttpMessageHandler httpMessageHandler = innerHandler;
			IEnumerable<DelegatingHandler> enumerable = handlers.Reverse<DelegatingHandler>();
			foreach (DelegatingHandler delegatingHandler in enumerable)
			{
				if (delegatingHandler == null)
				{
					throw Error.Argument("handlers", Resources.DelegatingHandlerArrayContainsNullItem, new object[]
					{
						typeof(DelegatingHandler).Name
					});
				}
				if (delegatingHandler.InnerHandler != null)
				{
					throw Error.Argument("handlers", Resources.DelegatingHandlerArrayHasNonNullInnerHandler, new object[]
					{
						typeof(DelegatingHandler).Name,
						"InnerHandler",
						delegatingHandler.GetType().Name
					});
				}
				delegatingHandler.InnerHandler = httpMessageHandler;
				httpMessageHandler = delegatingHandler;
			}
			return httpMessageHandler;
		}
	}
}
