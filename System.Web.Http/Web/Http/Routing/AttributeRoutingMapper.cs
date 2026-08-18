using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Properties;

namespace System.Web.Http.Routing
{
	// Token: 0x0200007C RID: 124
	internal static class AttributeRoutingMapper
	{
		// Token: 0x0600033E RID: 830 RVA: 0x0000A2B0 File Offset: 0x000084B0
		public static void MapAttributeRoutes(HttpConfiguration configuration, IInlineConstraintResolver constraintResolver, IDirectRouteProvider directRouteProvider)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			if (constraintResolver == null)
			{
				throw new ArgumentNullException("constraintResolver");
			}
			if (directRouteProvider == null)
			{
				throw new ArgumentNullException("directRouteProvider");
			}
			RouteCollectionRoute aggregateRoute = new RouteCollectionRoute();
			configuration.Routes.Add("MS_attributerouteWebApi", aggregateRoute);
			Action<HttpConfiguration> previousInitializer = configuration.Initializer;
			configuration.Initializer = delegate(HttpConfiguration config)
			{
				previousInitializer(config);
				SubRouteCollection subRoutes = null;
				Func<SubRouteCollection> initializer = delegate()
				{
					subRoutes = new SubRouteCollection();
					AttributeRoutingMapper.AddRouteEntries(subRoutes, configuration, constraintResolver, directRouteProvider);
					return subRoutes;
				};
				aggregateRoute.EnsureInitialized(initializer);
				if (subRoutes != null)
				{
					AttributeRoutingMapper.AddGenerationHooksForSubRoutes(config.Routes, subRoutes.Entries);
				}
			};
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000A360 File Offset: 0x00008560
		private static void AddGenerationHooksForSubRoutes(HttpRouteCollection routeTable, IEnumerable<RouteEntry> entries)
		{
			foreach (RouteEntry routeEntry in entries)
			{
				string name = routeEntry.Name;
				if (name != null)
				{
					IHttpRoute route = routeEntry.Route;
					IHttpRoute route2 = new LinkGenerationRoute(route);
					routeTable.Add(name, route2);
				}
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000A3CC File Offset: 0x000085CC
		private static void AddRouteEntries(SubRouteCollection collector, HttpConfiguration configuration, IInlineConstraintResolver constraintResolver, IDirectRouteProvider directRouteProvider)
		{
			IHttpControllerSelector httpControllerSelector = configuration.Services.GetHttpControllerSelector();
			IDictionary<string, HttpControllerDescriptor> controllerMapping = httpControllerSelector.GetControllerMapping();
			if (controllerMapping != null)
			{
				foreach (HttpControllerDescriptor httpControllerDescriptor in controllerMapping.Values)
				{
					IHttpActionSelector actionSelector = httpControllerDescriptor.Configuration.Services.GetActionSelector();
					ILookup<string, HttpActionDescriptor> actionMapping = actionSelector.GetActionMapping(httpControllerDescriptor);
					if (actionMapping != null)
					{
						List<HttpActionDescriptor> actionDescriptors = actionMapping.SelectMany((IGrouping<string, HttpActionDescriptor> g) => g).ToList<HttpActionDescriptor>();
						IReadOnlyCollection<RouteEntry> directRoutes = directRouteProvider.GetDirectRoutes(httpControllerDescriptor, actionDescriptors, constraintResolver);
						if (directRoutes == null)
						{
							throw Error.InvalidOperation(SRResources.TypeMethodMustNotReturnNull, new object[]
							{
								typeof(IDirectRouteProvider).Name,
								"GetDirectRoutes"
							});
						}
						foreach (RouteEntry routeEntry in directRoutes)
						{
							if (routeEntry == null)
							{
								throw Error.InvalidOperation(SRResources.TypeMethodMustNotReturnNull, new object[]
								{
									typeof(IDirectRouteProvider).Name,
									"GetDirectRoutes"
								});
							}
							DirectRouteBuilder.ValidateRouteEntry(routeEntry);
							HttpControllerDescriptor targetControllerDescriptor = routeEntry.Route.GetTargetControllerDescriptor();
							if (targetControllerDescriptor == null)
							{
								HttpActionDescriptor[] targetActionDescriptors = routeEntry.Route.GetTargetActionDescriptors();
								foreach (HttpActionDescriptor actionDescriptor in targetActionDescriptors)
								{
									actionDescriptor.SetIsAttributeRouted(true);
								}
							}
							else
							{
								targetControllerDescriptor.SetIsAttributeRouted(true);
							}
						}
						collector.AddRange(directRoutes);
					}
				}
			}
		}

		// Token: 0x040000F4 RID: 244
		private const string AttributeRouteName = "MS_attributerouteWebApi";
	}
}
