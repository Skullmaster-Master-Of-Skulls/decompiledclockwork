using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Routing;

namespace System.Web.Mvc.Routing
{
	// Token: 0x0200009C RID: 156
	[Obsolete("Obsolete, do not use. To create custom Routes with attribute routing, use System.Web.Mvc.Routing.RouteFactoryAttribute")]
	public class RouteBuilder
	{
		// Token: 0x06000461 RID: 1121 RVA: 0x0000CD38 File Offset: 0x0000AF38
		public RouteBuilder() : this(new DefaultInlineConstraintResolver())
		{
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000CD45 File Offset: 0x0000AF45
		public RouteBuilder(IInlineConstraintResolver constraintResolver)
		{
			if (constraintResolver == null)
			{
				throw Error.ArgumentNull("constraintResolver");
			}
			this.ConstraintResolver = constraintResolver;
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000CD62 File Offset: 0x0000AF62
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x0000CD6A File Offset: 0x0000AF6A
		public IInlineConstraintResolver ConstraintResolver { get; private set; }

		// Token: 0x06000465 RID: 1125 RVA: 0x0000CD74 File Offset: 0x0000AF74
		public Route BuildDirectRoute(string routeTemplate, ControllerDescriptor controllerDescriptor)
		{
			if (routeTemplate == null)
			{
				throw Error.ArgumentNull("routeTemplate");
			}
			if (controllerDescriptor == null)
			{
				throw Error.ArgumentNull("controllerDescriptor");
			}
			string controllerName = controllerDescriptor.ControllerName;
			RouteAreaAttribute areaFrom = controllerDescriptor.GetAreaFrom();
			string areaName = controllerDescriptor.GetAreaName(areaFrom);
			RouteValueDictionary defaults = new RouteValueDictionary
			{
				{
					"controller",
					controllerName
				}
			};
			Type controllerType = controllerDescriptor.ControllerType;
			RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
			if (areaName != null)
			{
				routeValueDictionary.Add("area", areaName);
				routeValueDictionary.Add("UseNamespaceFallback", false);
				if (controllerType != null)
				{
					routeValueDictionary.Add("Namespaces", new string[]
					{
						controllerType.Namespace
					});
				}
			}
			RouteValueDictionary constraints = new RouteValueDictionary();
			string url = InlineRouteTemplateParser.ParseRouteTemplate(routeTemplate, defaults, constraints, this.ConstraintResolver);
			return new Route(url, new MvcRouteHandler())
			{
				Defaults = defaults,
				Constraints = constraints,
				DataTokens = routeValueDictionary
			};
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000CE6C File Offset: 0x0000B06C
		public Route BuildDirectRoute(string routeTemplate, IEnumerable<string> allowedMethods, string controllerName, string actionName, MethodInfo targetMethod, string areaName)
		{
			if (routeTemplate == null)
			{
				throw Error.ArgumentNull("routeTemplate");
			}
			if (controllerName == null)
			{
				throw Error.ArgumentNull("controllerName");
			}
			if (actionName == null)
			{
				throw Error.ArgumentNull("actionName");
			}
			RouteValueDictionary defaults = new RouteValueDictionary
			{
				{
					"controller",
					controllerName
				},
				{
					"action",
					actionName
				}
			};
			RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
			if (allowedMethods != null)
			{
				string[] array = allowedMethods.ToArray<string>();
				if (array.Length > 0)
				{
					routeValueDictionary.Add("httpMethod", new HttpMethodConstraint(array));
				}
			}
			RouteValueDictionary routeValueDictionary2 = new RouteValueDictionary();
			if (areaName != null)
			{
				routeValueDictionary2.Add("area", areaName);
				routeValueDictionary2.Add("UseNamespaceFallback", false);
				if (targetMethod.DeclaringType != null)
				{
					routeValueDictionary2.Add("Namespaces", new string[]
					{
						targetMethod.DeclaringType.Namespace
					});
				}
			}
			string routeTemplate2 = InlineRouteTemplateParser.ParseRouteTemplate(routeTemplate, defaults, routeValueDictionary, this.ConstraintResolver);
			return this.BuildDirectRoute(defaults, routeValueDictionary, routeValueDictionary2, routeTemplate2, targetMethod);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000CF6C File Offset: 0x0000B16C
		public virtual Route BuildDirectRoute(RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, string routeTemplate, MethodInfo targetMethod)
		{
			return new Route(routeTemplate, new MvcRouteHandler())
			{
				Defaults = defaults,
				Constraints = constraints,
				DataTokens = dataTokens
			};
		}
	}
}
