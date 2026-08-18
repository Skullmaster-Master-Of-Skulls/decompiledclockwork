using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200058A RID: 1418
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateProductReq : BaseMessageReq
	{
		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06001D7B RID: 7547 RVA: 0x0000D7AB File Offset: 0x0000B9AB
		// (set) Token: 0x06001D7C RID: 7548 RVA: 0x0000D7B3 File Offset: 0x0000B9B3
		[DataMember]
		public InventoryProductDTO Product { get; set; }
	}
}
