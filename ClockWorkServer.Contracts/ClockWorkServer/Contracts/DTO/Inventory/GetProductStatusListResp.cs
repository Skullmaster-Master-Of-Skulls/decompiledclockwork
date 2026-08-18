using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005AF RID: 1455
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductStatusListResp
	{
		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06001E00 RID: 7680 RVA: 0x0000DAFB File Offset: 0x0000BCFB
		// (set) Token: 0x06001E01 RID: 7681 RVA: 0x0000DB03 File Offset: 0x0000BD03
		[DataMember]
		public IList<InventoryProductStatusDTO> ProductStatusList { get; set; }
	}
}
