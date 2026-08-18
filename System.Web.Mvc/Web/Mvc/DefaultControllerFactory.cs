using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc.Properties;
using System.Web.Mvc.Routing;
using System.Web.Routing;
using System.Web.SessionState;

namespace System.Web.Mvc
{
	// Token: 0x020001E3 RID: 483
	public class DefaultControllerFactory : IControllerFactory
	{
		// Token: 0x06000E81 RID: 3713 RVA: 0x00026353 File Offset: 0x00024553
		public DefaultControllerFactory() : this(null, null, null)
		{
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0002635E File Offset: 0x0002455E
		public DefaultControllerFactory(IControllerActivator controllerActivator) : this(controllerActivator, null, null)
		{
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0002636C File Offset: 0x0002456C
		internal DefaultControllerFactory(IControllerActivator controllerActivator, IResolver<IControllerActivator> activatorResolver, IDependencyResolver dependencyResolver)
		{
			if (controllerActivator != null)
			{
				this._controllerActivator = controllerActivator;
				return;
			}
			IResolver<IControllerActivator> activatorResolver2 = activatorResolver;
			if (activatorResolver == null)
			{
				activatorResolver2 = new SingleServiceResolver<IControllerActivator>(() => null, new DefaultControllerFactory.DefaultControllerActivator(dependencyResolver), "DefaultControllerFactory constructor");
			}
			this._activatorResolver = activatorResolver2;
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x000263C2 File Offset: 0x000245C2
		private IControllerActivator ControllerActivator
		{
			get
			{
				if (this._controllerActivator != null)
				{
					return this._controllerActivator;
				}
				this._controllerActivator = this._activatorResolver.Current;
				return this._controllerActivator;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x000263EA File Offset: 0x000245EA
		// (set) Token: 0x06000E86 RID: 3718 RVA: 0x00026405 File Offset: 0x00024605
		internal IBuildManager BuildManager
		{
			get
			{
				if (this._buildManager == null)
				{
					this._buildManager = new BuildManagerWrapper();
				}
				return this._buildManager;
			}
			set
			{
				this._buildManager = value;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x0002640E File Offset: 0x0002460E
		// (set) Token: 0x06000E88 RID: 3720 RVA: 0x0002641F File Offset: 0x0002461F
		internal ControllerBuilder ControllerBuilder
		{
			get
			{
				return this._controllerBuilder ?? ControllerBuilder.Current;
			}
			set
			{
				this._controllerBuilder = value;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x00026428 File Offset: 0x00024628
		// (set) Token: 0x06000E8A RID: 3722 RVA: 0x00026439 File Offset: 0x00024639
		internal ControllerTypeCache ControllerTypeCache
		{
			get
			{
				return this._instanceControllerTypeCache ?? DefaultControllerFactory._staticControllerTypeCache;
			}
			set
			{
				this._instanceControllerTypeCache = value;
			}
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00026444 File Offset: 0x00024644
		internal static InvalidOperationException CreateAmbiguousControllerException(RouteBase route, string controllerName, ICollection<Type> matchingTypes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Type type in matchingTypes)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append(type.FullName);
			}
			Route route2 = route as Route;
			string message;
			if (route2 != null)
			{
				message = string.Format(CultureInfo.CurrentCulture, MvcResources.DefaultControllerFactory_ControllerNameAmbiguous_WithRouteUrl, new object[]
				{
					controllerName,
					route2.Url,
					stringBuilder,
					Environment.NewLine
				});
			}
			else
			{
				message = string.Format(CultureInfo.CurrentCulture, MvcResources.DefaultControllerFactory_ControllerNameAmbiguous_WithoutRouteUrl, new object[]
				{
					controllerName,
					stringBuilder,
					Environment.NewLine
				});
			}
			return new InvalidOperationException(message);
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0002651C File Offset: 0x0002471C
		private static InvalidOperationException CreateDirectRouteAmbiguousControllerException(ICollection<Type> matchingTypes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Type type in matchingTypes)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append(type.FullName);
			}
			string message = string.Format(CultureInfo.CurrentCulture, MvcResources.DefaultControllerFactory_DirectRouteAmbiguous, new object[]
			{
				stringBuilder,
				Environment.NewLine
			});
			return new InvalidOperationException(message);
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x000265A8 File Offset: 0x000247A8
		public virtual IController CreateController(RequestContext requestContext, string controllerName)
		{
			if (requestContext == null)
			{
				throw new ArgumentNullException("requestContext");
			}
			if (string.IsNullOrEmpty(controllerName) && !requestContext.RouteData.HasDirectRouteMatch())
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "controllerName");
			}
			Type controllerType = this.GetControllerType(requestContext, controllerName);
			return this.GetControllerInstance(requestContext, controllerType);
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x000265FC File Offset: 0x000247FC
		protected internal virtual IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			if (controllerType == null)
			{
				throw new HttpException(404, string.Format(CultureInfo.CurrentCulture, MvcResources.DefaultControllerFactory_NoControllerFound, new object[]
				{
					requestContext.HttpContext.Request.Path
				}));
			}
			if (!typeof(IController).IsAssignableFrom(controllerType))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, MvcResources.DefaultControllerFactory_TypeDoesNotSubclassControllerBase, new object[]
				{
					controllerType
				}), "controllerType");
			}
			return this.ControllerActivator.Create(requestContext, controllerType);
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x000266C0 File Offset: 0x000248C0
		protected internal virtual SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, Type controllerType)
		{
			if (controllerType == null)
			{
				return SessionStateBehavior.Default;
			}
			return DefaultControllerFactory._sessionStateCache.GetOrAdd(controllerType, delegate(Type type)
			{
				SessionStateAttribute sessionStateAttribute = type.GetCustomAttributes(typeof(SessionStateAttribute), true).OfType<SessionStateAttribute>().FirstOrDefault<SessionStateAttribute>();
				if (sessionStateAttribute == null)
				{
					return SessionStateBehavior.Default;
				}
				return sessionStateAttribute.Behavior;
			});
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x000266F8 File Offset: 0x000248F8
		protected internal virtual Type GetControllerType(RequestContext requestContext, string controllerName)
		{
			if (requestContext == null)
			{
				throw new ArgumentNullException("requestContext");
			}
			if (string.IsNullOrEmpty(controllerName) && (requestContext.RouteData == null || !requestContext.RouteData.HasDirectRouteMatch()))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "controllerName");
			}
			RouteData routeData = requestContext.RouteData;
			if (routeData != null && routeData.HasDirectRouteMatch())
			{
				return DefaultControllerFactory.GetControllerTypeFromDirectRoute(routeData);
			}
			object obj;
			if (routeData.DataTokens.TryGetValue("Namespaces", out obj))
			{
				IEnumerable<string> enumerable = obj as IEnumerable<string>;
				if (enumerable != null && enumerable.Any<string>())
				{
					HashSet<string> namespaces = new HashSet<string>(enumerable, StringComparer.OrdinalIgnoreCase);
					Type controllerTypeWithinNamespaces = this.GetControllerTypeWithinNamespaces(routeData.Route, controllerName, namespaces);
					if (controllerTypeWithinNamespaces != null || false.Equals(routeData.DataTokens["UseNamespaceFallback"]))
					{
						return controllerTypeWithinNamespaces;
					}
				}
			}
			if (this.ControllerBuilder.DefaultNamespaces.Count > 0)
			{
				HashSet<string> namespaces2 = new HashSet<string>(this.ControllerBuilder.DefaultNamespaces, StringComparer.OrdinalIgnoreCase);
				Type controllerTypeWithinNamespaces = this.GetControllerTypeWithinNamespaces(routeData.Route, controllerName, namespaces2);
				if (controllerTypeWithinNamespaces != null)
				{
					return controllerTypeWithinNamespaces;
				}
			}
			return this.GetControllerTypeWithinNamespaces(routeData.Route, controllerName, null);
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x00026818 File Offset: 0x00024A18
		private static Type GetControllerTypeFromDirectRoute(RouteData routeData)
		{
			IEnumerable<RouteData> directRouteMatches = routeData.GetDirectRouteMatches();
			List<Type> list = new List<Type>();
			foreach (RouteData routeData2 in directRouteMatches)
			{
				if (routeData2 != null)
				{
					Type targetControllerType = routeData2.GetTargetControllerType();
					if (targetControllerType == null)
					{
						throw new InvalidOperationException(MvcResources.DirectRoute_MissingControllerType);
					}
					if (!list.Contains(targetControllerType))
					{
						list.Add(targetControllerType);
					}
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			throw DefaultControllerFactory.CreateDirectRouteAmbiguousControllerException(list);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x000268BC File Offset: 0x00024ABC
		private Type GetControllerTypeWithinNamespaces(RouteBase route, string controllerName, HashSet<string> namespaces)
		{
			this.ControllerTypeCache.EnsureInitialized(this.BuildManager);
			ICollection<Type> controllerTypes = this.ControllerTypeCache.GetControllerTypes(controllerName, namespaces);
			switch (controllerTypes.Count)
			{
			case 0:
				return null;
			case 1:
				return controllerTypes.First<Type>();
			default:
				throw DefaultControllerFactory.CreateAmbiguousControllerException(route, controllerName, controllerTypes);
			}
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x00026910 File Offset: 0x00024B10
		public virtual void ReleaseController(IController controller)
		{
			IDisposable disposable = controller as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0002692D File Offset: 0x00024B2D
		internal IReadOnlyList<Type> GetControllerTypes()
		{
			this.ControllerTypeCache.EnsureInitialized(this.BuildManager);
			return this.ControllerTypeCache.GetControllerTypes();
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0002694C File Offset: 0x00024B4C
		SessionStateBehavior IControllerFactory.GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
		{
			if (requestContext == null)
			{
				throw new ArgumentNullException("requestContext");
			}
			if (string.IsNullOrEmpty(controllerName))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "controllerName");
			}
			Type controllerType = this.GetControllerType(requestContext, controllerName);
			return this.GetControllerSessionBehavior(requestContext, controllerType);
		}

		// Token: 0x040003D0 RID: 976
		private static readonly ConcurrentDictionary<Type, SessionStateBehavior> _sessionStateCache = new ConcurrentDictionary<Type, SessionStateBehavior>();

		// Token: 0x040003D1 RID: 977
		private static ControllerTypeCache _staticControllerTypeCache = new ControllerTypeCache();

		// Token: 0x040003D2 RID: 978
		private IBuildManager _buildManager;

		// Token: 0x040003D3 RID: 979
		private IResolver<IControllerActivator> _activatorResolver;

		// Token: 0x040003D4 RID: 980
		private IControllerActivator _controllerActivator;

		// Token: 0x040003D5 RID: 981
		private ControllerBuilder _controllerBuilder;

		// Token: 0x040003D6 RID: 982
		private ControllerTypeCache _instanceControllerTypeCache;

		// Token: 0x020001E4 RID: 484
		private class DefaultControllerActivator : IControllerActivator
		{
			// Token: 0x06000E99 RID: 3737 RVA: 0x000269A6 File Offset: 0x00024BA6
			public DefaultControllerActivator() : this(null)
			{
			}

			// Token: 0x06000E9A RID: 3738 RVA: 0x000269C8 File Offset: 0x00024BC8
			public DefaultControllerActivator(IDependencyResolver resolver)
			{
				if (resolver == null)
				{
					this._resolverThunk = (() => DependencyResolver.Current);
					return;
				}
				this._resolverThunk = (() => resolver);
			}

			// Token: 0x06000E9B RID: 3739 RVA: 0x00026A30 File Offset: 0x00024C30
			public IController Create(RequestContext requestContext, Type controllerType)
			{
				IController result;
				try
				{
					result = (IController)(this._resolverThunk().GetService(controllerType) ?? Activator.CreateInstance(controllerType));
				}
				catch (Exception innerException)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.DefaultControllerFactory_ErrorCreatingController, new object[]
					{
						controllerType
					}), innerException);
				}
				return result;
			}

			// Token: 0x040003D9 RID: 985
			private Func<IDependencyResolver> _resolverThunk;
		}
	}
}
