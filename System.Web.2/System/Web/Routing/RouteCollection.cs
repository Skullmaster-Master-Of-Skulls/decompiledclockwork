using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Web.Hosting;
using System.Web.UI;

namespace System.Web.Routing
{
	// Token: 0x0200014A RID: 330
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class RouteCollection : Collection<RouteBase>
	{
		// Token: 0x06001336 RID: 4918 RVA: 0x00037A20 File Offset: 0x00035C20
		public RouteCollection()
		{
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x00037A43 File Offset: 0x00035C43
		public RouteCollection(VirtualPathProvider virtualPathProvider)
		{
			this.VPP = virtualPathProvider;
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x00037A6D File Offset: 0x00035C6D
		// (set) Token: 0x06001339 RID: 4921 RVA: 0x00037A75 File Offset: 0x00035C75
		public bool AppendTrailingSlash { get; set; }

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x00037A7E File Offset: 0x00035C7E
		// (set) Token: 0x0600133B RID: 4923 RVA: 0x00037A86 File Offset: 0x00035C86
		public bool LowercaseUrls { get; set; }

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x00037A8F File Offset: 0x00035C8F
		// (set) Token: 0x0600133D RID: 4925 RVA: 0x00037A97 File Offset: 0x00035C97
		public bool RouteExistingFiles { get; set; }

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x00037AA0 File Offset: 0x00035CA0
		// (set) Token: 0x0600133F RID: 4927 RVA: 0x00037AB6 File Offset: 0x00035CB6
		private VirtualPathProvider VPP
		{
			get
			{
				if (this._vpp == null)
				{
					return HostingEnvironment.VirtualPathProvider;
				}
				return this._vpp;
			}
			set
			{
				this._vpp = value;
			}
		}

		// Token: 0x170005DB RID: 1499
		public RouteBase this[string name]
		{
			get
			{
				if (string.IsNullOrEmpty(name))
				{
					return null;
				}
				RouteBase result;
				if (this._namedMap.TryGetValue(name, out result))
				{
					return result;
				}
				return null;
			}
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00037AEC File Offset: 0x00035CEC
		public void Add(string name, RouteBase item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (!string.IsNullOrEmpty(name) && this._namedMap.ContainsKey(name))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, SR.GetString("RouteCollection_DuplicateName"), new object[]
				{
					name
				}), "name");
			}
			base.Add(item);
			if (!string.IsNullOrEmpty(name))
			{
				this._namedMap[name] = item;
			}
			Route route = item as Route;
			if (route != null && route.RouteHandler != null)
			{
				TelemetryLogger.LogHttpHandler(route.RouteHandler.GetType());
			}
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00037B84 File Offset: 0x00035D84
		public Route MapPageRoute(string routeName, string routeUrl, string physicalFile)
		{
			return this.MapPageRoute(routeName, routeUrl, physicalFile, true, null, null, null);
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x00037B93 File Offset: 0x00035D93
		public Route MapPageRoute(string routeName, string routeUrl, string physicalFile, bool checkPhysicalUrlAccess)
		{
			return this.MapPageRoute(routeName, routeUrl, physicalFile, checkPhysicalUrlAccess, null, null, null);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x00037BA3 File Offset: 0x00035DA3
		public Route MapPageRoute(string routeName, string routeUrl, string physicalFile, bool checkPhysicalUrlAccess, RouteValueDictionary defaults)
		{
			return this.MapPageRoute(routeName, routeUrl, physicalFile, checkPhysicalUrlAccess, defaults, null, null);
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x00037BB4 File Offset: 0x00035DB4
		public Route MapPageRoute(string routeName, string routeUrl, string physicalFile, bool checkPhysicalUrlAccess, RouteValueDictionary defaults, RouteValueDictionary constraints)
		{
			return this.MapPageRoute(routeName, routeUrl, physicalFile, checkPhysicalUrlAccess, defaults, constraints, null);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x00037BC8 File Offset: 0x00035DC8
		public Route MapPageRoute(string routeName, string routeUrl, string physicalFile, bool checkPhysicalUrlAccess, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens)
		{
			if (routeUrl == null)
			{
				throw new ArgumentNullException("routeUrl");
			}
			Route route = new Route(routeUrl, defaults, constraints, dataTokens, new PageRouteHandler(physicalFile, checkPhysicalUrlAccess));
			this.Add(routeName, route);
			return route;
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x00037C01 File Offset: 0x00035E01
		protected override void ClearItems()
		{
			this._namedMap.Clear();
			base.ClearItems();
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x00037C14 File Offset: 0x00035E14
		public IDisposable GetReadLock()
		{
			this._rwLock.EnterReadLock();
			return new RouteCollection.ReadLockDisposable(this._rwLock);
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00037C2C File Offset: 0x00035E2C
		private RequestContext GetRequestContext(RequestContext requestContext)
		{
			if (requestContext != null)
			{
				return requestContext;
			}
			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null)
			{
				throw new InvalidOperationException(SR.GetString("RouteCollection_RequiresContext"));
			}
			return new RequestContext(new HttpContextWrapper(httpContext), new RouteData());
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x00037C68 File Offset: 0x00035E68
		private bool IsRouteToExistingFile(HttpContextBase httpContext)
		{
			string appRelativeCurrentExecutionFilePath = httpContext.Request.AppRelativeCurrentExecutionFilePath;
			return appRelativeCurrentExecutionFilePath != "~/" && this.VPP != null && (this.VPP.FileExists(appRelativeCurrentExecutionFilePath) || this.VPP.DirectoryExists(appRelativeCurrentExecutionFilePath));
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x00037CB4 File Offset: 0x00035EB4
		public RouteData GetRouteData(HttpContextBase httpContext)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (httpContext.Request == null)
			{
				throw new ArgumentException(SR.GetString("RouteTable_ContextMissingRequest"), "httpContext");
			}
			if (base.Count == 0)
			{
				return null;
			}
			bool flag = false;
			bool flag2 = false;
			if (!this.RouteExistingFiles)
			{
				flag = this.IsRouteToExistingFile(httpContext);
				flag2 = true;
				if (flag)
				{
					return null;
				}
			}
			using (this.GetReadLock())
			{
				foreach (RouteBase routeBase in this)
				{
					RouteData routeData = routeBase.GetRouteData(httpContext);
					if (routeData != null)
					{
						if (!routeBase.RouteExistingFiles)
						{
							if (!flag2)
							{
								flag = this.IsRouteToExistingFile(httpContext);
							}
							if (flag)
							{
								return null;
							}
						}
						return routeData;
					}
				}
			}
			return null;
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00037D9C File Offset: 0x00035F9C
		private string NormalizeVirtualPath(RequestContext requestContext, string virtualPath)
		{
			string text = Util.GetUrlWithApplicationPath(requestContext.HttpContext, virtualPath);
			if (this.LowercaseUrls || this.AppendTrailingSlash)
			{
				int num = text.IndexOfAny(new char[]
				{
					'?',
					'#'
				});
				string text2;
				string str;
				if (num >= 0)
				{
					text2 = text.Substring(0, num);
					str = text.Substring(num);
				}
				else
				{
					text2 = text;
					str = "";
				}
				if (this.LowercaseUrls)
				{
					text2 = text2.ToLowerInvariant();
				}
				if (this.AppendTrailingSlash && !text2.EndsWith("/"))
				{
					text2 += "/";
				}
				text = text2 + str;
			}
			return text;
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00037E38 File Offset: 0x00036038
		public VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
		{
			requestContext = this.GetRequestContext(requestContext);
			using (this.GetReadLock())
			{
				foreach (RouteBase routeBase in this)
				{
					VirtualPathData virtualPath = routeBase.GetVirtualPath(requestContext, values);
					if (virtualPath != null)
					{
						virtualPath.VirtualPath = this.NormalizeVirtualPath(requestContext, virtualPath.VirtualPath);
						return virtualPath;
					}
				}
			}
			return null;
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x00037EC8 File Offset: 0x000360C8
		public VirtualPathData GetVirtualPath(RequestContext requestContext, string name, RouteValueDictionary values)
		{
			requestContext = this.GetRequestContext(requestContext);
			if (string.IsNullOrEmpty(name))
			{
				return this.GetVirtualPath(requestContext, values);
			}
			RouteBase routeBase;
			bool flag;
			using (this.GetReadLock())
			{
				flag = this._namedMap.TryGetValue(name, out routeBase);
			}
			if (!flag)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, SR.GetString("RouteCollection_NameNotFound"), new object[]
				{
					name
				}), "name");
			}
			VirtualPathData virtualPath = routeBase.GetVirtualPath(requestContext, values);
			if (virtualPath != null)
			{
				virtualPath.VirtualPath = this.NormalizeVirtualPath(requestContext, virtualPath.VirtualPath);
				return virtualPath;
			}
			return null;
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x00037F70 File Offset: 0x00036170
		public IDisposable GetWriteLock()
		{
			this._rwLock.EnterWriteLock();
			return new RouteCollection.WriteLockDisposable(this._rwLock);
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00037F88 File Offset: 0x00036188
		public void Ignore(string url)
		{
			this.Ignore(url, null);
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00037F94 File Offset: 0x00036194
		public void Ignore(string url, object constraints)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			RouteCollection.IgnoreRouteInternal item = new RouteCollection.IgnoreRouteInternal(url)
			{
				Constraints = new RouteValueDictionary(constraints)
			};
			base.Add(item);
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00037FCC File Offset: 0x000361CC
		protected override void InsertItem(int index, RouteBase item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (base.Contains(item))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("RouteCollection_DuplicateEntry"), new object[0]), "item");
			}
			base.InsertItem(index, item);
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0003801D File Offset: 0x0003621D
		protected override void RemoveItem(int index)
		{
			this.RemoveRouteName(index);
			base.RemoveItem(index);
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x00038030 File Offset: 0x00036230
		private void RemoveRouteName(int index)
		{
			RouteBase routeBase = base[index];
			foreach (KeyValuePair<string, RouteBase> keyValuePair in this._namedMap)
			{
				if (keyValuePair.Value == routeBase)
				{
					this._namedMap.Remove(keyValuePair.Key);
					break;
				}
			}
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x000380A4 File Offset: 0x000362A4
		protected override void SetItem(int index, RouteBase item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (base.Contains(item))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("RouteCollection_DuplicateEntry"), new object[0]), "item");
			}
			this.RemoveRouteName(index);
			base.SetItem(index, item);
		}

		// Token: 0x040014D1 RID: 5329
		private Dictionary<string, RouteBase> _namedMap = new Dictionary<string, RouteBase>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040014D2 RID: 5330
		private VirtualPathProvider _vpp;

		// Token: 0x040014D3 RID: 5331
		private ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();

		// Token: 0x02000909 RID: 2313
		private class ReadLockDisposable : IDisposable
		{
			// Token: 0x060068E5 RID: 26853 RVA: 0x00175EBC File Offset: 0x001740BC
			public ReadLockDisposable(ReaderWriterLockSlim rwLock)
			{
				this._rwLock = rwLock;
			}

			// Token: 0x060068E6 RID: 26854 RVA: 0x00175ECB File Offset: 0x001740CB
			void IDisposable.Dispose()
			{
				this._rwLock.ExitReadLock();
			}

			// Token: 0x04003716 RID: 14102
			private ReaderWriterLockSlim _rwLock;
		}

		// Token: 0x0200090A RID: 2314
		private class WriteLockDisposable : IDisposable
		{
			// Token: 0x060068E7 RID: 26855 RVA: 0x00175ED8 File Offset: 0x001740D8
			public WriteLockDisposable(ReaderWriterLockSlim rwLock)
			{
				this._rwLock = rwLock;
			}

			// Token: 0x060068E8 RID: 26856 RVA: 0x00175EE7 File Offset: 0x001740E7
			void IDisposable.Dispose()
			{
				this._rwLock.ExitWriteLock();
			}

			// Token: 0x04003717 RID: 14103
			private ReaderWriterLockSlim _rwLock;
		}

		// Token: 0x0200090B RID: 2315
		private sealed class IgnoreRouteInternal : Route
		{
			// Token: 0x060068E9 RID: 26857 RVA: 0x00175EF4 File Offset: 0x001740F4
			public IgnoreRouteInternal(string url) : base(url, new StopRoutingHandler())
			{
			}

			// Token: 0x060068EA RID: 26858 RVA: 0x0000298D File Offset: 0x00000B8D
			public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary routeValues)
			{
				return null;
			}
		}
	}
}
