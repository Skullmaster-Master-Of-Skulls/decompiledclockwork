using System;

namespace System.Web
{
	// Token: 0x020000CF RID: 207
	public interface IHttpHandlerFactory
	{
		// Token: 0x06000DE2 RID: 3554
		IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated);

		// Token: 0x06000DE3 RID: 3555
		void ReleaseHandler(IHttpHandler handler);
	}
}
