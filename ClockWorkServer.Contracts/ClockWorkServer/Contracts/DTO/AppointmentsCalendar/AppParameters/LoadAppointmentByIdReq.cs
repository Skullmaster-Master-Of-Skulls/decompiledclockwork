using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B17 RID: 2839
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentByIdReq : BaseMessageReq
	{
		// Token: 0x170015FC RID: 5628
		// (get) Token: 0x06003BE5 RID: 15333 RVA: 0x0001D1BB File Offset: 0x0001B3BB
		// (set) Token: 0x06003BE6 RID: 15334 RVA: 0x0001D1C3 File Offset: 0x0001B3C3
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
