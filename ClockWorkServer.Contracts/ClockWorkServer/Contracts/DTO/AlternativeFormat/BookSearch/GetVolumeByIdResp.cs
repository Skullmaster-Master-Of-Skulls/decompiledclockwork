using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat.BookSearch
{
	// Token: 0x02000C73 RID: 3187
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetVolumeByIdResp
	{
		// Token: 0x17001886 RID: 6278
		// (get) Token: 0x06004273 RID: 17011 RVA: 0x000206FF File Offset: 0x0001E8FF
		// (set) Token: 0x06004274 RID: 17012 RVA: 0x00020707 File Offset: 0x0001E907
		[DataMember]
		public EBookSearchResultDTO BookSearchResult { get; set; }
	}
}
