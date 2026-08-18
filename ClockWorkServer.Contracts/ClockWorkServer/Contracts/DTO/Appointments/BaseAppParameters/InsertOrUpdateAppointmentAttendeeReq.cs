using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000961 RID: 2401
	[DataContract(Namespace = "http://tpro.ca")]
	public class InsertOrUpdateAppointmentAttendeeReq : BaseMessageReq
	{
		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x06003106 RID: 12550 RVA: 0x00017E6E File Offset: 0x0001606E
		// (set) Token: 0x06003107 RID: 12551 RVA: 0x00017E76 File Offset: 0x00016076
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x06003108 RID: 12552 RVA: 0x00017E7F File Offset: 0x0001607F
		// (set) Token: 0x06003109 RID: 12553 RVA: 0x00017E87 File Offset: 0x00016087
		[DataMember]
		public AttendeeDTO Attendee { get; set; }
	}
}
