using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A75 RID: 2677
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestsSmallByExamIdReq : BaseReportMessageReq
	{
		// Token: 0x1700146C RID: 5228
		// (get) Token: 0x0600381E RID: 14366 RVA: 0x0001B3F8 File Offset: 0x000195F8
		// (set) Token: 0x0600381F RID: 14367 RVA: 0x0001B400 File Offset: 0x00019600
		[DataMember]
		public BookingsManagementContextDTO Context { get; set; }

		// Token: 0x1700146D RID: 5229
		// (get) Token: 0x06003820 RID: 14368 RVA: 0x0001B409 File Offset: 0x00019609
		// (set) Token: 0x06003821 RID: 14369 RVA: 0x0001B411 File Offset: 0x00019611
		[DataMember]
		public int ExamId { get; set; }
	}
}
