using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000970 RID: 2416
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAttendeesByAppointmentIdsResp
	{
		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x06003155 RID: 12629 RVA: 0x0001808E File Offset: 0x0001628E
		// (set) Token: 0x06003156 RID: 12630 RVA: 0x00018096 File Offset: 0x00016296
		[DataMember]
		public Dictionary<int, List<AttendeeDTO>> AppointmentIdsWithAttendees { get; set; }
	}
}
