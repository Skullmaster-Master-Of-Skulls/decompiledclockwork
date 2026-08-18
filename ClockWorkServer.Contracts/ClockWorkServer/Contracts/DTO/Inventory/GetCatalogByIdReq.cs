using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000502 RID: 1282
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCatalogByIdReq : BaseMessageReq
	{
		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06001B2B RID: 6955 RVA: 0x0000C87E File Offset: 0x0000AA7E
		// (set) Token: 0x06001B2C RID: 6956 RVA: 0x0000C886 File Offset: 0x0000AA86
		[DataMember]
		public int CatalogId { get; set; }
	}
}
