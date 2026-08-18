using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B22 RID: 2850
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateAppointmentReq : BaseMessageReq
	{
		// Token: 0x1700160D RID: 5645
		// (get) Token: 0x06003C12 RID: 15378 RVA: 0x0001D2DC File Offset: 0x0001B4DC
		// (set) Token: 0x06003C13 RID: 15379 RVA: 0x0001D2E4 File Offset: 0x0001B4E4
		[DataMember]
		public AppointmentDTO Appointment { get; set; }
	}
}
