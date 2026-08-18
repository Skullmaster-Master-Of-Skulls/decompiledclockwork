using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews.ViewEntities;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A7A RID: 2682
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestsSmallByAppointmentIdsResp
	{
		// Token: 0x17001474 RID: 5236
		// (get) Token: 0x06003833 RID: 14387 RVA: 0x0001B480 File Offset: 0x00019680
		// (set) Token: 0x06003834 RID: 14388 RVA: 0x0001B488 File Offset: 0x00019688
		[DataMember]
		public IList<TestBookingSmallDTO> BookingsSmall { get; set; }
	}
}
