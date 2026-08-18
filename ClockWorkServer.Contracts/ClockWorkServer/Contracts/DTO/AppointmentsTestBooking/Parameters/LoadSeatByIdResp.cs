using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A86 RID: 2694
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadSeatByIdResp
	{
		// Token: 0x17001484 RID: 5252
		// (get) Token: 0x0600385F RID: 14431 RVA: 0x0001B590 File Offset: 0x00019790
		// (set) Token: 0x06003860 RID: 14432 RVA: 0x0001B598 File Offset: 0x00019798
		[DataMember]
		public AppointmentRoomDTO Seat { get; set; }
	}
}
