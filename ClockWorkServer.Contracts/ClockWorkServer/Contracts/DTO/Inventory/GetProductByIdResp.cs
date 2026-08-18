using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200057B RID: 1403
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductByIdResp
	{
		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06001D42 RID: 7490 RVA: 0x0000D646 File Offset: 0x0000B846
		// (set) Token: 0x06001D43 RID: 7491 RVA: 0x0000D64E File Offset: 0x0000B84E
		[DataMember]
		public InventoryProductDTO Product { get; set; }
	}
}
