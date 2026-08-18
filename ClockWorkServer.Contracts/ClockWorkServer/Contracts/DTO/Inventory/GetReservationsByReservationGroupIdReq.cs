using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005CC RID: 1484
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetReservationsByReservationGroupIdReq : BaseMessageReq
	{
		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06001E73 RID: 7795 RVA: 0x0000DDD6 File Offset: 0x0000BFD6
		// (set) Token: 0x06001E74 RID: 7796 RVA: 0x0000DDDE File Offset: 0x0000BFDE
		[DataMember]
		public int ReservationGroupId { get; set; }
	}
}
