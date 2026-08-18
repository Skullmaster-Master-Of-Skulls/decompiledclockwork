using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;

namespace System.Web.Optimization
{
	// Token: 0x02000016 RID: 22
	public class FileExtensionReplacementList
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003FEF File Offset: 0x000021EF
		internal int Count
		{
			get
			{
				return this._entries.Count;
			}
		}

		// Token: 0x1700003A RID: 58
		internal FileExtensionReplacementList.Entry this[int index]
		{
			get
			{
				return this._entries[index];
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000400A File Offset: 0x0000220A
		public void Add(string extension)
		{
			this.Add(extension, OptimizationMode.Always);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004014 File Offset: 0x00002214
		public void Add(string extension, OptimizationMode mode)
		{
			this._entries.Add(new FileExtensionReplacementList.Entry(extension, mode));
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004028 File Offset: 0x00002228
		public void Clear()
		{
			this._entries.Clear();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004038 File Offset: 0x00002238
		private static BundleFile FindReplacementFile(BundleContext context, BundleFile file, string replacementExtension)
		{
			string directoryName = Path.GetDirectoryName(file.VirtualFile.VirtualPath);
			string extension = Path.GetExtension(file.VirtualFile.Name);
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.VirtualFile.Name);
			string text = fileNameWithoutExtension + "." + replacementExtension;
			if (extension.Length > 0)
			{
				text += extension;
			}
			string virtualPath = Path.Combine(directoryName, text);
			string text2 = Path.Combine(Path.GetDirectoryName(file.IncludedVirtualPath), text);
			text2 = text2.Replace('\\', '/');
			if (context.VirtualPathProvider.FileExists(virtualPath))
			{
				return new BundleFile(text2, context.VirtualPathProvider.GetFile(virtualPath));
			}
			return null;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000040E4 File Offset: 0x000022E4
		public virtual IEnumerable<BundleFile> ReplaceFileExtensions(BundleContext context, IEnumerable<BundleFile> files)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (files == null || this._entries.Count == 0)
			{
				return files;
			}
			List<BundleFile> list = new List<BundleFile>();
			HashSet<VirtualFile> hashSet = new HashSet<VirtualFile>(VirtualFileComparer.Instance);
			foreach (BundleFile bundleFile in files)
			{
				if (!hashSet.Contains(bundleFile.VirtualFile))
				{
					bool flag = false;
					foreach (FileExtensionReplacementList.Entry entry in this._entries)
					{
						if (entry.UseEntry(context.EnableOptimizations))
						{
							string extension = entry.Extension;
							BundleFile bundleFile2 = FileExtensionReplacementList.FindReplacementFile(context, bundleFile, extension);
							if (bundleFile2 != null)
							{
								if (!hashSet.Contains(bundleFile2.VirtualFile))
								{
									list.Add(bundleFile2);
									hashSet.Add(bundleFile2.VirtualFile);
								}
								flag = true;
								break;
							}
						}
					}
					if (!flag)
					{
						list.Add(bundleFile);
						hashSet.Add(bundleFile.VirtualFile);
					}
				}
			}
			return list;
		}

		// Token: 0x04000044 RID: 68
		private List<FileExtensionReplacementList.Entry> _entries = new List<FileExtensionReplacementList.Entry>();

		// Token: 0x02000017 RID: 23
		internal sealed class Entry
		{
			// Token: 0x060000D6 RID: 214 RVA: 0x0000422F File Offset: 0x0000242F
			public Entry(string extension, OptimizationMode mode)
			{
				this.Extension = extension;
				this.Mode = mode;
			}

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004245 File Offset: 0x00002445
			// (set) Token: 0x060000D8 RID: 216 RVA: 0x0000424D File Offset: 0x0000244D
			public string Extension { get; set; }

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004256 File Offset: 0x00002456
			// (set) Token: 0x060000DA RID: 218 RVA: 0x0000425E File Offset: 0x0000245E
			public OptimizationMode Mode { get; set; }

			// Token: 0x060000DB RID: 219 RVA: 0x00004268 File Offset: 0x00002468
			public bool UseEntry(bool optimizationMode)
			{
				switch (this.Mode)
				{
				case OptimizationMode.Always:
					return true;
				case OptimizationMode.WhenEnabled:
					return optimizationMode;
				case OptimizationMode.WhenDisabled:
					return !optimizationMode;
				default:
					return false;
				}
			}
		}
	}
}
