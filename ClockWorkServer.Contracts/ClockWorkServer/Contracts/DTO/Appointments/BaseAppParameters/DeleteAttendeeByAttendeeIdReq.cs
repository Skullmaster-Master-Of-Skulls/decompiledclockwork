using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000960 RID: 2400
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteAttendeeByAttendeeIdReq : BaseMessageReq
	{
		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x06003103 RID: 12547 RVA: 0x00017E5D File Offset: 0x0001605D
		// (set) Token: 0x06003104 RID: 12548 RVA: 0x00017E65 File Offset: 0x00016065
		[DataMember]
		public int AttendeeId { get; set; }
	}
}
