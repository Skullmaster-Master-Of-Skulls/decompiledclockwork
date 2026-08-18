using System;
using System.Web.Routing;

namespace System.Web.Mvc.Async
{
	// Token: 0x02000101 RID: 257
	public interface IAsyncController : IController
	{
		// Token: 0x0600068E RID: 1678
		IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state);

		// Token: 0x0600068F RID: 1679
		void EndExecute(IAsyncResult asyncResult);
	}
}
