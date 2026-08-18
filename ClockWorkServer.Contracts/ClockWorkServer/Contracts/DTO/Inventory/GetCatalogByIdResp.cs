using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000503 RID: 1283
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCatalogByIdResp
	{
		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06001B2E RID: 6958 RVA: 0x0000C88F File Offset: 0x0000AA8F
		// (set) Token: 0x06001B2F RID: 6959 RVA: 0x0000C897 File Offset: 0x0000AA97
		[DataMember]
		public InventoryCatalogDTO Catalog { get; set; }
	}
}
