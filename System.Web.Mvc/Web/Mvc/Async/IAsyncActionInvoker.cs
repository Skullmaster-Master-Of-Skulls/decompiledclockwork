using System;

namespace System.Web.Mvc.Async
{
	// Token: 0x020000EB RID: 235
	public interface IAsyncActionInvoker : IActionInvoker
	{
		// Token: 0x06000619 RID: 1561
		IAsyncResult BeginInvokeAction(ControllerContext controllerContext, string actionName, AsyncCallback callback, object state);

		// Token: 0x0600061A RID: 1562
		bool EndInvokeAction(IAsyncResult asyncResult);
	}
}
