using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200059D RID: 1437
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductHistoryByIdResp
	{
		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06001DC0 RID: 7616 RVA: 0x0000D954 File Offset: 0x0000BB54
		// (set) Token: 0x06001DC1 RID: 7617 RVA: 0x0000D95C File Offset: 0x0000BB5C
		[DataMember]
		public IList<InventoryProductSnapshotDTO> ProductSnapshotList { get; set; }
	}
}
