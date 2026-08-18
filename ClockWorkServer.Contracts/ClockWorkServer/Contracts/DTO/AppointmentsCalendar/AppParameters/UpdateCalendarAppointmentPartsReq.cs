using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B24 RID: 2852
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateCalendarAppointmentPartsReq : BaseMessageReq
	{
		// Token: 0x1700160F RID: 5647
		// (get) Token: 0x06003C18 RID: 15384 RVA: 0x0001D2FE File Offset: 0x0001B4FE
		// (set) Token: 0x06003C19 RID: 15385 RVA: 0x0001D306 File Offset: 0x0001B506
		[DataMember]
		public AppointmentDTO Appointment { get; set; }

		// Token: 0x17001610 RID: 5648
		// (get) Token: 0x06003C1A RID: 15386 RVA: 0x0001D30F File Offset: 0x0001B50F
		// (set) Token: 0x06003C1B RID: 15387 RVA: 0x0001D317 File Offset: 0x0001B517
		[DataMember]
		public eAppointmentPart PartsToUpdate { get; set; }
	}
}
