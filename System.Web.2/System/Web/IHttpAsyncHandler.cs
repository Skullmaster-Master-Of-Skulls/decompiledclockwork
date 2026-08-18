using System;

namespace System.Web
{
	// Token: 0x020000CD RID: 205
	public interface IHttpAsyncHandler : IHttpHandler
	{
		// Token: 0x06000DDE RID: 3550
		IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData);

		// Token: 0x06000DDF RID: 3551
		void EndProcessRequest(IAsyncResult result);
	}
}
