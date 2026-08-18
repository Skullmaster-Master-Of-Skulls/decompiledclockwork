using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc.Async;
using System.Web.Mvc.Properties;
using System.Web.Routing;

namespace System.Web.Mvc.Routing
{
	// Token: 0x0200009A RID: 154
	internal static class AttributeRoutingMapper
	{
		// Token: 0x0600043C RID: 1084 RVA: 0x0000C4F4 File Offset: 0x0000A6F4
		public static void MapAttributeRoutes(RouteCollection routes, IInlineConstraintResolver constraintResolver)
		{
			if (routes == null)
			{
				throw new ArgumentNullException("routes");
			}
			if (constraintResolver == null)
			{
				throw new ArgumentNullException("constraintResolver");
			}
			AttributeRoutingMapper.MapAttributeRoutes(routes, constraintResolver, new DefaultDirectRouteProvider());
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000C520 File Offset: 0x0000A720
		public static void MapAttributeRoutes(RouteCollection routes, IInlineConstraintResolver constraintResolver, IDirectRouteProvider directRouteProvider)
		{
			if (routes == null)
			{
				throw new ArgumentNullException("routes");
			}
			if (constraintResolver == null)
			{
				throw new ArgumentNullException("constraintResolver");
			}
			if (directRouteProvider == null)
			{
				throw new ArgumentNullException("directRouteProvider");
			}
			DefaultControllerFactory defaultControllerFactory;
			if ((defaultControllerFactory = (DependencyResolver.Current.GetService<IControllerFactory>() as DefaultControllerFactory)) == null)
			{
				defaultControllerFactory = ((ControllerBuilder.Current.GetControllerFactory() as DefaultControllerFactory) ?? new DefaultControllerFactory());
			}
			DefaultControllerFactory defaultControllerFactory2 = defaultControllerFactory;
			IReadOnlyList<Type> controllerTypes = defaultControllerFactory2.GetControllerTypes();
			AttributeRoutingMapper.MapAttributeRoutes(routes, controllerTypes, constraintResolver, directRouteProvider);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000C593 File Offset: 0x0000A793
		public static void MapAttributeRoutes(RouteCollection routes, IEnumerable<Type> controllerTypes)
		{
			AttributeRoutingMapper.MapAttributeRoutes(routes, controllerTypes, new DefaultInlineConstraintResolver());
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000C5A1 File Offset: 0x0000A7A1
		public static void MapAttributeRoutes(RouteCollection routes, IEnumerable<Type> controllerTypes, IInlineConstraintResolver constraintResolver)
		{
			AttributeRoutingMapper.MapAttributeRoutes(routes, controllerTypes, constraintResolver, new DefaultDirectRouteProvider());
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000C5DC File Offset: 0x0000A7DC
		public static void MapAttributeRoutes(RouteCollection routes, IEnumerable<Type> controllerTypes, IInlineConstraintResolver constraintResolver, IDirectRouteProvider directRouteProvider)
		{
			if (routes == null)
			{
				throw new ArgumentNullException("routes");
			}
			if (controllerTypes == null)
			{
				throw new ArgumentNullException("controllerTypes");
			}
			if (constraintResolver == null)
			{
				throw new ArgumentNullException("constraintResolver");
			}
			if (directRouteProvider == null)
			{
				throw new ArgumentNullException("directRouteProvider");
			}
			SubRouteCollection subRouteCollection = new SubRouteCollection();
			AttributeRoutingMapper.AddRouteEntries(subRouteCollection, controllerTypes, constraintResolver, directRouteProvider);
			IReadOnlyCollection<RouteEntry> entries = subRouteCollection.Entries;
			if (entries.Count > 0)
			{
				RouteCollectionRoute item = new RouteCollectionRoute(subRouteCollection);
				routes.Add(item);
				RouteEntry[] entries2 = (from r in entries
				orderby r.Route.GetOrder()
				select r).ThenBy(delegate(RouteEntry r)
				{
					if (!r.Route.GetTargetIsAction())
					{
						return 1;
					}
					return 0;
				}).ThenBy((RouteEntry r) => r.Route.GetPrecedence()).ToArray<RouteEntry>();
				AttributeRoutingMapper.AddGenerationHooksForSubRoutes(routes, entries2);
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000C6C8 File Offset: 0x0000A8C8
		internal static IReadOnlyCollection<RouteEntry> GetAttributeRoutes(Type controllerType)
		{
			SubRouteCollection subRouteCollection = new SubRouteCollection();
			AttributeRoutingMapper.AddRouteEntries(subRouteCollection, new Type[]
			{
				controllerType
			}, new DefaultInlineConstraintResolver(), new DefaultDirectRouteProvider());
			return subRouteCollection.Entries;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000C700 File Offset: 0x0000A900
		private static void AddGenerationHooksForSubRoutes(RouteCollection routeTable, IList<RouteEntry> entries)
		{
			foreach (RouteEntry routeEntry in entries)
			{
				Route route = routeEntry.Route;
				RouteBase item = new LinkGenerationRoute(route);
				string name = routeEntry.Name;
				if (name == null)
				{
					routeTable.Add(item);
				}
				else
				{
					routeTable.Add(name, item);
				}
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000C770 File Offset: 0x0000A970
		internal static void AddRouteEntries(SubRouteCollection collector, IEnumerable<Type> controllerTypes, IInlineConstraintResolver constraintResolver, IDirectRouteProvider directRouteProvider)
		{
			IEnumerable<ReflectedAsyncControllerDescriptor> controllerDescriptors = AttributeRoutingMapper.GetControllerDescriptors(controllerTypes);
			foreach (ReflectedAsyncControllerDescriptor reflectedAsyncControllerDescriptor in controllerDescriptors)
			{
				List<ActionDescriptor> actionDescriptors = AttributeRoutingMapper.GetActionDescriptors(reflectedAsyncControllerDescriptor);
				IReadOnlyCollection<RouteEntry> directRoutes = directRouteProvider.GetDirectRoutes(reflectedAsyncControllerDescriptor, actionDescriptors, constraintResolver);
				if (directRoutes == null)
				{
					throw Error.InvalidOperation(MvcResources.TypeMethodMustNotReturnNull, new object[]
					{
						typeof(IDirectRouteProvider).Name,
						"GetDirectRoutes"
					});
				}
				foreach (RouteEntry routeEntry in directRoutes)
				{
					if (routeEntry == null)
					{
						throw Error.InvalidOperation(MvcResources.TypeMethodMustNotReturnNull, new object[]
						{
							typeof(IDirectRouteProvider).Name,
							"GetDirectRoutes"
						});
					}
					DirectRouteBuilder.ValidateRouteEntry(routeEntry);
					if (routeEntry.Route.GetTargetIsAction())
					{
						ActionDescriptor[] targetActionDescriptors = routeEntry.Route.GetTargetActionDescriptors();
						using (IEnumerator<IMethodInfoActionDescriptor> enumerator3 = targetActionDescriptors.OfType<IMethodInfoActionDescriptor>().GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								IMethodInfoActionDescriptor methodInfoActionDescriptor = enumerator3.Current;
								MethodInfo methodInfo = methodInfoActionDescriptor.MethodInfo;
								if (methodInfo != null)
								{
									reflectedAsyncControllerDescriptor.Selector.StandardRouteMethods.Remove(methodInfo);
								}
							}
							continue;
						}
					}
					reflectedAsyncControllerDescriptor.Selector.StandardRouteMethods.Clear();
				}
				collector.AddRange(directRoutes);
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000C95C File Offset: 0x0000AB5C
		private static IEnumerable<ReflectedAsyncControllerDescriptor> GetControllerDescriptors(IEnumerable<Type> controllerTypes)
		{
			Func<Type, ControllerDescriptor> descriptorFactory = ReflectedAsyncControllerDescriptor.DefaultDescriptorFactory;
			ControllerDescriptorCache descriptorsCache = new AsyncControllerActionInvoker().DescriptorCache;
			return (from type in controllerTypes
			select descriptorsCache.GetDescriptor<Type>(type, descriptorFactory, type)).Cast<ReflectedAsyncControllerDescriptor>();
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000C9A4 File Offset: 0x0000ABA4
		private static List<ActionDescriptor> GetActionDescriptors(ReflectedAsyncControllerDescriptor controller)
		{
			AsyncActionMethodSelector selector = controller.Selector;
			List<ActionDescriptor> list = new List<ActionDescriptor>();
			foreach (MethodInfo methodInfo in selector.ActionMethods)
			{
				string actionName = selector.GetActionName(methodInfo);
				ActionDescriptorCreator actionDescriptorDelegate = selector.GetActionDescriptorDelegate(methodInfo);
				ActionDescriptor item = actionDescriptorDelegate(actionName, controller);
				list.Add(item);
			}
			return list;
		}
	}
}
