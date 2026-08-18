using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B06 RID: 2822
	[DataContract(Namespace = "http://tpro.ca")]
	public class MarkStudentBannedFromOnlineAppointmentBookingResp
	{
		// Token: 0x170015E3 RID: 5603
		// (get) Token: 0x06003BA2 RID: 15266 RVA: 0x0001D012 File Offset: 0x0001B212
		// (set) Token: 0x06003BA3 RID: 15267 RVA: 0x0001D01A File Offset: 0x0001B21A
		[DataMember]
		public DateTime? DateStudentWasBannedUntil { get; set; }
	}
}
