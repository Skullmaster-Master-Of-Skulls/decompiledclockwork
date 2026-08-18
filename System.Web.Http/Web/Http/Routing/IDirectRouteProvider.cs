using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;

namespace System.Web.Http.Routing
{
	// Token: 0x02000010 RID: 16
	public interface IDirectRouteProvider
	{
		// Token: 0x0600007C RID: 124
		IReadOnlyList<RouteEntry> GetDirectRoutes(HttpControllerDescriptor controllerDescriptor, IReadOnlyList<HttpActionDescriptor> actionDescriptors, IInlineConstraintResolver constraintResolver);
	}
}
