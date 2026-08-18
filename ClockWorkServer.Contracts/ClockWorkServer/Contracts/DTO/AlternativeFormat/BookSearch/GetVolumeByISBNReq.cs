using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AlternativeFormat.BookSearch;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat.BookSearch
{
	// Token: 0x02000C70 RID: 3184
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetVolumeByISBNReq : BaseMessageReq
	{
		// Token: 0x17001881 RID: 6273
		// (get) Token: 0x06004266 RID: 16998 RVA: 0x000206AA File Offset: 0x0001E8AA
		// (set) Token: 0x06004267 RID: 16999 RVA: 0x000206B2 File Offset: 0x0001E8B2
		[DataMember]
		public string ISBN { get; set; }

		// Token: 0x17001882 RID: 6274
		// (get) Token: 0x06004268 RID: 17000 RVA: 0x000206BB File Offset: 0x0001E8BB
		// (set) Token: 0x06004269 RID: 17001 RVA: 0x000206C3 File Offset: 0x0001E8C3
		[DataMember]
		public eBookSearchProviderType SearchType { get; set; }
	}
}
