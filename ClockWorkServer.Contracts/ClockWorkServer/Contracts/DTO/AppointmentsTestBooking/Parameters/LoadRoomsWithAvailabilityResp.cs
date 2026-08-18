using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A88 RID: 2696
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadRoomsWithAvailabilityResp
	{
		// Token: 0x17001489 RID: 5257
		// (get) Token: 0x0600386B RID: 14443 RVA: 0x0001B5E5 File Offset: 0x000197E5
		// (set) Token: 0x0600386C RID: 14444 RVA: 0x0001B5ED File Offset: 0x000197ED
		[DataMember]
		public IList<AppointmentRoomWithAvailabilityDTO> RoomsWithAvailability { get; set; }
	}
}
