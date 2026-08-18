using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005C9 RID: 1481
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateReservationResp
	{
		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06001E6C RID: 7788 RVA: 0x0000DDB4 File Offset: 0x0000BFB4
		// (set) Token: 0x06001E6D RID: 7789 RVA: 0x0000DDBC File Offset: 0x0000BFBC
		[DataMember]
		public int ReservationGroupId { get; set; }
	}
}
