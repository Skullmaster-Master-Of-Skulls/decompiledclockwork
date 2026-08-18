using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B32 RID: 2866
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentWithSpecialPermissionsReq : BaseMessageReq
	{
		// Token: 0x17001622 RID: 5666
		// (get) Token: 0x06003C4C RID: 15436 RVA: 0x0001D441 File Offset: 0x0001B641
		// (set) Token: 0x06003C4D RID: 15437 RVA: 0x0001D449 File Offset: 0x0001B649
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
