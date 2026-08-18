using System;

namespace System.Web.Script.Services
{
	// Token: 0x020000EF RID: 239
	internal class RestClientProxyHandler : IHttpHandler
	{
		// Token: 0x06000CE3 RID: 3299 RVA: 0x0002B478 File Offset: 0x00029678
		public void ProcessRequest(HttpContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			string clientProxyScript = WebServiceClientProxyGenerator.GetClientProxyScript(context);
			if (clientProxyScript != null)
			{
				context.Response.ContentType = "application/x-javascript";
				context.Response.Write(clientProxyScript);
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x0001359B File Offset: 0x0001179B
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}
