using System;

namespace System.Web.Mvc
{
	// Token: 0x020000F3 RID: 243
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public abstract class ActionFilterAttribute : FilterAttribute, IActionFilter, IResultFilter
	{
		// Token: 0x06000644 RID: 1604 RVA: 0x00011DF5 File Offset: 0x0000FFF5
		public virtual void OnActionExecuting(ActionExecutingContext filterContext)
		{
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00011DF7 File Offset: 0x0000FFF7
		public virtual void OnActionExecuted(ActionExecutedContext filterContext)
		{
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00011DF9 File Offset: 0x0000FFF9
		public virtual void OnResultExecuting(ResultExecutingContext filterContext)
		{
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00011DFB File Offset: 0x0000FFFB
		public virtual void OnResultExecuted(ResultExecutedContext filterContext)
		{
		}
	}
}
