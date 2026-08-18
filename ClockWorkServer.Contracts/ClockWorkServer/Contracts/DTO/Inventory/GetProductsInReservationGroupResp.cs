using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005A3 RID: 1443
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductsInReservationGroupResp
	{
		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06001DDE RID: 7646 RVA: 0x0000DA37 File Offset: 0x0000BC37
		// (set) Token: 0x06001DDF RID: 7647 RVA: 0x0000DA3F File Offset: 0x0000BC3F
		[DataMember]
		public IList<InventoryProductDTO> Products { get; set; }
	}
}
