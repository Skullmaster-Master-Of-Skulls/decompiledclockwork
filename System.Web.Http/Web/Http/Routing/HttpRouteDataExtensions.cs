using System;
using System.Collections.Generic;

namespace System.Web.Http.Routing
{
	// Token: 0x02000084 RID: 132
	public static class HttpRouteDataExtensions
	{
		// Token: 0x0600036F RID: 879 RVA: 0x0000AB78 File Offset: 0x00008D78
		public static void RemoveOptionalRoutingParameters(this IHttpRouteData routeData)
		{
			HttpRouteDataExtensions.RemoveOptionalRoutingParameters(routeData.Values);
			IEnumerable<IHttpRouteData> subRoutes = routeData.GetSubRoutes();
			if (subRoutes != null)
			{
				foreach (IHttpRouteData routeData2 in subRoutes)
				{
					routeData2.RemoveOptionalRoutingParameters();
				}
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000ABD4 File Offset: 0x00008DD4
		private static void RemoveOptionalRoutingParameters(IDictionary<string, object> routeValueDictionary)
		{
			int count = routeValueDictionary.Count;
			int num = 0;
			string[] array = new string[count];
			foreach (KeyValuePair<string, object> keyValuePair in routeValueDictionary)
			{
				if (keyValuePair.Value == RouteParameter.Optional)
				{
					array[num] = keyValuePair.Key;
					num++;
				}
			}
			for (int i = 0; i < num; i++)
			{
				string key = array[i];
				routeValueDictionary.Remove(key);
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000AC68 File Offset: 0x00008E68
		public static IEnumerable<IHttpRouteData> GetSubRoutes(this IHttpRouteData routeData)
		{
			IHttpRouteData[] result = null;
			if (routeData.Values.TryGetValue("MS_SubRoutes", out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000AC90 File Offset: 0x00008E90
		internal static CandidateAction[] GetDirectRouteCandidates(this IHttpRouteData routeData)
		{
			IEnumerable<IHttpRouteData> subRoutes = routeData.GetSubRoutes();
			if (subRoutes != null)
			{
				List<CandidateAction> list = new List<CandidateAction>();
				foreach (IHttpRouteData httpRouteData in subRoutes)
				{
					CandidateAction[] directRouteCandidates = httpRouteData.Route.GetDirectRouteCandidates();
					if (directRouteCandidates != null)
					{
						list.AddRange(directRouteCandidates);
					}
				}
				return list.ToArray();
			}
			if (routeData.Route == null)
			{
				return null;
			}
			return routeData.Route.GetDirectRouteCandidates();
		}
	}
}
