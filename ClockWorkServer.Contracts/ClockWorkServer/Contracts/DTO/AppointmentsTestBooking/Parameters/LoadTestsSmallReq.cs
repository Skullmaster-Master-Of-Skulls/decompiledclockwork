using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A67 RID: 2663
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestsSmallReq : BaseReportMessageReq
	{
		// Token: 0x17001453 RID: 5203
		// (get) Token: 0x060037DE RID: 14302 RVA: 0x0001B24F File Offset: 0x0001944F
		// (set) Token: 0x060037DF RID: 14303 RVA: 0x0001B257 File Offset: 0x00019457
		[DataMember]
		public BookingsManagementContextDTO Context { get; set; }

		// Token: 0x17001454 RID: 5204
		// (get) Token: 0x060037E0 RID: 14304 RVA: 0x0001B260 File Offset: 0x00019460
		// (set) Token: 0x060037E1 RID: 14305 RVA: 0x0001B268 File Offset: 0x00019468
		[DataMember]
		public DateTime? StartDate { get; set; }

		// Token: 0x17001455 RID: 5205
		// (get) Token: 0x060037E2 RID: 14306 RVA: 0x0001B271 File Offset: 0x00019471
		// (set) Token: 0x060037E3 RID: 14307 RVA: 0x0001B279 File Offset: 0x00019479
		[DataMember]
		public DateTime? EndDate { get; set; }

		// Token: 0x17001456 RID: 5206
		// (get) Token: 0x060037E4 RID: 14308 RVA: 0x0001B282 File Offset: 0x00019482
		// (set) Token: 0x060037E5 RID: 14309 RVA: 0x0001B28A File Offset: 0x0001948A
		[DataMember]
		public bool HideCancelled { get; set; }
	}
}
