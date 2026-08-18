using System;

namespace System.Web
{
	// Token: 0x020000D0 RID: 208
	internal interface IHttpHandlerFactory2 : IHttpHandlerFactory
	{
		// Token: 0x06000DE4 RID: 3556
		IHttpHandler GetHandler(HttpContext context, string requestType, VirtualPath virtualPath, string physicalPath);
	}
}
