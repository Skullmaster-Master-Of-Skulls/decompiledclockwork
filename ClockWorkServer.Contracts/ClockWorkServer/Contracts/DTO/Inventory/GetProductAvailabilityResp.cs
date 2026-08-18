using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005A1 RID: 1441
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductAvailabilityResp
	{
		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x0000DA04 File Offset: 0x0000BC04
		// (set) Token: 0x06001DD7 RID: 7639 RVA: 0x0000DA0C File Offset: 0x0000BC0C
		[DataMember]
		public IList<InventoryProductBookedTimeDTO> ProductBookedTimeList { get; set; }
	}
}
