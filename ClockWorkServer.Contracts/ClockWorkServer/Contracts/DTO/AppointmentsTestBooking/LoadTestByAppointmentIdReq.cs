using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A04 RID: 2564
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestByAppointmentIdReq : BaseMessageReq
	{
		// Token: 0x1700132E RID: 4910
		// (get) Token: 0x06003537 RID: 13623 RVA: 0x00019DE1 File Offset: 0x00017FE1
		// (set) Token: 0x06003538 RID: 13624 RVA: 0x00019DE9 File Offset: 0x00017FE9
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
