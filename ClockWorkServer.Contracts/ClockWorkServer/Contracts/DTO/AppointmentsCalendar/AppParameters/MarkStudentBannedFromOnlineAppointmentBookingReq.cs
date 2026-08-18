using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B05 RID: 2821
	[DataContract(Namespace = "http://tpro.ca")]
	public class MarkStudentBannedFromOnlineAppointmentBookingReq : BaseMessageReq
	{
		// Token: 0x170015E2 RID: 5602
		// (get) Token: 0x06003B9F RID: 15263 RVA: 0x0001D001 File Offset: 0x0001B201
		// (set) Token: 0x06003BA0 RID: 15264 RVA: 0x0001D009 File Offset: 0x0001B209
		[DataMember]
		public int PersonId { get; set; }
	}
}
