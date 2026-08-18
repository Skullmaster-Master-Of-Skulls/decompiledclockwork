using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200056F RID: 1391
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateLocationReq : BaseMessageReq
	{
		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06001CA4 RID: 7332 RVA: 0x0000D16D File Offset: 0x0000B36D
		// (set) Token: 0x06001CA5 RID: 7333 RVA: 0x0000D175 File Offset: 0x0000B375
		[DataMember]
		public InventoryLocationDTO Location { get; set; }
	}
}
