using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent.BookingRequest;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x0200019E RID: 414
	[DataContract(Namespace = "http://tpro.ca")]
	public class TryToBookTutorAppointmentReq : BaseMessageReq
	{
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x0000454E File Offset: 0x0000274E
		// (set) Token: 0x0600099F RID: 2463 RVA: 0x00004556 File Offset: 0x00002756
		[DataMember]
		public AppointmentBookingReqDTO BookingRequest { get; set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x0000455F File Offset: 0x0000275F
		// (set) Token: 0x060009A1 RID: 2465 RVA: 0x00004567 File Offset: 0x00002767
		[DataMember]
		public bool BookAppointmentNow { get; set; }
	}
}
