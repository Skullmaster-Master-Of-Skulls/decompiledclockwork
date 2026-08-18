using System;

namespace System.Web.Mvc
{
	// Token: 0x020000E9 RID: 233
	public interface IActionInvoker
	{
		// Token: 0x060005FA RID: 1530
		bool InvokeAction(ControllerContext controllerContext, string actionName);
	}
}
