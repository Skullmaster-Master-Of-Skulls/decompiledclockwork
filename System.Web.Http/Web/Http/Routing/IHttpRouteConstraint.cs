using System;
using System.Collections.Generic;
using System.Net.Http;

namespace System.Web.Http.Routing
{
	// Token: 0x02000087 RID: 135
	public interface IHttpRouteConstraint
	{
		// Token: 0x06000376 RID: 886
		bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection);
	}
}
