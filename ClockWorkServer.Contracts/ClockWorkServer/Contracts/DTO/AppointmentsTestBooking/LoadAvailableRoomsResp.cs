using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009C4 RID: 2500
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAvailableRoomsResp
	{
		// Token: 0x170012A1 RID: 4769
		// (get) Token: 0x060033D9 RID: 13273 RVA: 0x00019375 File Offset: 0x00017575
		// (set) Token: 0x060033DA RID: 13274 RVA: 0x0001937D File Offset: 0x0001757D
		[DataMember]
		public IList<RoomDTO> Rooms { get; set; }
	}
}
