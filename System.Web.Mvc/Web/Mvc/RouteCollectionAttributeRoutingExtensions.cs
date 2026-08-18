using System;
using System.Web.Mvc.Routing;
using System.Web.Routing;

namespace System.Web.Mvc
{
	// Token: 0x0200009D RID: 157
	public static class RouteCollectionAttributeRoutingExtensions
	{
		// Token: 0x06000468 RID: 1128 RVA: 0x0000CF9E File Offset: 0x0000B19E
		public static void MapMvcAttributeRoutes(this RouteCollection routes)
		{
			if (routes == null)
			{
				throw new ArgumentNullException("routes");
			}
			AttributeRoutingMapper.MapAttributeRoutes(routes, new DefaultInlineConstraintResolver());
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000CFB9 File Offset: 0x0000B1B9
		public static void MapMvcAttributeRoutes(this RouteCollection routes, IInlineConstraintResolver constraintResolver)
		{
			if (routes == null)
			{
				throw new ArgumentNullException("routes");
			}
			if (constraintResolver == null)
			{
				throw new ArgumentNullException("constraintResolver");
			}
			AttributeRoutingMapper.MapAttributeRoutes(routes, constraintResolver);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000CFDE File Offset: 0x0000B1DE
		public static void MapMvcAttributeRoutes(this RouteCollection routes, IDirectRouteProvider directRouteProvider)
		{
			if (routes == null)
			{
				throw new ArgumentNullException("routes");
			}
			if (directRouteProvider == null)
			{
				throw new ArgumentNullException("directRouteProvider");
			}
			AttributeRoutingMapper.MapAttributeRoutes(routes, new DefaultInlineConstraintResolver(), directRouteProvider);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000D008 File Offset: 0x0000B208
		public static void MapMvcAttributeRoutes(this RouteCollection routes, IInlineConstraintResolver constraintResolver, IDirectRouteProvider directRouteProvider)
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
			AttributeRoutingMapper.MapAttributeRoutes(routes, constraintResolver, directRouteProvider);
		}
	}
}
