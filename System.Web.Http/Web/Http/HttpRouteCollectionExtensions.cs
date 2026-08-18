using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Web.Http.Batch;
using System.Web.Http.Routing;

namespace System.Web.Http
{
	// Token: 0x020000FF RID: 255
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpRouteCollectionExtensions
	{
		// Token: 0x0600062E RID: 1582 RVA: 0x0001477B File Offset: 0x0001297B
		public static IHttpRoute MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate)
		{
			return routes.MapHttpRoute(name, routeTemplate, null, null, null);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00014788 File Offset: 0x00012988
		public static IHttpRoute MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate, object defaults)
		{
			return routes.MapHttpRoute(name, routeTemplate, defaults, null, null);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00014795 File Offset: 0x00012995
		public static IHttpRoute MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate, object defaults, object constraints)
		{
			return routes.MapHttpRoute(name, routeTemplate, defaults, constraints, null);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000147A4 File Offset: 0x000129A4
		public static IHttpRoute MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate, object defaults, object constraints, HttpMessageHandler handler)
		{
			if (routes == null)
			{
				throw Error.ArgumentNull("routes");
			}
			HttpRouteValueDictionary defaults2 = new HttpRouteValueDictionary(defaults);
			HttpRouteValueDictionary constraints2 = new HttpRouteValueDictionary(constraints);
			IHttpRoute httpRoute = routes.CreateRoute(routeTemplate, defaults2, constraints2, null, handler);
			routes.Add(name, httpRoute);
			return httpRoute;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x000147E4 File Offset: 0x000129E4
		public static IHttpRoute MapHttpBatchRoute(this HttpRouteCollection routes, string routeName, string routeTemplate, HttpBatchHandler batchHandler)
		{
			return routes.MapHttpRoute(routeName, routeTemplate, null, null, batchHandler);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x000147F1 File Offset: 0x000129F1
		public static IHttpRoute IgnoreRoute(this HttpRouteCollection routes, string routeName, string routeTemplate)
		{
			return routes.IgnoreRoute(routeName, routeTemplate, null);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x000147FC File Offset: 0x000129FC
		public static IHttpRoute IgnoreRoute(this HttpRouteCollection routes, string routeName, string routeTemplate, object constraints)
		{
			if (routes == null)
			{
				throw new ArgumentNullException("routes");
			}
			if (routeName == null)
			{
				throw new ArgumentNullException("routeName");
			}
			if (routeTemplate == null)
			{
				throw new ArgumentNullException("routeTemplate");
			}
			HttpRouteCollectionExtensions.IgnoreHttpRouteInternal ignoreHttpRouteInternal = new HttpRouteCollectionExtensions.IgnoreHttpRouteInternal(routeTemplate, new HttpRouteValueDictionary(constraints), new StopRoutingHandler());
			routes.Add(routeName, ignoreHttpRouteInternal);
			return ignoreHttpRouteInternal;
		}

		// Token: 0x02000101 RID: 257
		private sealed class IgnoreHttpRouteInternal : HttpRoute
		{
			// Token: 0x0600064E RID: 1614 RVA: 0x00014CF4 File Offset: 0x00012EF4
			public IgnoreHttpRouteInternal(string routeTemplate, HttpRouteValueDictionary constraints, HttpMessageHandler handler)
			{
				HttpRouteValueDictionary dataTokens = null;
				base..ctor(routeTemplate, null, constraints, dataTokens, handler);
			}

			// Token: 0x0600064F RID: 1615 RVA: 0x00014D12 File Offset: 0x00012F12
			public override IHttpVirtualPathData GetVirtualPath(HttpRequestMessage request, IDictionary<string, object> values)
			{
				return null;
			}
		}
	}
}
