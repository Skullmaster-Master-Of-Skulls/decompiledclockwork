using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000585 RID: 1413
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductsByCategoryResp
	{
		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06001D68 RID: 7528 RVA: 0x0000D734 File Offset: 0x0000B934
		// (set) Token: 0x06001D69 RID: 7529 RVA: 0x0000D73C File Offset: 0x0000B93C
		[DataMember]
		public IList<InventoryProductDTO> Products { get; set; }
	}
}
