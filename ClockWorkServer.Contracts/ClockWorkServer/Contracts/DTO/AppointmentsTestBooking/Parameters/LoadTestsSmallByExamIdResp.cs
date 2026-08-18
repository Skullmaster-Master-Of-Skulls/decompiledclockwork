using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews.ViewEntities;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A76 RID: 2678
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestsSmallByExamIdResp
	{
		// Token: 0x1700146E RID: 5230
		// (get) Token: 0x06003823 RID: 14371 RVA: 0x0001B41A File Offset: 0x0001961A
		// (set) Token: 0x06003824 RID: 14372 RVA: 0x0001B422 File Offset: 0x00019622
		[DataMember]
		public IList<TestBookingSmallDTO> BookingsSmall { get; set; }
	}
}
