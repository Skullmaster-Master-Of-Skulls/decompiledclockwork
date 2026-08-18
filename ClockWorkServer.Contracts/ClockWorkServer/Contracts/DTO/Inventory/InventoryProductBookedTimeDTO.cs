using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000577 RID: 1399
	[DataContract(Namespace = "http://tpro.ca")]
	public class InventoryProductBookedTimeDTO
	{
		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06001D22 RID: 7458 RVA: 0x0000D558 File Offset: 0x0000B758
		// (set) Token: 0x06001D23 RID: 7459 RVA: 0x0000D560 File Offset: 0x0000B760
		[DataMember]
		public int Id { get; set; }

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06001D24 RID: 7460 RVA: 0x0000D569 File Offset: 0x0000B769
		// (set) Token: 0x06001D25 RID: 7461 RVA: 0x0000D571 File Offset: 0x0000B771
		[DataMember]
		public Guid ProductUniqueId { get; set; }

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06001D26 RID: 7462 RVA: 0x0000D57A File Offset: 0x0000B77A
		// (set) Token: 0x06001D27 RID: 7463 RVA: 0x0000D582 File Offset: 0x0000B782
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06001D28 RID: 7464 RVA: 0x0000D58B File Offset: 0x0000B78B
		// (set) Token: 0x06001D29 RID: 7465 RVA: 0x0000D593 File Offset: 0x0000B793
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06001D2A RID: 7466 RVA: 0x0000D59C File Offset: 0x0000B79C
		// (set) Token: 0x06001D2B RID: 7467 RVA: 0x0000D5A4 File Offset: 0x0000B7A4
		[DataMember]
		public PersonBaseDTO To { get; set; }

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06001D2C RID: 7468 RVA: 0x0000D5AD File Offset: 0x0000B7AD
		// (set) Token: 0x06001D2D RID: 7469 RVA: 0x0000D5B5 File Offset: 0x0000B7B5
		[DataMember]
		public PersonBaseDTO From { get; set; }

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06001D2E RID: 7470 RVA: 0x0000D5BE File Offset: 0x0000B7BE
		// (set) Token: 0x06001D2F RID: 7471 RVA: 0x0000D5C6 File Offset: 0x0000B7C6
		[DataMember]
		public InventoryProductBookingType BookingType { get; set; }
	}
}
