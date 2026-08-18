using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews.ViewEntities;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A6E RID: 2670
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestFullByAppIdResp
	{
		// Token: 0x17001462 RID: 5218
		// (get) Token: 0x06003803 RID: 14339 RVA: 0x0001B34E File Offset: 0x0001954E
		// (set) Token: 0x06003804 RID: 14340 RVA: 0x0001B356 File Offset: 0x00019556
		[DataMember]
		public TestBookingFullDTO BookingsLarge { get; set; }
	}
}
