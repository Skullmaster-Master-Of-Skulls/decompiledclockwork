using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005B9 RID: 1465
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetReservationsResp
	{
		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06001E3A RID: 7738 RVA: 0x0000DC93 File Offset: 0x0000BE93
		// (set) Token: 0x06001E3B RID: 7739 RVA: 0x0000DC9B File Offset: 0x0000BE9B
		[DataMember]
		public IList<InventoryReservationDTO> Reservations { get; set; }
	}
}
