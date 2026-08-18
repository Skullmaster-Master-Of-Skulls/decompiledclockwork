using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005CA RID: 1482
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateReservationGroupReq : BaseMessageReq
	{
		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06001E6F RID: 7791 RVA: 0x0000DDC5 File Offset: 0x0000BFC5
		// (set) Token: 0x06001E70 RID: 7792 RVA: 0x0000DDCD File Offset: 0x0000BFCD
		[DataMember]
		public InventoryReservationGroupDTO ReservationGroup { get; set; }
	}
}
