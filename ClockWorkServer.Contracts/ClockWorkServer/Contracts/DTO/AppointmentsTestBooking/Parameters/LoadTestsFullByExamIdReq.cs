using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A73 RID: 2675
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestsFullByExamIdReq : BaseReportMessageReq
	{
		// Token: 0x17001469 RID: 5225
		// (get) Token: 0x06003816 RID: 14358 RVA: 0x0001B3C5 File Offset: 0x000195C5
		// (set) Token: 0x06003817 RID: 14359 RVA: 0x0001B3CD File Offset: 0x000195CD
		[DataMember]
		public BookingsManagementContextDTO Context { get; set; }

		// Token: 0x1700146A RID: 5226
		// (get) Token: 0x06003818 RID: 14360 RVA: 0x0001B3D6 File Offset: 0x000195D6
		// (set) Token: 0x06003819 RID: 14361 RVA: 0x0001B3DE File Offset: 0x000195DE
		[DataMember]
		public int ExamId { get; set; }
	}
}
