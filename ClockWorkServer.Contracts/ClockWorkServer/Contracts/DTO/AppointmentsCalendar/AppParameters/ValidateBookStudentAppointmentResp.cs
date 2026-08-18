using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent.BookingRequest;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B02 RID: 2818
	[DataContract(Namespace = "http://tpro.ca")]
	public class ValidateBookStudentAppointmentResp
	{
		// Token: 0x170015DF RID: 5599
		// (get) Token: 0x06003B96 RID: 15254 RVA: 0x0001CFCE File Offset: 0x0001B1CE
		// (set) Token: 0x06003B97 RID: 15255 RVA: 0x0001CFD6 File Offset: 0x0001B1D6
		[DataMember]
		public AppointmentBookingResDTO BookingResult { get; set; }
	}
}
