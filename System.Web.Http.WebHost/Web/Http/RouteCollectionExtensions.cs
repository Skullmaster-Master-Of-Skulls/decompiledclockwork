using System;
using System.ComponentModel;
using System.Net.Http;
using System.Web.Http.Routing;
using System.Web.Http.WebHost.Routing;
using System.Web.Routing;

namespace System.Web.Http
{
	// Token: 0x02000028 RID: 40
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class RouteCollectionExtensions
	{
		// Token: 0x06000120 RID: 288 RVA: 0x00006BD0 File Offset: 0x00004DD0
		public static Route MapHttpRoute(this RouteCollection routes, string name, string routeTemplate)
		{
			return routes.MapHttpRoute(name, routeTemplate, null, null, null);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006BDD File Offset: 0x00004DDD
		public static Route MapHttpRoute(this RouteCollection routes, string name, string routeTemplate, object defaults)
		{
			return routes.MapHttpRoute(name, routeTemplate, defaults, null, null);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00006BEA File Offset: 0x00004DEA
		public static Route MapHttpRoute(this RouteCollection routes, string name, string routeTemplate, object defaults, object constraints)
		{
			return routes.MapHttpRoute(name, routeTemplate, defaults, constraints, null);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006BF8 File Offset: 0x00004DF8
		public static Route MapHttpRoute(this RouteCollection routes, string name, string routeTemplate, object defaults, object constraints, HttpMessageHandler handler)
		{
			if (routes == null)
			{
				throw Error.ArgumentNull("routes");
			}
			HttpRouteValueDictionary defaults2 = new HttpRouteValueDictionary(defaults);
			HttpRouteValueDictionary constraints2 = new HttpRouteValueDictionary(constraints);
			HostedHttpRoute hostedHttpRoute = (HostedHttpRoute)GlobalConfiguration.Configuration.Routes.CreateRoute(routeTemplate, defaults2, constraints2, null, handler);
			Route originalRoute = hostedHttpRoute.OriginalRoute;
			routes.Add(name, originalRoute);
			return originalRoute;
		}
	}
}
