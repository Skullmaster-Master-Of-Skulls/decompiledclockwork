using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebGrease.Configuration;
using WebGrease.Extensions;

namespace WebGrease
{
	// Token: 0x020000E6 RID: 230
	public class CacheSection : ICacheSection
	{
		// Token: 0x06000EF3 RID: 3827 RVA: 0x00045B18 File Offset: 0x00043D18
		private CacheSection()
		{
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x00045B76 File Offset: 0x00043D76
		public ICacheSection Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x00045B7E File Offset: 0x00043D7E
		// (set) Token: 0x06000EF6 RID: 3830 RVA: 0x00045B86 File Offset: 0x00043D86
		public string UniqueKey { get; private set; }

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x00045B8F File Offset: 0x00043D8F
		private List<CacheSection> ChildCacheSections
		{
			get
			{
				return this.childCacheSections.Value;
			}
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x00045B9C File Offset: 0x00043D9C
		public static CacheSection Begin(IWebGreaseContext context, string cacheCategory, string uniqueKey, ICacheSection parentCacheSection = null, bool autoLoad = true)
		{
			CacheSection cacheSection = new CacheSection
			{
				parent = (parentCacheSection as CacheSection),
				cacheCategory = cacheCategory,
				context = context,
				UniqueKey = uniqueKey
			};
			cacheSection.absolutePath = context.Cache.GetAbsoluteCacheFilePath(cacheSection.cacheCategory, context.GetValueHash(cacheSection.UniqueKey) + ".cache.json");
			if (cacheSection.parent != null)
			{
				cacheSection.parent.AddChildCacheSection(cacheSection);
			}
			CacheSection.EnsureCachePath(context, cacheCategory);
			if (autoLoad)
			{
				cacheSection.Load();
			}
			return cacheSection;
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x00045C28 File Offset: 0x00043E28
		public void Load()
		{
			try
			{
				FileInfo fileInfo = new FileInfo(this.absolutePath);
				if (fileInfo.Exists)
				{
					this.cachedSection = ReadOnlyCacheSection.Load(fileInfo.FullName, this.context);
					this.isUnsaved = (this.cachedSection != null);
				}
			}
			catch (PathTooLongException inner)
			{
				throw new BuildWorkflowException("Path to long: {0}".InvariantFormat(new object[]
				{
					this.absolutePath
				}), inner);
			}
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x00045CA8 File Offset: 0x00043EA8
		public T GetCacheData<T>(string id) where T : new()
		{
			if (this.cachedSection != null)
			{
				ContentItem contentItem = this.GetCachedContentItems(id, false).FirstOrDefault<ContentItem>();
				if (contentItem != null && !string.IsNullOrWhiteSpace(contentItem.Content))
				{
					return contentItem.Content.FromJson(true);
				}
			}
			if (default(T) != null)
			{
				return default(T);
			}
			return Activator.CreateInstance<T>();
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x00045D08 File Offset: 0x00043F08
		public void SetCacheData<T>(string id, T obj) where T : new()
		{
			string content = obj.ToJson(true);
			this.AddResult(ContentItem.FromContent(content, new ResourcePivotKey[0]), id, false);
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x00045D80 File Offset: 0x00043F80
		public void AddResult(ContentItem contentItem, string id, bool isEndResult)
		{
			this.isUnsaved = true;
			Safe.Lock(this.cacheResults, int.MaxValue, delegate()
			{
				this.cacheResults.Add(CacheResult.FromContentFile(this.context, this.cacheCategory, isEndResult, id, contentItem));
			});
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x00045DD4 File Offset: 0x00043FD4
		public void AddSourceDependency(string file)
		{
			if (!File.Exists(file))
			{
				throw new BuildWorkflowException("Cannot add a source dependency that does not exists on disk: {0}".InvariantFormat(new object[]
				{
					file
				}));
			}
			this.AddSourceDependency(new InputSpec
			{
				Path = file
			});
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00045E1C File Offset: 0x0004401C
		public void AddSourceDependency(string directory, string searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly)
		{
			if (!Directory.Exists(directory))
			{
				throw new BuildWorkflowException("Cannot add a source dependency that does not exists on disk: {0}".InvariantFormat(new object[]
				{
					directory
				}));
			}
			this.AddSourceDependency(new InputSpec
			{
				Path = directory,
				SearchPattern = searchPattern,
				SearchOption = searchOption
			});
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x00045F10 File Offset: 0x00044110
		public void AddSourceDependency(InputSpec inputSpec)
		{
			this.isUnsaved = true;
			string key = inputSpec.ToJson(true);
			Safe.UniqueKeyLock(key, int.MaxValue, delegate
			{
				if (!this.sourceDependencies.ContainsKey(key))
				{
					this.sourceDependencies.Add(key, CacheSourceDependency.Create(this.context, new InputSpec
					{
						IsOptional = inputSpec.IsOptional,
						Path = inputSpec.Path,
						SearchOption = inputSpec.SearchOption,
						SearchPattern = inputSpec.SearchPattern
					}));
				}
			});
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x00045F66 File Offset: 0x00044166
		public bool CanBeRestoredFromCache()
		{
			return this.cachedSection != null && this.cachedSection.CanBeRestoredFromCache();
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00045F7D File Offset: 0x0004417D
		public bool CanBeSkipped()
		{
			return this.cachedSection != null && this.cachedSection.CanBeSkipped();
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00045F94 File Offset: 0x00044194
		public void EndSection()
		{
			this.context.Cache.EndSection(this);
			this.Dispose();
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x00045FAD File Offset: 0x000441AD
		public IEnumerable<CacheResult> GetCacheResults(string fileCategory = null, bool endResultOnly = false)
		{
			return this.cachedSection.GetCacheResults(fileCategory, endResultOnly);
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x00045FBC File Offset: 0x000441BC
		public ContentItem GetCachedContentItem(string fileCategory)
		{
			CacheResult cacheResult = this.GetCacheResults(fileCategory, false).FirstOrDefault<CacheResult>();
			if (cacheResult == null)
			{
				return null;
			}
			return ContentItem.FromCacheResult(cacheResult, new ResourcePivotKey[0]);
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x00045FE8 File Offset: 0x000441E8
		public ContentItem GetCachedContentItem(string fileCategory, string relativeDestinationFile, string relativeHashedDestinationFile = null, IEnumerable<ResourcePivotKey> contentPivots = null)
		{
			return ContentItem.FromCacheResult(this.GetCacheResults(fileCategory, false).FirstOrDefault<CacheResult>(), relativeDestinationFile, relativeHashedDestinationFile, (contentPivots != null) ? contentPivots.ToArray<ResourcePivotKey>() : null);
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0004601A File Offset: 0x0004421A
		public IEnumerable<ContentItem> GetCachedContentItems(string fileCategory, bool endResultOnly = false)
		{
			return from crf in this.GetCacheResults(fileCategory, endResultOnly)
			select ContentItem.FromCacheResult(crf, new ResourcePivotKey[0]);
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x00046048 File Offset: 0x00044248
		public void Save()
		{
			if (this.isUnsaved)
			{
				this.isUnsaved = false;
				FileInfo fileInfo = new FileInfo(this.absolutePath);
				if (fileInfo.Directory != null && !fileInfo.Directory.Exists)
				{
					fileInfo.Directory.Create();
				}
				File.WriteAllText(this.absolutePath, CacheSection.ToReadOnlyCacheSectionJson(this));
				this.Touch();
			}
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x000460A8 File Offset: 0x000442A8
		private static void EnsureCachePath(IWebGreaseContext context, string cacheCategory)
		{
			string absoluteCacheFilePath = context.Cache.GetAbsoluteCacheFilePath(cacheCategory, string.Empty);
			if (!Directory.Exists(absoluteCacheFilePath))
			{
				Directory.CreateDirectory(absoluteCacheFilePath);
			}
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x000462B4 File Offset: 0x000444B4
		private static string ToReadOnlyCacheSectionJson(CacheSection cacheSection)
		{
			return new
			{
				sourceDependencies = cacheSection.sourceDependencies.Values,
				cacheResults = cacheSection.cacheResults,
				children = from ccs in cacheSection.ChildCacheSections
				select ccs.absolutePath,
				absolutePath = cacheSection.absolutePath
			}.ToJson(false);
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0004632C File Offset: 0x0004452C
		private void AddChildCacheSection(CacheSection cacheSection)
		{
			Safe.Lock(this.ChildCacheSections, delegate()
			{
				this.ChildCacheSections.Add(cacheSection);
			});
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00046364 File Offset: 0x00044564
		private void Dispose()
		{
			if (this.cachedSection != null)
			{
				this.cachedSection.Dispose();
			}
			this.context = null;
			this.parent = null;
			this.sourceDependencies.Clear();
			this.ChildCacheSections.Clear();
			this.childCacheSections = null;
			this.cacheResults.Clear();
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x000463CD File Offset: 0x000445CD
		private void Touch()
		{
			this.context.Touch(this.absolutePath);
			this.cacheResults.ForEach(delegate(CacheResult cr)
			{
				this.context.Touch(cr.CachedFilePath);
			});
		}

		// Token: 0x040005C3 RID: 1475
		private readonly List<CacheResult> cacheResults = new List<CacheResult>();

		// Token: 0x040005C4 RID: 1476
		private readonly IDictionary<string, CacheSourceDependency> sourceDependencies = new Dictionary<string, CacheSourceDependency>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040005C5 RID: 1477
		private Lazy<List<CacheSection>> childCacheSections = new Lazy<List<CacheSection>>(() => new List<CacheSection>(), true);

		// Token: 0x040005C6 RID: 1478
		private string cacheCategory;

		// Token: 0x040005C7 RID: 1479
		private ReadOnlyCacheSection cachedSection;

		// Token: 0x040005C8 RID: 1480
		private IWebGreaseContext context;

		// Token: 0x040005C9 RID: 1481
		private bool isUnsaved = true;

		// Token: 0x040005CA RID: 1482
		private CacheSection parent;

		// Token: 0x040005CB RID: 1483
		private string absolutePath;
	}
}
