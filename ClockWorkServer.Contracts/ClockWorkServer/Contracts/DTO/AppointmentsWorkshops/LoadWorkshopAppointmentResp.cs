using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x0200090E RID: 2318
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadWorkshopAppointmentResp
	{
		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x06002F0A RID: 12042 RVA: 0x00016590 File Offset: 0x00014790
		// (set) Token: 0x06002F0B RID: 12043 RVA: 0x00016598 File Offset: 0x00014798
		[DataMember]
		public WorkshopAppointmentDTO Appointment { get; set; }
	}
}
