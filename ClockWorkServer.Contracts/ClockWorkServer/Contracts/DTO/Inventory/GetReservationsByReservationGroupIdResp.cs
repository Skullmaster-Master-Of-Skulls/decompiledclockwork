using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005CD RID: 1485
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetReservationsByReservationGroupIdResp
	{
		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06001E76 RID: 7798 RVA: 0x0000DDE7 File Offset: 0x0000BFE7
		// (set) Token: 0x06001E77 RID: 7799 RVA: 0x0000DDEF File Offset: 0x0000BFEF
		[DataMember]
		public IList<InventoryReservationDTO> Reservations { get; set; }
	}
}
