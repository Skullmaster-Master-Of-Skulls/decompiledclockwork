using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A6F RID: 2671
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestSmallByAppIdReq : BaseReportMessageReq
	{
		// Token: 0x17001463 RID: 5219
		// (get) Token: 0x06003806 RID: 14342 RVA: 0x0001B35F File Offset: 0x0001955F
		// (set) Token: 0x06003807 RID: 14343 RVA: 0x0001B367 File Offset: 0x00019567
		[DataMember]
		public BookingsManagementContextDTO Context { get; set; }

		// Token: 0x17001464 RID: 5220
		// (get) Token: 0x06003808 RID: 14344 RVA: 0x0001B370 File Offset: 0x00019570
		// (set) Token: 0x06003809 RID: 14345 RVA: 0x0001B378 File Offset: 0x00019578
		[DataMember]
		public int AppId { get; set; }
	}
}
