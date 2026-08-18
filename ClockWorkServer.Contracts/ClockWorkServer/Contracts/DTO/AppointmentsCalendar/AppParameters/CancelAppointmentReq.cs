using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B1A RID: 2842
	[DataContract(Namespace = "http://tpro.ca")]
	public class CancelAppointmentReq : BaseMessageReq
	{
		// Token: 0x170015FF RID: 5631
		// (get) Token: 0x06003BEE RID: 15342 RVA: 0x0001D1EE File Offset: 0x0001B3EE
		// (set) Token: 0x06003BEF RID: 15343 RVA: 0x0001D1F6 File Offset: 0x0001B3F6
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17001600 RID: 5632
		// (get) Token: 0x06003BF0 RID: 15344 RVA: 0x0001D1FF File Offset: 0x0001B3FF
		// (set) Token: 0x06003BF1 RID: 15345 RVA: 0x0001D207 File Offset: 0x0001B407
		[DataMember]
		public AppCancelInfoDTO AppointmentCancelInfo { get; set; }
	}
}
