using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200095D RID: 2397
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAttendeeByAttendeeIdReq : BaseMessageReq
	{
		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x060030F8 RID: 12536 RVA: 0x00017E19 File Offset: 0x00016019
		// (set) Token: 0x060030F9 RID: 12537 RVA: 0x00017E21 File Offset: 0x00016021
		[DataMember]
		public int AttendeeId { get; set; }
	}
}
