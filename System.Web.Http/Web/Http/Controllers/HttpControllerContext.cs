using System;
using System.Net.Http;
using System.Web.Http.Routing;

namespace System.Web.Http.Controllers
{
	// Token: 0x0200014C RID: 332
	public class HttpControllerContext
	{
		// Token: 0x06000837 RID: 2103 RVA: 0x0001AC1C File Offset: 0x00018E1C
		public HttpControllerContext(HttpRequestContext requestContext, HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, IHttpController controller)
		{
			if (requestContext == null)
			{
				throw Error.ArgumentNull("requestContext");
			}
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (controllerDescriptor == null)
			{
				throw Error.ArgumentNull("controllerDescriptor");
			}
			if (controller == null)
			{
				throw Error.ArgumentNull("controller");
			}
			this._requestContext = requestContext;
			this._request = request;
			this._controllerDescriptor = controllerDescriptor;
			this._controller = controller;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001AC88 File Offset: 0x00018E88
		public HttpControllerContext(HttpConfiguration configuration, IHttpRouteData routeData, HttpRequestMessage request)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			if (routeData == null)
			{
				throw Error.ArgumentNull("routeData");
			}
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			this._requestContext = new HttpRequestContext
			{
				Configuration = configuration,
				RouteData = routeData
			};
			this._request = request;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001ACE7 File Offset: 0x00018EE7
		public HttpControllerContext()
		{
			this._requestContext = new HttpRequestContext();
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x0001ACFA File Offset: 0x00018EFA
		// (set) Token: 0x0600083B RID: 2107 RVA: 0x0001AD07 File Offset: 0x00018F07
		public HttpConfiguration Configuration
		{
			get
			{
				return this._requestContext.Configuration;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._requestContext.Configuration = value;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x0001AD1E File Offset: 0x00018F1E
		// (set) Token: 0x0600083D RID: 2109 RVA: 0x0001AD26 File Offset: 0x00018F26
		public HttpControllerDescriptor ControllerDescriptor
		{
			get
			{
				return this._controllerDescriptor;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._controllerDescriptor = value;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x0001AD38 File Offset: 0x00018F38
		// (set) Token: 0x0600083F RID: 2111 RVA: 0x0001AD40 File Offset: 0x00018F40
		public IHttpController Controller
		{
			get
			{
				return this._controller;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._controller = value;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x0001AD52 File Offset: 0x00018F52
		// (set) Token: 0x06000841 RID: 2113 RVA: 0x0001AD5A File Offset: 0x00018F5A
		public HttpRequestMessage Request
		{
			get
			{
				return this._request;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._request = value;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x0001AD6C File Offset: 0x00018F6C
		// (set) Token: 0x06000843 RID: 2115 RVA: 0x0001AD74 File Offset: 0x00018F74
		public HttpRequestContext RequestContext
		{
			get
			{
				return this._requestContext;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._requestContext = value;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x0001AD86 File Offset: 0x00018F86
		// (set) Token: 0x06000845 RID: 2117 RVA: 0x0001AD93 File Offset: 0x00018F93
		public IHttpRouteData RouteData
		{
			get
			{
				return this._requestContext.RouteData;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._requestContext.RouteData = value;
			}
		}

		// Token: 0x04000264 RID: 612
		private HttpRequestContext _requestContext;

		// Token: 0x04000265 RID: 613
		private HttpRequestMessage _request;

		// Token: 0x04000266 RID: 614
		private HttpControllerDescriptor _controllerDescriptor;

		// Token: 0x04000267 RID: 615
		private IHttpController _controller;
	}
}
