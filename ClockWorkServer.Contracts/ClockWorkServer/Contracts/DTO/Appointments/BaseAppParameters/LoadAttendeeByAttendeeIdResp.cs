using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200095E RID: 2398
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAttendeeByAttendeeIdResp
	{
		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x060030FB RID: 12539 RVA: 0x00017E2A File Offset: 0x0001602A
		// (set) Token: 0x060030FC RID: 12540 RVA: 0x00017E32 File Offset: 0x00016032
		[DataMember]
		public AttendeeDTO Attendee { get; set; }
	}
}
