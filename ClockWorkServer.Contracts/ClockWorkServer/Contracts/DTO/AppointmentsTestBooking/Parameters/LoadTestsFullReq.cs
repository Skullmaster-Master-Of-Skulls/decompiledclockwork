using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A65 RID: 2661
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestsFullReq : BaseReportMessageReq
	{
		// Token: 0x1700144D RID: 5197
		// (get) Token: 0x060037D0 RID: 14288 RVA: 0x0001B1E9 File Offset: 0x000193E9
		// (set) Token: 0x060037D1 RID: 14289 RVA: 0x0001B1F1 File Offset: 0x000193F1
		[DataMember]
		public BookingsManagementContextDTO Context { get; set; }

		// Token: 0x1700144E RID: 5198
		// (get) Token: 0x060037D2 RID: 14290 RVA: 0x0001B1FA File Offset: 0x000193FA
		// (set) Token: 0x060037D3 RID: 14291 RVA: 0x0001B202 File Offset: 0x00019402
		[DataMember]
		public DateTime? StartDate { get; set; }

		// Token: 0x1700144F RID: 5199
		// (get) Token: 0x060037D4 RID: 14292 RVA: 0x0001B20B File Offset: 0x0001940B
		// (set) Token: 0x060037D5 RID: 14293 RVA: 0x0001B213 File Offset: 0x00019413
		[DataMember]
		public DateTime? EndDate { get; set; }

		// Token: 0x17001450 RID: 5200
		// (get) Token: 0x060037D6 RID: 14294 RVA: 0x0001B21C File Offset: 0x0001941C
		// (set) Token: 0x060037D7 RID: 14295 RVA: 0x0001B224 File Offset: 0x00019424
		[DataMember]
		public bool HideCancelled { get; set; }
	}
}
