using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent.BookingRequest;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x0200019F RID: 415
	[DataContract(Namespace = "http://tpro.ca")]
	public class TryToBookTutorAppointmentResp
	{
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x00004570 File Offset: 0x00002770
		// (set) Token: 0x060009A4 RID: 2468 RVA: 0x00004578 File Offset: 0x00002778
		[DataMember]
		public AppointmentBookingResDTO BookingResult { get; set; }
	}
}
