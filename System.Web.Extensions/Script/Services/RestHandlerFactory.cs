using System;

namespace System.Web.Script.Services
{
	// Token: 0x020000F2 RID: 242
	internal class RestHandlerFactory : IHttpHandlerFactory
	{
		// Token: 0x06000CF5 RID: 3317 RVA: 0x0002BA82 File Offset: 0x00029C82
		public virtual IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (RestHandlerFactory.IsClientProxyRequest(context.Request.PathInfo))
			{
				return new RestClientProxyHandler();
			}
			return RestHandler.CreateHandler(context);
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x000032F4 File Offset: 0x000014F4
		public virtual void ReleaseHandler(IHttpHandler handler)
		{
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0002BAB0 File Offset: 0x00029CB0
		internal static bool IsRestRequest(HttpContext context)
		{
			return RestHandlerFactory.IsRestMethodCall(context.Request) || RestHandlerFactory.IsClientProxyRequest(context.Request.PathInfo);
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0002BAD1 File Offset: 0x00029CD1
		internal static bool IsRestMethodCall(HttpRequest request)
		{
			return !string.IsNullOrEmpty(request.PathInfo) && (request.ContentType.StartsWith("application/json;", StringComparison.OrdinalIgnoreCase) || string.Equals(request.ContentType, "application/json", StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0002BB08 File Offset: 0x00029D08
		internal static bool IsClientProxyDebugRequest(string pathInfo)
		{
			return string.Equals(pathInfo, "/jsdebug", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0002BB16 File Offset: 0x00029D16
		internal static bool IsClientProxyRequest(string pathInfo)
		{
			return string.Equals(pathInfo, "/js", StringComparison.OrdinalIgnoreCase) || RestHandlerFactory.IsClientProxyDebugRequest(pathInfo);
		}

		// Token: 0x04000394 RID: 916
		internal const string ClientProxyRequestPathInfo = "/js";

		// Token: 0x04000395 RID: 917
		internal const string ClientDebugProxyRequestPathInfo = "/jsdebug";
	}
}
