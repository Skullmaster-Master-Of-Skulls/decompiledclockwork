using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200057D RID: 1405
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductBySerialNumberResp
	{
		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06001D4A RID: 7498 RVA: 0x0000D679 File Offset: 0x0000B879
		// (set) Token: 0x06001D4B RID: 7499 RVA: 0x0000D681 File Offset: 0x0000B881
		[DataMember]
		public InventoryProductDTO Product { get; set; }
	}
}
