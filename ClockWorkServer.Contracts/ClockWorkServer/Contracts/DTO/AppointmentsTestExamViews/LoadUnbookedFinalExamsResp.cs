using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestExamViews
{
	// Token: 0x020009A9 RID: 2473
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadUnbookedFinalExamsResp
	{
		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x0600322C RID: 12844 RVA: 0x000185D6 File Offset: 0x000167D6
		// (set) Token: 0x0600322D RID: 12845 RVA: 0x000185DE File Offset: 0x000167DE
		[DataMember]
		public IList<PotentialFinalExamBookingDTO> PotentialFinalExams { get; set; }
	}
}
