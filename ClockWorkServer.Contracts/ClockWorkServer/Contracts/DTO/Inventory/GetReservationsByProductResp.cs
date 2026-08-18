using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005B5 RID: 1461
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetReservationsByProductResp
	{
		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06001E26 RID: 7718 RVA: 0x0000DC0B File Offset: 0x0000BE0B
		// (set) Token: 0x06001E27 RID: 7719 RVA: 0x0000DC13 File Offset: 0x0000BE13
		[DataMember]
		public IList<InventoryReservationDTO> Reservations { get; set; }
	}
}
