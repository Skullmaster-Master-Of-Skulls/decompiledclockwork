using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Optimization.Resources;

namespace System.Web.Optimization
{
	// Token: 0x02000008 RID: 8
	internal sealed class AssetManager
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000023C0 File Offset: 0x000005C0
		public AssetManager(HttpContextBase context)
		{
			this._httpContext = context;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000023CF File Offset: 0x000005CF
		internal HttpContextBase Context
		{
			get
			{
				return this._httpContext;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000023E0 File Offset: 0x000005E0
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002409 File Offset: 0x00000609
		internal Func<string, string, string> ResolveUrlMethod
		{
			get
			{
				Func<string, string, string> result;
				if ((result = this._resolveUrlMethod) == null)
				{
					result = ((string basePath, string relativePath) => UrlUtil.Url(basePath, relativePath));
				}
				return result;
			}
			set
			{
				this._resolveUrlMethod = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002412 File Offset: 0x00000612
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002423 File Offset: 0x00000623
		internal IBundleResolver Resolver
		{
			get
			{
				return this._resolver ?? BundleResolver.Current;
			}
			set
			{
				this._resolver = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000242C File Offset: 0x0000062C
		// (set) Token: 0x06000023 RID: 35 RVA: 0x0000243D File Offset: 0x0000063D
		internal BundleCollection Bundles
		{
			get
			{
				return this._bundles ?? BundleTable.Bundles;
			}
			set
			{
				this._bundles = value;
				this._bundles.Context = this.Context;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002457 File Offset: 0x00000657
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002477 File Offset: 0x00000677
		internal bool OptimizationEnabled
		{
			get
			{
				if (this._optimizationEnabled != null)
				{
					return this._optimizationEnabled.Value;
				}
				return BundleTable.EnableOptimizations;
			}
			set
			{
				this._optimizationEnabled = new bool?(value);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002488 File Offset: 0x00000688
		public static AssetManager GetInstance(HttpContextBase context)
		{
			if (context == null)
			{
				return null;
			}
			AssetManager assetManager = (AssetManager)context.Items[AssetManager.AssetsManagerKey];
			if (assetManager == null)
			{
				assetManager = new AssetManager(context);
				context.Items[AssetManager.AssetsManagerKey] = assetManager;
			}
			return assetManager;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024F4 File Offset: 0x000006F4
		private IEnumerable<AssetManager.AssetTag> EliminateDuplicatesAndResolveUrls(IEnumerable<AssetManager.AssetTag> refs)
		{
			List<AssetManager.AssetTag> list = new List<AssetManager.AssetTag>();
			HashSet<string> hashSet = new HashSet<string>();
			HashSet<string> bundledContents = new HashSet<string>();
			IBundleResolver resolver = this.Resolver;
			foreach (AssetManager.AssetTag assetTag in refs)
			{
				if (assetTag.IsStaticAsset)
				{
					list.Add(assetTag);
				}
				else
				{
					string value = assetTag.Value;
					if (!hashSet.Contains(value))
					{
						if (resolver.IsBundleVirtualPath(value))
						{
							IEnumerable<string> bundleContents = resolver.GetBundleContents(value);
							foreach (string virtualPath in bundleContents)
							{
								bundledContents.Add(this.ResolveVirtualPath(virtualPath));
							}
							assetTag.Value = resolver.GetBundleUrl(value);
							list.Add(assetTag);
						}
						else
						{
							string text = this.ResolveVirtualPath(value);
							if (!hashSet.Contains(text))
							{
								hashSet.Add(text);
								assetTag.Value = text;
								list.Add(assetTag);
							}
						}
						hashSet.Add(value);
					}
				}
			}
			return from asset in list
			where asset.IsStaticAsset || !bundledContents.Contains(asset.Value)
			select asset;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002650 File Offset: 0x00000850
		private IEnumerable<AssetManager.AssetTag> DeterminePathsToRender(IEnumerable<string> assets)
		{
			List<AssetManager.AssetTag> list = new List<AssetManager.AssetTag>();
			foreach (string text in assets)
			{
				if (this.Resolver.IsBundleVirtualPath(text))
				{
					if (!this.OptimizationEnabled)
					{
						IEnumerable<string> bundleContents = this.Resolver.GetBundleContents(text);
						using (IEnumerator<string> enumerator2 = bundleContents.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								string value = enumerator2.Current;
								list.Add(new AssetManager.AssetTag(value));
							}
							continue;
						}
					}
					list.Add(new AssetManager.AssetTag(text));
					if (this.Bundles.UseCdn)
					{
						Bundle bundleFor = this.Bundles.GetBundleFor(text);
						if (bundleFor != null && !string.IsNullOrEmpty(bundleFor.CdnPath) && !string.IsNullOrEmpty(bundleFor.CdnFallbackExpression))
						{
							list.Add(new AssetManager.AssetTag(string.Format(CultureInfo.InvariantCulture, OptimizationResources.CdnFallBackScriptString, new object[]
							{
								bundleFor.CdnFallbackExpression,
								this.ResolveVirtualPath(text)
							}))
							{
								IsStaticAsset = true
							});
						}
					}
				}
				else
				{
					list.Add(new AssetManager.AssetTag(text));
				}
			}
			return this.EliminateDuplicatesAndResolveUrls(list);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000027CC File Offset: 0x000009CC
		public IHtmlString RenderExplicit(string tagFormat, params string[] paths)
		{
			IEnumerable<AssetManager.AssetTag> enumerable = this.DeterminePathsToRender(paths);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (AssetManager.AssetTag assetTag in enumerable)
			{
				stringBuilder.Append(assetTag.Render(tagFormat));
				stringBuilder.Append(Environment.NewLine);
			}
			return new HtmlString(stringBuilder.ToString());
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002840 File Offset: 0x00000A40
		internal string ResolveVirtualPath(string virtualPath)
		{
			Uri uri;
			if (Uri.TryCreate(virtualPath, UriKind.Absolute, out uri))
			{
				return virtualPath;
			}
			string arg = "";
			if (this._httpContext.Request != null)
			{
				arg = this._httpContext.Request.AppRelativeCurrentExecutionFilePath;
			}
			return this.ResolveUrlMethod(arg, virtualPath);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000288B File Offset: 0x00000A8B
		internal HtmlString ResolveUrl(string url)
		{
			if (this.Resolver.IsBundleVirtualPath(url))
			{
				return new HtmlString(this.Bundles.ResolveBundleUrl(url));
			}
			return new HtmlString(this.ResolveVirtualPath(url));
		}

		// Token: 0x04000008 RID: 8
		internal static readonly object AssetsManagerKey = typeof(AssetManager);

		// Token: 0x04000009 RID: 9
		private readonly HttpContextBase _httpContext;

		// Token: 0x0400000A RID: 10
		private Func<string, string, string> _resolveUrlMethod;

		// Token: 0x0400000B RID: 11
		private IBundleResolver _resolver;

		// Token: 0x0400000C RID: 12
		private BundleCollection _bundles;

		// Token: 0x0400000D RID: 13
		private bool? _optimizationEnabled;

		// Token: 0x02000009 RID: 9
		internal class AssetTag
		{
			// Token: 0x1700000B RID: 11
			// (get) Token: 0x0600002E RID: 46 RVA: 0x000028CA File Offset: 0x00000ACA
			// (set) Token: 0x0600002F RID: 47 RVA: 0x000028D2 File Offset: 0x00000AD2
			public string Value { get; set; }

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000030 RID: 48 RVA: 0x000028DB File Offset: 0x00000ADB
			// (set) Token: 0x06000031 RID: 49 RVA: 0x000028E3 File Offset: 0x00000AE3
			public bool IsStaticAsset { get; set; }

			// Token: 0x06000032 RID: 50 RVA: 0x000028EC File Offset: 0x00000AEC
			public AssetTag(string value)
			{
				this.Value = value;
			}

			// Token: 0x06000033 RID: 51 RVA: 0x000028FC File Offset: 0x00000AFC
			public string Render(string tagFormat)
			{
				if (this.IsStaticAsset)
				{
					return this.Value;
				}
				return string.Format(CultureInfo.InvariantCulture, tagFormat, new object[]
				{
					HttpUtility.UrlPathEncode(this.Value)
				});
			}
		}
	}
}
