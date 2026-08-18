using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B14 RID: 2836
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentByIdResp
	{
		// Token: 0x170015F9 RID: 5625
		// (get) Token: 0x06003BDC RID: 15324 RVA: 0x0001D188 File Offset: 0x0001B388
		// (set) Token: 0x06003BDD RID: 15325 RVA: 0x0001D190 File Offset: 0x0001B390
		[DataMember]
		public AppointmentDTO Appointment { get; set; }
	}
}
