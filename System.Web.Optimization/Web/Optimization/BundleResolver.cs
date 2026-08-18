using System;
using System.Collections.Generic;

namespace System.Web.Optimization
{
	// Token: 0x02000011 RID: 17
	public class BundleResolver : IBundleResolver
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003B45 File Offset: 0x00001D45
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00003B55 File Offset: 0x00001D55
		public static IBundleResolver Current
		{
			get
			{
				return BundleResolver._current ?? BundleResolver._default;
			}
			set
			{
				BundleResolver._current = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003B5D File Offset: 0x00001D5D
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00003B65 File Offset: 0x00001D65
		private BundleCollection Bundles { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003B6E File Offset: 0x00001D6E
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00003B84 File Offset: 0x00001D84
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

		// Token: 0x060000A2 RID: 162 RVA: 0x00003B8D File Offset: 0x00001D8D
		public BundleResolver() : this(BundleTable.Bundles)
		{
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003B9A File Offset: 0x00001D9A
		public BundleResolver(BundleCollection bundles) : this(bundles, null)
		{
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003BA4 File Offset: 0x00001DA4
		public BundleResolver(BundleCollection bundles, HttpContextBase context)
		{
			this.Bundles = bundles;
			this.Context = context;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003BBA File Offset: 0x00001DBA
		public bool IsBundleVirtualPath(string virtualPath)
		{
			return ExceptionUtil.ValidateVirtualPath(virtualPath, "virtualPath") == null && this.Bundles.GetBundleFor(virtualPath) != null;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003BE0 File Offset: 0x00001DE0
		public IEnumerable<string> GetBundleContents(string virtualPath)
		{
			if (ExceptionUtil.ValidateVirtualPath(virtualPath, "virtualPath") != null)
			{
				return null;
			}
			Bundle bundleFor = this.Bundles.GetBundleFor(virtualPath);
			if (bundleFor == null)
			{
				return null;
			}
			List<string> list = new List<string>();
			BundleContext context = new BundleContext(this.Context, this.Bundles, virtualPath);
			BundleResponse bundleResponse = bundleFor.GetBundleResponse(context);
			foreach (BundleFile bundleFile in bundleResponse.Files)
			{
				list.Add(bundleFile.IncludedVirtualPath);
			}
			return list;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003C7C File Offset: 0x00001E7C
		public string GetBundleUrl(string virtualPath)
		{
			if (ExceptionUtil.ValidateVirtualPath(virtualPath, "virtualPath") != null)
			{
				return null;
			}
			return this.Bundles.ResolveBundleUrl(virtualPath);
		}

		// Token: 0x04000030 RID: 48
		private static BundleResolver _default = new BundleResolver();

		// Token: 0x04000031 RID: 49
		private static IBundleResolver _current;

		// Token: 0x04000032 RID: 50
		private HttpContextBase _context;
	}
}
