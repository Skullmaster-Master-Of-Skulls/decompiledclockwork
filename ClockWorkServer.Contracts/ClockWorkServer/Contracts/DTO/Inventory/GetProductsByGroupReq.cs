using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000586 RID: 1414
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductsByGroupReq : BaseMessageReq
	{
		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06001D6B RID: 7531 RVA: 0x0000D745 File Offset: 0x0000B945
		// (set) Token: 0x06001D6C RID: 7532 RVA: 0x0000D74D File Offset: 0x0000B94D
		[DataMember]
		public int WorkingCatalogId { get; set; }

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06001D6D RID: 7533 RVA: 0x0000D756 File Offset: 0x0000B956
		// (set) Token: 0x06001D6E RID: 7534 RVA: 0x0000D75E File Offset: 0x0000B95E
		[DataMember]
		public int GroupId { get; set; }
	}
}
