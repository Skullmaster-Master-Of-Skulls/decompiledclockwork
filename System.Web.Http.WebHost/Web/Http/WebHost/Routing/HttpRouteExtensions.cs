using System;
using System.Collections.Generic;
using System.Web.Http.Routing;
using System.Web.Routing;

namespace System.Web.Http.WebHost.Routing
{
	// Token: 0x0200001F RID: 31
	internal static class HttpRouteExtensions
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x000049D8 File Offset: 0x00002BD8
		public static Route ToRoute(this IHttpRoute httpRoute)
		{
			if (httpRoute == null)
			{
				throw Error.ArgumentNull("httpRoute");
			}
			HostedHttpRoute hostedHttpRoute = httpRoute as HostedHttpRoute;
			if (hostedHttpRoute != null)
			{
				return hostedHttpRoute.OriginalRoute;
			}
			IRouteHandler routeHandler;
			if (!(httpRoute.Handler is System.Web.Http.Routing.StopRoutingHandler))
			{
				IRouteHandler instance = HttpControllerRouteHandler.Instance;
				routeHandler = instance;
			}
			else
			{
				routeHandler = new System.Web.Routing.StopRoutingHandler();
			}
			IRouteHandler routeHandler2 = routeHandler;
			return new HttpWebRoute(httpRoute.RouteTemplate, HttpRouteExtensions.MakeRouteValueDictionary(httpRoute.Defaults), HttpRouteExtensions.MakeRouteValueDictionary(httpRoute.Constraints), HttpRouteExtensions.MakeRouteValueDictionary(httpRoute.DataTokens), routeHandler2, httpRoute);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004A4E File Offset: 0x00002C4E
		private static RouteValueDictionary MakeRouteValueDictionary(IDictionary<string, object> dictionary)
		{
			if (dictionary != null)
			{
				return new RouteValueDictionary(dictionary);
			}
			return new RouteValueDictionary();
		}
	}
}
