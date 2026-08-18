using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B1B RID: 2843
	[DataContract(Namespace = "http://tpro.ca")]
	public class UnCancelAppointmentReq : BaseMessageReq
	{
		// Token: 0x17001601 RID: 5633
		// (get) Token: 0x06003BF3 RID: 15347 RVA: 0x0001D210 File Offset: 0x0001B410
		// (set) Token: 0x06003BF4 RID: 15348 RVA: 0x0001D218 File Offset: 0x0001B418
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
