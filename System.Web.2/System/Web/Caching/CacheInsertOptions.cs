using System;

namespace System.Web.Caching
{
	// Token: 0x02000875 RID: 2165
	public class CacheInsertOptions
	{
		// Token: 0x17001C90 RID: 7312
		// (get) Token: 0x060065F9 RID: 26105 RVA: 0x00167856 File Offset: 0x00165A56
		// (set) Token: 0x060065FA RID: 26106 RVA: 0x0016785E File Offset: 0x00165A5E
		public CacheDependency Dependencies { get; set; }

		// Token: 0x17001C91 RID: 7313
		// (get) Token: 0x060065FB RID: 26107 RVA: 0x00167867 File Offset: 0x00165A67
		// (set) Token: 0x060065FC RID: 26108 RVA: 0x0016786F File Offset: 0x00165A6F
		public DateTime AbsoluteExpiration { get; set; } = Cache.NoAbsoluteExpiration;

		// Token: 0x17001C92 RID: 7314
		// (get) Token: 0x060065FD RID: 26109 RVA: 0x00167878 File Offset: 0x00165A78
		// (set) Token: 0x060065FE RID: 26110 RVA: 0x00167880 File Offset: 0x00165A80
		public TimeSpan SlidingExpiration { get; set; } = Cache.NoSlidingExpiration;

		// Token: 0x17001C93 RID: 7315
		// (get) Token: 0x060065FF RID: 26111 RVA: 0x00167889 File Offset: 0x00165A89
		// (set) Token: 0x06006600 RID: 26112 RVA: 0x00167891 File Offset: 0x00165A91
		public CacheItemPriority Priority { get; set; } = CacheItemPriority.Normal;

		// Token: 0x17001C94 RID: 7316
		// (get) Token: 0x06006601 RID: 26113 RVA: 0x0016789A File Offset: 0x00165A9A
		// (set) Token: 0x06006602 RID: 26114 RVA: 0x001678A2 File Offset: 0x00165AA2
		public CacheItemRemovedCallback OnRemovedCallback { get; set; }
	}
}
