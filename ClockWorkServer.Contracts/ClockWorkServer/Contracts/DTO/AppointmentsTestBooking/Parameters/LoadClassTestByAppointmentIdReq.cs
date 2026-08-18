using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A5E RID: 2654
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadClassTestByAppointmentIdReq : BaseMessageReq
	{
		// Token: 0x17001441 RID: 5185
		// (get) Token: 0x060037B1 RID: 14257 RVA: 0x0001B11D File Offset: 0x0001931D
		// (set) Token: 0x060037B2 RID: 14258 RVA: 0x0001B125 File Offset: 0x00019325
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
