using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Optimization.Resources;

namespace System.Web.Optimization
{
	// Token: 0x0200000A RID: 10
	public class Bundle
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002939 File Offset: 0x00000B39
		protected Bundle()
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002960 File Offset: 0x00000B60
		public Bundle(string virtualPath, string cdnPath, params IBundleTransform[] transforms)
		{
			this.CdnPath = cdnPath;
			this.Path = virtualPath;
			if (!virtualPath.StartsWith("~/", StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, OptimizationResources.UrlMappings_only_app_relative_url_allowed, new object[]
				{
					virtualPath
				}), "virtualPath");
			}
			if (transforms != null)
			{
				foreach (IBundleTransform item in transforms)
				{
					this._transforms.Add(item);
				}
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000029F5 File Offset: 0x00000BF5
		public Bundle(string virtualPath, params IBundleTransform[] transforms) : this(virtualPath, null, transforms)
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A00 File Offset: 0x00000C00
		public Bundle(string virtualPath) : this(virtualPath, null, null)
		{
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002A0B File Offset: 0x00000C0B
		public Bundle(string virtualPath, string cdnPath) : this(virtualPath, cdnPath, null)
		{
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002A16 File Offset: 0x00000C16
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002A1E File Offset: 0x00000C1E
		public string Path
		{
			get
			{
				return this._path;
			}
			protected set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw ExceptionUtil.PropertyNullOrEmpty("Path");
				}
				this._path = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002A3A File Offset: 0x00000C3A
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002A42 File Offset: 0x00000C42
		public string CdnPath { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002A4B File Offset: 0x00000C4B
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002A53 File Offset: 0x00000C53
		public virtual string CdnFallbackExpression { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002A5C File Offset: 0x00000C5C
		public IList<IBundleTransform> Transforms
		{
			get
			{
				return this._transforms;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002A64 File Offset: 0x00000C64
		internal ItemRegistry Items
		{
			get
			{
				if (this._items == null)
				{
					this._items = new ItemRegistry();
				}
				return this._items;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002A7F File Offset: 0x00000C7F
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002A95 File Offset: 0x00000C95
		public virtual IBundleOrderer Orderer
		{
			get
			{
				if (this._orderer == null)
				{
					return DefaultBundleOrderer.Instance;
				}
				return this._orderer;
			}
			set
			{
				this._orderer = value;
				this.InvalidateCacheEntries();
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002AA4 File Offset: 0x00000CA4
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002ABA File Offset: 0x00000CBA
		public virtual IBundleBuilder Builder
		{
			get
			{
				if (this._builder == null)
				{
					return DefaultBundleBuilder.Instance;
				}
				return this._builder;
			}
			set
			{
				this._builder = value;
				this.InvalidateCacheEntries();
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002AC9 File Offset: 0x00000CC9
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002AD1 File Offset: 0x00000CD1
		public virtual bool EnableFileExtensionReplacements
		{
			get
			{
				return this._enableReplacements;
			}
			set
			{
				this._enableReplacements = value;
				this.InvalidateCacheEntries();
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002AE0 File Offset: 0x00000CE0
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public string ConcatenationToken { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002AF1 File Offset: 0x00000CF1
		internal IList<string> CacheKeys
		{
			get
			{
				return this._cacheKeys;
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002AFC File Offset: 0x00000CFC
		public virtual IEnumerable<BundleFile> EnumerateFiles(BundleContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			List<BundleFile> list = new List<BundleFile>();
			foreach (BundleItem bundleItem in this.Items)
			{
				bundleItem.AddFiles(list, context);
			}
			return list;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002B68 File Offset: 0x00000D68
		public virtual BundleResponse ApplyTransforms(BundleContext context, string bundleContent, IEnumerable<BundleFile> bundleFiles)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			BundleResponse bundleResponse = new BundleResponse(bundleContent, bundleFiles);
			if (this.Transforms != null && this.Transforms.Count > 0)
			{
				using (IEnumerator<IBundleTransform> enumerator = this.Transforms.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IBundleTransform bundleTransform = enumerator.Current;
						bundleTransform.Process(context, bundleResponse);
					}
					return bundleResponse;
				}
			}
			DefaultTransform.Instance.Process(context, bundleResponse);
			return bundleResponse;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002BF0 File Offset: 0x00000DF0
		public virtual BundleResponse GenerateBundleResponse(BundleContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			IEnumerable<BundleFile> enumerable = this.EnumerateFiles(context);
			enumerable = context.BundleCollection.IgnoreList.FilterIgnoredFiles(context, enumerable);
			enumerable = this.Orderer.OrderFiles(context, enumerable);
			if (this.EnableFileExtensionReplacements)
			{
				enumerable = context.BundleCollection.FileExtensionReplacementList.ReplaceFileExtensions(context, enumerable);
			}
			string bundleContent = this.Builder.BuildBundleContent(this, context, enumerable);
			return this.ApplyTransforms(context, bundleContent, enumerable);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C68 File Offset: 0x00000E68
		public virtual Bundle Include(params string[] virtualPaths)
		{
			Exception ex = this.Items.Include(virtualPaths);
			if (ex != null)
			{
				throw ex;
			}
			this.InvalidateCacheEntries();
			return this;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002C90 File Offset: 0x00000E90
		public virtual Bundle Include(string virtualPath, params IItemTransform[] transforms)
		{
			Exception ex = this.Items.IncludePath(virtualPath, transforms);
			if (ex != null)
			{
				throw ex;
			}
			this.InvalidateCacheEntries();
			return this;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002CB7 File Offset: 0x00000EB7
		public virtual Bundle IncludeDirectory(string directoryVirtualPath, string searchPattern)
		{
			return this.IncludeDirectory(directoryVirtualPath, searchPattern, false);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002CC4 File Offset: 0x00000EC4
		public virtual Bundle IncludeDirectory(string directoryVirtualPath, string searchPattern, bool searchSubdirectories)
		{
			if (ExceptionUtil.IsPureWildcardSearchPattern(searchPattern))
			{
				throw new ArgumentException(OptimizationResources.InvalidWildcardSearchPattern, "searchPattern");
			}
			PatternType patternType = PatternHelper.GetPatternType(searchPattern);
			Exception ex = PatternHelper.ValidatePattern(patternType, searchPattern, "virtualPaths");
			if (ex != null)
			{
				throw ex;
			}
			ex = this.Items.IncludeDirectory(directoryVirtualPath, searchPattern, patternType, searchSubdirectories, new IItemTransform[0]);
			if (ex != null)
			{
				throw ex;
			}
			return this;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002D20 File Offset: 0x00000F20
		internal static bool GetInstrumentationMode(HttpContextBase context)
		{
			if (context == null || context.Request == null)
			{
				return false;
			}
			string userAgent = context.Request.UserAgent;
			return !string.IsNullOrEmpty(userAgent) && Regex.IsMatch(userAgent, "Eureka/(?<version>[\\d\\.]+)");
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002D60 File Offset: 0x00000F60
		internal void ProcessRequest(BundleContext context)
		{
			context.EnableInstrumentation = Bundle.GetInstrumentationMode(context.HttpContext);
			BundleResponse bundleResponse = this.GetBundleResponse(context);
			bool flag = false;
			HttpRequestBase request = context.HttpContext.Request;
			if (request != null)
			{
				string text = request.QueryString.Get("v");
				if (text != null && bundleResponse.GetContentHashCode() != text)
				{
					flag = true;
				}
			}
			string text2 = request.Headers["If-Modified-Since"];
			DateTimeOffset right;
			if (!flag && !context.EnableInstrumentation && !string.IsNullOrEmpty(text2) && DateTimeOffset.TryParse(text2, out right) && bundleResponse.CreationDate < right)
			{
				context.HttpContext.Response.StatusCode = 304;
				return;
			}
			Bundle.SetHeaders(bundleResponse, context, flag);
			context.HttpContext.Response.Write(bundleResponse.Content);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002E30 File Offset: 0x00001030
		internal BundleResponse GetBundleResponse(BundleContext context)
		{
			BundleResponse bundleResponse = this.CacheLookup(context);
			if (bundleResponse == null || context.EnableInstrumentation)
			{
				bundleResponse = this.GenerateBundleResponse(context);
				this.UpdateCache(context, bundleResponse);
			}
			return bundleResponse;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002E64 File Offset: 0x00001064
		internal string GetBundleUrl(BundleContext context, bool includeContentHash = true)
		{
			string text = context.BundleVirtualPath;
			if (includeContentHash)
			{
				BundleResponse bundleResponse = this.GetBundleResponse(context);
				text = text + "?v=" + bundleResponse.GetContentHashCode();
			}
			return AssetManager.GetInstance(context.HttpContext).ResolveVirtualPath(text);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002EA6 File Offset: 0x000010A6
		public virtual string GetCacheKey(BundleContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			return "System.Web.Optimization.Bundle:" + context.BundleVirtualPath;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002EC8 File Offset: 0x000010C8
		public virtual BundleResponse CacheLookup(BundleContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			IBundleCache cache = context.BundleCollection.Cache;
			if (cache.IsEnabled(context))
			{
				BundleResponse bundleResponse = cache.Get(context, this);
				if (bundleResponse != null)
				{
					return bundleResponse;
				}
			}
			return null;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002F08 File Offset: 0x00001108
		public virtual void UpdateCache(BundleContext context, BundleResponse response)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			IBundleCache cache = context.BundleCollection.Cache;
			if (cache.IsEnabled(context))
			{
				cache.Put(context, this, response);
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002F50 File Offset: 0x00001150
		internal void InvalidateCacheEntries()
		{
			if (HttpContext.Current != null && HttpContext.Current.Cache != null)
			{
				List<string> list = new List<string>(this._cacheKeys);
				this._cacheKeys.Clear();
				foreach (string key in list)
				{
					HttpContext.Current.Cache.Remove(key);
				}
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002FD4 File Offset: 0x000011D4
		private static void SetHeaders(BundleResponse response, BundleContext context, bool noCache)
		{
			if (context.HttpContext.Response != null)
			{
				if (response.ContentType != null)
				{
					context.HttpContext.Response.ContentType = response.ContentType;
				}
				if (!context.EnableInstrumentation && context.HttpContext.Response.Cache != null)
				{
					HttpCachePolicyBase cache = context.HttpContext.Response.Cache;
					if (noCache)
					{
						cache.SetCacheability(HttpCacheability.NoCache);
						return;
					}
					cache.SetCacheability(response.Cacheability);
					cache.SetOmitVaryStar(true);
					cache.SetExpires(DateTime.Now.AddYears(1));
					cache.SetValidUntilExpires(true);
					cache.SetLastModified(DateTime.Now);
					cache.VaryByHeaders["User-Agent"] = true;
				}
			}
		}

		// Token: 0x04000011 RID: 17
		private const string VersionQueryString = "v";

		// Token: 0x04000012 RID: 18
		private IBundleOrderer _orderer;

		// Token: 0x04000013 RID: 19
		private IBundleBuilder _builder;

		// Token: 0x04000014 RID: 20
		private string _path;

		// Token: 0x04000015 RID: 21
		private ItemRegistry _items;

		// Token: 0x04000016 RID: 22
		private List<string> _cacheKeys = new List<string>();

		// Token: 0x04000017 RID: 23
		private bool _enableReplacements = true;

		// Token: 0x04000018 RID: 24
		private IList<IBundleTransform> _transforms = new List<IBundleTransform>();
	}
}
