using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200050A RID: 1290
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateCatalogReq : BaseMessageReq
	{
		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x0000C8E4 File Offset: 0x0000AAE4
		// (set) Token: 0x06001B40 RID: 6976 RVA: 0x0000C8EC File Offset: 0x0000AAEC
		[DataMember]
		public InventoryCatalogDTO Catalog { get; set; }
	}
}
