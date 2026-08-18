using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005BD RID: 1469
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetReservationsByWhoMadeItInDateRangeResp
	{
		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06001E4A RID: 7754 RVA: 0x0000DCF9 File Offset: 0x0000BEF9
		// (set) Token: 0x06001E4B RID: 7755 RVA: 0x0000DD01 File Offset: 0x0000BF01
		[DataMember]
		public IList<InventoryReservationDTO> Reservations { get; set; }
	}
}
