using System;
using System.Web.Routing;
using System.Web.WebPages;

namespace System.Web.Mvc
{
	// Token: 0x0200005E RID: 94
	public class ControllerContext
	{
		// Token: 0x06000279 RID: 633 RVA: 0x00008858 File Offset: 0x00006A58
		public ControllerContext()
		{
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00008860 File Offset: 0x00006A60
		protected ControllerContext(ControllerContext controllerContext)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			this.Controller = controllerContext.Controller;
			this.RequestContext = controllerContext.RequestContext;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000888E File Offset: 0x00006A8E
		public ControllerContext(HttpContextBase httpContext, RouteData routeData, ControllerBase controller) : this(new RequestContext(httpContext, routeData), controller)
		{
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000889E File Offset: 0x00006A9E
		public ControllerContext(RequestContext requestContext, ControllerBase controller)
		{
			if (requestContext == null)
			{
				throw new ArgumentNullException("requestContext");
			}
			if (controller == null)
			{
				throw new ArgumentNullException("controller");
			}
			this.RequestContext = requestContext;
			this.Controller = controller;
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600027D RID: 637 RVA: 0x000088D0 File Offset: 0x00006AD0
		// (set) Token: 0x0600027E RID: 638 RVA: 0x000088D8 File Offset: 0x00006AD8
		public virtual ControllerBase Controller { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600027F RID: 639 RVA: 0x000088E1 File Offset: 0x00006AE1
		// (set) Token: 0x06000280 RID: 640 RVA: 0x000088EE File Offset: 0x00006AEE
		public IDisplayMode DisplayMode
		{
			get
			{
				return DisplayModeProvider.GetDisplayMode(this.HttpContext);
			}
			set
			{
				DisplayModeProvider.SetDisplayMode(this.HttpContext, value);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000281 RID: 641 RVA: 0x000088FC File Offset: 0x00006AFC
		// (set) Token: 0x06000282 RID: 642 RVA: 0x0000892C File Offset: 0x00006B2C
		public virtual HttpContextBase HttpContext
		{
			get
			{
				if (this._httpContext == null)
				{
					this._httpContext = ((this._requestContext != null) ? this._requestContext.HttpContext : new ControllerContext.EmptyHttpContext());
				}
				return this._httpContext;
			}
			set
			{
				this._httpContext = value;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00008938 File Offset: 0x00006B38
		public virtual bool IsChildAction
		{
			get
			{
				RouteData routeData = this.RouteData;
				return routeData != null && routeData.DataTokens.ContainsKey("ParentActionViewContext");
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00008961 File Offset: 0x00006B61
		public ViewContext ParentActionViewContext
		{
			get
			{
				return this.RouteData.DataTokens["ParentActionViewContext"] as ViewContext;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00008980 File Offset: 0x00006B80
		// (set) Token: 0x06000286 RID: 646 RVA: 0x000089C8 File Offset: 0x00006BC8
		public RequestContext RequestContext
		{
			get
			{
				if (this._requestContext == null)
				{
					HttpContextBase httpContext = this.HttpContext ?? new ControllerContext.EmptyHttpContext();
					RouteData routeData = this.RouteData ?? new RouteData();
					this._requestContext = new RequestContext(httpContext, routeData);
				}
				return this._requestContext;
			}
			set
			{
				this._requestContext = value;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000287 RID: 647 RVA: 0x000089D1 File Offset: 0x00006BD1
		// (set) Token: 0x06000288 RID: 648 RVA: 0x00008A01 File Offset: 0x00006C01
		public virtual RouteData RouteData
		{
			get
			{
				if (this._routeData == null)
				{
					this._routeData = ((this._requestContext != null) ? this._requestContext.RouteData : new RouteData());
				}
				return this._routeData;
			}
			set
			{
				this._routeData = value;
			}
		}

		// Token: 0x0400007E RID: 126
		internal const string ParentActionViewContextToken = "ParentActionViewContext";

		// Token: 0x0400007F RID: 127
		private HttpContextBase _httpContext;

		// Token: 0x04000080 RID: 128
		private RequestContext _requestContext;

		// Token: 0x04000081 RID: 129
		private RouteData _routeData;

		// Token: 0x0200005F RID: 95
		private sealed class EmptyHttpContext : HttpContextBase
		{
		}
	}
}
