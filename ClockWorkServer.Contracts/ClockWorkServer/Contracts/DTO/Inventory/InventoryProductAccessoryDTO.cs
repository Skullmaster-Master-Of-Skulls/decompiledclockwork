using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000572 RID: 1394
	[DataContract(Namespace = "http://tpro.ca")]
	public class InventoryProductAccessoryDTO
	{
		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06001CCB RID: 7371 RVA: 0x0000D29F File Offset: 0x0000B49F
		// (set) Token: 0x06001CCC RID: 7372 RVA: 0x0000D2A7 File Offset: 0x0000B4A7
		[DataMember]
		public string Name { get; set; }

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06001CCD RID: 7373 RVA: 0x0000D2B0 File Offset: 0x0000B4B0
		// (set) Token: 0x06001CCE RID: 7374 RVA: 0x0000D2B8 File Offset: 0x0000B4B8
		[DataMember]
		public string Description { get; set; }
	}
}
