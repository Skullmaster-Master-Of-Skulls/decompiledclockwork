using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A6D RID: 2669
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestFullByAppIdReq : BaseReportMessageReq
	{
		// Token: 0x17001460 RID: 5216
		// (get) Token: 0x060037FE RID: 14334 RVA: 0x0001B32C File Offset: 0x0001952C
		// (set) Token: 0x060037FF RID: 14335 RVA: 0x0001B334 File Offset: 0x00019534
		[DataMember]
		public BookingsManagementContextDTO Context { get; set; }

		// Token: 0x17001461 RID: 5217
		// (get) Token: 0x06003800 RID: 14336 RVA: 0x0001B33D File Offset: 0x0001953D
		// (set) Token: 0x06003801 RID: 14337 RVA: 0x0001B345 File Offset: 0x00019545
		[DataMember]
		public int AppId { get; set; }
	}
}
