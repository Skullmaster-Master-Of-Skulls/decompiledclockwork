using System;
using System.Collections.Generic;
using System.Web.Hosting;

namespace System.Web.Optimization
{
	// Token: 0x02000021 RID: 33
	public class BundleContext
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600010A RID: 266 RVA: 0x000047E4 File Offset: 0x000029E4
		// (set) Token: 0x0600010B RID: 267 RVA: 0x000047EC File Offset: 0x000029EC
		public HttpContextBase HttpContext { get; internal set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600010C RID: 268 RVA: 0x000047F5 File Offset: 0x000029F5
		// (set) Token: 0x0600010D RID: 269 RVA: 0x000047FD File Offset: 0x000029FD
		public BundleCollection BundleCollection { get; internal set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00004806 File Offset: 0x00002A06
		// (set) Token: 0x0600010F RID: 271 RVA: 0x0000480E File Offset: 0x00002A0E
		public string BundleVirtualPath { get; internal set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00004817 File Offset: 0x00002A17
		// (set) Token: 0x06000111 RID: 273 RVA: 0x0000481F File Offset: 0x00002A1F
		public bool EnableInstrumentation { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00004828 File Offset: 0x00002A28
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00004830 File Offset: 0x00002A30
		public bool EnableOptimizations { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00004839 File Offset: 0x00002A39
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00004841 File Offset: 0x00002A41
		internal VirtualPathProvider VirtualPathProvider { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000116 RID: 278 RVA: 0x0000484C File Offset: 0x00002A4C
		// (set) Token: 0x06000117 RID: 279 RVA: 0x000048C9 File Offset: 0x00002AC9
		public bool UseServerCache
		{
			get
			{
				if (this._useServerCache != null)
				{
					return this._useServerCache.Value;
				}
				return this.HttpContext != null && this.HttpContext.Cache != null && !this.EnableInstrumentation && this.HttpContext.ApplicationInstance != null && string.Equals(this.HttpContext.ApplicationInstance.GetOutputCacheProviderName(this.HttpContext.ApplicationInstance.Context), "AspNetInternalProvider", StringComparison.Ordinal);
			}
			set
			{
				this._useServerCache = new bool?(value);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000048D7 File Offset: 0x00002AD7
		internal HashSet<string> CacheDependencyDirectories
		{
			get
			{
				return this._cacheDependencyDirectories;
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000048E0 File Offset: 0x00002AE0
		public BundleContext(HttpContextBase context, BundleCollection collection, string bundleVirtualPath)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (bundleVirtualPath == null)
			{
				throw new ArgumentNullException("bundleVirtualPath");
			}
			this.HttpContext = context;
			this.BundleCollection = collection;
			this.BundleVirtualPath = bundleVirtualPath;
			this.VirtualPathProvider = BundleTable.VirtualPathProvider;
			this.EnableOptimizations = BundleTable.EnableOptimizations;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004953 File Offset: 0x00002B53
		internal BundleContext()
		{
		}

		// Token: 0x04000052 RID: 82
		private bool? _useServerCache;

		// Token: 0x04000053 RID: 83
		private HashSet<string> _cacheDependencyDirectories = new HashSet<string>();
	}
}
