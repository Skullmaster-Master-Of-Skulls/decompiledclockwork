using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;

namespace System.Web.Http.Batch
{
	// Token: 0x02000026 RID: 38
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class BatchHttpRequestMessageExtensions
	{
		// Token: 0x060000FB RID: 251 RVA: 0x00005BDC File Offset: 0x00003DDC
		public static void CopyBatchRequestProperties(this HttpRequestMessage subRequest, HttpRequestMessage batchRequest)
		{
			if (subRequest == null)
			{
				throw new ArgumentNullException("subRequest");
			}
			if (batchRequest == null)
			{
				throw new ArgumentNullException("batchRequest");
			}
			foreach (KeyValuePair<string, object> item in batchRequest.Properties)
			{
				if (!BatchHttpRequestMessageExtensions.BatchRequestPropertyExclusions.Contains(item.Key))
				{
					subRequest.Properties.Add(item);
				}
			}
			HttpRequestContext requestContext = subRequest.GetRequestContext();
			if (requestContext != null)
			{
				BatchHttpRequestContext context = new BatchHttpRequestContext(requestContext)
				{
					Url = new UrlHelper(subRequest)
				};
				subRequest.SetRequestContext(context);
			}
		}

		// Token: 0x0400004B RID: 75
		private const string HttpBatchContextKey = "MS_HttpBatchContext";

		// Token: 0x0400004C RID: 76
		private static readonly string[] BatchRequestPropertyExclusions = new string[]
		{
			HttpPropertyKeys.HttpRouteDataKey,
			HttpPropertyKeys.DisposableRequestResourcesKey,
			HttpPropertyKeys.SynchronizationContextKey,
			HttpPropertyKeys.HttpConfigurationKey,
			"MS_RoutingContext",
			"MS_HttpBatchContext"
		};
	}
}
