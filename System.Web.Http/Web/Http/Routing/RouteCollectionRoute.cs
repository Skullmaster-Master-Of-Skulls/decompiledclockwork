using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Properties;

namespace System.Web.Http.Routing
{
	// Token: 0x02000080 RID: 128
	internal class RouteCollectionRoute : IHttpRoute, IReadOnlyCollection<IHttpRoute>, IEnumerable<IHttpRoute>, IEnumerable
	{
		// Token: 0x06000353 RID: 851 RVA: 0x0000A970 File Offset: 0x00008B70
		public void EnsureInitialized(Func<IReadOnlyCollection<IHttpRoute>> initializer)
		{
			if (this._beingInitialized && this._subRoutes == null)
			{
				return;
			}
			try
			{
				this._beingInitialized = true;
				this._subRoutes = initializer();
			}
			finally
			{
				this._beingInitialized = false;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000A9BC File Offset: 0x00008BBC
		private IReadOnlyCollection<IHttpRoute> SubRoutes
		{
			get
			{
				if (this._subRoutes == null)
				{
					string message = Error.Format(SRResources.Object_NotYetInitialized, new object[0]);
					throw new InvalidOperationException(message);
				}
				return this._subRoutes;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000A9EF File Offset: 0x00008BEF
		public string RouteTemplate
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000A9F6 File Offset: 0x00008BF6
		public IDictionary<string, object> Defaults
		{
			get
			{
				return RouteCollectionRoute._empty;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000A9FD File Offset: 0x00008BFD
		public IDictionary<string, object> Constraints
		{
			get
			{
				return RouteCollectionRoute._empty;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000AA04 File Offset: 0x00008C04
		public IDictionary<string, object> DataTokens
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000AA07 File Offset: 0x00008C07
		public HttpMessageHandler Handler
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000AA0C File Offset: 0x00008C0C
		public IHttpRouteData GetRouteData(string virtualPathRoot, HttpRequestMessage request)
		{
			List<IHttpRouteData> list = new List<IHttpRouteData>();
			foreach (IHttpRoute httpRoute in this.SubRoutes)
			{
				IHttpRouteData routeData = httpRoute.GetRouteData(virtualPathRoot, request);
				if (routeData != null)
				{
					list.Add(routeData);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			return new RouteCollectionRoute.RouteCollectionRouteData(this, list.ToArray());
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000AA84 File Offset: 0x00008C84
		public IHttpVirtualPathData GetVirtualPath(HttpRequestMessage request, IDictionary<string, object> values)
		{
			return null;
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000AA87 File Offset: 0x00008C87
		public int Count
		{
			get
			{
				return this.SubRoutes.Count;
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000AA94 File Offset: 0x00008C94
		public IEnumerator<IHttpRoute> GetEnumerator()
		{
			return this.SubRoutes.GetEnumerator();
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000AAA1 File Offset: 0x00008CA1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.SubRoutes.GetEnumerator();
		}

		// Token: 0x040000F8 RID: 248
		public const string SubRouteDataKey = "MS_SubRoutes";

		// Token: 0x040000F9 RID: 249
		private IReadOnlyCollection<IHttpRoute> _subRoutes;

		// Token: 0x040000FA RID: 250
		private static readonly IDictionary<string, object> _empty = EmptyReadOnlyDictionary<string, object>.Value;

		// Token: 0x040000FB RID: 251
		private bool _beingInitialized;

		// Token: 0x02000082 RID: 130
		private class RouteCollectionRouteData : IHttpRouteData
		{
			// Token: 0x06000362 RID: 866 RVA: 0x0000AABC File Offset: 0x00008CBC
			public RouteCollectionRouteData(IHttpRoute parent, IHttpRouteData[] subRouteDatas)
			{
				this.Route = parent;
				this.Values = new HttpRouteValueDictionary
				{
					{
						"MS_SubRoutes",
						subRouteDatas
					}
				};
			}

			// Token: 0x1700018D RID: 397
			// (get) Token: 0x06000363 RID: 867 RVA: 0x0000AAEF File Offset: 0x00008CEF
			// (set) Token: 0x06000364 RID: 868 RVA: 0x0000AAF7 File Offset: 0x00008CF7
			public IHttpRoute Route { get; private set; }

			// Token: 0x1700018E RID: 398
			// (get) Token: 0x06000365 RID: 869 RVA: 0x0000AB00 File Offset: 0x00008D00
			// (set) Token: 0x06000366 RID: 870 RVA: 0x0000AB08 File Offset: 0x00008D08
			public IDictionary<string, object> Values { get; private set; }
		}
	}
}
