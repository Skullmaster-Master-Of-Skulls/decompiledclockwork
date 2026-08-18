using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews.ViewEntities;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A74 RID: 2676
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestsFullByExamIdResp
	{
		// Token: 0x1700146B RID: 5227
		// (get) Token: 0x0600381B RID: 14363 RVA: 0x0001B3E7 File Offset: 0x000195E7
		// (set) Token: 0x0600381C RID: 14364 RVA: 0x0001B3EF File Offset: 0x000195EF
		[DataMember]
		public IList<TestBookingFullDTO> BookingsLarge { get; set; }
	}
}
