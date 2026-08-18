using System;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	// Token: 0x02000147 RID: 327
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class RequestContext
	{
		// Token: 0x06001319 RID: 4889 RVA: 0x000030B5 File Offset: 0x000012B5
		public RequestContext()
		{
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x000375E3 File Offset: 0x000357E3
		public RequestContext(HttpContextBase httpContext, RouteData routeData)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (routeData == null)
			{
				throw new ArgumentNullException("routeData");
			}
			this.HttpContext = httpContext;
			this.RouteData = routeData;
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x00037615 File Offset: 0x00035815
		// (set) Token: 0x0600131C RID: 4892 RVA: 0x0003761D File Offset: 0x0003581D
		public virtual HttpContextBase HttpContext { get; set; }

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x00037626 File Offset: 0x00035826
		// (set) Token: 0x0600131E RID: 4894 RVA: 0x0003762E File Offset: 0x0003582E
		public virtual RouteData RouteData { get; set; }
	}
}
