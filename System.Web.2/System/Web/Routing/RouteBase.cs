using System;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	// Token: 0x02000149 RID: 329
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class RouteBase
	{
		// Token: 0x06001331 RID: 4913
		public abstract RouteData GetRouteData(HttpContextBase httpContext);

		// Token: 0x06001332 RID: 4914
		public abstract VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values);

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001333 RID: 4915 RVA: 0x00037A00 File Offset: 0x00035C00
		// (set) Token: 0x06001334 RID: 4916 RVA: 0x00037A08 File Offset: 0x00035C08
		public bool RouteExistingFiles
		{
			get
			{
				return this._routeExistingFiles;
			}
			set
			{
				this._routeExistingFiles = value;
			}
		}

		// Token: 0x040014D0 RID: 5328
		private bool _routeExistingFiles = true;
	}
}
