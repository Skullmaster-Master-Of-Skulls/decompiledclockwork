using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews.ViewEntities;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A70 RID: 2672
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestSmallByAppIdResp
	{
		// Token: 0x17001465 RID: 5221
		// (get) Token: 0x0600380B RID: 14347 RVA: 0x0001B381 File Offset: 0x00019581
		// (set) Token: 0x0600380C RID: 14348 RVA: 0x0001B389 File Offset: 0x00019589
		[DataMember]
		public TestBookingSmallDTO BookingsSmall { get; set; }
	}
}
