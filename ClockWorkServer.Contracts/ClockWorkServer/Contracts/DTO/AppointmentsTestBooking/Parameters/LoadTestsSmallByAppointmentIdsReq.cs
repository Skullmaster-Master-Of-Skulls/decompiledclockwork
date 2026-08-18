using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A79 RID: 2681
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestsSmallByAppointmentIdsReq : BaseReportMessageReq
	{
		// Token: 0x17001472 RID: 5234
		// (get) Token: 0x0600382E RID: 14382 RVA: 0x0001B45E File Offset: 0x0001965E
		// (set) Token: 0x0600382F RID: 14383 RVA: 0x0001B466 File Offset: 0x00019666
		[DataMember]
		public BookingsManagementContextDTO Context { get; set; }

		// Token: 0x17001473 RID: 5235
		// (get) Token: 0x06003830 RID: 14384 RVA: 0x0001B46F File Offset: 0x0001966F
		// (set) Token: 0x06003831 RID: 14385 RVA: 0x0001B477 File Offset: 0x00019677
		[DataMember]
		public IList<int> AppointmentIds { get; set; }
	}
}
