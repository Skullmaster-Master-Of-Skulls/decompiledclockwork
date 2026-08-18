using System;
using System.Collections.Generic;
using System.Web.Routing;

namespace System.Web.Mvc
{
	// Token: 0x020001B3 RID: 435
	internal static class RouteValuesHelpers
	{
		// Token: 0x06000C45 RID: 3141 RVA: 0x0002093D File Offset: 0x0001EB3D
		public static RouteValueDictionary GetRouteValues(RouteValueDictionary routeValues)
		{
			if (routeValues == null)
			{
				return new RouteValueDictionary();
			}
			return new RouteValueDictionary(routeValues);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00020950 File Offset: 0x0001EB50
		public static RouteValueDictionary MergeRouteValues(string actionName, string controllerName, RouteValueDictionary implicitRouteValues, RouteValueDictionary routeValues, bool includeImplicitMvcValues)
		{
			RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
			if (includeImplicitMvcValues)
			{
				object value;
				if (implicitRouteValues != null && implicitRouteValues.TryGetValue("action", out value))
				{
					routeValueDictionary["action"] = value;
				}
				if (implicitRouteValues != null && implicitRouteValues.TryGetValue("controller", out value))
				{
					routeValueDictionary["controller"] = value;
				}
			}
			if (routeValues != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in RouteValuesHelpers.GetRouteValues(routeValues))
				{
					routeValueDictionary[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			if (actionName != null)
			{
				routeValueDictionary["action"] = actionName;
			}
			if (controllerName != null)
			{
				routeValueDictionary["controller"] = controllerName;
			}
			return routeValueDictionary;
		}
	}
}
