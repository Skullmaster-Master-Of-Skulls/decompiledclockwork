using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews.ViewEntities;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A66 RID: 2662
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestsFullResp
	{
		// Token: 0x17001451 RID: 5201
		// (get) Token: 0x060037D9 RID: 14297 RVA: 0x0001B22D File Offset: 0x0001942D
		// (set) Token: 0x060037DA RID: 14298 RVA: 0x0001B235 File Offset: 0x00019435
		[DataMember]
		public IList<TestBookingFullDTO> BookingsLarge { get; set; }

		// Token: 0x17001452 RID: 5202
		// (get) Token: 0x060037DB RID: 14299 RVA: 0x0001B23E File Offset: 0x0001943E
		// (set) Token: 0x060037DC RID: 14300 RVA: 0x0001B246 File Offset: 0x00019446
		[DataMember]
		public IList<string> ExtendedColumnNames { get; set; }
	}
}
