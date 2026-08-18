using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000574 RID: 1396
	[DataContract(Namespace = "http://tpro.ca")]
	public class InventoryProductStatusDTO
	{
		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06001CDB RID: 7387 RVA: 0x0000D316 File Offset: 0x0000B516
		// (set) Token: 0x06001CDC RID: 7388 RVA: 0x0000D31E File Offset: 0x0000B51E
		[DataMember]
		public int ProductStatusId { get; set; }

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06001CDD RID: 7389 RVA: 0x0000D327 File Offset: 0x0000B527
		// (set) Token: 0x06001CDE RID: 7390 RVA: 0x0000D32F File Offset: 0x0000B52F
		[DataMember]
		public string Name { get; set; }

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06001CDF RID: 7391 RVA: 0x0000D338 File Offset: 0x0000B538
		// (set) Token: 0x06001CE0 RID: 7392 RVA: 0x0000D340 File Offset: 0x0000B540
		[DataMember]
		public string Description { get; set; }
	}
}
