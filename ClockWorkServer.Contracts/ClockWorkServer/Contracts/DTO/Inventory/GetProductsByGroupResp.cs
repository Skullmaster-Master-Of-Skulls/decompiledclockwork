using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000587 RID: 1415
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductsByGroupResp
	{
		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06001D70 RID: 7536 RVA: 0x0000D767 File Offset: 0x0000B967
		// (set) Token: 0x06001D71 RID: 7537 RVA: 0x0000D76F File Offset: 0x0000B96F
		[DataMember]
		public IList<InventoryProductDTO> Products { get; set; }
	}
}
