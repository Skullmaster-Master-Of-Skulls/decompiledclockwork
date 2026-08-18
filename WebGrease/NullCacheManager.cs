using System;
using System.Collections.Generic;

namespace WebGrease
{
	// Token: 0x020000E9 RID: 233
	internal class NullCacheManager : ICacheManager
	{
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x00046591 File Offset: 0x00044791
		public ICacheSection CurrentCacheSection
		{
			get
			{
				return NullCacheManager.EmptyCacheSection;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x00046598 File Offset: 0x00044798
		public IDictionary<string, ReadOnlyCacheSection> LoadedCacheSections
		{
			get
			{
				return NullCacheManager.EmptyReadOnlyCacheSections;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x0004659F File Offset: 0x0004479F
		public string RootPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x000465A2 File Offset: 0x000447A2
		public ICacheSection BeginSection(WebGreaseSectionKey webGreaseSectionKey, bool autoLoad = true)
		{
			return NullCacheManager.EmptyCacheSection;
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x000465A9 File Offset: 0x000447A9
		public void CleanUp()
		{
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x000465AB File Offset: 0x000447AB
		public void EndSection(ICacheSection cacheSection)
		{
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x000465AD File Offset: 0x000447AD
		public string GetAbsoluteCacheFilePath(string category, string fileName)
		{
			return null;
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x000465B0 File Offset: 0x000447B0
		public void SetContext(IWebGreaseContext newContext)
		{
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x000465B2 File Offset: 0x000447B2
		public string StoreInCache(string cacheCategory, ContentItem contentItem)
		{
			return null;
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x000465B5 File Offset: 0x000447B5
		public void LockedFileCacheAction(string lockFileContent, Action action)
		{
			action();
		}

		// Token: 0x040005D4 RID: 1492
		internal static readonly ICacheSection EmptyCacheSection = new NullCacheSection();

		// Token: 0x040005D5 RID: 1493
		private static readonly Dictionary<string, ReadOnlyCacheSection> EmptyReadOnlyCacheSections = new Dictionary<string, ReadOnlyCacheSection>();
	}
}
