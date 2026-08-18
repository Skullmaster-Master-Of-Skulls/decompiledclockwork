using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200058D RID: 1421
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateProductResp
	{
		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06001D84 RID: 7556 RVA: 0x0000D7DE File Offset: 0x0000B9DE
		// (set) Token: 0x06001D85 RID: 7557 RVA: 0x0000D7E6 File Offset: 0x0000B9E6
		[DataMember]
		public string ProductUniqueId { get; set; }

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06001D86 RID: 7558 RVA: 0x0000D7EF File Offset: 0x0000B9EF
		// (set) Token: 0x06001D87 RID: 7559 RVA: 0x0000D7F7 File Offset: 0x0000B9F7
		[DataMember]
		public int ProductDynamicDataId { get; set; }

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06001D88 RID: 7560 RVA: 0x0000D800 File Offset: 0x0000BA00
		// (set) Token: 0x06001D89 RID: 7561 RVA: 0x0000D808 File Offset: 0x0000BA08
		[DataMember]
		public string Barcode { get; set; }
	}
}
