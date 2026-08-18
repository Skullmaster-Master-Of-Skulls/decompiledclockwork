using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000589 RID: 1417
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductsByLoanResp
	{
		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06001D78 RID: 7544 RVA: 0x0000D79A File Offset: 0x0000B99A
		// (set) Token: 0x06001D79 RID: 7545 RVA: 0x0000D7A2 File Offset: 0x0000B9A2
		[DataMember]
		public IList<InventoryProductDTO> Products { get; set; }
	}
}
