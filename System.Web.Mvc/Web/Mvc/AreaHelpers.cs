using System;
using System.Web.Routing;

namespace System.Web.Mvc
{
	// Token: 0x02000151 RID: 337
	internal static class AreaHelpers
	{
		// Token: 0x060008A0 RID: 2208 RVA: 0x00017E70 File Offset: 0x00016070
		public static string GetAreaName(RouteBase route)
		{
			IRouteWithArea routeWithArea = route as IRouteWithArea;
			if (routeWithArea != null)
			{
				return routeWithArea.Area;
			}
			Route route2 = route as Route;
			if (route2 != null && route2.DataTokens != null)
			{
				return route2.DataTokens["area"] as string;
			}
			return null;
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00017EB8 File Offset: 0x000160B8
		public static string GetAreaName(RouteData routeData)
		{
			object obj;
			if (routeData.DataTokens.TryGetValue("area", out obj))
			{
				return obj as string;
			}
			return AreaHelpers.GetAreaName(routeData.Route);
		}
	}
}
