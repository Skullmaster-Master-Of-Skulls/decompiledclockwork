using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Web.Optimization
{
	// Token: 0x0200000B RID: 11
	public class BundleCollection : IEnumerable<Bundle>, IEnumerable
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00003090 File Offset: 0x00001290
		public BundleCollection()
		{
			BundleCollection.AddDefaultFileExtensionReplacements(this.FileExtensionReplacementList);
			BundleCollection.AddDefaultFileOrderings(this.FileSetOrderList);
			BundleCollection.AddDefaultIgnorePatterns(this.DirectoryFilter);
			this.Cache = new HttpContextCache();
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000312B File Offset: 0x0000132B
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00003133 File Offset: 0x00001333
		internal IBundleCache Cache { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000313C File Offset: 0x0000133C
		public IList<BundleFileSetOrdering> FileSetOrderList
		{
			get
			{
				return this._orderPriority;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003144 File Offset: 0x00001344
		public IgnoreList IgnoreList
		{
			get
			{
				return this._ignoreList;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000314C File Offset: 0x0000134C
		public IgnoreList DirectoryFilter
		{
			get
			{
				return this._directoryFilter;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003154 File Offset: 0x00001354
		// (set) Token: 0x06000061 RID: 97 RVA: 0x0000315C File Offset: 0x0000135C
		public FileExtensionReplacementList FileExtensionReplacementList
		{
			get
			{
				return this._replacementList;
			}
			set
			{
				this._replacementList = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003165 File Offset: 0x00001365
		internal Dictionary<string, DynamicFolderBundle> DynamicBundles
		{
			get
			{
				return this._dynamicBundles;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000063 RID: 99 RVA: 0x0000316D File Offset: 0x0000136D
		internal Dictionary<string, Bundle> StaticBundles
		{
			get
			{
				return this._staticBundles;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003175 File Offset: 0x00001375
		// (set) Token: 0x06000065 RID: 101 RVA: 0x0000318B File Offset: 0x0000138B
		internal HttpContextBase Context
		{
			get
			{
				return this._context ?? new HttpContextWrapper(HttpContext.Current);
			}
			set
			{
				this._context = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003194 File Offset: 0x00001394
		// (set) Token: 0x06000067 RID: 103 RVA: 0x0000319C File Offset: 0x0000139C
		public bool UseCdn { get; set; }

		// Token: 0x06000068 RID: 104 RVA: 0x000031A8 File Offset: 0x000013A8
		public void Add(Bundle bundle)
		{
			if (bundle == null)
			{
				throw new ArgumentNullException("bundle");
			}
			string path = bundle.Path;
			Bundle bundle2 = null;
			DynamicFolderBundle dynamicFolderBundle = bundle as DynamicFolderBundle;
			if (dynamicFolderBundle != null)
			{
				if (this.DynamicBundles.ContainsKey(path))
				{
					bundle2 = this.DynamicBundles[path];
				}
				this.DynamicBundles[path] = dynamicFolderBundle;
			}
			else
			{
				bundle2 = this.GetBundleFor(path);
				this.StaticBundles[path] = bundle;
			}
			if (bundle2 != null)
			{
				bundle2.InvalidateCacheEntries();
			}
			this._bundles[path] = bundle;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000322C File Offset: 0x0000142C
		public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
		{
			if (ignoreList == null)
			{
				throw new ArgumentNullException("ignoreList");
			}
			ignoreList.Ignore("*.intellisense.js");
			ignoreList.Ignore("*-vsdoc.js");
			ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
			ignoreList.Ignore("*.min.js", OptimizationMode.WhenDisabled);
			ignoreList.Ignore("*.min.css", OptimizationMode.WhenDisabled);
			ignoreList.Ignore("*.map");
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000328C File Offset: 0x0000148C
		public static void AddDefaultFileOrderings(IList<BundleFileSetOrdering> list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			list.Add(new BundleFileSetOrdering("css")
			{
				Files = 
				{
					"reset.css",
					"normalize.css"
				}
			});
			list.Add(new BundleFileSetOrdering("jquery")
			{
				Files = 
				{
					"jquery.js",
					"jquery-min.js",
					"jquery-*",
					"jquery-ui*",
					"jquery.ui*",
					"jquery.unobtrusive*",
					"jquery.validate*"
				}
			});
			list.Add(new BundleFileSetOrdering("modernizr")
			{
				Files = 
				{
					"modernizr-*"
				}
			});
			list.Add(new BundleFileSetOrdering("dojo")
			{
				Files = 
				{
					"dojo.*"
				}
			});
			list.Add(new BundleFileSetOrdering("moo")
			{
				Files = 
				{
					"mootools-core*",
					"mootools-*"
				}
			});
			list.Add(new BundleFileSetOrdering("prototype")
			{
				Files = 
				{
					"prototype.js",
					"prototype-*",
					"scriptaculous-*"
				}
			});
			list.Add(new BundleFileSetOrdering("ext")
			{
				Files = 
				{
					"ext.js",
					"ext-*"
				}
			});
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003452 File Offset: 0x00001652
		public static void AddDefaultFileExtensionReplacements(FileExtensionReplacementList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			list.Add("min", OptimizationMode.WhenEnabled);
			list.Add("debug", OptimizationMode.WhenDisabled);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000347A File Offset: 0x0000167A
		public string ResolveBundleUrl(string bundleVirtualPath)
		{
			return this.ResolveBundleUrl(bundleVirtualPath, true);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003484 File Offset: 0x00001684
		public string ResolveBundleUrl(string bundleVirtualPath, bool includeContentHash)
		{
			Exception ex = ExceptionUtil.ValidateVirtualPath(bundleVirtualPath, "bundleVirtualPath");
			if (ex != null)
			{
				throw ex;
			}
			Bundle bundleFor = this.GetBundleFor(bundleVirtualPath);
			if (bundleFor == null)
			{
				return null;
			}
			if (this.UseCdn && !string.IsNullOrEmpty(bundleFor.CdnPath))
			{
				return bundleFor.CdnPath;
			}
			return bundleFor.GetBundleUrl(new BundleContext(this.Context, this, bundleVirtualPath), includeContentHash);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000034E0 File Offset: 0x000016E0
		public Bundle GetBundleFor(string bundleVirtualPath)
		{
			Exception ex = ExceptionUtil.ValidateVirtualPath(bundleVirtualPath, "bundleVirtualPath");
			if (ex != null)
			{
				throw ex;
			}
			if (this.StaticBundles.ContainsKey(bundleVirtualPath))
			{
				return this.StaticBundles[bundleVirtualPath];
			}
			if (this.DynamicBundles.Count > 0)
			{
				bundleVirtualPath = bundleVirtualPath.Replace("\\", "/");
				int num = bundleVirtualPath.LastIndexOf("/", StringComparison.Ordinal);
				string key = bundleVirtualPath.Substring(num + 1);
				if (this.DynamicBundles.ContainsKey(key))
				{
					return this.DynamicBundles[key];
				}
			}
			return null;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000356C File Offset: 0x0000176C
		public void Clear()
		{
			this._bundles.Clear();
			this.DynamicBundles.Clear();
			this.StaticBundles.Clear();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000358F File Offset: 0x0000178F
		public void ResetAll()
		{
			this.Clear();
			this.FileExtensionReplacementList.Clear();
			this.IgnoreList.Clear();
			this.DirectoryFilter.Clear();
			this.FileSetOrderList.Clear();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000035C4 File Offset: 0x000017C4
		public bool Remove(Bundle bundle)
		{
			if (bundle == null)
			{
				throw new ArgumentNullException("bundle");
			}
			bool flag = this._bundles.Remove(bundle.Path);
			if (flag)
			{
				if (bundle is DynamicFolderBundle)
				{
					this.DynamicBundles.Remove(bundle.Path);
				}
				else
				{
					this.StaticBundles.Remove(bundle.Path);
				}
			}
			return flag;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003623 File Offset: 0x00001823
		public int Count
		{
			get
			{
				return this._bundles.Count;
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003630 File Offset: 0x00001830
		public ReadOnlyCollection<Bundle> GetRegisteredBundles()
		{
			return new ReadOnlyCollection<Bundle>(new List<Bundle>(this._bundles.Values));
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003647 File Offset: 0x00001847
		protected virtual IEnumerator<Bundle> GetEnumerator()
		{
			return this._bundles.Values.GetEnumerator();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000365E File Offset: 0x0000185E
		IEnumerator<Bundle> IEnumerable<Bundle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003666 File Offset: 0x00001866
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400001C RID: 28
		private Dictionary<string, Bundle> _bundles = new Dictionary<string, Bundle>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400001D RID: 29
		private Dictionary<string, DynamicFolderBundle> _dynamicBundles = new Dictionary<string, DynamicFolderBundle>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400001E RID: 30
		private Dictionary<string, Bundle> _staticBundles = new Dictionary<string, Bundle>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400001F RID: 31
		private List<BundleFileSetOrdering> _orderPriority = new List<BundleFileSetOrdering>();

		// Token: 0x04000020 RID: 32
		private IgnoreList _ignoreList = new IgnoreList();

		// Token: 0x04000021 RID: 33
		private IgnoreList _directoryFilter = new IgnoreList();

		// Token: 0x04000022 RID: 34
		private FileExtensionReplacementList _replacementList = new FileExtensionReplacementList();

		// Token: 0x04000023 RID: 35
		private HttpContextBase _context;
	}
}
