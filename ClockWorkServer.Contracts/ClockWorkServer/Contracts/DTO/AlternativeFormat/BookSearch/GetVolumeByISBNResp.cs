using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat.BookSearch
{
	// Token: 0x02000C71 RID: 3185
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetVolumeByISBNResp
	{
		// Token: 0x17001883 RID: 6275
		// (get) Token: 0x0600426B RID: 17003 RVA: 0x000206CC File Offset: 0x0001E8CC
		// (set) Token: 0x0600426C RID: 17004 RVA: 0x000206D4 File Offset: 0x0001E8D4
		[DataMember]
		public EBookSearchResultDTO BookSearchResult { get; set; }
	}
}
