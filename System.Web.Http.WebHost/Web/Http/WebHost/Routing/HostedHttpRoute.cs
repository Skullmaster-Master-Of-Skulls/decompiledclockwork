using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;
using System.Web.Routing;

namespace System.Web.Http.WebHost.Routing
{
	// Token: 0x0200001A RID: 26
	internal class HostedHttpRoute : IHttpRoute
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00004560 File Offset: 0x00002760
		public HostedHttpRoute(string uriTemplate, IDictionary<string, object> defaults, IDictionary<string, object> constraints, IDictionary<string, object> dataTokens, HttpMessageHandler handler)
		{
			RouteValueDictionary defaults2 = (defaults != null) ? new RouteValueDictionary(defaults) : null;
			RouteValueDictionary constraints2 = (constraints != null) ? new RouteValueDictionary(constraints) : null;
			RouteValueDictionary dataTokens2 = (dataTokens != null) ? new RouteValueDictionary(dataTokens) : null;
			this.OriginalRoute = new HttpWebRoute(uriTemplate, defaults2, constraints2, dataTokens2, HttpControllerRouteHandler.Instance, this);
			this.Handler = handler;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000045B9 File Offset: 0x000027B9
		public string RouteTemplate
		{
			get
			{
				return this.OriginalRoute.Url;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000045C6 File Offset: 0x000027C6
		public IDictionary<string, object> Defaults
		{
			get
			{
				return this.OriginalRoute.Defaults;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000045D3 File Offset: 0x000027D3
		public IDictionary<string, object> Constraints
		{
			get
			{
				return this.OriginalRoute.Constraints;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000045E0 File Offset: 0x000027E0
		public IDictionary<string, object> DataTokens
		{
			get
			{
				return this.OriginalRoute.DataTokens;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000045ED File Offset: 0x000027ED
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x000045F5 File Offset: 0x000027F5
		public HttpMessageHandler Handler { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000045FE File Offset: 0x000027FE
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00004606 File Offset: 0x00002806
		internal Route OriginalRoute { get; private set; }

		// Token: 0x060000BC RID: 188 RVA: 0x00004610 File Offset: 0x00002810
		public IHttpRouteData GetRouteData(string rootVirtualPath, HttpRequestMessage request)
		{
			if (rootVirtualPath == null)
			{
				throw Error.ArgumentNull("rootVirtualPath");
			}
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			HttpContextBase httpContextBase = request.GetHttpContext();
			if (httpContextBase == null)
			{
				httpContextBase = new HttpRequestMessageContextWrapper(rootVirtualPath, request);
			}
			RouteData routeData = this.OriginalRoute.GetRouteData(httpContextBase);
			if (routeData != null)
			{
				return new HostedHttpRouteData(routeData);
			}
			return null;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004664 File Offset: 0x00002864
		public IHttpVirtualPathData GetVirtualPath(HttpRequestMessage request, IDictionary<string, object> values)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			HttpContextBase httpContext = request.GetHttpContext();
			if (httpContext != null)
			{
				HostedHttpRouteData hostedHttpRouteData = request.GetRouteData() as HostedHttpRouteData;
				if (hostedHttpRouteData != null)
				{
					RequestContext requestContext = new RequestContext(httpContext, hostedHttpRouteData.OriginalRouteData);
					VirtualPathData virtualPath = this.OriginalRoute.GetVirtualPath(requestContext, new RouteValueDictionary(values));
					if (virtualPath != null)
					{
						return new HostedHttpVirtualPathData(virtualPath, hostedHttpRouteData.Route);
					}
				}
			}
			return null;
		}
	}
}
