using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005B7 RID: 1463
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetReservationsByProductInDateRangeResp
	{
		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06001E32 RID: 7730 RVA: 0x0000DC60 File Offset: 0x0000BE60
		// (set) Token: 0x06001E33 RID: 7731 RVA: 0x0000DC68 File Offset: 0x0000BE68
		[DataMember]
		public IList<InventoryReservationDTO> Reservations { get; set; }
	}
}
