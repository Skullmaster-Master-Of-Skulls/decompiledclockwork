using System;
using System.Web.Routing;

namespace System.Web.Http.WebHost
{
	// Token: 0x02000027 RID: 39
	public class HttpControllerRouteHandler : IRouteHandler
	{
		// Token: 0x0600011A RID: 282 RVA: 0x00006B75 File Offset: 0x00004D75
		protected HttpControllerRouteHandler()
		{
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00006B7D File Offset: 0x00004D7D
		public static HttpControllerRouteHandler Instance
		{
			get
			{
				return HttpControllerRouteHandler._instance.Value;
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006B89 File Offset: 0x00004D89
		IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
		{
			return this.GetHttpHandler(requestContext);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006B92 File Offset: 0x00004D92
		protected virtual IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			return new HttpControllerHandler(requestContext.RouteData);
		}

		// Token: 0x0400004C RID: 76
		private static readonly Lazy<HttpControllerRouteHandler> _instance = new Lazy<HttpControllerRouteHandler>(() => new HttpControllerRouteHandler(), true);
	}
}
