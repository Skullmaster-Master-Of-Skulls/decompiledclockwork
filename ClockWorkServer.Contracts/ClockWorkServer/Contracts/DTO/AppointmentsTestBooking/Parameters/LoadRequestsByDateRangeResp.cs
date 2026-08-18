using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A56 RID: 2646
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadRequestsByDateRangeResp
	{
		// Token: 0x17001437 RID: 5175
		// (get) Token: 0x06003795 RID: 14229 RVA: 0x0001B073 File Offset: 0x00019273
		// (set) Token: 0x06003796 RID: 14230 RVA: 0x0001B07B File Offset: 0x0001927B
		[DataMember]
		public IList<ExamRequestDTO> ExamRequests { get; set; }
	}
}
