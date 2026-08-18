using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200059F RID: 1439
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductHistoryByBarcodeResp
	{
		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06001DC8 RID: 7624 RVA: 0x0000D987 File Offset: 0x0000BB87
		// (set) Token: 0x06001DC9 RID: 7625 RVA: 0x0000D98F File Offset: 0x0000BB8F
		[DataMember]
		public IList<InventoryProductSnapshotDTO> ProductSnapshotList { get; set; }
	}
}
