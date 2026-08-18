using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000584 RID: 1412
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductsByCategoryReq : BaseMessageReq
	{
		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06001D63 RID: 7523 RVA: 0x0000D712 File Offset: 0x0000B912
		// (set) Token: 0x06001D64 RID: 7524 RVA: 0x0000D71A File Offset: 0x0000B91A
		[DataMember]
		public int WorkingCatalogId { get; set; }

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06001D65 RID: 7525 RVA: 0x0000D723 File Offset: 0x0000B923
		// (set) Token: 0x06001D66 RID: 7526 RVA: 0x0000D72B File Offset: 0x0000B92B
		[DataMember]
		public string CategoryName { get; set; }
	}
}
