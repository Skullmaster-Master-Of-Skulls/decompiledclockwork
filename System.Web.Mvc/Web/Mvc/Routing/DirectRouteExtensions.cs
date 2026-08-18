using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace System.Web.Mvc.Routing
{
	// Token: 0x0200009B RID: 155
	internal static class DirectRouteExtensions
	{
		// Token: 0x06000449 RID: 1097 RVA: 0x0000CA04 File Offset: 0x0000AC04
		public static decimal GetPrecedence(this RouteData routeData)
		{
			return routeData.GetRouteDataTokenValue("MS_DirectRoutePrecedence");
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000CA11 File Offset: 0x0000AC11
		public static decimal GetPrecedence(this Route route)
		{
			return route.GetRouteDataTokenValue("MS_DirectRoutePrecedence");
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000CA1E File Offset: 0x0000AC1E
		public static void SetPrecedence(this Route route, decimal precedence)
		{
			route.SetRouteDataTokenValue("MS_DirectRoutePrecedence", precedence);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000CA2C File Offset: 0x0000AC2C
		public static int GetOrder(this RouteData routeData)
		{
			return routeData.GetRouteDataTokenValue("MS_DirectRouteOrder");
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000CA39 File Offset: 0x0000AC39
		public static int GetOrder(this Route route)
		{
			return route.GetRouteDataTokenValue("MS_DirectRouteOrder");
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000CA46 File Offset: 0x0000AC46
		public static void SetOrder(this Route route, int order)
		{
			route.SetRouteDataTokenValue("MS_DirectRouteOrder", order);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000CA54 File Offset: 0x0000AC54
		public static bool GetTargetIsAction(this RouteData routeData)
		{
			return routeData.GetRouteDataTokenValue("MS_DirectRouteTargetIsAction");
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000CA61 File Offset: 0x0000AC61
		public static bool GetTargetIsAction(this Route route)
		{
			return route.GetRouteDataTokenValue("MS_DirectRouteTargetIsAction");
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000CA6E File Offset: 0x0000AC6E
		public static void SetTargetIsAction(this Route route, bool targetIsAction)
		{
			route.SetRouteDataTokenValue("MS_DirectRouteTargetIsAction", targetIsAction);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000CA7C File Offset: 0x0000AC7C
		public static ControllerDescriptor GetTargetControllerDescriptor(this Route route)
		{
			ActionDescriptor[] targetActionDescriptors = route.GetTargetActionDescriptors();
			ControllerDescriptor controllerDescriptor = null;
			foreach (ActionDescriptor actionDescriptor in targetActionDescriptors)
			{
				if (controllerDescriptor == null)
				{
					controllerDescriptor = actionDescriptor.ControllerDescriptor;
				}
				else if (controllerDescriptor != actionDescriptor.ControllerDescriptor)
				{
					return null;
				}
			}
			return controllerDescriptor;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000CACC File Offset: 0x0000ACCC
		public static ControllerDescriptor GetTargetControllerDescriptor(this RouteData routeData)
		{
			ActionDescriptor[] targetActionDescriptors = routeData.GetTargetActionDescriptors();
			ControllerDescriptor controllerDescriptor = null;
			foreach (ActionDescriptor actionDescriptor in targetActionDescriptors)
			{
				if (controllerDescriptor == null)
				{
					controllerDescriptor = actionDescriptor.ControllerDescriptor;
				}
				else if (controllerDescriptor != actionDescriptor.ControllerDescriptor)
				{
					return null;
				}
			}
			return controllerDescriptor;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000CB1C File Offset: 0x0000AD1C
		public static Type GetTargetControllerType(this RouteData routeData)
		{
			ControllerDescriptor targetControllerDescriptor = routeData.GetTargetControllerDescriptor();
			if (targetControllerDescriptor != null)
			{
				return targetControllerDescriptor.ControllerType;
			}
			return null;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000CB3B File Offset: 0x0000AD3B
		public static ActionDescriptor[] GetTargetActionDescriptors(this RouteData routeData)
		{
			return routeData.GetRouteDataTokenValue("MS_DirectRouteActions");
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000CB48 File Offset: 0x0000AD48
		public static ActionDescriptor[] GetTargetActionDescriptors(this Route route)
		{
			return route.GetRouteDataTokenValue("MS_DirectRouteActions");
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000CB55 File Offset: 0x0000AD55
		public static void SetTargetActionDescriptors(this Route route, ActionDescriptor[] actionDescriptors)
		{
			if (actionDescriptors == null || actionDescriptors.Length == 0)
			{
				throw Error.ParameterCannotBeNullOrEmpty("actionDescriptors");
			}
			route.SetRouteDataTokenValue("MS_DirectRouteActions", actionDescriptors);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000CB76 File Offset: 0x0000AD76
		public static bool HasDirectRouteMatch(this RouteData routeData)
		{
			if (routeData == null)
			{
				throw Error.ArgumentNull("routeData");
			}
			return routeData.Values.ContainsKey("MS_DirectRouteMatches");
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000CB96 File Offset: 0x0000AD96
		public static IEnumerable<RouteData> GetDirectRouteMatches(this RouteData routeData)
		{
			return routeData.GetRouteDataValue("MS_DirectRouteMatches") ?? Enumerable.Empty<RouteData>();
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000CBAC File Offset: 0x0000ADAC
		public static void SetDirectRouteMatches(this RouteData routeData, IEnumerable<RouteData> matches)
		{
			if (matches == null || !matches.Any<RouteData>())
			{
				throw Error.ParameterCannotBeNullOrEmpty("matches");
			}
			routeData.SetRouteDataValue("MS_DirectRouteMatches", matches);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000CBD0 File Offset: 0x0000ADD0
		public static bool IsDirectRoute(this RouteBase routeBase)
		{
			Route route = routeBase as Route;
			return route != null && route.GetTargetActionDescriptors() != null;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000CBF8 File Offset: 0x0000ADF8
		private static T GetRouteDataTokenValue<T>(this Route route, string key)
		{
			if (route == null)
			{
				throw Error.ArgumentNull("route");
			}
			if (key == null)
			{
				throw Error.ArgumentNull("key");
			}
			T result;
			if (route.DataTokens != null && route.DataTokens.TryGetValue(key, out result))
			{
				return result;
			}
			return default(T);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000CC44 File Offset: 0x0000AE44
		private static T GetRouteDataTokenValue<T>(this RouteData routeData, string key)
		{
			if (routeData == null)
			{
				throw Error.ArgumentNull("route");
			}
			if (key == null)
			{
				throw Error.ArgumentNull("key");
			}
			return (routeData.Route as Route).GetRouteDataTokenValue(key);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000CC74 File Offset: 0x0000AE74
		private static void SetRouteDataTokenValue<T>(this Route route, string key, T value)
		{
			if (route == null)
			{
				throw Error.ArgumentNull("route");
			}
			if (key == null)
			{
				throw Error.ArgumentNull("key");
			}
			if (route.DataTokens == null)
			{
				route.DataTokens = new RouteValueDictionary();
			}
			route.DataTokens[key] = value;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000CCC4 File Offset: 0x0000AEC4
		private static T GetRouteDataValue<T>(this RouteData routeData, string key)
		{
			if (routeData == null)
			{
				throw Error.ArgumentNull("routeData");
			}
			if (key == null)
			{
				throw Error.ArgumentNull("key");
			}
			T result;
			if (routeData.Values.TryGetValue(key, out result))
			{
				return result;
			}
			return default(T);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000CD08 File Offset: 0x0000AF08
		private static void SetRouteDataValue<T>(this RouteData routeData, string key, T value)
		{
			if (routeData == null)
			{
				throw Error.ArgumentNull("routeData");
			}
			if (key == null)
			{
				throw Error.ArgumentNull("key");
			}
			routeData.Values[key] = value;
		}
	}
}
