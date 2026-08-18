using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005A8 RID: 1448
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateProductStatusReq : BaseMessageReq
	{
		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06001DEF RID: 7663 RVA: 0x0000DAA6 File Offset: 0x0000BCA6
		// (set) Token: 0x06001DF0 RID: 7664 RVA: 0x0000DAAE File Offset: 0x0000BCAE
		[DataMember]
		public InventoryProductStatusDTO ProductStatus { get; set; }
	}
}
