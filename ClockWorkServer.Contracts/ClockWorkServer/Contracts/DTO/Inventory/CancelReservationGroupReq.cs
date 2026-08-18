using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005C6 RID: 1478
	[DataContract(Namespace = "http://tpro.ca")]
	public class CancelReservationGroupReq : BaseMessageReq
	{
		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06001E65 RID: 7781 RVA: 0x0000DD92 File Offset: 0x0000BF92
		// (set) Token: 0x06001E66 RID: 7782 RVA: 0x0000DD9A File Offset: 0x0000BF9A
		[DataMember]
		public int ReservationGroupId { get; set; }
	}
}
