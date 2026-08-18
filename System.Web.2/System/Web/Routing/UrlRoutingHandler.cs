using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	// Token: 0x02000153 RID: 339
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class UrlRoutingHandler : IHttpHandler
	{
		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x00007722 File Offset: 0x00005922
		protected virtual bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x00038A2B File Offset: 0x00036C2B
		// (set) Token: 0x0600138F RID: 5007 RVA: 0x00038A46 File Offset: 0x00036C46
		public RouteCollection RouteCollection
		{
			get
			{
				if (this._routeCollection == null)
				{
					this._routeCollection = RouteTable.Routes;
				}
				return this._routeCollection;
			}
			set
			{
				this._routeCollection = value;
			}
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00038A4F File Offset: 0x00036C4F
		protected virtual void ProcessRequest(HttpContext httpContext)
		{
			this.ProcessRequest(new HttpContextWrapper(httpContext));
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00038A60 File Offset: 0x00036C60
		protected virtual void ProcessRequest(HttpContextBase httpContext)
		{
			RouteData routeData = this.RouteCollection.GetRouteData(httpContext);
			if (routeData == null)
			{
				throw new HttpException(404, SR.GetString("UrlRoutingHandler_NoRouteMatches"));
			}
			IRouteHandler routeHandler = routeData.RouteHandler;
			if (routeHandler == null)
			{
				throw new InvalidOperationException(SR.GetString("UrlRoutingModule_NoRouteHandler"));
			}
			RequestContext requestContext = new RequestContext(httpContext, routeData);
			IHttpHandler httpHandler = routeHandler.GetHttpHandler(requestContext);
			if (httpHandler == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, SR.GetString("UrlRoutingModule_NoHttpHandler"), new object[]
				{
					routeHandler.GetType()
				}));
			}
			this.VerifyAndProcessRequest(httpHandler, httpContext);
		}

		// Token: 0x06001392 RID: 5010
		protected abstract void VerifyAndProcessRequest(IHttpHandler httpHandler, HttpContextBase httpContext);

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x00038AF0 File Offset: 0x00036CF0
		bool IHttpHandler.IsReusable
		{
			get
			{
				return this.IsReusable;
			}
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x00038AF8 File Offset: 0x00036CF8
		void IHttpHandler.ProcessRequest(HttpContext context)
		{
			this.ProcessRequest(context);
		}

		// Token: 0x040014E0 RID: 5344
		private RouteCollection _routeCollection;
	}
}
