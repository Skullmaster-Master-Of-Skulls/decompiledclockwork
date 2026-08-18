using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000505 RID: 1285
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCatalogByNameResp
	{
		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x0000C8B1 File Offset: 0x0000AAB1
		// (set) Token: 0x06001B35 RID: 6965 RVA: 0x0000C8B9 File Offset: 0x0000AAB9
		[DataMember]
		public InventoryCatalogDTO Catalog { get; set; }
	}
}
