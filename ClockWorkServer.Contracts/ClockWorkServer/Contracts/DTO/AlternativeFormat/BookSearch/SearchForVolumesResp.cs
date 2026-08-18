using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat.BookSearch
{
	// Token: 0x02000C6F RID: 3183
	[DataContract(Namespace = "http://tpro.ca")]
	public class SearchForVolumesResp
	{
		// Token: 0x17001880 RID: 6272
		// (get) Token: 0x06004263 RID: 16995 RVA: 0x00020699 File Offset: 0x0001E899
		// (set) Token: 0x06004264 RID: 16996 RVA: 0x000206A1 File Offset: 0x0001E8A1
		[DataMember]
		public IList<EBookSearchResultDTO> BookSearchResult { get; set; }
	}
}
