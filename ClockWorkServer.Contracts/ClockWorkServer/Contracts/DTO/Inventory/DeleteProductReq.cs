using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200058E RID: 1422
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteProductReq : BaseMessageReq
	{
		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06001D8B RID: 7563 RVA: 0x0000D811 File Offset: 0x0000BA11
		// (set) Token: 0x06001D8C RID: 7564 RVA: 0x0000D819 File Offset: 0x0000BA19
		[DataMember]
		public string ProductUniqueId { get; set; }
	}
}
