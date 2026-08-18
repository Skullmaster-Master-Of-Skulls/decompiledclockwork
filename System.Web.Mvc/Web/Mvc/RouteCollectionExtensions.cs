using System;
using System.Collections.Generic;
using System.Web.Mvc.Routing;
using System.Web.Routing;
using System.Web.WebPages;

namespace System.Web.Mvc
{
	// Token: 0x020001E7 RID: 487
	public static class RouteCollectionExtensions
	{
		// Token: 0x06000EA4 RID: 3748 RVA: 0x00026B34 File Offset: 0x00024D34
		private static RouteCollection FilterRouteCollectionByArea(RouteCollection routes, string areaName, out bool usingAreas)
		{
			if (areaName == null)
			{
				areaName = string.Empty;
			}
			usingAreas = false;
			RouteCollection routeCollection = new RouteCollection
			{
				AppendTrailingSlash = routes.AppendTrailingSlash,
				LowercaseUrls = routes.LowercaseUrls,
				RouteExistingFiles = routes.RouteExistingFiles
			};
			using (routes.GetReadLock())
			{
				foreach (RouteBase routeBase in routes)
				{
					string text = AreaHelpers.GetAreaName(routeBase) ?? string.Empty;
					usingAreas |= (text.Length > 0);
					if (string.Equals(text, areaName, StringComparison.OrdinalIgnoreCase))
					{
						routeCollection.Add(routeBase);
					}
				}
			}
			if (!usingAreas)
			{
				return routes;
			}
			return routeCollection;
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00026C0C File Offset: 0x00024E0C
		public static VirtualPathData GetVirtualPathForArea(this RouteCollection routes, RequestContext requestContext, RouteValueDictionary values)
		{
			return routes.GetVirtualPathForArea(requestContext, null, values);
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x00026C18 File Offset: 0x00024E18
		public static VirtualPathData GetVirtualPathForArea(this RouteCollection routes, RequestContext requestContext, string name, RouteValueDictionary values)
		{
			bool flag;
			return routes.GetVirtualPathForArea(requestContext, name, values, out flag);
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00026C30 File Offset: 0x00024E30
		internal static VirtualPathData GetVirtualPathForArea(this RouteCollection routes, RequestContext requestContext, string name, RouteValueDictionary values, out bool usingAreas)
		{
			if (routes == null)
			{
				throw new ArgumentNullException("routes");
			}
			if (!string.IsNullOrEmpty(name))
			{
				usingAreas = false;
				return routes.GetVirtualPath(requestContext, name, values);
			}
			string areaName = null;
			if (values != null)
			{
				object obj;
				if (values.TryGetValue("area", out obj))
				{
					areaName = (obj as string);
				}
				else if (requestContext != null)
				{
					areaName = AreaHelpers.GetAreaName(requestContext.RouteData);
				}
			}
			RouteValueDictionary routeValueDictionary = values;
			RouteCollection routeCollection = RouteCollectionExtensions.FilterRouteCollectionByArea(routes, areaName, out usingAreas);
			if (usingAreas)
			{
				routeValueDictionary = new RouteValueDictionary(values);
				routeValueDictionary.Remove("area");
			}
			return routeCollection.GetVirtualPath(requestContext, routeValueDictionary);
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00026CBD File Offset: 0x00024EBD
		public static void IgnoreRoute(this RouteCollection routes, string url)
		{
			routes.IgnoreRoute(url, null);
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x00026CC8 File Offset: 0x00024EC8
		public static void IgnoreRoute(this RouteCollection routes, string url, object constraints)
		{
			if (routes == null)
			{
				throw new ArgumentNullException("routes");
			}
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			RouteCollectionExtensions.IgnoreRouteInternal ignoreRouteInternal = new RouteCollectionExtensions.IgnoreRouteInternal(url)
			{
				Constraints = RouteCollectionExtensions.CreateRouteValueDictionaryUncached(constraints)
			};
			ConstraintValidation.Validate(ignoreRouteInternal);
			routes.Add(ignoreRouteInternal);
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00026D13 File Offset: 0x00024F13
		public static Route MapRoute(this RouteCollection routes, string name, string url)
		{
			return routes.MapRoute(name, url, null, null);
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00026D1F File Offset: 0x00024F1F
		public static Route MapRoute(this RouteCollection routes, string name, string url, object defaults)
		{
			return routes.MapRoute(name, url, defaults, null);
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x00026D2B File Offset: 0x00024F2B
		public static Route MapRoute(this RouteCollection routes, string name, string url, object defaults, object constraints)
		{
			return routes.MapRoute(name, url, defaults, constraints, null);
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00026D39 File Offset: 0x00024F39
		public static Route MapRoute(this RouteCollection routes, string name, string url, string[] namespaces)
		{
			return routes.MapRoute(name, url, null, null, namespaces);
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00026D46 File Offset: 0x00024F46
		public static Route MapRoute(this RouteCollection routes, string name, string url, object defaults, string[] namespaces)
		{
			return routes.MapRoute(name, url, defaults, null, namespaces);
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x00026D54 File Offset: 0x00024F54
		public static Route MapRoute(this RouteCollection routes, string name, string url, object defaults, object constraints, string[] namespaces)
		{
			if (routes == null)
			{
				throw new ArgumentNullException("routes");
			}
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			Route route = new Route(url, new MvcRouteHandler())
			{
				Defaults = RouteCollectionExtensions.CreateRouteValueDictionaryUncached(defaults),
				Constraints = RouteCollectionExtensions.CreateRouteValueDictionaryUncached(constraints),
				DataTokens = new RouteValueDictionary()
			};
			ConstraintValidation.Validate(route);
			if (namespaces != null && namespaces.Length > 0)
			{
				route.DataTokens["Namespaces"] = namespaces;
			}
			routes.Add(name, route);
			return route;
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x00026DDC File Offset: 0x00024FDC
		private static RouteValueDictionary CreateRouteValueDictionaryUncached(object values)
		{
			IDictionary<string, object> dictionary = values as IDictionary<string, object>;
			if (dictionary != null)
			{
				return new RouteValueDictionary(dictionary);
			}
			return TypeHelper.ObjectToDictionaryUncached(values);
		}

		// Token: 0x020001E8 RID: 488
		private sealed class IgnoreRouteInternal : Route
		{
			// Token: 0x06000EB1 RID: 3761 RVA: 0x00026E00 File Offset: 0x00025000
			public IgnoreRouteInternal(string url) : base(url, new StopRoutingHandler())
			{
			}

			// Token: 0x06000EB2 RID: 3762 RVA: 0x00026E0E File Offset: 0x0002500E
			public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary routeValues)
			{
				return null;
			}
		}
	}
}
