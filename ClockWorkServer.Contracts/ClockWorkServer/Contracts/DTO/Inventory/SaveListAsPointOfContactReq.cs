using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005A5 RID: 1445
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveListAsPointOfContactReq : BaseMsmqMessageReq
	{
		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06001DE4 RID: 7652 RVA: 0x0000DA62 File Offset: 0x0000BC62
		// (set) Token: 0x06001DE5 RID: 7653 RVA: 0x0000DA6A File Offset: 0x0000BC6A
		[DataMember]
		public IList<InventoryProductSnapshotDTO> ProductSnapshotList { get; set; }
	}
}
