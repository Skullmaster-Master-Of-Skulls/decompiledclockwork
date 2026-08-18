using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent.BookingRequest;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B00 RID: 2816
	[DataContract(Namespace = "http://tpro.ca")]
	public class TryToBookStudentAppointmentResp
	{
		// Token: 0x170015DA RID: 5594
		// (get) Token: 0x06003B8A RID: 15242 RVA: 0x0001CF79 File Offset: 0x0001B179
		// (set) Token: 0x06003B8B RID: 15243 RVA: 0x0001CF81 File Offset: 0x0001B181
		[DataMember]
		public AppointmentBookingResDTO BookingResult { get; set; }
	}
}
