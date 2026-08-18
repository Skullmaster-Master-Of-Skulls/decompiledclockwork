using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009C6 RID: 2502
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestRulesResp
	{
		// Token: 0x170012A5 RID: 4773
		// (get) Token: 0x060033E3 RID: 13283 RVA: 0x000193B9 File Offset: 0x000175B9
		// (set) Token: 0x060033E4 RID: 13284 RVA: 0x000193C1 File Offset: 0x000175C1
		[DataMember]
		public IList<TestRuleDTO> TestRules { get; set; }
	}
}
