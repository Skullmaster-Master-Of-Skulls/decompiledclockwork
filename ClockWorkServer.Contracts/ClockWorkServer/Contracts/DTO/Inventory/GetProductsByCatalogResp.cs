using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000581 RID: 1409
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductsByCatalogResp
	{
		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06001D58 RID: 7512 RVA: 0x0000D6CE File Offset: 0x0000B8CE
		// (set) Token: 0x06001D59 RID: 7513 RVA: 0x0000D6D6 File Offset: 0x0000B8D6
		[DataMember]
		public IList<InventoryProductDTO> Products { get; set; }
	}
}
