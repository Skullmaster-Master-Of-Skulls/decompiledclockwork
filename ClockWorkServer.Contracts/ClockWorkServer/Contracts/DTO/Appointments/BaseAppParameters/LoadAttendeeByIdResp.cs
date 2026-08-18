using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200095C RID: 2396
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAttendeeByIdResp
	{
		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x060030F5 RID: 12533 RVA: 0x00017E08 File Offset: 0x00016008
		// (set) Token: 0x060030F6 RID: 12534 RVA: 0x00017E10 File Offset: 0x00016010
		[DataMember]
		public AttendeeDTO Attendee { get; set; }
	}
}
