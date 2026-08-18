using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000594 RID: 1428
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChangeProductsCategoryReq : BaseMessageReq
	{
		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06001D9D RID: 7581 RVA: 0x0000D877 File Offset: 0x0000BA77
		// (set) Token: 0x06001D9E RID: 7582 RVA: 0x0000D87F File Offset: 0x0000BA7F
		[DataMember]
		public IList<int> Products { get; set; }

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06001D9F RID: 7583 RVA: 0x0000D888 File Offset: 0x0000BA88
		// (set) Token: 0x06001DA0 RID: 7584 RVA: 0x0000D890 File Offset: 0x0000BA90
		[DataMember]
		public string CategoryName { get; set; }
	}
}
