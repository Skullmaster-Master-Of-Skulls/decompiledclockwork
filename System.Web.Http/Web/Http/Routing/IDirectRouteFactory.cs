using System;

namespace System.Web.Http.Routing
{
	// Token: 0x0200000F RID: 15
	public interface IDirectRouteFactory
	{
		// Token: 0x0600007B RID: 123
		RouteEntry CreateRoute(DirectRouteFactoryContext context);
	}
}
