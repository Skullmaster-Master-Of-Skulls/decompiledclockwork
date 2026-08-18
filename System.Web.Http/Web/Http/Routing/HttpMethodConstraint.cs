using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace System.Web.Http.Routing
{
	// Token: 0x02000105 RID: 261
	public class HttpMethodConstraint : IHttpRouteConstraint
	{
		// Token: 0x0600065B RID: 1627 RVA: 0x00014D5A File Offset: 0x00012F5A
		public HttpMethodConstraint(params HttpMethod[] allowedMethods)
		{
			if (allowedMethods == null)
			{
				throw Error.ArgumentNull("allowedMethods");
			}
			this.AllowedMethods = new Collection<HttpMethod>(allowedMethods);
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x00014D7C File Offset: 0x00012F7C
		// (set) Token: 0x0600065D RID: 1629 RVA: 0x00014D84 File Offset: 0x00012F84
		public Collection<HttpMethod> AllowedMethods { get; private set; }

		// Token: 0x0600065E RID: 1630 RVA: 0x00014D90 File Offset: 0x00012F90
		protected virtual bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (route == null)
			{
				throw Error.ArgumentNull("route");
			}
			if (parameterName == null)
			{
				throw Error.ArgumentNull("parameterName");
			}
			if (values == null)
			{
				throw Error.ArgumentNull("values");
			}
			switch (routeDirection)
			{
			case HttpRouteDirection.UriResolution:
				return this.AllowedMethods.Contains(request.Method);
			case HttpRouteDirection.UriGeneration:
			{
				HttpMethod item;
				return !values.TryGetValue(parameterName, out item) || this.AllowedMethods.Contains(item);
			}
			default:
				throw Error.InvalidEnumArgument(string.Empty, (int)routeDirection, typeof(HttpRouteDirection));
			}
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00014E2C File Offset: 0x0001302C
		bool IHttpRouteConstraint.Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
		{
			return this.Match(request, route, parameterName, values, routeDirection);
		}
	}
}
