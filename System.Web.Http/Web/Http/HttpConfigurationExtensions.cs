using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Routing;

namespace System.Web.Http
{
	// Token: 0x020000B6 RID: 182
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpConfigurationExtensions
	{
		// Token: 0x06000414 RID: 1044 RVA: 0x0000CC20 File Offset: 0x0000AE20
		public static void BindParameter(this HttpConfiguration configuration, Type type, IModelBinder binder)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (binder == null)
			{
				throw Error.ArgumentNull("binder");
			}
			configuration.Services.Insert(typeof(ModelBinderProvider), 0, new SimpleModelBinderProvider(type, binder));
			configuration.ParameterBindingRules.Insert(0, type, (HttpParameterDescriptor param) => param.BindWithModelBinding(binder));
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000CCAA File Offset: 0x0000AEAA
		public static void MapHttpAttributeRoutes(this HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			AttributeRoutingMapper.MapAttributeRoutes(configuration, new DefaultInlineConstraintResolver(), new DefaultDirectRouteProvider());
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000CCCA File Offset: 0x0000AECA
		public static void MapHttpAttributeRoutes(this HttpConfiguration configuration, IInlineConstraintResolver constraintResolver)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			if (constraintResolver == null)
			{
				throw new ArgumentNullException("constraintResolver");
			}
			AttributeRoutingMapper.MapAttributeRoutes(configuration, constraintResolver, new DefaultDirectRouteProvider());
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000CCF4 File Offset: 0x0000AEF4
		public static void MapHttpAttributeRoutes(this HttpConfiguration configuration, IDirectRouteProvider directRouteProvider)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			if (directRouteProvider == null)
			{
				throw new ArgumentNullException("directRouteProvider");
			}
			AttributeRoutingMapper.MapAttributeRoutes(configuration, new DefaultInlineConstraintResolver(), directRouteProvider);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000CD1E File Offset: 0x0000AF1E
		public static void MapHttpAttributeRoutes(this HttpConfiguration configuration, IInlineConstraintResolver constraintResolver, IDirectRouteProvider directRouteProvider)
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
			AttributeRoutingMapper.MapAttributeRoutes(configuration, constraintResolver, directRouteProvider);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000CD54 File Offset: 0x0000AF54
		internal static IReadOnlyCollection<IHttpRoute> GetAttributeRoutes(this HttpConfiguration configuration)
		{
			configuration.EnsureInitialized();
			HttpRouteCollection routes = configuration.Routes;
			foreach (IHttpRoute httpRoute in routes)
			{
				IReadOnlyCollection<IHttpRoute> readOnlyCollection = httpRoute as IReadOnlyCollection<IHttpRoute>;
				if (readOnlyCollection != null)
				{
					return readOnlyCollection;
				}
			}
			return null;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000CDB8 File Offset: 0x0000AFB8
		public static void SuppressHostPrincipal(this HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			configuration.MessageHandlers.Insert(0, new SuppressHostPrincipalMessageHandler());
		}
	}
}
