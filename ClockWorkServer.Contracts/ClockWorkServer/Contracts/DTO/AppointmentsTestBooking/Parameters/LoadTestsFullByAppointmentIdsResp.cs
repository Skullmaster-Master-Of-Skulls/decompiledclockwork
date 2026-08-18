using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews.ViewEntities;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A78 RID: 2680
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestsFullByAppointmentIdsResp
	{
		// Token: 0x17001471 RID: 5233
		// (get) Token: 0x0600382B RID: 14379 RVA: 0x0001B44D File Offset: 0x0001964D
		// (set) Token: 0x0600382C RID: 14380 RVA: 0x0001B455 File Offset: 0x00019655
		[DataMember]
		public IList<TestBookingFullDTO> BookingsFull { get; set; }
	}
}
