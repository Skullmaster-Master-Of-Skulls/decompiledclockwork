using System;
using System.Collections.Generic;
using System.Net.Http;

namespace System.Web.Http.Routing
{
	// Token: 0x0200007F RID: 127
	public interface IHttpRoute
	{
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600034B RID: 843
		string RouteTemplate { get; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600034C RID: 844
		IDictionary<string, object> Defaults { get; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600034D RID: 845
		IDictionary<string, object> Constraints { get; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600034E RID: 846
		IDictionary<string, object> DataTokens { get; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600034F RID: 847
		HttpMessageHandler Handler { get; }

		// Token: 0x06000350 RID: 848
		IHttpRouteData GetRouteData(string virtualPathRoot, HttpRequestMessage request);

		// Token: 0x06000351 RID: 849
		IHttpVirtualPathData GetVirtualPath(HttpRequestMessage request, IDictionary<string, object> values);
	}
}
