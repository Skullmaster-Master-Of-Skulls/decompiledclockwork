using System;
using System.Web.Routing;

namespace System.Web.Mvc.Routing
{
	// Token: 0x0200004A RID: 74
	internal class LinkGenerationRoute : Route
	{
		// Token: 0x0600020C RID: 524 RVA: 0x00007901 File Offset: 0x00005B01
		public LinkGenerationRoute(Route innerRoute) : base(innerRoute.Url, innerRoute.Defaults, innerRoute.Constraints, innerRoute.DataTokens, innerRoute.RouteHandler)
		{
			if (innerRoute == null)
			{
				throw Error.ArgumentNull("innerRoute");
			}
			this._innerRoute = innerRoute;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000793C File Offset: 0x00005B3C
		public override RouteData GetRouteData(HttpContextBase httpContext)
		{
			return null;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000793F File Offset: 0x00005B3F
		public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
		{
			return this._innerRoute.GetVirtualPath(requestContext, values);
		}

		// Token: 0x04000060 RID: 96
		private readonly Route _innerRoute;
	}
}
