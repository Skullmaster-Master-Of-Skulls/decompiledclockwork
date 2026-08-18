using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005C8 RID: 1480
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateReservationReq : BaseMessageReq
	{
		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06001E69 RID: 7785 RVA: 0x0000DDA3 File Offset: 0x0000BFA3
		// (set) Token: 0x06001E6A RID: 7786 RVA: 0x0000DDAB File Offset: 0x0000BFAB
		[DataMember]
		public InventoryReservationDTO Reservation { get; set; }
	}
}
