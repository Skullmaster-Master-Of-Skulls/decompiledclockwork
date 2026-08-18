using System;
using System.Collections.Generic;
using System.Net.Http;

namespace System.Web.Http.Routing
{
	// Token: 0x02000083 RID: 131
	internal class LinkGenerationRoute : IHttpRoute
	{
		// Token: 0x06000367 RID: 871 RVA: 0x0000AB11 File Offset: 0x00008D11
		public LinkGenerationRoute(IHttpRoute innerRoute)
		{
			if (innerRoute == null)
			{
				throw new ArgumentNullException("innerRoute");
			}
			this._innerRoute = innerRoute;
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000AB2E File Offset: 0x00008D2E
		public string RouteTemplate
		{
			get
			{
				return this._innerRoute.RouteTemplate;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000AB3B File Offset: 0x00008D3B
		public IDictionary<string, object> Defaults
		{
			get
			{
				return this._innerRoute.Defaults;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000AB48 File Offset: 0x00008D48
		public IDictionary<string, object> Constraints
		{
			get
			{
				return this._innerRoute.Constraints;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000AB55 File Offset: 0x00008D55
		public IDictionary<string, object> DataTokens
		{
			get
			{
				return this._innerRoute.DataTokens;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000AB62 File Offset: 0x00008D62
		public HttpMessageHandler Handler
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000AB65 File Offset: 0x00008D65
		public IHttpRouteData GetRouteData(string virtualPathRoot, HttpRequestMessage request)
		{
			return null;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000AB68 File Offset: 0x00008D68
		public IHttpVirtualPathData GetVirtualPath(HttpRequestMessage request, IDictionary<string, object> values)
		{
			return this._innerRoute.GetVirtualPath(request, values);
		}

		// Token: 0x040000FE RID: 254
		private readonly IHttpRoute _innerRoute;
	}
}
