using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http.Routing;
using System.Web.Http.WebHost.Properties;
using System.Web.Routing;

namespace System.Web.Http.WebHost.Routing
{
	// Token: 0x02000019 RID: 25
	internal class HostedHttpRouteCollection : HttpRouteCollection
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00004174 File Offset: 0x00002374
		public HostedHttpRouteCollection(RouteCollection routeCollection) : this(routeCollection, null)
		{
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000417E File Offset: 0x0000237E
		public HostedHttpRouteCollection(RouteCollection routeCollection, string virtualPathRoot)
		{
			if (routeCollection == null)
			{
				throw Error.ArgumentNull("routeCollection");
			}
			this._routeCollection = routeCollection;
			this._virtualPathRoot = virtualPathRoot;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000041A2 File Offset: 0x000023A2
		public override string VirtualPathRoot
		{
			get
			{
				if (this._virtualPathRoot == null)
				{
					return HostingEnvironment.ApplicationVirtualPath;
				}
				return this._virtualPathRoot;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000041B8 File Offset: 0x000023B8
		public override int Count
		{
			get
			{
				return this._routeCollection.Count;
			}
		}

		// Token: 0x1700002D RID: 45
		public override IHttpRoute this[string name]
		{
			get
			{
				HttpWebRoute httpWebRoute = this._routeCollection[name] as HttpWebRoute;
				if (httpWebRoute != null)
				{
					return httpWebRoute.HttpRoute;
				}
				throw Error.KeyNotFound();
			}
		}

		// Token: 0x1700002E RID: 46
		public override IHttpRoute this[int index]
		{
			get
			{
				HttpWebRoute httpWebRoute = this._routeCollection[index] as HttpWebRoute;
				if (httpWebRoute != null)
				{
					return httpWebRoute.HttpRoute;
				}
				throw Error.ArgumentOutOfRange("index", index, SRResources.RouteCollectionOutOfRange, new object[0]);
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000423C File Offset: 0x0000243C
		public override IHttpRouteData GetRouteData(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			HttpContextBase httpContextBase = request.GetHttpContext();
			if (httpContextBase == null)
			{
				httpContextBase = new HttpRequestMessageContextWrapper(this.VirtualPathRoot, request);
			}
			if (httpContextBase.GetHttpRequestMessage() == null)
			{
				httpContextBase.SetHttpRequestMessage(request);
			}
			RouteData routeData = this._routeCollection.GetRouteData(httpContextBase);
			if (routeData != null && !(routeData.RouteHandler is System.Web.Routing.StopRoutingHandler))
			{
				return new HostedHttpRouteData(routeData);
			}
			return null;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000042A4 File Offset: 0x000024A4
		public override IHttpVirtualPathData GetVirtualPath(HttpRequestMessage request, string name, IDictionary<string, object> values)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			HttpContextBase httpContextBase = request.GetHttpContext();
			if (httpContextBase == null)
			{
				httpContextBase = new HttpRequestMessageContextWrapper(this.VirtualPathRoot, request);
			}
			if (httpContextBase.GetHttpRequestMessage() == null)
			{
				httpContextBase.SetHttpRequestMessage(request);
			}
			IHttpRouteData routeData = request.GetRouteData();
			if (routeData == null)
			{
				return null;
			}
			RequestContext requestContext = new RequestContext(httpContextBase, routeData.ToRouteData());
			RouteValueDictionary routeValueDictionary = (values != null) ? new RouteValueDictionary(values) : new RouteValueDictionary();
			VirtualPathData virtualPathData = this._routeCollection.GetVirtualPath(requestContext, name, routeValueDictionary);
			if (virtualPathData != null)
			{
				if (!(virtualPathData.Route is HttpWebRoute) && routeValueDictionary.Remove("httproute"))
				{
					VirtualPathData virtualPath = this._routeCollection.GetVirtualPath(requestContext, name, routeValueDictionary);
					if (virtualPath != null)
					{
						virtualPathData = virtualPath;
					}
				}
				return new HostedHttpVirtualPathData(virtualPathData, routeData.Route);
			}
			return null;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004368 File Offset: 0x00002568
		public override IHttpRoute CreateRoute(string uriTemplate, IDictionary<string, object> defaults, IDictionary<string, object> constraints, IDictionary<string, object> dataTokens, HttpMessageHandler handler)
		{
			if (constraints != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in constraints)
				{
					this.ValidateConstraint(uriTemplate, keyValuePair.Key, keyValuePair.Value);
				}
			}
			return new HostedHttpRoute(uriTemplate, defaults, constraints, dataTokens, handler);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000043D0 File Offset: 0x000025D0
		protected override void ValidateConstraint(string routeTemplate, string name, object constraint)
		{
			HttpWebRoute.ValidateConstraint(routeTemplate, name, constraint);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000043DA File Offset: 0x000025DA
		public override void Add(string name, IHttpRoute route)
		{
			this._routeCollection.Add(name, route.ToRoute());
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000043EE File Offset: 0x000025EE
		public override void Clear()
		{
			this._routeCollection.Clear();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000043FC File Offset: 0x000025FC
		public override bool Contains(IHttpRoute item)
		{
			foreach (RouteBase routeBase in this._routeCollection)
			{
				HttpWebRoute httpWebRoute = routeBase as HttpWebRoute;
				if (httpWebRoute != null && httpWebRoute.HttpRoute == item)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000445C File Offset: 0x0000265C
		public override bool ContainsKey(string name)
		{
			return this._routeCollection[name] != null;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004470 File Offset: 0x00002670
		public override void CopyTo(IHttpRoute[] array, int arrayIndex)
		{
			throw HostedHttpRouteCollection.NotSupportedByHostedRouteCollection();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004477 File Offset: 0x00002677
		public override void CopyTo(KeyValuePair<string, IHttpRoute>[] array, int arrayIndex)
		{
			throw HostedHttpRouteCollection.NotSupportedByRouteCollection();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000447E File Offset: 0x0000267E
		public override void Insert(int index, string name, IHttpRoute value)
		{
			throw HostedHttpRouteCollection.NotSupportedByRouteCollection();
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004485 File Offset: 0x00002685
		public override bool Remove(string name)
		{
			throw HostedHttpRouteCollection.NotSupportedByRouteCollection();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004494 File Offset: 0x00002694
		public override IEnumerator<IHttpRoute> GetEnumerator()
		{
			return (from httpWebRoute in this._routeCollection.OfType<HttpWebRoute>()
			select httpWebRoute.HttpRoute).GetEnumerator();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000044C8 File Offset: 0x000026C8
		public override bool TryGetValue(string name, out IHttpRoute route)
		{
			HttpWebRoute httpWebRoute = this._routeCollection[name] as HttpWebRoute;
			if (httpWebRoute != null)
			{
				route = httpWebRoute.HttpRoute;
				return true;
			}
			route = null;
			return false;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000044F8 File Offset: 0x000026F8
		private static NotSupportedException NotSupportedByRouteCollection()
		{
			return Error.NotSupported(SRResources.RouteCollectionNotSupported, new object[]
			{
				typeof(HostedHttpRouteCollection).Name
			});
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000452C File Offset: 0x0000272C
		private static NotSupportedException NotSupportedByHostedRouteCollection()
		{
			return Error.NotSupported(SRResources.RouteCollectionUseDirectly, new object[]
			{
				typeof(RouteCollection).Name
			});
		}

		// Token: 0x04000027 RID: 39
		private readonly RouteCollection _routeCollection;

		// Token: 0x04000028 RID: 40
		private readonly string _virtualPathRoot;
	}
}
