using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200057A RID: 1402
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductByIdReq : BaseMessageReq
	{
		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06001D3D RID: 7485 RVA: 0x0000D624 File Offset: 0x0000B824
		// (set) Token: 0x06001D3E RID: 7486 RVA: 0x0000D62C File Offset: 0x0000B82C
		[DataMember]
		public int WorkingCatalogId { get; set; }

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06001D3F RID: 7487 RVA: 0x0000D635 File Offset: 0x0000B835
		// (set) Token: 0x06001D40 RID: 7488 RVA: 0x0000D63D File Offset: 0x0000B83D
		[DataMember]
		public string ProductUniqueId { get; set; }
	}
}
