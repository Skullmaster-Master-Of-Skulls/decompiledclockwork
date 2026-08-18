using System;
using System.Web.Routing;

namespace System.Web.Mvc
{
	// Token: 0x020000B9 RID: 185
	public interface IControllerActivator
	{
		// Token: 0x060004F7 RID: 1271
		IController Create(RequestContext requestContext, Type controllerType);
	}
}
