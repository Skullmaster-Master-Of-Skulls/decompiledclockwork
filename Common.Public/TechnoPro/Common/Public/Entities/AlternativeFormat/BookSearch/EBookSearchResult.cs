using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat.BookSearch
{
	// Token: 0x02000597 RID: 1431
	public class EBookSearchResult : BusinessBase<string>
	{
		// Token: 0x17001397 RID: 5015
		// (get) Token: 0x06002E96 RID: 11926 RVA: 0x0003326C File Offset: 0x0003146C
		// (set) Token: 0x06002E97 RID: 11927 RVA: 0x00033274 File Offset: 0x00031474
		public string ISBN { get; set; }

		// Token: 0x17001398 RID: 5016
		// (get) Token: 0x06002E98 RID: 11928 RVA: 0x0003327D File Offset: 0x0003147D
		// (set) Token: 0x06002E99 RID: 11929 RVA: 0x00033285 File Offset: 0x00031485
		public string Url { get; set; }

		// Token: 0x17001399 RID: 5017
		// (get) Token: 0x06002E9A RID: 11930 RVA: 0x0003328E File Offset: 0x0003148E
		// (set) Token: 0x06002E9B RID: 11931 RVA: 0x00033296 File Offset: 0x00031496
		public string Title { get; set; }

		// Token: 0x1700139A RID: 5018
		// (get) Token: 0x06002E9C RID: 11932 RVA: 0x0003329F File Offset: 0x0003149F
		// (set) Token: 0x06002E9D RID: 11933 RVA: 0x000332A7 File Offset: 0x000314A7
		public IList<string> Authors { get; set; }

		// Token: 0x1700139B RID: 5019
		// (get) Token: 0x06002E9E RID: 11934 RVA: 0x000332B0 File Offset: 0x000314B0
		// (set) Token: 0x06002E9F RID: 11935 RVA: 0x000332B8 File Offset: 0x000314B8
		public string Publisher { get; set; }

		// Token: 0x1700139C RID: 5020
		// (get) Token: 0x06002EA0 RID: 11936 RVA: 0x000332C1 File Offset: 0x000314C1
		// (set) Token: 0x06002EA1 RID: 11937 RVA: 0x000332C9 File Offset: 0x000314C9
		public DateTime? PublisherDate { get; set; }

		// Token: 0x1700139D RID: 5021
		// (get) Token: 0x06002EA2 RID: 11938 RVA: 0x000332D2 File Offset: 0x000314D2
		// (set) Token: 0x06002EA3 RID: 11939 RVA: 0x000332DA File Offset: 0x000314DA
		public string Summary { get; set; }

		// Token: 0x1700139E RID: 5022
		// (get) Token: 0x06002EA4 RID: 11940 RVA: 0x000332E3 File Offset: 0x000314E3
		// (set) Token: 0x06002EA5 RID: 11941 RVA: 0x000332EB File Offset: 0x000314EB
		public int PageCount { get; set; }

		// Token: 0x1700139F RID: 5023
		// (get) Token: 0x06002EA6 RID: 11942 RVA: 0x000332F4 File Offset: 0x000314F4
		// (set) Token: 0x06002EA7 RID: 11943 RVA: 0x000332FC File Offset: 0x000314FC
		public string Language { get; set; }

		// Token: 0x170013A0 RID: 5024
		// (get) Token: 0x06002EA8 RID: 11944 RVA: 0x00033305 File Offset: 0x00031505
		// (set) Token: 0x06002EA9 RID: 11945 RVA: 0x0003330D File Offset: 0x0003150D
		public string ThumbnailUrl { get; set; }

		// Token: 0x170013A1 RID: 5025
		// (get) Token: 0x06002EAA RID: 11946 RVA: 0x00033316 File Offset: 0x00031516
		// (set) Token: 0x06002EAB RID: 11947 RVA: 0x0003331E File Offset: 0x0003151E
		public string CoverImageUrl { get; set; }

		// Token: 0x170013A2 RID: 5026
		// (get) Token: 0x06002EAC RID: 11948 RVA: 0x00033327 File Offset: 0x00031527
		// (set) Token: 0x06002EAD RID: 11949 RVA: 0x0003332F File Offset: 0x0003152F
		public eBookSearchProviderName SearchEngine { get; set; }
	}
}
