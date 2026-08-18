using System;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat.BookSearch
{
	// Token: 0x02000596 RID: 1430
	public class EBookSearchRequest
	{
		// Token: 0x17001390 RID: 5008
		// (get) Token: 0x06002E87 RID: 11911 RVA: 0x000331E5 File Offset: 0x000313E5
		// (set) Token: 0x06002E88 RID: 11912 RVA: 0x000331ED File Offset: 0x000313ED
		public string Id { get; set; }

		// Token: 0x17001391 RID: 5009
		// (get) Token: 0x06002E89 RID: 11913 RVA: 0x000331F6 File Offset: 0x000313F6
		// (set) Token: 0x06002E8A RID: 11914 RVA: 0x000331FE File Offset: 0x000313FE
		public string SearchText { get; set; }

		// Token: 0x17001392 RID: 5010
		// (get) Token: 0x06002E8B RID: 11915 RVA: 0x00033207 File Offset: 0x00031407
		// (set) Token: 0x06002E8C RID: 11916 RVA: 0x0003320F File Offset: 0x0003140F
		public string ISBN { get; set; }

		// Token: 0x17001393 RID: 5011
		// (get) Token: 0x06002E8D RID: 11917 RVA: 0x00033218 File Offset: 0x00031418
		// (set) Token: 0x06002E8E RID: 11918 RVA: 0x00033220 File Offset: 0x00031420
		public string Title { get; set; }

		// Token: 0x17001394 RID: 5012
		// (get) Token: 0x06002E8F RID: 11919 RVA: 0x00033229 File Offset: 0x00031429
		// (set) Token: 0x06002E90 RID: 11920 RVA: 0x00033231 File Offset: 0x00031431
		public string Author { get; set; }

		// Token: 0x17001395 RID: 5013
		// (get) Token: 0x06002E91 RID: 11921 RVA: 0x0003323A File Offset: 0x0003143A
		// (set) Token: 0x06002E92 RID: 11922 RVA: 0x00033242 File Offset: 0x00031442
		public string Publisher { get; set; }

		// Token: 0x17001396 RID: 5014
		// (get) Token: 0x06002E93 RID: 11923 RVA: 0x0003324B File Offset: 0x0003144B
		// (set) Token: 0x06002E94 RID: 11924 RVA: 0x00033253 File Offset: 0x00031453
		public int MaxNumberOfBooksToReturn { get; set; }

		// Token: 0x0400207B RID: 8315
		public eBookSearchProviderType SearchProviderType = eBookSearchProviderType.All;
	}
}
