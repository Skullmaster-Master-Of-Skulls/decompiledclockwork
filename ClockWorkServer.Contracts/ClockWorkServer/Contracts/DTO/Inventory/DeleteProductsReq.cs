using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000590 RID: 1424
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteProductsReq : BaseMessageReq
	{
		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06001D91 RID: 7569 RVA: 0x0000D833 File Offset: 0x0000BA33
		// (set) Token: 0x06001D92 RID: 7570 RVA: 0x0000D83B File Offset: 0x0000BA3B
		[DataMember]
		public IList<Guid> ProductUniqueIds { get; set; }
	}
}
