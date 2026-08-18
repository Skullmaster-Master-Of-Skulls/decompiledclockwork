using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Routing;

namespace System.Web.Mvc.Routing
{
	// Token: 0x0200004B RID: 75
	internal class RouteCollectionRoute : RouteBase, IReadOnlyCollection<RouteBase>, IEnumerable<RouteBase>, IEnumerable
	{
		// Token: 0x0600020F RID: 527 RVA: 0x0000794E File Offset: 0x00005B4E
		public RouteCollectionRoute(IReadOnlyCollection<RouteBase> subRoutes)
		{
			if (subRoutes == null)
			{
				throw new ArgumentNullException("subRoutes");
			}
			this._subRoutes = subRoutes;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000796C File Offset: 0x00005B6C
		public override RouteData GetRouteData(HttpContextBase httpContext)
		{
			List<RouteData> list = new List<RouteData>();
			foreach (RouteBase routeBase in this._subRoutes)
			{
				RouteData routeData = routeBase.GetRouteData(httpContext);
				if (routeData != null)
				{
					list.Add(routeData);
				}
			}
			return RouteCollectionRoute.CreateDirectRouteMatch(this, list);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000079D4 File Offset: 0x00005BD4
		public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
		{
			return null;
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000212 RID: 530 RVA: 0x000079D7 File Offset: 0x00005BD7
		public int Count
		{
			get
			{
				return this._subRoutes.Count;
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000079E4 File Offset: 0x00005BE4
		public IEnumerator<RouteBase> GetEnumerator()
		{
			return this._subRoutes.GetEnumerator();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000079F1 File Offset: 0x00005BF1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._subRoutes.GetEnumerator();
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00007A00 File Offset: 0x00005C00
		public static RouteData CreateDirectRouteMatch(RouteBase route, List<RouteData> matches)
		{
			if (matches.Count == 0)
			{
				return null;
			}
			RouteData routeData = new RouteData();
			routeData.Route = route;
			routeData.RouteHandler = new MvcRouteHandler();
			routeData.SetDirectRouteMatches(matches);
			ControllerDescriptor targetControllerDescriptor = matches[0].GetTargetControllerDescriptor();
			if (targetControllerDescriptor != null)
			{
				routeData.Values["controller"] = targetControllerDescriptor.ControllerName;
			}
			return routeData;
		}

		// Token: 0x04000061 RID: 97
		private readonly IReadOnlyCollection<RouteBase> _subRoutes;
	}
}
