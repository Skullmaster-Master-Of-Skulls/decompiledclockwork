using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200057F RID: 1407
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductByBarCodeResp
	{
		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06001D52 RID: 7506 RVA: 0x0000D6AC File Offset: 0x0000B8AC
		// (set) Token: 0x06001D53 RID: 7507 RVA: 0x0000D6B4 File Offset: 0x0000B8B4
		[DataMember]
		public InventoryProductDTO Product { get; set; }
	}
}
