using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005B0 RID: 1456
	[DataContract(Namespace = "http://tpro.ca")]
	public class InventoryReservationDTO
	{
		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06001E03 RID: 7683 RVA: 0x0000DB0C File Offset: 0x0000BD0C
		// (set) Token: 0x06001E04 RID: 7684 RVA: 0x0000DB14 File Offset: 0x0000BD14
		[DataMember]
		public int ReservationId { get; set; }

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06001E05 RID: 7685 RVA: 0x0000DB1D File Offset: 0x0000BD1D
		// (set) Token: 0x06001E06 RID: 7686 RVA: 0x0000DB25 File Offset: 0x0000BD25
		[DataMember]
		public InventoryProductDTO ReservedProduct { get; set; }

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06001E07 RID: 7687 RVA: 0x0000DB2E File Offset: 0x0000BD2E
		// (set) Token: 0x06001E08 RID: 7688 RVA: 0x0000DB36 File Offset: 0x0000BD36
		[DataMember]
		public InventoryReservationGroupDTO Group { get; set; }
	}
}
