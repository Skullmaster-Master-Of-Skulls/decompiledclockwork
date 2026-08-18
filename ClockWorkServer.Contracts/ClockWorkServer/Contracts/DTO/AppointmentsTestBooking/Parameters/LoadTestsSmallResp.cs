using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews.ViewEntities;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A68 RID: 2664
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestsSmallResp
	{
		// Token: 0x17001457 RID: 5207
		// (get) Token: 0x060037E7 RID: 14311 RVA: 0x0001B293 File Offset: 0x00019493
		// (set) Token: 0x060037E8 RID: 14312 RVA: 0x0001B29B File Offset: 0x0001949B
		[DataMember]
		public IList<TestBookingSmallDTO> BookingsSmall { get; set; }

		// Token: 0x17001458 RID: 5208
		// (get) Token: 0x060037E9 RID: 14313 RVA: 0x0001B2A4 File Offset: 0x000194A4
		// (set) Token: 0x060037EA RID: 14314 RVA: 0x0001B2AC File Offset: 0x000194AC
		[DataMember]
		public IList<string> ExtendedColumnNames { get; set; }
	}
}
