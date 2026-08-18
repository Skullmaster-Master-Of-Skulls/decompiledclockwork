using System;

namespace WebGrease
{
	// Token: 0x020000E4 RID: 228
	public class CacheResult
	{
		// Token: 0x06000ED5 RID: 3797 RVA: 0x00045A3F File Offset: 0x00043C3F
		private CacheResult()
		{
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x00045A47 File Offset: 0x00043C47
		// (set) Token: 0x06000ED7 RID: 3799 RVA: 0x00045A4F File Offset: 0x00043C4F
		public string RelativeContentPath { get; private set; }

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x00045A58 File Offset: 0x00043C58
		// (set) Token: 0x06000ED9 RID: 3801 RVA: 0x00045A60 File Offset: 0x00043C60
		public string RelativeHashedContentPath { get; private set; }

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x00045A69 File Offset: 0x00043C69
		// (set) Token: 0x06000EDB RID: 3803 RVA: 0x00045A71 File Offset: 0x00043C71
		public string CachedFilePath { get; private set; }

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x00045A7A File Offset: 0x00043C7A
		// (set) Token: 0x06000EDD RID: 3805 RVA: 0x00045A82 File Offset: 0x00043C82
		public string FileCategory { get; private set; }

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000EDE RID: 3806 RVA: 0x00045A8B File Offset: 0x00043C8B
		// (set) Token: 0x06000EDF RID: 3807 RVA: 0x00045A93 File Offset: 0x00043C93
		public string ContentHash { get; private set; }

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x00045A9C File Offset: 0x00043C9C
		// (set) Token: 0x06000EE1 RID: 3809 RVA: 0x00045AA4 File Offset: 0x00043CA4
		public bool EndResult { get; private set; }

		// Token: 0x06000EE2 RID: 3810 RVA: 0x00045AB0 File Offset: 0x00043CB0
		public static CacheResult FromContentFile(IWebGreaseContext context, string cacheCategory, bool endResult, string fileCategory, ContentItem contentItem)
		{
			return new CacheResult
			{
				EndResult = endResult,
				FileCategory = fileCategory,
				CachedFilePath = context.Cache.StoreInCache(cacheCategory, contentItem),
				ContentHash = contentItem.GetContentHash(context),
				RelativeContentPath = contentItem.RelativeContentPath,
				RelativeHashedContentPath = contentItem.RelativeHashedContentPath
			};
		}
	}
}
