using System;

namespace System.Web.Mvc.Routing
{
	// Token: 0x0200000E RID: 14
	internal class RouteInfoDirectRouteFactory : IDirectRouteFactory
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00003144 File Offset: 0x00001344
		public RouteInfoDirectRouteFactory(IRouteInfoProvider infoProvider)
		{
			if (infoProvider == null)
			{
				throw new ArgumentNullException("infoProvider");
			}
			this._infoProvider = infoProvider;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003164 File Offset: 0x00001364
		public RouteEntry CreateRoute(DirectRouteFactoryContext context)
		{
			IDirectRouteBuilder directRouteBuilder = context.CreateBuilder(this._infoProvider.Template);
			directRouteBuilder.Name = this._infoProvider.Name;
			return directRouteBuilder.Build();
		}

		// Token: 0x0400001C RID: 28
		private readonly IRouteInfoProvider _infoProvider;
	}
}
