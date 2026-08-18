using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005BB RID: 1467
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetReservationsByWhoMadeItResp
	{
		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06001E40 RID: 7744 RVA: 0x0000DCB5 File Offset: 0x0000BEB5
		// (set) Token: 0x06001E41 RID: 7745 RVA: 0x0000DCBD File Offset: 0x0000BEBD
		[DataMember]
		public IList<InventoryReservationDTO> Reservations { get; set; }
	}
}
