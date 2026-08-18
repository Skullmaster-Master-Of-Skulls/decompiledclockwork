using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;

namespace System.Web.Http.Routing
{
	// Token: 0x0200007D RID: 125
	public class DefaultDirectRouteProvider : IDirectRouteProvider
	{
		// Token: 0x06000342 RID: 834 RVA: 0x0000A5AC File Offset: 0x000087AC
		public virtual IReadOnlyList<RouteEntry> GetDirectRoutes(HttpControllerDescriptor controllerDescriptor, IReadOnlyList<HttpActionDescriptor> actionDescriptors, IInlineConstraintResolver constraintResolver)
		{
			List<RouteEntry> list = new List<RouteEntry>();
			List<HttpActionDescriptor> list2 = new List<HttpActionDescriptor>();
			foreach (HttpActionDescriptor httpActionDescriptor in actionDescriptors)
			{
				IReadOnlyList<IDirectRouteFactory> actionRouteFactories = this.GetActionRouteFactories(httpActionDescriptor);
				if (actionRouteFactories != null && actionRouteFactories.Count > 0)
				{
					IReadOnlyCollection<RouteEntry> actionDirectRoutes = this.GetActionDirectRoutes(httpActionDescriptor, actionRouteFactories, constraintResolver);
					if (actionDirectRoutes != null)
					{
						list.AddRange(actionDirectRoutes);
					}
				}
				else
				{
					list2.Add(httpActionDescriptor);
				}
			}
			if (list2.Count > 0)
			{
				IReadOnlyList<IDirectRouteFactory> controllerRouteFactories = this.GetControllerRouteFactories(controllerDescriptor);
				if (controllerRouteFactories != null && controllerRouteFactories.Count > 0)
				{
					IReadOnlyCollection<RouteEntry> controllerDirectRoutes = this.GetControllerDirectRoutes(controllerDescriptor, list2, controllerRouteFactories, constraintResolver);
					if (controllerDirectRoutes != null)
					{
						list.AddRange(controllerDirectRoutes);
					}
				}
			}
			return list;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000A66C File Offset: 0x0000886C
		protected virtual IReadOnlyList<IDirectRouteFactory> GetControllerRouteFactories(HttpControllerDescriptor controllerDescriptor)
		{
			Collection<IDirectRouteFactory> customAttributes = controllerDescriptor.GetCustomAttributes<IDirectRouteFactory>(false);
			Collection<IHttpRouteInfoProvider> customAttributes2 = controllerDescriptor.GetCustomAttributes<IHttpRouteInfoProvider>(false);
			List<IDirectRouteFactory> list = new List<IDirectRouteFactory>();
			list.AddRange(customAttributes);
			foreach (IHttpRouteInfoProvider httpRouteInfoProvider in customAttributes2)
			{
				if (!(httpRouteInfoProvider is IDirectRouteFactory))
				{
					list.Add(new RouteInfoDirectRouteFactory(httpRouteInfoProvider));
				}
			}
			return list;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000A6E4 File Offset: 0x000088E4
		protected virtual IReadOnlyList<IDirectRouteFactory> GetActionRouteFactories(HttpActionDescriptor actionDescriptor)
		{
			ReflectedHttpActionDescriptor reflectedHttpActionDescriptor = actionDescriptor as ReflectedHttpActionDescriptor;
			if (reflectedHttpActionDescriptor != null && reflectedHttpActionDescriptor.MethodInfo != null && reflectedHttpActionDescriptor.MethodInfo.DeclaringType != actionDescriptor.ControllerDescriptor.ControllerType)
			{
				return null;
			}
			Collection<IDirectRouteFactory> customAttributes = actionDescriptor.GetCustomAttributes<IDirectRouteFactory>(false);
			Collection<IHttpRouteInfoProvider> customAttributes2 = actionDescriptor.GetCustomAttributes<IHttpRouteInfoProvider>(false);
			List<IDirectRouteFactory> list = new List<IDirectRouteFactory>();
			list.AddRange(customAttributes);
			foreach (IHttpRouteInfoProvider httpRouteInfoProvider in customAttributes2)
			{
				if (!(httpRouteInfoProvider is IDirectRouteFactory))
				{
					list.Add(new RouteInfoDirectRouteFactory(httpRouteInfoProvider));
				}
			}
			return list;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000A798 File Offset: 0x00008998
		protected virtual IReadOnlyList<RouteEntry> GetControllerDirectRoutes(HttpControllerDescriptor controllerDescriptor, IReadOnlyList<HttpActionDescriptor> actionDescriptors, IReadOnlyList<IDirectRouteFactory> factories, IInlineConstraintResolver constraintResolver)
		{
			return DefaultDirectRouteProvider.CreateRouteEntries(this.GetRoutePrefix(controllerDescriptor), factories, actionDescriptors, constraintResolver, false);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000A7AC File Offset: 0x000089AC
		protected virtual IReadOnlyList<RouteEntry> GetActionDirectRoutes(HttpActionDescriptor actionDescriptor, IReadOnlyList<IDirectRouteFactory> factories, IInlineConstraintResolver constraintResolver)
		{
			return DefaultDirectRouteProvider.CreateRouteEntries(this.GetRoutePrefix(actionDescriptor.ControllerDescriptor), factories, new HttpActionDescriptor[]
			{
				actionDescriptor
			}, constraintResolver, true);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000A7DC File Offset: 0x000089DC
		protected virtual string GetRoutePrefix(HttpControllerDescriptor controllerDescriptor)
		{
			Collection<IRoutePrefix> customAttributes = controllerDescriptor.GetCustomAttributes<IRoutePrefix>(false);
			if (customAttributes == null)
			{
				return null;
			}
			if (customAttributes.Count > 1)
			{
				string message = Error.Format(SRResources.RoutePrefix_CannotSupportMultiRoutePrefix, new object[]
				{
					controllerDescriptor.ControllerType.FullName
				});
				throw new InvalidOperationException(message);
			}
			if (customAttributes.Count == 1)
			{
				IRoutePrefix routePrefix = customAttributes[0];
				if (routePrefix != null)
				{
					string prefix = routePrefix.Prefix;
					if (prefix == null)
					{
						string message2 = Error.Format(SRResources.RoutePrefix_PrefixCannotBeNull, new object[]
						{
							controllerDescriptor.ControllerType.FullName
						});
						throw new InvalidOperationException(message2);
					}
					if (prefix.EndsWith("/", StringComparison.Ordinal))
					{
						throw Error.InvalidOperation(SRResources.AttributeRoutes_InvalidPrefix, new object[]
						{
							prefix,
							controllerDescriptor.ControllerName
						});
					}
					return prefix;
				}
			}
			return null;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000A8AC File Offset: 0x00008AAC
		private static IReadOnlyList<RouteEntry> CreateRouteEntries(string prefix, IReadOnlyCollection<IDirectRouteFactory> factories, IReadOnlyCollection<HttpActionDescriptor> actions, IInlineConstraintResolver constraintResolver, bool targetIsAction)
		{
			List<RouteEntry> list = new List<RouteEntry>();
			foreach (IDirectRouteFactory factory in factories)
			{
				RouteEntry item = DefaultDirectRouteProvider.CreateRouteEntry(prefix, factory, actions, constraintResolver, targetIsAction);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000A908 File Offset: 0x00008B08
		private static RouteEntry CreateRouteEntry(string prefix, IDirectRouteFactory factory, IReadOnlyCollection<HttpActionDescriptor> actions, IInlineConstraintResolver constraintResolver, bool targetIsAction)
		{
			DirectRouteFactoryContext context = new DirectRouteFactoryContext(prefix, actions, constraintResolver, targetIsAction);
			RouteEntry routeEntry = factory.CreateRoute(context);
			if (routeEntry == null)
			{
				throw Error.InvalidOperation(SRResources.TypeMethodMustNotReturnNull, new object[]
				{
					typeof(IDirectRouteFactory).Name,
					"CreateRoute"
				});
			}
			DirectRouteBuilder.ValidateRouteEntry(routeEntry);
			return routeEntry;
		}
	}
}
