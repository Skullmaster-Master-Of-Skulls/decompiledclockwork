using System;
using System.Collections.Generic;
using System.Data.Common.QueryCache;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Mapping;
using System.Security.Permissions;
using System.Threading;
using System.Xml;

namespace System.Data.Metadata.Edm
{
	// Token: 0x02000201 RID: 513
	internal static class MetadataCache
	{
		// Token: 0x060021B4 RID: 8628 RVA: 0x000769B5 File Offset: 0x00074BB5
		private static void PeriodicCleanupCallback(object state)
		{
			MetadataCache.DoCacheClean<MetadataCache.EdmMetadataEntry>(MetadataCache._edmLevelCache, MetadataCache._edmLevelLock);
			MetadataCache.DoCacheClean<MetadataCache.StoreMetadataEntry>(MetadataCache._storeLevelCache, MetadataCache._storeLevelLock);
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x000769D8 File Offset: 0x00074BD8
		internal static List<MetadataArtifactLoader> SplitPaths(string paths)
		{
			HashSet<string> uriRegistry = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			List<MetadataArtifactLoader> list = new List<MetadataArtifactLoader>();
			List<string> list2 = new List<string>();
			for (int num = paths.IndexOf("|datadirectory|", StringComparison.OrdinalIgnoreCase); num != -1; num = paths.IndexOf("|datadirectory|", StringComparison.OrdinalIgnoreCase))
			{
				int num2 = (num == 0) ? -1 : paths.LastIndexOf("|", num - 1, StringComparison.Ordinal);
				int num3 = num2 + 1;
				int num4 = paths.IndexOf("|", num + "|datadirectory|".Length, StringComparison.Ordinal);
				if (num4 == -1)
				{
					list2.Add(paths.Substring(num3));
					paths = paths.Remove(num3);
					break;
				}
				list2.Add(paths.Substring(num3, num4 - num3));
				paths = paths.Remove(num3, num4 - num3);
			}
			string[] array = paths.Split(new string[]
			{
				"|"
			}, StringSplitOptions.RemoveEmptyEntries);
			if (list2.Count > 0)
			{
				list2.AddRange(array);
				array = list2.ToArray();
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].Trim();
				if (array[i].Length > 0)
				{
					list.Add(MetadataArtifactLoader.Create(array[i], MetadataArtifactLoader.ExtensionCheck.All, null, uriRegistry));
				}
			}
			return list;
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x00076B0C File Offset: 0x00074D0C
		private static void DoCacheClean<T>(Dictionary<string, T> cache, object objectToLock) where T : MetadataCache.MetadataEntry
		{
			if (cache != null)
			{
				List<KeyValuePair<string, T>> list = null;
				lock (objectToLock)
				{
					if (objectToLock == MetadataCache._storeLevelLock && MetadataCache._metadataEntriesRemovedFromCache.Count != 0)
					{
						int num = MetadataCache._metadataEntriesRemovedFromCache.Count - 1;
						while (0 <= num)
						{
							if (!MetadataCache._metadataEntriesRemovedFromCache[num].IsEntryStillValid())
							{
								MetadataCache._metadataEntriesRemovedFromCache[num].CleanupQueryCache();
								MetadataCache._metadataEntriesRemovedFromCache.RemoveAt(num);
							}
							num--;
						}
					}
					foreach (KeyValuePair<string, T> item in cache)
					{
						if (item.Value.PeriodicCleanUpThread())
						{
							if (list == null)
							{
								list = new List<KeyValuePair<string, T>>();
							}
							list.Add(item);
						}
					}
					if (list != null)
					{
						for (int i = 0; i < list.Count; i++)
						{
							list[i].Value.Clear();
							cache.Remove(list[i].Key);
						}
					}
				}
			}
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x00076C48 File Offset: 0x00074E48
		internal static EdmItemCollection GetOrCreateEdmItemCollection(string cacheKey, MetadataArtifactLoader loader, out object entryToken)
		{
			MetadataCache.EdmMetadataEntry cacheEntry = MetadataCache.GetCacheEntry<MetadataCache.EdmMetadataEntry>(MetadataCache._edmLevelCache, cacheKey, MetadataCache._edmLevelLock, default(MetadataCache.EdmMetadataEntryConstructor), out entryToken);
			MetadataCache.LoadItemCollection<MetadataCache.EdmMetadataEntry>(new MetadataCache.EdmItemCollectionLoader(loader), cacheEntry);
			return cacheEntry.EdmItemCollection;
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x00076C8C File Offset: 0x00074E8C
		internal static StorageMappingItemCollection GetOrCreateStoreAndMappingItemCollections(string cacheKey, MetadataArtifactLoader loader, EdmItemCollection edmItemCollection, out object entryToken)
		{
			MetadataCache.StoreMetadataEntry cacheEntry = MetadataCache.GetCacheEntry<MetadataCache.StoreMetadataEntry>(MetadataCache._storeLevelCache, cacheKey, MetadataCache._storeLevelLock, default(MetadataCache.StoreMetadataEntryConstructor), out entryToken);
			MetadataCache.LoadItemCollection<MetadataCache.StoreMetadataEntry>(new MetadataCache.StoreItemCollectionLoader(edmItemCollection, loader), cacheEntry);
			return cacheEntry.StorageMappingItemCollection;
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x00076CD1 File Offset: 0x00074ED1
		internal static List<MetadataArtifactLoader> GetOrCreateMetdataArtifactLoader(string paths)
		{
			return MetadataCache._artifactLoaderCache.Evaluate(paths);
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x00076CE0 File Offset: 0x00074EE0
		private static T GetCacheEntry<T>(Dictionary<string, T> cache, string cacheKey, object objectToLock, MetadataCache.IMetadataEntryConstructor<T> metadataEntry, out object entryToken) where T : MetadataCache.MetadataEntry
		{
			T metadataEntry2;
			lock (objectToLock)
			{
				if (cache.TryGetValue(cacheKey, out metadataEntry2))
				{
					entryToken = metadataEntry2.EnsureToken();
				}
				else
				{
					metadataEntry2 = metadataEntry.GetMetadataEntry();
					entryToken = metadataEntry2.EnsureToken();
					cache.Add(cacheKey, metadataEntry2);
				}
			}
			return metadataEntry2;
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x00076D50 File Offset: 0x00074F50
		private static void LoadItemCollection<T>(MetadataCache.IItemCollectionLoader<T> itemCollectionLoader, T entry) where T : MetadataCache.MetadataEntry
		{
			bool flag = true;
			if (!entry.IsLoaded)
			{
				object obj = entry;
				lock (obj)
				{
					if (!entry.IsLoaded)
					{
						itemCollectionLoader.LoadItemCollection(entry);
						flag = false;
					}
				}
			}
			if (flag)
			{
				entry.CheckFilePermission();
			}
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x00076DC0 File Offset: 0x00074FC0
		internal static void Clear()
		{
			object edmLevelLock = MetadataCache._edmLevelLock;
			lock (edmLevelLock)
			{
				MetadataCache._edmLevelCache.Clear();
			}
			object storeLevelLock = MetadataCache._storeLevelLock;
			lock (storeLevelLock)
			{
				foreach (MetadataCache.StoreMetadataEntry storeMetadataEntry in MetadataCache._storeLevelCache.Values)
				{
					if (storeMetadataEntry.IsEntryStillValid())
					{
						MetadataCache._metadataEntriesRemovedFromCache.Add(storeMetadataEntry);
					}
					else
					{
						storeMetadataEntry.Clear();
					}
				}
				MetadataCache._storeLevelCache.Clear();
			}
			Memoizer<string, List<MetadataArtifactLoader>> value = new Memoizer<string, List<MetadataArtifactLoader>>(new Func<string, List<MetadataArtifactLoader>>(MetadataCache.SplitPaths), null);
			Interlocked.CompareExchange<Memoizer<string, List<MetadataArtifactLoader>>>(ref MetadataCache._artifactLoaderCache, value, MetadataCache._artifactLoaderCache);
		}

		// Token: 0x04000EC9 RID: 3785
		private const string s_dataDirectory = "|datadirectory|";

		// Token: 0x04000ECA RID: 3786
		private const string s_metadataPathSeparator = "|";

		// Token: 0x04000ECB RID: 3787
		private const int cleanupPeriod = 300000;

		// Token: 0x04000ECC RID: 3788
		private static readonly Dictionary<string, MetadataCache.EdmMetadataEntry> _edmLevelCache = new Dictionary<string, MetadataCache.EdmMetadataEntry>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000ECD RID: 3789
		private static readonly Dictionary<string, MetadataCache.StoreMetadataEntry> _storeLevelCache = new Dictionary<string, MetadataCache.StoreMetadataEntry>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000ECE RID: 3790
		private static readonly List<MetadataCache.StoreMetadataEntry> _metadataEntriesRemovedFromCache = new List<MetadataCache.StoreMetadataEntry>();

		// Token: 0x04000ECF RID: 3791
		private static Memoizer<string, List<MetadataArtifactLoader>> _artifactLoaderCache = new Memoizer<string, List<MetadataArtifactLoader>>(new Func<string, List<MetadataArtifactLoader>>(MetadataCache.SplitPaths), null);

		// Token: 0x04000ED0 RID: 3792
		private static readonly object _edmLevelLock = new object();

		// Token: 0x04000ED1 RID: 3793
		private static readonly object _storeLevelLock = new object();

		// Token: 0x04000ED2 RID: 3794
		private static Timer timer = new Timer(new TimerCallback(MetadataCache.PeriodicCleanupCallback), null, 300000, 300000);

		// Token: 0x02000526 RID: 1318
		private abstract class MetadataEntry
		{
			// Token: 0x06003E43 RID: 15939 RVA: 0x000E7FBF File Offset: 0x000E61BF
			internal MetadataEntry()
			{
				this._entryTokenReference = new WeakReference(null);
				this._weakReferenceItemCollection = new WeakReference(null);
			}

			// Token: 0x17000B18 RID: 2840
			// (get) Token: 0x06003E44 RID: 15940 RVA: 0x000E7FDF File Offset: 0x000E61DF
			protected ItemCollection ItemCollection
			{
				get
				{
					return this._itemCollection;
				}
			}

			// Token: 0x06003E45 RID: 15941 RVA: 0x000E7FE7 File Offset: 0x000E61E7
			protected void UpdateMetadataEntry(ItemCollection itemCollection, FileIOPermission filePermissions)
			{
				this._weakReferenceItemCollection.Target = itemCollection;
				this._filePermissions = filePermissions;
				this._itemCollection = itemCollection;
			}

			// Token: 0x17000B19 RID: 2841
			// (get) Token: 0x06003E46 RID: 15942 RVA: 0x000E8003 File Offset: 0x000E6203
			internal bool IsLoaded
			{
				get
				{
					return this._itemCollection != null;
				}
			}

			// Token: 0x06003E47 RID: 15943 RVA: 0x000E8010 File Offset: 0x000E6210
			internal bool PeriodicCleanUpThread()
			{
				if (this._markEntryForCleanup)
				{
					if (this._itemCollection != null)
					{
						this._itemCollection = null;
					}
					else if (!this._weakReferenceItemCollection.IsAlive)
					{
						this._filePermissions = null;
						return true;
					}
				}
				else if (!this._entryTokenReference.IsAlive)
				{
					this._markEntryForCleanup = true;
				}
				return false;
			}

			// Token: 0x06003E48 RID: 15944 RVA: 0x000E8064 File Offset: 0x000E6264
			internal object EnsureToken()
			{
				object obj = this._entryTokenReference.Target;
				ItemCollection itemCollection = (ItemCollection)this._weakReferenceItemCollection.Target;
				if (this._entryTokenReference.IsAlive)
				{
					return obj;
				}
				if (this._itemCollection == null)
				{
					if (this._weakReferenceItemCollection.IsAlive)
					{
						this._itemCollection = itemCollection;
					}
					else
					{
						this._filePermissions = null;
					}
				}
				obj = new object();
				this._entryTokenReference.Target = obj;
				this._markEntryForCleanup = false;
				return obj;
			}

			// Token: 0x06003E49 RID: 15945 RVA: 0x000E80DC File Offset: 0x000E62DC
			internal void CheckFilePermission()
			{
				if (this._filePermissions != null)
				{
					this._filePermissions.Demand();
				}
			}

			// Token: 0x06003E4A RID: 15946 RVA: 0x000089D0 File Offset: 0x00006BD0
			internal virtual void Clear()
			{
			}

			// Token: 0x06003E4B RID: 15947 RVA: 0x000E80F1 File Offset: 0x000E62F1
			internal bool IsEntryStillValid()
			{
				return this._entryTokenReference.IsAlive;
			}

			// Token: 0x04001B62 RID: 7010
			private WeakReference _entryTokenReference;

			// Token: 0x04001B63 RID: 7011
			private ItemCollection _itemCollection;

			// Token: 0x04001B64 RID: 7012
			private WeakReference _weakReferenceItemCollection;

			// Token: 0x04001B65 RID: 7013
			private bool _markEntryForCleanup;

			// Token: 0x04001B66 RID: 7014
			private FileIOPermission _filePermissions;
		}

		// Token: 0x02000527 RID: 1319
		private class EdmMetadataEntry : MetadataCache.MetadataEntry
		{
			// Token: 0x17000B1A RID: 2842
			// (get) Token: 0x06003E4C RID: 15948 RVA: 0x000E80FE File Offset: 0x000E62FE
			internal EdmItemCollection EdmItemCollection
			{
				get
				{
					return (EdmItemCollection)base.ItemCollection;
				}
			}

			// Token: 0x06003E4D RID: 15949 RVA: 0x000E810C File Offset: 0x000E630C
			internal void LoadEdmItemCollection(MetadataArtifactLoader loader)
			{
				List<XmlReader> xmlReaders = loader.CreateReaders(DataSpace.CSpace);
				try
				{
					EdmItemCollection itemCollection = new EdmItemCollection(xmlReaders, loader.GetPaths(DataSpace.CSpace));
					List<string> list = new List<string>();
					loader.CollectFilePermissionPaths(list, DataSpace.CSpace);
					FileIOPermission filePermissions = null;
					if (list.Count > 0)
					{
						filePermissions = new FileIOPermission(FileIOPermissionAccess.Read, list.ToArray());
					}
					base.UpdateMetadataEntry(itemCollection, filePermissions);
				}
				finally
				{
					Helper.DisposeXmlReaders(xmlReaders);
				}
			}
		}

		// Token: 0x02000528 RID: 1320
		private class StoreMetadataEntry : MetadataCache.MetadataEntry
		{
			// Token: 0x06003E4F RID: 15951 RVA: 0x000E8178 File Offset: 0x000E6378
			internal StoreMetadataEntry()
			{
			}

			// Token: 0x17000B1B RID: 2843
			// (get) Token: 0x06003E50 RID: 15952 RVA: 0x000E8180 File Offset: 0x000E6380
			internal StorageMappingItemCollection StorageMappingItemCollection
			{
				get
				{
					return (StorageMappingItemCollection)base.ItemCollection;
				}
			}

			// Token: 0x06003E51 RID: 15953 RVA: 0x000E8190 File Offset: 0x000E6390
			internal void LoadStoreCollection(EdmItemCollection edmItemCollection, MetadataArtifactLoader loader)
			{
				StoreItemCollection storeItemCollection = null;
				IEnumerable<XmlReader> xmlReaders = loader.CreateReaders(DataSpace.SSpace);
				try
				{
					storeItemCollection = new StoreItemCollection(xmlReaders, loader.GetPaths(DataSpace.SSpace));
				}
				finally
				{
					Helper.DisposeXmlReaders(xmlReaders);
				}
				if (this._queryCacheManager != null)
				{
					this._queryCacheManager.Clear();
				}
				this._queryCacheManager = storeItemCollection.QueryCacheManager;
				StorageMappingItemCollection itemCollection = null;
				IEnumerable<XmlReader> xmlReaders2 = loader.CreateReaders(DataSpace.CSSpace);
				try
				{
					itemCollection = new StorageMappingItemCollection(edmItemCollection, storeItemCollection, xmlReaders2, loader.GetPaths(DataSpace.CSSpace));
				}
				finally
				{
					Helper.DisposeXmlReaders(xmlReaders2);
				}
				List<string> list = new List<string>();
				loader.CollectFilePermissionPaths(list, DataSpace.SSpace);
				loader.CollectFilePermissionPaths(list, DataSpace.CSSpace);
				FileIOPermission filePermissions = null;
				if (list.Count > 0)
				{
					filePermissions = new FileIOPermission(FileIOPermissionAccess.Read, list.ToArray());
				}
				base.UpdateMetadataEntry(itemCollection, filePermissions);
			}

			// Token: 0x06003E52 RID: 15954 RVA: 0x000E825C File Offset: 0x000E645C
			internal override void Clear()
			{
				this.CleanupQueryCache();
				base.Clear();
			}

			// Token: 0x06003E53 RID: 15955 RVA: 0x000E826A File Offset: 0x000E646A
			internal void CleanupQueryCache()
			{
				if (this._queryCacheManager != null)
				{
					this._queryCacheManager.Dispose();
					this._queryCacheManager = null;
				}
			}

			// Token: 0x04001B67 RID: 7015
			private QueryCacheManager _queryCacheManager;
		}

		// Token: 0x02000529 RID: 1321
		private interface IMetadataEntryConstructor<T>
		{
			// Token: 0x06003E54 RID: 15956
			T GetMetadataEntry();
		}

		// Token: 0x0200052A RID: 1322
		private struct EdmMetadataEntryConstructor : MetadataCache.IMetadataEntryConstructor<MetadataCache.EdmMetadataEntry>
		{
			// Token: 0x06003E55 RID: 15957 RVA: 0x000E8286 File Offset: 0x000E6486
			public MetadataCache.EdmMetadataEntry GetMetadataEntry()
			{
				return new MetadataCache.EdmMetadataEntry();
			}
		}

		// Token: 0x0200052B RID: 1323
		private struct StoreMetadataEntryConstructor : MetadataCache.IMetadataEntryConstructor<MetadataCache.StoreMetadataEntry>
		{
			// Token: 0x06003E56 RID: 15958 RVA: 0x000E828D File Offset: 0x000E648D
			public MetadataCache.StoreMetadataEntry GetMetadataEntry()
			{
				return new MetadataCache.StoreMetadataEntry();
			}
		}

		// Token: 0x0200052C RID: 1324
		private interface IItemCollectionLoader<T> where T : MetadataCache.MetadataEntry
		{
			// Token: 0x06003E57 RID: 15959
			void LoadItemCollection(T entry);
		}

		// Token: 0x0200052D RID: 1325
		private struct EdmItemCollectionLoader : MetadataCache.IItemCollectionLoader<MetadataCache.EdmMetadataEntry>
		{
			// Token: 0x06003E58 RID: 15960 RVA: 0x000E8294 File Offset: 0x000E6494
			public EdmItemCollectionLoader(MetadataArtifactLoader loader)
			{
				this._loader = loader;
			}

			// Token: 0x06003E59 RID: 15961 RVA: 0x000E829D File Offset: 0x000E649D
			public void LoadItemCollection(MetadataCache.EdmMetadataEntry entry)
			{
				entry.LoadEdmItemCollection(this._loader);
			}

			// Token: 0x04001B68 RID: 7016
			private MetadataArtifactLoader _loader;
		}

		// Token: 0x0200052E RID: 1326
		private struct StoreItemCollectionLoader : MetadataCache.IItemCollectionLoader<MetadataCache.StoreMetadataEntry>
		{
			// Token: 0x06003E5A RID: 15962 RVA: 0x000E82AB File Offset: 0x000E64AB
			internal StoreItemCollectionLoader(EdmItemCollection edmItemCollection, MetadataArtifactLoader loader)
			{
				if (loader.GetPaths(DataSpace.SSpace) == null || loader.GetPaths(DataSpace.SSpace).Count == 0)
				{
					throw EntityUtil.Metadata(Strings.AtleastOneSSDLNeeded);
				}
				this._edmItemCollection = edmItemCollection;
				this._loader = loader;
			}

			// Token: 0x06003E5B RID: 15963 RVA: 0x000E82DD File Offset: 0x000E64DD
			public void LoadItemCollection(MetadataCache.StoreMetadataEntry entry)
			{
				entry.LoadStoreCollection(this._edmItemCollection, this._loader);
			}

			// Token: 0x04001B69 RID: 7017
			private EdmItemCollection _edmItemCollection;

			// Token: 0x04001B6A RID: 7018
			private MetadataArtifactLoader _loader;
		}
	}
}
