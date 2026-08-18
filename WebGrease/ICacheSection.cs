using System;
using System.Collections.Generic;
using System.IO;
using WebGrease.Configuration;

namespace WebGrease
{
	// Token: 0x020000E5 RID: 229
	public interface ICacheSection
	{
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000EE3 RID: 3811
		ICacheSection Parent { get; }

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000EE4 RID: 3812
		string UniqueKey { get; }

		// Token: 0x06000EE5 RID: 3813
		void EndSection();

		// Token: 0x06000EE6 RID: 3814
		bool CanBeRestoredFromCache();

		// Token: 0x06000EE7 RID: 3815
		void AddResult(ContentItem contentItem, string id, bool isEndResult = false);

		// Token: 0x06000EE8 RID: 3816
		void AddSourceDependency(string file);

		// Token: 0x06000EE9 RID: 3817
		void AddSourceDependency(string directory, string searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly);

		// Token: 0x06000EEA RID: 3818
		void AddSourceDependency(InputSpec inputSpec);

		// Token: 0x06000EEB RID: 3819
		void Save();

		// Token: 0x06000EEC RID: 3820
		bool CanBeSkipped();

		// Token: 0x06000EED RID: 3821
		ContentItem GetCachedContentItem(string fileCategory);

		// Token: 0x06000EEE RID: 3822
		IEnumerable<ContentItem> GetCachedContentItems(string fileCategory, bool endResultOnly = false);

		// Token: 0x06000EEF RID: 3823
		T GetCacheData<T>(string id) where T : new();

		// Token: 0x06000EF0 RID: 3824
		void SetCacheData<T>(string id, T obj) where T : new();

		// Token: 0x06000EF1 RID: 3825
		ContentItem GetCachedContentItem(string fileCategory, string relativeDestinationFile, string relativeHashedDestinationFile = null, IEnumerable<ResourcePivotKey> contentPivots = null);

		// Token: 0x06000EF2 RID: 3826
		void Load();
	}
}
