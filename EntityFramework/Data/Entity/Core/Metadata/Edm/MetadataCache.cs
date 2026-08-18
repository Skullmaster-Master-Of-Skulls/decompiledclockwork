using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.EntityClient.Internal;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200050D RID: 1293
	internal class MetadataCache
	{
		// Token: 0x06003070 RID: 12400 RVA: 0x000E81EC File Offset: 0x000E63EC
		private static List<MetadataArtifactLoader> SplitPaths(string paths)
		{
			HashSet<string> uriRegistry = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			List<string> list = new List<string>();
			for (int num = paths.IndexOf("|datadirectory|", StringComparison.OrdinalIgnoreCase); num != -1; num = paths.IndexOf("|datadirectory|", StringComparison.OrdinalIgnoreCase))
			{
				int num2 = (num == 0) ? -1 : paths.LastIndexOf("|", num - 1, StringComparison.Ordinal);
				int num3 = num2 + 1;
				int num4 = paths.IndexOf("|", num + "|datadirectory|".Length, StringComparison.Ordinal);
				if (num4 == -1)
				{
					list.Add(paths.Substring(num3));
					paths = paths.Remove(num3);
					break;
				}
				list.Add(paths.Substring(num3, num4 - num3));
				paths = paths.Remove(num3, num4 - num3);
			}
			string[] array = paths.Split(new string[]
			{
				"|"
			}, StringSplitOptions.RemoveEmptyEntries);
			if (list.Count > 0)
			{
				list.AddRange(array);
				array = list.ToArray();
			}
			List<MetadataArtifactLoader> list2 = new List<MetadataArtifactLoader>();
			List<MetadataArtifactLoader> list3 = new List<MetadataArtifactLoader>();
			List<MetadataArtifactLoader> list4 = new List<MetadataArtifactLoader>();
			List<MetadataArtifactLoader> list5 = new List<MetadataArtifactLoader>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].Trim();
				if (array[i].Length > 0)
				{
					MetadataArtifactLoader item = MetadataArtifactLoader.Create(array[i], MetadataArtifactLoader.ExtensionCheck.All, null, uriRegistry);
					if (array[i].EndsWith(".csdl", StringComparison.OrdinalIgnoreCase))
					{
						list2.Add(item);
					}
					else if (array[i].EndsWith(".msl", StringComparison.OrdinalIgnoreCase))
					{
						list3.Add(item);
					}
					else if (array[i].EndsWith(".ssdl", StringComparison.OrdinalIgnoreCase))
					{
						list4.Add(item);
					}
					else
					{
						list5.Add(item);
					}
				}
			}
			list5.AddRange(list4);
			list5.AddRange(list3);
			list5.AddRange(list2);
			return list5;
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x000E83B8 File Offset: 0x000E65B8
		public MetadataWorkspace GetMetadataWorkspace(DbConnectionOptions effectiveConnectionOptions)
		{
			MetadataArtifactLoader artifactLoader = this.GetArtifactLoader(effectiveConnectionOptions);
			string cacheKey = MetadataCache.CreateMetadataCacheKey(artifactLoader.GetPaths(), effectiveConnectionOptions["provider"]);
			return this.GetMetadataWorkspace(cacheKey, artifactLoader);
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x000E83EC File Offset: 0x000E65EC
		public MetadataArtifactLoader GetArtifactLoader(DbConnectionOptions effectiveConnectionOptions)
		{
			string text = effectiveConnectionOptions["metadata"];
			if (!string.IsNullOrEmpty(text))
			{
				List<MetadataArtifactLoader> list = this._artifactLoaderCache.Evaluate(text);
				return MetadataArtifactLoader.Create(MetadataCache.ShouldRecalculateMetadataArtifactLoader(list) ? MetadataCache.SplitPaths(text) : list);
			}
			return MetadataArtifactLoader.Create(new List<MetadataArtifactLoader>());
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x000E84F8 File Offset: 0x000E66F8
		public MetadataWorkspace GetMetadataWorkspace(string cacheKey, MetadataArtifactLoader artifactLoader)
		{
			return this._cachedWorkspaces.GetOrAdd(cacheKey, delegate(string k)
			{
				EdmItemCollection edmItemCollection = MetadataCache.LoadEdmItemCollection(artifactLoader);
				Lazy<StorageMappingItemCollection> mappingLoader = new Lazy<StorageMappingItemCollection>(() => MetadataCache.LoadStoreCollection(edmItemCollection, artifactLoader));
				return new MetadataWorkspace(() => edmItemCollection, () => mappingLoader.Value.StoreItemCollection, () => mappingLoader.Value);
			});
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x000E852A File Offset: 0x000E672A
		public void Clear()
		{
			this._cachedWorkspaces.Clear();
			Interlocked.CompareExchange<Memoizer<string, List<MetadataArtifactLoader>>>(ref this._artifactLoaderCache, new Memoizer<string, List<MetadataArtifactLoader>>(new Func<string, List<MetadataArtifactLoader>>(MetadataCache.SplitPaths), null), this._artifactLoaderCache);
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x000E855C File Offset: 0x000E675C
		private static StorageMappingItemCollection LoadStoreCollection(EdmItemCollection edmItemCollection, MetadataArtifactLoader loader)
		{
			List<XmlReader> xmlReaders = loader.CreateReaders(DataSpace.SSpace);
			StoreItemCollection storeCollection;
			try
			{
				storeCollection = new StoreItemCollection(xmlReaders, loader.GetPaths(DataSpace.SSpace));
			}
			finally
			{
				Helper.DisposeXmlReaders(xmlReaders);
			}
			List<XmlReader> xmlReaders2 = loader.CreateReaders(DataSpace.CSSpace);
			StorageMappingItemCollection result;
			try
			{
				result = new StorageMappingItemCollection(edmItemCollection, storeCollection, xmlReaders2, loader.GetPaths(DataSpace.CSSpace));
			}
			finally
			{
				Helper.DisposeXmlReaders(xmlReaders2);
			}
			return result;
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x000E85C8 File Offset: 0x000E67C8
		private static EdmItemCollection LoadEdmItemCollection(MetadataArtifactLoader loader)
		{
			List<XmlReader> xmlReaders = loader.CreateReaders(DataSpace.CSpace);
			EdmItemCollection result;
			try
			{
				result = new EdmItemCollection(xmlReaders, loader.GetPaths(DataSpace.CSpace), false);
			}
			finally
			{
				Helper.DisposeXmlReaders(xmlReaders);
			}
			return result;
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x000E861F File Offset: 0x000E681F
		private static bool ShouldRecalculateMetadataArtifactLoader(IEnumerable<MetadataArtifactLoader> loaders)
		{
			return loaders.Any((MetadataArtifactLoader loader) => loader.GetType() == typeof(MetadataArtifactLoaderCompositeFile));
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x000E8644 File Offset: 0x000E6844
		private static string CreateMetadataCacheKey(IList<string> paths, string providerName)
		{
			int num = 0;
			string result;
			MetadataCache.CreateMetadataCacheKeyWithCount(paths, providerName, false, ref num, out result);
			MetadataCache.CreateMetadataCacheKeyWithCount(paths, providerName, true, ref num, out result);
			return result;
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x000E866C File Offset: 0x000E686C
		private static void CreateMetadataCacheKeyWithCount(IList<string> paths, string providerName, bool buildResult, ref int resultCount, out string result)
		{
			StringBuilder stringBuilder = buildResult ? new StringBuilder(resultCount) : null;
			resultCount = 0;
			if (!string.IsNullOrEmpty(providerName))
			{
				resultCount += providerName.Length + 1;
				if (buildResult)
				{
					stringBuilder.Append(providerName);
					stringBuilder.Append(";");
				}
			}
			if (paths != null)
			{
				for (int i = 0; i < paths.Count; i++)
				{
					if (paths[i].Length > 0)
					{
						if (i > 0)
						{
							resultCount++;
							if (buildResult)
							{
								stringBuilder.Append("|");
							}
						}
						resultCount += paths[i].Length;
						if (buildResult)
						{
							stringBuilder.Append(paths[i]);
						}
					}
				}
				resultCount++;
				if (buildResult)
				{
					stringBuilder.Append(";");
				}
			}
			result = (buildResult ? stringBuilder.ToString() : null);
		}

		// Token: 0x04001263 RID: 4707
		private const string DataDirectory = "|datadirectory|";

		// Token: 0x04001264 RID: 4708
		private const string MetadataPathSeparator = "|";

		// Token: 0x04001265 RID: 4709
		private const string SemicolonSeparator = ";";

		// Token: 0x04001266 RID: 4710
		public static readonly MetadataCache Instance = new MetadataCache();

		// Token: 0x04001267 RID: 4711
		private Memoizer<string, List<MetadataArtifactLoader>> _artifactLoaderCache = new Memoizer<string, List<MetadataArtifactLoader>>(new Func<string, List<MetadataArtifactLoader>>(MetadataCache.SplitPaths), null);

		// Token: 0x04001268 RID: 4712
		private readonly ConcurrentDictionary<string, MetadataWorkspace> _cachedWorkspaces = new ConcurrentDictionary<string, MetadataWorkspace>();
	}
}
