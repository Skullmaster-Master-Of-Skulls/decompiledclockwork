using System;
using System.Collections.Generic;
using System.Web.Http.Routing;
using System.Web.Routing;

namespace System.Web.Http.WebHost.Routing
{
	// Token: 0x0200001E RID: 30
	internal static class HttpRouteDataExtensions
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x0000493C File Offset: 0x00002B3C
		public static RouteData ToRouteData(this IHttpRouteData httpRouteData)
		{
			if (httpRouteData == null)
			{
				throw Error.ArgumentNull("httpRouteData");
			}
			HostedHttpRouteData hostedHttpRouteData = httpRouteData as HostedHttpRouteData;
			if (hostedHttpRouteData != null)
			{
				return hostedHttpRouteData.OriginalRouteData;
			}
			Route route = httpRouteData.Route.ToRoute();
			RouteData routeData = new RouteData(route, HttpControllerRouteHandler.Instance);
			foreach (KeyValuePair<string, object> keyValuePair in httpRouteData.Values)
			{
				routeData.Values.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return routeData;
		}
	}
}
