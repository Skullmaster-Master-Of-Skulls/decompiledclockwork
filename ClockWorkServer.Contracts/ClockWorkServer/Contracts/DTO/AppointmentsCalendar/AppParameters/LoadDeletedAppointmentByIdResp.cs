using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B1F RID: 2847
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDeletedAppointmentByIdResp
	{
		// Token: 0x1700160B RID: 5643
		// (get) Token: 0x06003C0B RID: 15371 RVA: 0x0001D2BA File Offset: 0x0001B4BA
		// (set) Token: 0x06003C0C RID: 15372 RVA: 0x0001D2C2 File Offset: 0x0001B4C2
		[DataMember]
		public AppointmentDTO Appointment { get; set; }
	}
}
