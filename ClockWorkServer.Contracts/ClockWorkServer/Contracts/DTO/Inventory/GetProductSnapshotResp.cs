using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200059B RID: 1435
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductSnapshotResp
	{
		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06001DB8 RID: 7608 RVA: 0x0000D921 File Offset: 0x0000BB21
		// (set) Token: 0x06001DB9 RID: 7609 RVA: 0x0000D929 File Offset: 0x0000BB29
		[DataMember]
		public InventoryProductSnapshotDTO ProductSnapshot { get; set; }
	}
}
