using System;
using System.Web.Mvc.Async;

namespace System.Web.Mvc
{
	// Token: 0x020000F4 RID: 244
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public class AsyncTimeoutAttribute : ActionFilterAttribute
	{
		// Token: 0x06000649 RID: 1609 RVA: 0x00011E05 File Offset: 0x00010005
		public AsyncTimeoutAttribute(int duration)
		{
			if (duration < -1)
			{
				throw Error.AsyncCommon_InvalidTimeout("duration");
			}
			this.Duration = duration;
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00011E23 File Offset: 0x00010023
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x00011E2B File Offset: 0x0001002B
		public int Duration { get; private set; }

		// Token: 0x0600064C RID: 1612 RVA: 0x00011E34 File Offset: 0x00010034
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (filterContext == null)
			{
				throw new ArgumentNullException("filterContext");
			}
			IAsyncManagerContainer asyncManagerContainer = filterContext.Controller as IAsyncManagerContainer;
			if (asyncManagerContainer == null)
			{
				throw Error.AsyncCommon_ControllerMustImplementIAsyncManagerContainer(filterContext.Controller.GetType());
			}
			asyncManagerContainer.AsyncManager.Timeout = this.Duration;
			base.OnActionExecuting(filterContext);
		}
	}
}
