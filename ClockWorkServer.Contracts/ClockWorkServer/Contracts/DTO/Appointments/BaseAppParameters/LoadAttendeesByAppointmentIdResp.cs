using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200095A RID: 2394
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAttendeesByAppointmentIdResp
	{
		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x060030ED RID: 12525 RVA: 0x00017DD5 File Offset: 0x00015FD5
		// (set) Token: 0x060030EE RID: 12526 RVA: 0x00017DDD File Offset: 0x00015FDD
		[DataMember]
		public IList<AttendeeDTO> Attendees { get; set; }
	}
}
