using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B20 RID: 2848
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateAppointmentReq : BaseMessageReq
	{
		// Token: 0x1700160C RID: 5644
		// (get) Token: 0x06003C0E RID: 15374 RVA: 0x0001D2CB File Offset: 0x0001B4CB
		// (set) Token: 0x06003C0F RID: 15375 RVA: 0x0001D2D3 File Offset: 0x0001B4D3
		[DataMember]
		public AppointmentDTO Appointment { get; set; }
	}
}
