using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B2A RID: 2858
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadBasicAppointmentInformationByUserAndDateRangeReq : BaseMessageReq
	{
		// Token: 0x17001617 RID: 5655
		// (get) Token: 0x06003C2E RID: 15406 RVA: 0x0001D386 File Offset: 0x0001B586
		// (set) Token: 0x06003C2F RID: 15407 RVA: 0x0001D38E File Offset: 0x0001B58E
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17001618 RID: 5656
		// (get) Token: 0x06003C30 RID: 15408 RVA: 0x0001D397 File Offset: 0x0001B597
		// (set) Token: 0x06003C31 RID: 15409 RVA: 0x0001D39F File Offset: 0x0001B59F
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17001619 RID: 5657
		// (get) Token: 0x06003C32 RID: 15410 RVA: 0x0001D3A8 File Offset: 0x0001B5A8
		// (set) Token: 0x06003C33 RID: 15411 RVA: 0x0001D3B0 File Offset: 0x0001B5B0
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x1700161A RID: 5658
		// (get) Token: 0x06003C34 RID: 15412 RVA: 0x0001D3B9 File Offset: 0x0001B5B9
		// (set) Token: 0x06003C35 RID: 15413 RVA: 0x0001D3C1 File Offset: 0x0001B5C1
		[DataMember]
		public bool HideCancelledAppointments { get; set; }
	}
}
