using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat.BookSearch
{
	// Token: 0x02000C6E RID: 3182
	[DataContract(Namespace = "http://tpro.ca")]
	public class SearchForVolumesReq : BaseMessageReq
	{
		// Token: 0x1700187F RID: 6271
		// (get) Token: 0x06004260 RID: 16992 RVA: 0x00020688 File Offset: 0x0001E888
		// (set) Token: 0x06004261 RID: 16993 RVA: 0x00020690 File Offset: 0x0001E890
		[DataMember]
		public EBookSearchRequestDTO BookSearchRequest { get; set; }
	}
}
