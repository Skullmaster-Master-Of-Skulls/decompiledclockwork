using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000566 RID: 1382
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetLocationByIdResp
	{
		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06001C8B RID: 7307 RVA: 0x0000D0E5 File Offset: 0x0000B2E5
		// (set) Token: 0x06001C8C RID: 7308 RVA: 0x0000D0ED File Offset: 0x0000B2ED
		[DataMember]
		public InventoryLocationDTO Location { get; set; }
	}
}
