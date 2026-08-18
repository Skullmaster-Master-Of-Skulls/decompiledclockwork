using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace System.Web.Mvc
{
	// Token: 0x02000153 RID: 339
	public class AreaRegistrationContext
	{
		// Token: 0x060008AA RID: 2218 RVA: 0x00017FF0 File Offset: 0x000161F0
		public AreaRegistrationContext(string areaName, RouteCollection routes) : this(areaName, routes, null)
		{
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00017FFC File Offset: 0x000161FC
		public AreaRegistrationContext(string areaName, RouteCollection routes, object state)
		{
			if (string.IsNullOrEmpty(areaName))
			{
				throw Error.ParameterCannotBeNullOrEmpty("areaName");
			}
			if (routes == null)
			{
				throw new ArgumentNullException("routes");
			}
			this.AreaName = areaName;
			this.Routes = routes;
			this.State = state;
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x00018055 File Offset: 0x00016255
		// (set) Token: 0x060008AD RID: 2221 RVA: 0x0001805D File Offset: 0x0001625D
		public string AreaName { get; private set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x00018066 File Offset: 0x00016266
		public ICollection<string> Namespaces
		{
			get
			{
				return this._namespaces;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x0001806E File Offset: 0x0001626E
		// (set) Token: 0x060008B0 RID: 2224 RVA: 0x00018076 File Offset: 0x00016276
		public RouteCollection Routes { get; private set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x0001807F File Offset: 0x0001627F
		// (set) Token: 0x060008B2 RID: 2226 RVA: 0x00018087 File Offset: 0x00016287
		public object State { get; private set; }

		// Token: 0x060008B3 RID: 2227 RVA: 0x00018090 File Offset: 0x00016290
		public Route MapRoute(string name, string url)
		{
			return this.MapRoute(name, url, null);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0001809B File Offset: 0x0001629B
		public Route MapRoute(string name, string url, object defaults)
		{
			return this.MapRoute(name, url, defaults, null);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x000180A7 File Offset: 0x000162A7
		public Route MapRoute(string name, string url, object defaults, object constraints)
		{
			return this.MapRoute(name, url, defaults, constraints, null);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x000180B5 File Offset: 0x000162B5
		public Route MapRoute(string name, string url, string[] namespaces)
		{
			return this.MapRoute(name, url, null, namespaces);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x000180C1 File Offset: 0x000162C1
		public Route MapRoute(string name, string url, object defaults, string[] namespaces)
		{
			return this.MapRoute(name, url, defaults, null, namespaces);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x000180D0 File Offset: 0x000162D0
		public Route MapRoute(string name, string url, object defaults, object constraints, string[] namespaces)
		{
			if (namespaces == null && this.Namespaces != null)
			{
				namespaces = this.Namespaces.ToArray<string>();
			}
			Route route = this.Routes.MapRoute(name, url, defaults, constraints, namespaces);
			route.DataTokens["area"] = this.AreaName;
			bool flag = namespaces == null || namespaces.Length == 0;
			route.DataTokens["UseNamespaceFallback"] = flag;
			return route;
		}

		// Token: 0x04000273 RID: 627
		private readonly HashSet<string> _namespaces = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
	}
}
