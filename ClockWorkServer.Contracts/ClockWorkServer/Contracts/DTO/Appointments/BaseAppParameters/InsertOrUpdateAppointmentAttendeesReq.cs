using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000963 RID: 2403
	[DataContract(Namespace = "http://tpro.ca")]
	public class InsertOrUpdateAppointmentAttendeesReq : BaseMessageReq
	{
		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x0600310E RID: 12558 RVA: 0x00017EA1 File Offset: 0x000160A1
		// (set) Token: 0x0600310F RID: 12559 RVA: 0x00017EA9 File Offset: 0x000160A9
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x06003110 RID: 12560 RVA: 0x00017EB2 File Offset: 0x000160B2
		// (set) Token: 0x06003111 RID: 12561 RVA: 0x00017EBA File Offset: 0x000160BA
		[DataMember]
		public IList<AttendeeDTO> Attendees { get; set; }
	}
}
