using System;

namespace System.Web.Http.Routing
{
	// Token: 0x02000014 RID: 20
	internal class RouteInfoDirectRouteFactory : IDirectRouteFactory
	{
		// Token: 0x0600008B RID: 139 RVA: 0x000037D8 File Offset: 0x000019D8
		public RouteInfoDirectRouteFactory(IHttpRouteInfoProvider infoProvider)
		{
			if (infoProvider == null)
			{
				throw new ArgumentNullException("infoProvider");
			}
			this._infoProvider = infoProvider;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000037F8 File Offset: 0x000019F8
		public RouteEntry CreateRoute(DirectRouteFactoryContext context)
		{
			IDirectRouteBuilder directRouteBuilder = context.CreateBuilder(this._infoProvider.Template);
			directRouteBuilder.Name = this._infoProvider.Name;
			directRouteBuilder.Order = this._infoProvider.Order;
			return directRouteBuilder.Build();
		}

		// Token: 0x04000023 RID: 35
		private readonly IHttpRouteInfoProvider _infoProvider;
	}
}
