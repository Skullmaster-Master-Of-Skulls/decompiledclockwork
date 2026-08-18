using System;
using System.Collections.Generic;
using System.Web.Http.Routing;
using System.Web.Routing;

namespace System.Web.Http.WebHost.Routing
{
	// Token: 0x0200001B RID: 27
	internal class HostedHttpRouteData : IHttpRouteData
	{
		// Token: 0x060000BE RID: 190 RVA: 0x000046CC File Offset: 0x000028CC
		public HostedHttpRouteData(RouteData routeData)
		{
			if (routeData == null)
			{
				throw Error.ArgumentNull("routeData");
			}
			this.OriginalRouteData = routeData;
			HttpWebRoute httpWebRoute = routeData.Route as HttpWebRoute;
			this.Route = ((httpWebRoute == null) ? null : httpWebRoute.HttpRoute);
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004712 File Offset: 0x00002912
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x0000471A File Offset: 0x0000291A
		public IHttpRoute Route { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004723 File Offset: 0x00002923
		public IDictionary<string, object> Values
		{
			get
			{
				return this.OriginalRouteData.Values;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004730 File Offset: 0x00002930
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00004738 File Offset: 0x00002938
		internal RouteData OriginalRouteData { get; private set; }
	}
}
