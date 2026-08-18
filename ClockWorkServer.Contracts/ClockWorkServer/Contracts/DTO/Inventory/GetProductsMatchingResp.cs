using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000579 RID: 1401
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductsMatchingResp
	{
		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06001D3A RID: 7482 RVA: 0x0000D613 File Offset: 0x0000B813
		// (set) Token: 0x06001D3B RID: 7483 RVA: 0x0000D61B File Offset: 0x0000B81B
		[DataMember]
		public IList<InventoryProductDTO> MatchingProducts { get; set; }
	}
}
