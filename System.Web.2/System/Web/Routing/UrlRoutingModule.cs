using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Web.Security;

namespace System.Web.Routing
{
	// Token: 0x02000154 RID: 340
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class UrlRoutingModule : IHttpModule
	{
		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x00038B01 File Offset: 0x00036D01
		// (set) Token: 0x06001397 RID: 5015 RVA: 0x00038B1C File Offset: 0x00036D1C
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

		// Token: 0x06001398 RID: 5016 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Dispose()
		{
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00038B28 File Offset: 0x00036D28
		protected virtual void Init(HttpApplication application)
		{
			if (application.Context.Items[UrlRoutingModule._contextKey] != null)
			{
				return;
			}
			application.Context.Items[UrlRoutingModule._contextKey] = UrlRoutingModule._contextKey;
			application.PostResolveRequestCache += this.OnApplicationPostResolveRequestCache;
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00038B7C File Offset: 0x00036D7C
		private void OnApplicationPostResolveRequestCache(object sender, EventArgs e)
		{
			HttpApplication httpApplication = (HttpApplication)sender;
			HttpContextBase context = new HttpContextWrapper(httpApplication.Context);
			this.PostResolveRequestCache(context);
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x00006164 File Offset: 0x00004364
		[Obsolete("This method is obsolete. Override the Init method to use the PostMapRequestHandler event.")]
		public virtual void PostMapRequestHandler(HttpContextBase context)
		{
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x00038BA4 File Offset: 0x00036DA4
		public virtual void PostResolveRequestCache(HttpContextBase context)
		{
			RouteData routeData = this.RouteCollection.GetRouteData(context);
			if (routeData == null)
			{
				return;
			}
			IRouteHandler routeHandler = routeData.RouteHandler;
			if (routeHandler == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, SR.GetString("UrlRoutingModule_NoRouteHandler"), new object[0]));
			}
			if (routeHandler is StopRoutingHandler)
			{
				return;
			}
			RequestContext requestContext = new RequestContext(context, routeData);
			context.Request.RequestContext = requestContext;
			IHttpHandler httpHandler = routeHandler.GetHttpHandler(requestContext);
			if (httpHandler == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, SR.GetString("UrlRoutingModule_NoHttpHandler"), new object[]
				{
					routeHandler.GetType()
				}));
			}
			if (!(httpHandler is UrlAuthFailureHandler))
			{
				context.RemapHandler(httpHandler);
				return;
			}
			if (FormsAuthenticationModule.FormsAuthRequired)
			{
				UrlAuthorizationModule.ReportUrlAuthorizationFailure(HttpContext.Current, this);
				return;
			}
			throw new HttpException(401, SR.GetString("Assess_Denied_Description3"));
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00038C74 File Offset: 0x00036E74
		void IHttpModule.Dispose()
		{
			this.Dispose();
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x00038C7C File Offset: 0x00036E7C
		void IHttpModule.Init(HttpApplication application)
		{
			this.Init(application);
		}

		// Token: 0x040014E1 RID: 5345
		private static readonly object _contextKey = new object();

		// Token: 0x040014E2 RID: 5346
		private static readonly object _requestDataKey = new object();

		// Token: 0x040014E3 RID: 5347
		private RouteCollection _routeCollection;
	}
}
