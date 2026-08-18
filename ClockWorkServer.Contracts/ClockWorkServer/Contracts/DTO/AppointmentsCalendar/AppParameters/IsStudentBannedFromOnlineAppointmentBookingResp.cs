using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B04 RID: 2820
	[DataContract(Namespace = "http://tpro.ca")]
	public class IsStudentBannedFromOnlineAppointmentBookingResp
	{
		// Token: 0x170015E1 RID: 5601
		// (get) Token: 0x06003B9C RID: 15260 RVA: 0x0001CFF0 File Offset: 0x0001B1F0
		// (set) Token: 0x06003B9D RID: 15261 RVA: 0x0001CFF8 File Offset: 0x0001B1F8
		[DataMember]
		public bool StudentIsBanned { get; set; }
	}
}
