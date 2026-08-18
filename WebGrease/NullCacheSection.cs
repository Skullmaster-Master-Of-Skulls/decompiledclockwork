using System;
using System.Collections.Generic;
using System.IO;
using WebGrease.Configuration;

namespace WebGrease
{
	// Token: 0x020000EA RID: 234
	public class NullCacheSection : ICacheSection
	{
		// Token: 0x06000F2B RID: 3883 RVA: 0x000465DB File Offset: 0x000447DB
		public NullCacheSection()
		{
			this.UniqueKey = string.Empty;
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x000465EE File Offset: 0x000447EE
		public ICacheSection Parent
		{
			get
			{
				return NullCacheManager.EmptyCacheSection;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x000465F5 File Offset: 0x000447F5
		// (set) Token: 0x06000F2E RID: 3886 RVA: 0x000465FD File Offset: 0x000447FD
		public string UniqueKey { get; private set; }

		// Token: 0x06000F2F RID: 3887 RVA: 0x00046606 File Offset: 0x00044806
		public void AddResult(ContentItem contentItem, string id, bool isEndResult)
		{
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x00046608 File Offset: 0x00044808
		public void AddSourceDependency(string file)
		{
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x0004660A File Offset: 0x0004480A
		public void AddSourceDependency(string directory, string searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly)
		{
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0004660C File Offset: 0x0004480C
		public void AddSourceDependency(InputSpec inputSpec)
		{
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x0004660E File Offset: 0x0004480E
		public bool CanBeRestoredFromCache()
		{
			return false;
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x00046611 File Offset: 0x00044811
		public bool CanBeSkipped()
		{
			return false;
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x00046614 File Offset: 0x00044814
		public void EndSection()
		{
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x00046616 File Offset: 0x00044816
		public ContentItem GetCachedContentItem(string fileCategory)
		{
			return null;
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x00046619 File Offset: 0x00044819
		public IEnumerable<ContentItem> GetCachedContentItems(string fileCategory, bool endResultOnly = false)
		{
			return NullCacheSection.EmptyContentItems;
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x00046620 File Offset: 0x00044820
		public ContentItem GetCachedContentItem(string fileCategory, string relativeDestinationFile, string relativeHashedDestinationFile = null, IEnumerable<ResourcePivotKey> contentPivots = null)
		{
			return null;
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x00046623 File Offset: 0x00044823
		public void Load()
		{
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x00046628 File Offset: 0x00044828
		public T GetCacheData<T>(string id) where T : new()
		{
			return default(T);
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0004663E File Offset: 0x0004483E
		public void SetCacheData<T>(string id, T obj) where T : new()
		{
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x00046640 File Offset: 0x00044840
		public void Save()
		{
		}

		// Token: 0x040005D6 RID: 1494
		private static readonly IEnumerable<ContentItem> EmptyContentItems = new ContentItem[0];
	}
}
