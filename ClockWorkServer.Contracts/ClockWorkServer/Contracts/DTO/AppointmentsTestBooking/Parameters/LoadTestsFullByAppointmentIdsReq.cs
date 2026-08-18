using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A77 RID: 2679
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestsFullByAppointmentIdsReq : BaseReportMessageReq
	{
		// Token: 0x1700146F RID: 5231
		// (get) Token: 0x06003826 RID: 14374 RVA: 0x0001B42B File Offset: 0x0001962B
		// (set) Token: 0x06003827 RID: 14375 RVA: 0x0001B433 File Offset: 0x00019633
		[DataMember]
		public BookingsManagementContextDTO Context { get; set; }

		// Token: 0x17001470 RID: 5232
		// (get) Token: 0x06003828 RID: 14376 RVA: 0x0001B43C File Offset: 0x0001963C
		// (set) Token: 0x06003829 RID: 14377 RVA: 0x0001B444 File Offset: 0x00019644
		[DataMember]
		public IList<int> AppointmentIds { get; set; }
	}
}
