using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000582 RID: 1410
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductsByRootCategoryReq : BaseMessageReq
	{
		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06001D5B RID: 7515 RVA: 0x0000D6DF File Offset: 0x0000B8DF
		// (set) Token: 0x06001D5C RID: 7516 RVA: 0x0000D6E7 File Offset: 0x0000B8E7
		[DataMember]
		public int WorkingCatalogId { get; set; }

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06001D5D RID: 7517 RVA: 0x0000D6F0 File Offset: 0x0000B8F0
		// (set) Token: 0x06001D5E RID: 7518 RVA: 0x0000D6F8 File Offset: 0x0000B8F8
		[DataMember]
		public string RootCategoryName { get; set; }
	}
}
