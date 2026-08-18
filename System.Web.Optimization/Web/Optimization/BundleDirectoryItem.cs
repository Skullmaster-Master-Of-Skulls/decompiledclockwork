using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Hosting;

namespace System.Web.Optimization
{
	// Token: 0x02000023 RID: 35
	internal class BundleDirectoryItem : BundleItem
	{
		// Token: 0x06000121 RID: 289 RVA: 0x000049DC File Offset: 0x00002BDC
		public BundleDirectoryItem(string path, string searchPattern, PatternType patternType, bool searchSubdirectories, IList<IItemTransform> transforms) : base(path, transforms)
		{
			this.SearchPattern = searchPattern;
			this.PatternType = patternType;
			this.SearchSubdirectories = searchSubdirectories;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000049FD File Offset: 0x00002BFD
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00004A05 File Offset: 0x00002C05
		public string SearchPattern { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00004A0E File Offset: 0x00002C0E
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00004A16 File Offset: 0x00002C16
		public PatternType PatternType { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00004A1F File Offset: 0x00002C1F
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00004A27 File Offset: 0x00002C27
		public bool SearchSubdirectories { get; set; }

		// Token: 0x06000128 RID: 296 RVA: 0x00004A30 File Offset: 0x00002C30
		private static void AddAllSubdirectories(VirtualDirectory dir, BundleContext context)
		{
			context.CacheDependencyDirectories.Add(dir.VirtualPath);
			foreach (object obj in dir.Directories)
			{
				VirtualDirectory dir2 = (VirtualDirectory)obj;
				BundleDirectoryItem.AddAllSubdirectories(dir2, context);
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00004AE4 File Offset: 0x00002CE4
		public void ProcessDirectory(BundleContext context, string directoryVirtualPath, VirtualDirectory dirInfo, List<BundleFile> files)
		{
			Regex regEx;
			IEnumerable<VirtualFile> enumerable;
			switch (this.PatternType)
			{
			case PatternType.Exact:
				enumerable = from VirtualFile file in dirInfo.Files
				where string.Equals(file.Name, this.SearchPattern, StringComparison.OrdinalIgnoreCase)
				select file;
				goto IL_E4;
			case PatternType.All:
				enumerable = dirInfo.Files.Cast<VirtualFile>();
				goto IL_E4;
			case PatternType.Version:
				regEx = PatternHelper.BuildRegex(this.SearchPattern);
				enumerable = from VirtualFile file in dirInfo.Files
				where regEx.IsMatch(file.Name)
				select file;
				goto IL_E4;
			}
			regEx = PatternHelper.BuildWildcardRegex(this.SearchPattern);
			enumerable = from VirtualFile file in dirInfo.Files
			where regEx.IsMatch(file.Name)
			select file;
			IL_E4:
			enumerable = enumerable.OrderBy((VirtualFile file) => file, VirtualFileComparer.Instance);
			List<BundleFile> list = new List<BundleFile>();
			foreach (VirtualFile virtualFile in enumerable)
			{
				list.Add(new BundleFile(Path.Combine(directoryVirtualPath, virtualFile.Name), virtualFile, base.Transforms));
			}
			files.AddRange(context.BundleCollection.DirectoryFilter.FilterIgnoredFiles(context, list));
			if (this.SearchSubdirectories)
			{
				foreach (object obj in dirInfo.Directories)
				{
					VirtualDirectory virtualDirectory = (VirtualDirectory)obj;
					this.ProcessDirectory(context, Path.Combine(directoryVirtualPath, virtualDirectory.Name), virtualDirectory, files);
				}
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004CDC File Offset: 0x00002EDC
		public override void AddFiles(List<BundleFile> files, BundleContext context)
		{
			VirtualDirectory directory = context.VirtualPathProvider.GetDirectory(base.VirtualPath);
			this.ProcessDirectory(context, base.VirtualPath, directory, files);
			if (context != null)
			{
				if (this.SearchSubdirectories)
				{
					BundleDirectoryItem.AddAllSubdirectories(directory, context);
					return;
				}
				context.CacheDependencyDirectories.Add(base.VirtualPath);
			}
		}
	}
}
