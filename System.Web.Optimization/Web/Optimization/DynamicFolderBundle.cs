using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization.Resources;

namespace System.Web.Optimization
{
	// Token: 0x02000034 RID: 52
	public class DynamicFolderBundle : Bundle
	{
		// Token: 0x06000170 RID: 368 RVA: 0x00005C9C File Offset: 0x00003E9C
		public DynamicFolderBundle(string pathSuffix, string searchPattern) : this(pathSuffix, searchPattern, false, null)
		{
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005CA8 File Offset: 0x00003EA8
		public DynamicFolderBundle(string pathSuffix, string searchPattern, params IBundleTransform[] transforms) : this(pathSuffix, searchPattern, false, transforms)
		{
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005CB4 File Offset: 0x00003EB4
		public DynamicFolderBundle(string pathSuffix, string searchPattern, bool searchSubdirectories) : this(pathSuffix, searchPattern, searchSubdirectories, null)
		{
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005CC0 File Offset: 0x00003EC0
		public DynamicFolderBundle(string pathSuffix, string searchPattern, bool searchSubdirectories, params IBundleTransform[] transforms)
		{
			base.Path = pathSuffix;
			if (DynamicFolderBundle.IsInvalidRouteUrl(pathSuffix))
			{
				throw new ArgumentException(OptimizationResources.DynamicFolderBundle_InvalidPath, "pathSuffix");
			}
			if (transforms != null)
			{
				foreach (IBundleTransform item in transforms)
				{
					base.Transforms.Add(item);
				}
			}
			this.SearchPattern = searchPattern;
			this.SearchSubdirectories = searchSubdirectories;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00005D25 File Offset: 0x00003F25
		// (set) Token: 0x06000175 RID: 373 RVA: 0x00005D30 File Offset: 0x00003F30
		public string SearchPattern
		{
			get
			{
				return this._searchPattern;
			}
			set
			{
				if (ExceptionUtil.IsPureWildcardSearchPattern(value))
				{
					throw new ArgumentException(OptimizationResources.InvalidWildcardSearchPattern, "value");
				}
				PatternType patternType = PatternHelper.GetPatternType(value);
				Exception ex = PatternHelper.ValidatePattern(patternType, value, "value");
				if (ex != null)
				{
					throw ex;
				}
				this._searchPattern = value;
				this.PatternType = patternType;
				base.InvalidateCacheEntries();
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00005D82 File Offset: 0x00003F82
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00005D8A File Offset: 0x00003F8A
		public new string CdnPath
		{
			get
			{
				return base.CdnPath;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00005D91 File Offset: 0x00003F91
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00005D99 File Offset: 0x00003F99
		internal PatternType PatternType { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00005DA2 File Offset: 0x00003FA2
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00005DAA File Offset: 0x00003FAA
		public bool SearchSubdirectories
		{
			get
			{
				return this._searchSubdirectories;
			}
			set
			{
				this._searchSubdirectories = value;
				base.InvalidateCacheEntries();
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005DBC File Offset: 0x00003FBC
		public override IEnumerable<BundleFile> EnumerateFiles(BundleContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			List<BundleFile> list = new List<BundleFile>();
			list.AddRange(base.EnumerateFiles(context));
			string directory = VirtualPathUtility.GetDirectory(context.BundleVirtualPath);
			if (context.VirtualPathProvider == null || context.VirtualPathProvider.DirectoryExists(directory))
			{
				new BundleDirectoryItem(directory, this.SearchPattern, this.PatternType, this.SearchSubdirectories, null).AddFiles(list, context);
				return list;
			}
			throw new InvalidOperationException(OptimizationResources.BundleDirectory_does_not_exist);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005E3A File Offset: 0x0000403A
		private static bool IsInvalidRouteUrl(string routeUrl)
		{
			return routeUrl.StartsWith("~", StringComparison.Ordinal) || routeUrl.StartsWith("/", StringComparison.Ordinal) || routeUrl.Contains('?');
		}

		// Token: 0x0400007B RID: 123
		private string _searchPattern;

		// Token: 0x0400007C RID: 124
		private bool _searchSubdirectories;
	}
}
