using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A84 RID: 2692
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllowedSeatsResp
	{
		// Token: 0x17001482 RID: 5250
		// (get) Token: 0x06003859 RID: 14425 RVA: 0x0001B56E File Offset: 0x0001976E
		// (set) Token: 0x0600385A RID: 14426 RVA: 0x0001B576 File Offset: 0x00019776
		[DataMember]
		public IList<AppointmentRoomDTO> Seats { get; set; }
	}
}
