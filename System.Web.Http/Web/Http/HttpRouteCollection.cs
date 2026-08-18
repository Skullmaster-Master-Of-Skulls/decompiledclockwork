using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;
using System.Web.Http.Routing;

namespace System.Web.Http
{
	// Token: 0x020000DE RID: 222
	public class HttpRouteCollection : ICollection<IHttpRoute>, IEnumerable<IHttpRoute>, IEnumerable, IDisposable
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x000116AB File Offset: 0x0000F8AB
		public HttpRouteCollection() : this("/")
		{
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x000116B8 File Offset: 0x0000F8B8
		public HttpRouteCollection(string virtualPathRoot)
		{
			if (virtualPathRoot == null)
			{
				throw Error.ArgumentNull("virtualPathRoot");
			}
			Uri uri = new Uri(HttpRouteCollection._referenceBaseAddress, virtualPathRoot);
			this._virtualPathRoot = "/" + uri.GetComponents(UriComponents.Path, UriFormat.Unescaped);
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x00011719 File Offset: 0x0000F919
		public virtual string VirtualPathRoot
		{
			get
			{
				return this._virtualPathRoot;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00011721 File Offset: 0x0000F921
		public virtual int Count
		{
			get
			{
				return this._collection.Count;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x0001172E File Offset: 0x0000F92E
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001F6 RID: 502
		public virtual IHttpRoute this[int index]
		{
			get
			{
				return this._collection[index];
			}
		}

		// Token: 0x170001F7 RID: 503
		public virtual IHttpRoute this[string name]
		{
			get
			{
				return this._dictionary[name];
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00011750 File Offset: 0x0000F950
		public virtual IHttpRouteData GetRouteData(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			for (int i = 0; i < this._collection.Count; i++)
			{
				string virtualPathRoot = this.GetVirtualPathRoot(request.GetRequestContext());
				IHttpRouteData routeData = this._collection[i].GetRouteData(virtualPathRoot, request);
				if (routeData != null)
				{
					return routeData;
				}
			}
			return null;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000117A8 File Offset: 0x0000F9A8
		public virtual IHttpVirtualPathData GetVirtualPath(HttpRequestMessage request, string name, IDictionary<string, object> values)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (name == null)
			{
				throw Error.ArgumentNull("name");
			}
			IHttpRoute httpRoute;
			if (!this._dictionary.TryGetValue(name, out httpRoute))
			{
				throw Error.Argument("name", SRResources.RouteCollection_NameNotFound, new object[]
				{
					name
				});
			}
			IHttpVirtualPathData virtualPath = httpRoute.GetVirtualPath(request, values);
			if (virtualPath == null)
			{
				return null;
			}
			string text = this.GetVirtualPathRoot(request.GetRequestContext());
			if (!text.EndsWith("/", StringComparison.Ordinal))
			{
				text += "/";
			}
			return new HttpVirtualPathData(virtualPath.Route, text + virtualPath.VirtualPath);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00011849 File Offset: 0x0000FA49
		private string GetVirtualPathRoot(HttpRequestContext requestContext)
		{
			if (requestContext != null)
			{
				return requestContext.VirtualPathRoot ?? string.Empty;
			}
			return this._virtualPathRoot;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00011864 File Offset: 0x0000FA64
		public IHttpRoute CreateRoute(string routeTemplate, object defaults, object constraints)
		{
			IDictionary<string, object> dataTokens = new Dictionary<string, object>();
			return this.CreateRoute(routeTemplate, new HttpRouteValueDictionary(defaults), new HttpRouteValueDictionary(constraints), dataTokens, null);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001188C File Offset: 0x0000FA8C
		public IHttpRoute CreateRoute(string routeTemplate, IDictionary<string, object> defaults, IDictionary<string, object> constraints, IDictionary<string, object> dataTokens)
		{
			return this.CreateRoute(routeTemplate, defaults, constraints, dataTokens, null);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0001189C File Offset: 0x0000FA9C
		public virtual IHttpRoute CreateRoute(string routeTemplate, IDictionary<string, object> defaults, IDictionary<string, object> constraints, IDictionary<string, object> dataTokens, HttpMessageHandler handler)
		{
			HttpRouteValueDictionary defaults2 = new HttpRouteValueDictionary(defaults);
			HttpRouteValueDictionary httpRouteValueDictionary = new HttpRouteValueDictionary(constraints);
			HttpRouteValueDictionary dataTokens2 = new HttpRouteValueDictionary(dataTokens);
			foreach (KeyValuePair<string, object> keyValuePair in httpRouteValueDictionary)
			{
				this.ValidateConstraint(routeTemplate, keyValuePair.Key, keyValuePair.Value);
			}
			return new HttpRoute(routeTemplate, defaults2, httpRouteValueDictionary, dataTokens2, handler);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001191C File Offset: 0x0000FB1C
		protected virtual void ValidateConstraint(string routeTemplate, string name, object constraint)
		{
			if (name == null)
			{
				throw Error.ArgumentNull("name");
			}
			if (constraint == null)
			{
				throw Error.ArgumentNull("constraint");
			}
			HttpRoute.ValidateConstraint(routeTemplate, name, constraint);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00011944 File Offset: 0x0000FB44
		void ICollection<IHttpRoute>.Add(IHttpRoute route)
		{
			throw Error.NotSupported(SRResources.Route_AddRemoveWithNoKeyNotSupported, new object[]
			{
				typeof(HttpRouteCollection).Name
			});
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00011975 File Offset: 0x0000FB75
		public virtual void Add(string name, IHttpRoute route)
		{
			if (name == null)
			{
				throw Error.ArgumentNull("name");
			}
			if (route == null)
			{
				throw Error.ArgumentNull("route");
			}
			this._dictionary.Add(name, route);
			this._collection.Add(route);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x000119AC File Offset: 0x0000FBAC
		public virtual void Clear()
		{
			this._dictionary.Clear();
			this._collection.Clear();
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x000119C4 File Offset: 0x0000FBC4
		public virtual bool Contains(IHttpRoute item)
		{
			if (item == null)
			{
				throw Error.ArgumentNull("item");
			}
			return this._collection.Contains(item);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x000119E0 File Offset: 0x0000FBE0
		public virtual bool ContainsKey(string name)
		{
			if (name == null)
			{
				throw Error.ArgumentNull("name");
			}
			return this._dictionary.ContainsKey(name);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x000119FC File Offset: 0x0000FBFC
		public virtual void CopyTo(IHttpRoute[] array, int arrayIndex)
		{
			this._collection.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00011A0B File Offset: 0x0000FC0B
		public virtual void CopyTo(KeyValuePair<string, IHttpRoute>[] array, int arrayIndex)
		{
			this._dictionary.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00011A1C File Offset: 0x0000FC1C
		public virtual void Insert(int index, string name, IHttpRoute value)
		{
			if (name == null)
			{
				throw Error.ArgumentNull("name");
			}
			if (value == null)
			{
				throw Error.ArgumentNull("value");
			}
			if (this._collection[index] != null)
			{
				this._dictionary.Add(name, value);
				this._collection.Insert(index, value);
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00011A70 File Offset: 0x0000FC70
		bool ICollection<IHttpRoute>.Remove(IHttpRoute route)
		{
			throw Error.NotSupported(SRResources.Route_AddRemoveWithNoKeyNotSupported, new object[]
			{
				typeof(HttpRouteCollection).Name
			});
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00011AA4 File Offset: 0x0000FCA4
		public virtual bool Remove(string name)
		{
			if (name == null)
			{
				throw Error.ArgumentNull("name");
			}
			IHttpRoute item;
			if (this._dictionary.TryGetValue(name, out item))
			{
				bool result = this._dictionary.Remove(name);
				this._collection.Remove(item);
				return result;
			}
			return false;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00011AEC File Offset: 0x0000FCEC
		public virtual IEnumerator<IHttpRoute> GetEnumerator()
		{
			return this._collection.GetEnumerator();
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00011AFE File Offset: 0x0000FCFE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.OnGetEnumerator();
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00011B06 File Offset: 0x0000FD06
		protected virtual IEnumerator OnGetEnumerator()
		{
			return this._collection.GetEnumerator();
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00011B18 File Offset: 0x0000FD18
		public virtual bool TryGetValue(string name, out IHttpRoute route)
		{
			return this._dictionary.TryGetValue(name, out route);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00011B27 File Offset: 0x0000FD27
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00011B38 File Offset: 0x0000FD38
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (disposing)
				{
					HashSet<IDisposable> hashSet = new HashSet<IDisposable>();
					foreach (IHttpRoute httpRoute in this)
					{
						if (httpRoute.Handler != null)
						{
							hashSet.Add(httpRoute.Handler);
						}
					}
					foreach (IDisposable disposable in hashSet)
					{
						disposable.Dispose();
					}
				}
				this._disposed = true;
			}
		}

		// Token: 0x0400018E RID: 398
		private static readonly Uri _referenceBaseAddress = new Uri("http://localhost");

		// Token: 0x0400018F RID: 399
		private readonly string _virtualPathRoot;

		// Token: 0x04000190 RID: 400
		private readonly List<IHttpRoute> _collection = new List<IHttpRoute>();

		// Token: 0x04000191 RID: 401
		private readonly IDictionary<string, IHttpRoute> _dictionary = new Dictionary<string, IHttpRoute>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000192 RID: 402
		private bool _disposed;
	}
}
