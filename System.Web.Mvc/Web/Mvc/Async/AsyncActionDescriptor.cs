using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc.Async
{
	// Token: 0x0200005B RID: 91
	public abstract class AsyncActionDescriptor : ActionDescriptor
	{
		// Token: 0x06000253 RID: 595
		public abstract IAsyncResult BeginExecute(ControllerContext controllerContext, IDictionary<string, object> parameters, AsyncCallback callback, object state);

		// Token: 0x06000254 RID: 596
		public abstract object EndExecute(IAsyncResult asyncResult);

		// Token: 0x06000255 RID: 597 RVA: 0x00008194 File Offset: 0x00006394
		public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters)
		{
			string message = string.Format(CultureInfo.CurrentCulture, MvcResources.AsyncActionDescriptor_CannotExecuteSynchronously, new object[]
			{
				this.ActionName
			});
			throw new InvalidOperationException(message);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000081C8 File Offset: 0x000063C8
		internal static AsyncManager GetAsyncManager(ControllerBase controller)
		{
			IAsyncManagerContainer asyncManagerContainer = controller as IAsyncManagerContainer;
			if (asyncManagerContainer == null)
			{
				throw Error.AsyncCommon_ControllerMustImplementIAsyncManagerContainer(controller.GetType());
			}
			return asyncManagerContainer.AsyncManager;
		}
	}
}
