using System;
using System.Security.Permissions;
using System.Security.Principal;
using System.Web.Compilation;
using System.Web.Security;
using System.Web.UI;

namespace System.Web.Routing
{
	// Token: 0x02000156 RID: 342
	public class PageRouteHandler : IRouteHandler
	{
		// Token: 0x060013A7 RID: 5031 RVA: 0x00038CEF File Offset: 0x00036EEF
		public PageRouteHandler(string virtualPath) : this(virtualPath, true)
		{
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x00038CFC File Offset: 0x00036EFC
		public PageRouteHandler(string virtualPath, bool checkPhysicalUrlAccess)
		{
			if (string.IsNullOrEmpty(virtualPath) || !virtualPath.StartsWith("~/", StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException(SR.GetString("PageRouteHandler_InvalidVirtualPath"), "virtualPath");
			}
			this.VirtualPath = virtualPath;
			this.CheckPhysicalUrlAccess = checkPhysicalUrlAccess;
			this._useRouteVirtualPath = this.VirtualPath.Contains("{");
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060013A9 RID: 5033 RVA: 0x00038D5E File Offset: 0x00036F5E
		// (set) Token: 0x060013AA RID: 5034 RVA: 0x00038D66 File Offset: 0x00036F66
		public string VirtualPath { get; private set; }

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x00038D6F File Offset: 0x00036F6F
		// (set) Token: 0x060013AC RID: 5036 RVA: 0x00038D77 File Offset: 0x00036F77
		public bool CheckPhysicalUrlAccess { get; private set; }

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x00038D80 File Offset: 0x00036F80
		private Route RouteVirtualPath
		{
			get
			{
				if (this._routeVirtualPath == null)
				{
					this._routeVirtualPath = new Route(this.VirtualPath.Substring(2), this);
				}
				return this._routeVirtualPath;
			}
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x00038DA8 File Offset: 0x00036FA8
		private bool CheckUrlAccess(string virtualPath, RequestContext requestContext)
		{
			IPrincipal principal = requestContext.HttpContext.User;
			if (principal == null)
			{
				principal = new GenericPrincipal(new GenericIdentity(string.Empty, string.Empty), new string[0]);
			}
			return this.CheckUrlAccessWithAssert(virtualPath, requestContext, principal);
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x00038DE8 File Offset: 0x00036FE8
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		private bool CheckUrlAccessWithAssert(string virtualPath, RequestContext requestContext, IPrincipal user)
		{
			return UrlAuthorizationModule.CheckUrlAccessForPrincipal(virtualPath, user, requestContext.HttpContext.Request.HttpMethod);
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x00038E04 File Offset: 0x00037004
		public virtual IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			if (requestContext == null)
			{
				throw new ArgumentNullException("requestContext");
			}
			string text = this.GetSubstitutedVirtualPath(requestContext);
			int num = text.IndexOf('?');
			if (num != -1)
			{
				text = text.Substring(0, num);
			}
			if (this.CheckPhysicalUrlAccess && !this.CheckUrlAccess(text, requestContext))
			{
				return new UrlAuthFailureHandler();
			}
			return BuildManager.CreateInstanceFromVirtualPath(text, typeof(Page)) as Page;
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00038E6C File Offset: 0x0003706C
		public string GetSubstitutedVirtualPath(RequestContext requestContext)
		{
			if (requestContext == null)
			{
				throw new ArgumentNullException("requestContext");
			}
			if (!this._useRouteVirtualPath)
			{
				return this.VirtualPath;
			}
			VirtualPathData virtualPath = this.RouteVirtualPath.GetVirtualPath(requestContext, requestContext.RouteData.Values);
			if (virtualPath == null)
			{
				return this.VirtualPath;
			}
			return "~/" + virtualPath.VirtualPath;
		}

		// Token: 0x040014E9 RID: 5353
		private bool _useRouteVirtualPath;

		// Token: 0x040014EA RID: 5354
		private Route _routeVirtualPath;
	}
}
