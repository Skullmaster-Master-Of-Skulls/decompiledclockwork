using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009E1 RID: 2529
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetTestCourseReq : BaseMessageReq
	{
		// Token: 0x17001304 RID: 4868
		// (get) Token: 0x060034C0 RID: 13504 RVA: 0x00019B17 File Offset: 0x00017D17
		// (set) Token: 0x060034C1 RID: 13505 RVA: 0x00019B1F File Offset: 0x00017D1F
		[DataMember]
		public AppointmentDTO Appointment { get; set; }
	}
}
