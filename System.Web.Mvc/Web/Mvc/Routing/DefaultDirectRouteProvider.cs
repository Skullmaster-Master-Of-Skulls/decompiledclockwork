using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc.Routing
{
	// Token: 0x0200003E RID: 62
	public class DefaultDirectRouteProvider : IDirectRouteProvider
	{
		// Token: 0x06000128 RID: 296 RVA: 0x000056B0 File Offset: 0x000038B0
		public virtual IReadOnlyList<RouteEntry> GetDirectRoutes(ControllerDescriptor controllerDescriptor, IReadOnlyList<ActionDescriptor> actionDescriptors, IInlineConstraintResolver constraintResolver)
		{
			List<RouteEntry> list = new List<RouteEntry>();
			List<ActionDescriptor> list2 = new List<ActionDescriptor>();
			foreach (ActionDescriptor actionDescriptor in actionDescriptors)
			{
				IReadOnlyList<IDirectRouteFactory> actionRouteFactories = this.GetActionRouteFactories(actionDescriptor);
				if (actionRouteFactories != null && actionRouteFactories.Count > 0)
				{
					IReadOnlyCollection<RouteEntry> actionDirectRoutes = this.GetActionDirectRoutes(actionDescriptor, actionRouteFactories, constraintResolver);
					if (actionDirectRoutes != null)
					{
						list.AddRange(actionDirectRoutes);
					}
				}
				else
				{
					list2.Add(actionDescriptor);
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

		// Token: 0x06000129 RID: 297 RVA: 0x00005770 File Offset: 0x00003970
		protected virtual IReadOnlyList<IDirectRouteFactory> GetControllerRouteFactories(ControllerDescriptor controllerDescriptor)
		{
			object[] customAttributes = controllerDescriptor.GetCustomAttributes(false);
			IEnumerable<IDirectRouteFactory> collection = customAttributes.OfType<IDirectRouteFactory>();
			IEnumerable<IRouteInfoProvider> enumerable = customAttributes.OfType<IRouteInfoProvider>();
			List<IDirectRouteFactory> list = new List<IDirectRouteFactory>();
			list.AddRange(collection);
			foreach (IRouteInfoProvider routeInfoProvider in enumerable)
			{
				if (!(routeInfoProvider is IDirectRouteFactory))
				{
					list.Add(new RouteInfoDirectRouteFactory(routeInfoProvider));
				}
			}
			return list;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000057F4 File Offset: 0x000039F4
		protected virtual IReadOnlyList<IDirectRouteFactory> GetActionRouteFactories(ActionDescriptor actionDescriptor)
		{
			IMethodInfoActionDescriptor methodInfoActionDescriptor = actionDescriptor as IMethodInfoActionDescriptor;
			if (methodInfoActionDescriptor != null && methodInfoActionDescriptor.MethodInfo != null && actionDescriptor.ControllerDescriptor != null && methodInfoActionDescriptor.MethodInfo.DeclaringType != actionDescriptor.ControllerDescriptor.ControllerType)
			{
				return null;
			}
			object[] customAttributes = actionDescriptor.GetCustomAttributes(false);
			IEnumerable<IDirectRouteFactory> collection = customAttributes.OfType<IDirectRouteFactory>();
			IEnumerable<IRouteInfoProvider> enumerable = customAttributes.OfType<IRouteInfoProvider>();
			List<IDirectRouteFactory> list = new List<IDirectRouteFactory>();
			list.AddRange(collection);
			foreach (IRouteInfoProvider routeInfoProvider in enumerable)
			{
				if (!(routeInfoProvider is IDirectRouteFactory))
				{
					list.Add(new RouteInfoDirectRouteFactory(routeInfoProvider));
				}
			}
			return list;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000058B8 File Offset: 0x00003AB8
		protected virtual IReadOnlyList<RouteEntry> GetControllerDirectRoutes(ControllerDescriptor controllerDescriptor, IReadOnlyList<ActionDescriptor> actionDescriptors, IReadOnlyList<IDirectRouteFactory> factories, IInlineConstraintResolver constraintResolver)
		{
			return DefaultDirectRouteProvider.CreateRouteEntries(this.GetAreaPrefix(controllerDescriptor), this.GetRoutePrefix(controllerDescriptor), factories, actionDescriptors, constraintResolver, false);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000058D4 File Offset: 0x00003AD4
		protected virtual IReadOnlyList<RouteEntry> GetActionDirectRoutes(ActionDescriptor actionDescriptor, IReadOnlyList<IDirectRouteFactory> factories, IInlineConstraintResolver constraintResolver)
		{
			return DefaultDirectRouteProvider.CreateRouteEntries(this.GetAreaPrefix(actionDescriptor.ControllerDescriptor), this.GetRoutePrefix(actionDescriptor.ControllerDescriptor), factories, new ActionDescriptor[]
			{
				actionDescriptor
			}, constraintResolver, true);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005910 File Offset: 0x00003B10
		protected virtual string GetRoutePrefix(ControllerDescriptor controllerDescriptor)
		{
			IRoutePrefix[] array = controllerDescriptor.GetCustomAttributes(false).OfType<IRoutePrefix>().ToArray<IRoutePrefix>();
			if (array == null)
			{
				return null;
			}
			if (array.Length > 1)
			{
				string message = Error.Format(MvcResources.RoutePrefix_CannotSupportMultiRoutePrefix, new object[]
				{
					controllerDescriptor.ControllerType.FullName
				});
				throw new InvalidOperationException(message);
			}
			if (array.Length == 1)
			{
				IRoutePrefix routePrefix = array[0];
				if (routePrefix != null)
				{
					string prefix = routePrefix.Prefix;
					if (prefix == null)
					{
						string message2 = Error.Format(MvcResources.RoutePrefix_PrefixCannotBeNull, new object[]
						{
							controllerDescriptor.ControllerType.FullName
						});
						throw new InvalidOperationException(message2);
					}
					if (prefix.StartsWith("/", StringComparison.Ordinal) || prefix.EndsWith("/", StringComparison.Ordinal))
					{
						string message3 = Error.Format(MvcResources.RoutePrefix_CannotStartOrEnd_WithForwardSlash, new object[]
						{
							prefix,
							controllerDescriptor.ControllerName
						});
						throw new InvalidOperationException(message3);
					}
					return prefix;
				}
			}
			return null;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000059FC File Offset: 0x00003BFC
		protected virtual string GetAreaPrefix(ControllerDescriptor controllerDescriptor)
		{
			RouteAreaAttribute areaFrom = controllerDescriptor.GetAreaFrom();
			string areaName = controllerDescriptor.GetAreaName(areaFrom);
			string text = (areaFrom != null) ? (areaFrom.AreaPrefix ?? areaFrom.AreaName) : null;
			DefaultDirectRouteProvider.ValidateAreaPrefixTemplate(text, areaName, controllerDescriptor);
			return text;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005A38 File Offset: 0x00003C38
		private static IReadOnlyList<RouteEntry> CreateRouteEntries(string areaPrefix, string controllerPrefix, IReadOnlyCollection<IDirectRouteFactory> factories, IReadOnlyCollection<ActionDescriptor> actions, IInlineConstraintResolver constraintResolver, bool targetIsAction)
		{
			List<RouteEntry> list = new List<RouteEntry>();
			foreach (IDirectRouteFactory factory in factories)
			{
				RouteEntry item = DefaultDirectRouteProvider.CreateRouteEntry(areaPrefix, controllerPrefix, factory, actions, constraintResolver, targetIsAction);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005A98 File Offset: 0x00003C98
		internal static RouteEntry CreateRouteEntry(string areaPrefix, string controllerPrefix, IDirectRouteFactory factory, IReadOnlyCollection<ActionDescriptor> actions, IInlineConstraintResolver constraintResolver, bool targetIsAction)
		{
			DirectRouteFactoryContext context = new DirectRouteFactoryContext(areaPrefix, controllerPrefix, actions, constraintResolver, targetIsAction);
			RouteEntry routeEntry = factory.CreateRoute(context);
			if (routeEntry == null)
			{
				throw Error.InvalidOperation(MvcResources.TypeMethodMustNotReturnNull, new object[]
				{
					typeof(IDirectRouteFactory).Name,
					"CreateRoute"
				});
			}
			DirectRouteBuilder.ValidateRouteEntry(routeEntry);
			return routeEntry;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005AF4 File Offset: 0x00003CF4
		private static void ValidateAreaPrefixTemplate(string areaPrefix, string areaName, ControllerDescriptor controllerDescriptor)
		{
			if (areaPrefix != null && areaPrefix.EndsWith("/", StringComparison.Ordinal))
			{
				string message = Error.Format(MvcResources.RouteAreaPrefix_CannotEnd_WithForwardSlash, new object[]
				{
					areaPrefix,
					areaName,
					controllerDescriptor.ControllerName
				});
				throw new InvalidOperationException(message);
			}
		}
	}
}
