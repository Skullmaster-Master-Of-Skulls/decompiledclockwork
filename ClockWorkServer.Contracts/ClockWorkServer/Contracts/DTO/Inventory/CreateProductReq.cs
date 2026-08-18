using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200058C RID: 1420
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateProductReq : BaseMessageReq
	{
		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06001D81 RID: 7553 RVA: 0x0000D7CD File Offset: 0x0000B9CD
		// (set) Token: 0x06001D82 RID: 7554 RVA: 0x0000D7D5 File Offset: 0x0000B9D5
		[DataMember]
		public InventoryProductDTO Product { get; set; }
	}
}
