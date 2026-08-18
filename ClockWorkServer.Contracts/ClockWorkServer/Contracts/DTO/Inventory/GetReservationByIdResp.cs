using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005B3 RID: 1459
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetReservationByIdResp
	{
		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06001E20 RID: 7712 RVA: 0x0000DBE9 File Offset: 0x0000BDE9
		// (set) Token: 0x06001E21 RID: 7713 RVA: 0x0000DBF1 File Offset: 0x0000BDF1
		[DataMember]
		public InventoryReservationDTO Reservation { get; set; }
	}
}
