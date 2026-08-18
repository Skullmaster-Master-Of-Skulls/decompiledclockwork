using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Hosting;
using System.Web.Optimization.Resources;

namespace System.Web.Optimization
{
	// Token: 0x02000024 RID: 36
	internal class ItemRegistry : List<BundleItem>
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00004D2F File Offset: 0x00002F2F
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00004D40 File Offset: 0x00002F40
		internal VirtualPathProvider VirtualPathProvider
		{
			get
			{
				return this._vpp ?? BundleTable.VirtualPathProvider;
			}
			set
			{
				this._vpp = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00004D49 File Offset: 0x00002F49
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00004D51 File Offset: 0x00002F51
		internal Bundle Bundle { get; set; }

		// Token: 0x06000132 RID: 306 RVA: 0x00004D64 File Offset: 0x00002F64
		internal Exception Include(params string[] virtualPaths)
		{
			foreach (string virtualPath in virtualPaths)
			{
				Exception ex = this.IncludePath(virtualPath, null);
				if (ex != null)
				{
					return ex;
				}
			}
			return null;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004DA0 File Offset: 0x00002FA0
		internal Exception IncludePath(string virtualPath, params IItemTransform[] transforms)
		{
			Exception ex = ExceptionUtil.ValidateVirtualPath(virtualPath, "virtualPath");
			if (ex != null)
			{
				return ex;
			}
			if (virtualPath.Contains('*') || virtualPath.Contains('{'))
			{
				int num = virtualPath.LastIndexOf('/');
				string text = virtualPath.Substring(0, num + 1);
				if (text.Contains('*'))
				{
					return new ArgumentException(string.Format(CultureInfo.CurrentCulture, OptimizationResources.InvalidPattern, new object[]
					{
						virtualPath
					}), "virtualPath");
				}
				string text2 = "";
				if (num < virtualPath.Length - 1)
				{
					text2 = virtualPath.Substring(num + 1);
				}
				PatternType patternType = PatternHelper.GetPatternType(text2);
				ex = PatternHelper.ValidatePattern(patternType, text2, "virtualPath");
				if (ex != null)
				{
					return ex;
				}
				ex = this.IncludeDirectory(text, text2, patternType, false, transforms);
				if (ex != null)
				{
					return ex;
				}
			}
			else if (this.VirtualPathProvider == null || this.VirtualPathProvider.FileExists(virtualPath))
			{
				base.Add(new BundleItem(virtualPath, transforms));
			}
			return null;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004E88 File Offset: 0x00003088
		internal Exception IncludeDirectory(string directoryVirtualPath, string searchPattern, PatternType patternType, bool searchSubdirectories, params IItemTransform[] transforms)
		{
			Exception ex = ExceptionUtil.ValidateVirtualPath(directoryVirtualPath, "directoryVirtualPath");
			if (ex != null)
			{
				return ex;
			}
			if (ExceptionUtil.IsPureWildcardSearchPattern(searchPattern))
			{
				return new ArgumentException(OptimizationResources.InvalidWildcardSearchPattern, "searchPattern");
			}
			if (this.VirtualPathProvider == null || this.VirtualPathProvider.DirectoryExists(directoryVirtualPath))
			{
				base.Add(new BundleDirectoryItem(directoryVirtualPath, searchPattern, patternType, searchSubdirectories, transforms));
				return null;
			}
			return new ArgumentException(OptimizationResources.BundleDirectory_does_not_exist, "directoryVirtualPath");
		}

		// Token: 0x04000060 RID: 96
		private VirtualPathProvider _vpp;
	}
}
