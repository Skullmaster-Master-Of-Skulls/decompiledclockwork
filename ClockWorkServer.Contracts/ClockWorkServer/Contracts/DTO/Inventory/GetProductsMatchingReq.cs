using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000578 RID: 1400
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductsMatchingReq : BaseMessageReq
	{
		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06001D31 RID: 7473 RVA: 0x0000D5CF File Offset: 0x0000B7CF
		// (set) Token: 0x06001D32 RID: 7474 RVA: 0x0000D5D7 File Offset: 0x0000B7D7
		[DataMember]
		public int WorkingCatalogId { get; set; }

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06001D33 RID: 7475 RVA: 0x0000D5E0 File Offset: 0x0000B7E0
		// (set) Token: 0x06001D34 RID: 7476 RVA: 0x0000D5E8 File Offset: 0x0000B7E8
		[DataMember]
		public string SearchText { get; set; }

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x0000D5F1 File Offset: 0x0000B7F1
		// (set) Token: 0x06001D36 RID: 7478 RVA: 0x0000D5F9 File Offset: 0x0000B7F9
		[DataMember]
		public InventoryProductSearchByField SearchByField { get; set; }

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06001D37 RID: 7479 RVA: 0x0000D602 File Offset: 0x0000B802
		// (set) Token: 0x06001D38 RID: 7480 RVA: 0x0000D60A File Offset: 0x0000B80A
		[DataMember]
		public bool ShowOnlyLoanedProducts { get; set; }
	}
}
