using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000583 RID: 1411
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductsByRootCategoryResp
	{
		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x0000D701 File Offset: 0x0000B901
		// (set) Token: 0x06001D61 RID: 7521 RVA: 0x0000D709 File Offset: 0x0000B909
		[DataMember]
		public IList<InventoryProductDTO> Products { get; set; }
	}
}
