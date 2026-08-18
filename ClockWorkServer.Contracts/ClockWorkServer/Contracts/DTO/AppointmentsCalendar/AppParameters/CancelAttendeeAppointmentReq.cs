using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B38 RID: 2872
	[DataContract(Namespace = "http://tpro.ca")]
	public class CancelAttendeeAppointmentReq : BaseMessageReq
	{
		// Token: 0x1700162F RID: 5679
		// (get) Token: 0x06003C6C RID: 15468 RVA: 0x0001D51E File Offset: 0x0001B71E
		// (set) Token: 0x06003C6D RID: 15469 RVA: 0x0001D526 File Offset: 0x0001B726
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17001630 RID: 5680
		// (get) Token: 0x06003C6E RID: 15470 RVA: 0x0001D52F File Offset: 0x0001B72F
		// (set) Token: 0x06003C6F RID: 15471 RVA: 0x0001D537 File Offset: 0x0001B737
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17001631 RID: 5681
		// (get) Token: 0x06003C70 RID: 15472 RVA: 0x0001D540 File Offset: 0x0001B740
		// (set) Token: 0x06003C71 RID: 15473 RVA: 0x0001D548 File Offset: 0x0001B748
		[DataMember]
		public AppCancelInfoDTO CancelInfo { get; set; }
	}
}
