using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B1E RID: 2846
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDeletedAppointmentByIdReq : BaseMessageReq
	{
		// Token: 0x1700160A RID: 5642
		// (get) Token: 0x06003C08 RID: 15368 RVA: 0x0001D2A9 File Offset: 0x0001B4A9
		// (set) Token: 0x06003C09 RID: 15369 RVA: 0x0001D2B1 File Offset: 0x0001B4B1
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
