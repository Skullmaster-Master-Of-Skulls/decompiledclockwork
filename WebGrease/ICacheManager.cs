using System;
using System.Collections.Generic;

namespace WebGrease
{
	// Token: 0x020000E2 RID: 226
	public interface ICacheManager
	{
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000EBE RID: 3774
		ICacheSection CurrentCacheSection { get; }

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000EBF RID: 3775
		IDictionary<string, ReadOnlyCacheSection> LoadedCacheSections { get; }

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000EC0 RID: 3776
		string RootPath { get; }

		// Token: 0x06000EC1 RID: 3777
		ICacheSection BeginSection(WebGreaseSectionKey webGreaseSectionKey, bool autoLoad = true);

		// Token: 0x06000EC2 RID: 3778
		void CleanUp();

		// Token: 0x06000EC3 RID: 3779
		void EndSection(ICacheSection cacheSection);

		// Token: 0x06000EC4 RID: 3780
		string GetAbsoluteCacheFilePath(string category, string fileName);

		// Token: 0x06000EC5 RID: 3781
		void SetContext(IWebGreaseContext newContext);

		// Token: 0x06000EC6 RID: 3782
		string StoreInCache(string cacheCategory, ContentItem contentItem);

		// Token: 0x06000EC7 RID: 3783
		void LockedFileCacheAction(string lockFileContent, Action action);
	}
}
