using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AlternativeFormat.BookSearch;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat.BookSearch
{
	// Token: 0x02000C6D RID: 3181
	[DataContract(Namespace = "http://tpro.ca")]
	public class EBookSearchResultDTO
	{
		// Token: 0x17001872 RID: 6258
		// (get) Token: 0x06004245 RID: 16965 RVA: 0x000205AB File Offset: 0x0001E7AB
		// (set) Token: 0x06004246 RID: 16966 RVA: 0x000205B3 File Offset: 0x0001E7B3
		[DataMember]
		public string Id { get; set; }

		// Token: 0x17001873 RID: 6259
		// (get) Token: 0x06004247 RID: 16967 RVA: 0x000205BC File Offset: 0x0001E7BC
		// (set) Token: 0x06004248 RID: 16968 RVA: 0x000205C4 File Offset: 0x0001E7C4
		[DataMember]
		public string ISBN { get; set; }

		// Token: 0x17001874 RID: 6260
		// (get) Token: 0x06004249 RID: 16969 RVA: 0x000205CD File Offset: 0x0001E7CD
		// (set) Token: 0x0600424A RID: 16970 RVA: 0x000205D5 File Offset: 0x0001E7D5
		[DataMember]
		public string Url { get; set; }

		// Token: 0x17001875 RID: 6261
		// (get) Token: 0x0600424B RID: 16971 RVA: 0x000205DE File Offset: 0x0001E7DE
		// (set) Token: 0x0600424C RID: 16972 RVA: 0x000205E6 File Offset: 0x0001E7E6
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17001876 RID: 6262
		// (get) Token: 0x0600424D RID: 16973 RVA: 0x000205EF File Offset: 0x0001E7EF
		// (set) Token: 0x0600424E RID: 16974 RVA: 0x000205F7 File Offset: 0x0001E7F7
		[DataMember]
		public IList<string> Authors { get; set; }

		// Token: 0x17001877 RID: 6263
		// (get) Token: 0x0600424F RID: 16975 RVA: 0x00020600 File Offset: 0x0001E800
		// (set) Token: 0x06004250 RID: 16976 RVA: 0x00020608 File Offset: 0x0001E808
		[DataMember]
		public string Publisher { get; set; }

		// Token: 0x17001878 RID: 6264
		// (get) Token: 0x06004251 RID: 16977 RVA: 0x00020611 File Offset: 0x0001E811
		// (set) Token: 0x06004252 RID: 16978 RVA: 0x00020619 File Offset: 0x0001E819
		[DataMember]
		public DateTime? PublisherDate { get; set; }

		// Token: 0x17001879 RID: 6265
		// (get) Token: 0x06004253 RID: 16979 RVA: 0x00020622 File Offset: 0x0001E822
		// (set) Token: 0x06004254 RID: 16980 RVA: 0x0002062A File Offset: 0x0001E82A
		[DataMember]
		public string Summary { get; set; }

		// Token: 0x1700187A RID: 6266
		// (get) Token: 0x06004255 RID: 16981 RVA: 0x00020633 File Offset: 0x0001E833
		// (set) Token: 0x06004256 RID: 16982 RVA: 0x0002063B File Offset: 0x0001E83B
		[DataMember]
		public int PageCount { get; set; }

		// Token: 0x1700187B RID: 6267
		// (get) Token: 0x06004257 RID: 16983 RVA: 0x00020644 File Offset: 0x0001E844
		// (set) Token: 0x06004258 RID: 16984 RVA: 0x0002064C File Offset: 0x0001E84C
		[DataMember]
		public string Language { get; set; }

		// Token: 0x1700187C RID: 6268
		// (get) Token: 0x06004259 RID: 16985 RVA: 0x00020655 File Offset: 0x0001E855
		// (set) Token: 0x0600425A RID: 16986 RVA: 0x0002065D File Offset: 0x0001E85D
		[DataMember]
		public string ThumbnailUrl { get; set; }

		// Token: 0x1700187D RID: 6269
		// (get) Token: 0x0600425B RID: 16987 RVA: 0x00020666 File Offset: 0x0001E866
		// (set) Token: 0x0600425C RID: 16988 RVA: 0x0002066E File Offset: 0x0001E86E
		[DataMember]
		public string CoverImageUrl { get; set; }

		// Token: 0x1700187E RID: 6270
		// (get) Token: 0x0600425D RID: 16989 RVA: 0x00020677 File Offset: 0x0001E877
		// (set) Token: 0x0600425E RID: 16990 RVA: 0x0002067F File Offset: 0x0001E87F
		[DataMember]
		public eBookSearchProviderName SearchEngine { get; set; }
	}
}
