using System;

namespace System.Web.Mvc
{
	// Token: 0x020000F1 RID: 241
	public interface IActionFilter
	{
		// Token: 0x06000640 RID: 1600
		void OnActionExecuting(ActionExecutingContext filterContext);

		// Token: 0x06000641 RID: 1601
		void OnActionExecuted(ActionExecutedContext filterContext);
	}
}
