using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B23 RID: 2851
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateAppointmentResp
	{
		// Token: 0x1700160E RID: 5646
		// (get) Token: 0x06003C15 RID: 15381 RVA: 0x0001D2ED File Offset: 0x0001B4ED
		// (set) Token: 0x06003C16 RID: 15382 RVA: 0x0001D2F5 File Offset: 0x0001B4F5
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
