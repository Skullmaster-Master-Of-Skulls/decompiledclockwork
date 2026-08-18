using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;

namespace System.Web.Optimization
{
	// Token: 0x02000033 RID: 51
	public class DefaultBundleOrderer : IBundleOrderer
	{
		// Token: 0x0600016B RID: 363 RVA: 0x000058F0 File Offset: 0x00003AF0
		private static Dictionary<string, HashSet<BundleFile>> BuildFileMap(IEnumerable<BundleFile> files)
		{
			Dictionary<string, HashSet<BundleFile>> dictionary = new Dictionary<string, HashSet<BundleFile>>(StringComparer.OrdinalIgnoreCase);
			foreach (BundleFile bundleFile in files)
			{
				string name = bundleFile.VirtualFile.Name;
				if (dictionary.ContainsKey(name))
				{
					dictionary[name].Add(bundleFile);
				}
				else
				{
					dictionary[name] = new HashSet<BundleFile>(BundleFileComparer.Instance)
					{
						bundleFile
					};
				}
			}
			return dictionary;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000059C8 File Offset: 0x00003BC8
		private static void AddOrderingFiles(BundleFileSetOrdering ordering, IEnumerable<BundleFile> files, Dictionary<string, HashSet<BundleFile>> fileMap, HashSet<VirtualFile> foundFiles, List<BundleFile> result)
		{
			foreach (string text in ordering.Files)
			{
				if (text.EndsWith("*", StringComparison.OrdinalIgnoreCase))
				{
					string prefix = text.Substring(0, text.Length - 1);
					IEnumerable<BundleFile> enumerable = from f in files
					where !foundFiles.Contains(f.VirtualFile) && f.VirtualFile.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
					select f;
					using (IEnumerator<BundleFile> enumerator2 = enumerable.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							BundleFile bundleFile = enumerator2.Current;
							result.Add(bundleFile);
							foundFiles.Add(bundleFile.VirtualFile);
						}
						continue;
					}
				}
				if (fileMap.ContainsKey(text))
				{
					List<BundleFile> list = new List<BundleFile>(fileMap[text]);
					list.Sort(BundleFileComparer.Instance);
					foreach (BundleFile bundleFile2 in list)
					{
						if (!foundFiles.Contains(bundleFile2.VirtualFile))
						{
							result.Add(bundleFile2);
							foundFiles.Add(bundleFile2.VirtualFile);
						}
					}
				}
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005B74 File Offset: 0x00003D74
		public virtual IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (files == null)
			{
				throw new ArgumentNullException("files");
			}
			if (context.BundleCollection.FileSetOrderList.Count == 0)
			{
				return files;
			}
			List<BundleFile> list = new List<BundleFile>();
			List<BundleFile> list2 = new List<BundleFile>(files);
			Dictionary<string, HashSet<BundleFile>> dictionary = DefaultBundleOrderer.BuildFileMap(list2);
			if (dictionary.Count == 0)
			{
				return list;
			}
			HashSet<VirtualFile> hashSet = new HashSet<VirtualFile>(VirtualFileComparer.Instance);
			foreach (BundleFileSetOrdering ordering in context.BundleCollection.FileSetOrderList)
			{
				DefaultBundleOrderer.AddOrderingFiles(ordering, list2, dictionary, hashSet, list);
			}
			foreach (BundleFile bundleFile in list2)
			{
				if (!hashSet.Contains(bundleFile.VirtualFile))
				{
					list.Add(bundleFile);
					hashSet.Add(bundleFile.VirtualFile);
				}
			}
			return list;
		}

		// Token: 0x0400007A RID: 122
		internal static DefaultBundleOrderer Instance = new DefaultBundleOrderer();
	}
}
