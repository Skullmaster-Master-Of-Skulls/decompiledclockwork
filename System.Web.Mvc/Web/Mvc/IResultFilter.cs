using System;

namespace System.Web.Mvc
{
	// Token: 0x020000F2 RID: 242
	public interface IResultFilter
	{
		// Token: 0x06000642 RID: 1602
		void OnResultExecuting(ResultExecutingContext filterContext);

		// Token: 0x06000643 RID: 1603
		void OnResultExecuted(ResultExecutedContext filterContext);
	}
}
