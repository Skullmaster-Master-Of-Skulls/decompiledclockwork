using System;
using System.Web.Routing;
using System.Web.SessionState;

namespace System.Web.Mvc
{
	// Token: 0x020001E2 RID: 482
	public interface IControllerFactory
	{
		// Token: 0x06000E7E RID: 3710
		IController CreateController(RequestContext requestContext, string controllerName);

		// Token: 0x06000E7F RID: 3711
		SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName);

		// Token: 0x06000E80 RID: 3712
		void ReleaseController(IController controller);
	}
}
