using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200050C RID: 1292
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateCatalogReq : BaseMessageReq
	{
		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06001B45 RID: 6981 RVA: 0x0000C906 File Offset: 0x0000AB06
		// (set) Token: 0x06001B46 RID: 6982 RVA: 0x0000C90E File Offset: 0x0000AB0E
		[DataMember]
		public InventoryCatalogDTO Catalog { get; set; }
	}
}
