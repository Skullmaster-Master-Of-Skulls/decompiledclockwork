using System;
using System.Collections.Generic;

namespace System.Web.Mvc.Routing
{
	// Token: 0x0200003D RID: 61
	public interface IDirectRouteProvider
	{
		// Token: 0x06000127 RID: 295
		IReadOnlyList<RouteEntry> GetDirectRoutes(ControllerDescriptor controllerDescriptor, IReadOnlyList<ActionDescriptor> actionDescriptors, IInlineConstraintResolver constraintResolver);
	}
}
