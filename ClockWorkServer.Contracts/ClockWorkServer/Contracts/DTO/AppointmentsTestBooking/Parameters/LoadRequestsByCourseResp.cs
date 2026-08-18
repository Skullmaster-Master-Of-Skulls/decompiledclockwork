using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A5A RID: 2650
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadRequestsByCourseResp
	{
		// Token: 0x1700143C RID: 5180
		// (get) Token: 0x060037A3 RID: 14243 RVA: 0x0001B0C8 File Offset: 0x000192C8
		// (set) Token: 0x060037A4 RID: 14244 RVA: 0x0001B0D0 File Offset: 0x000192D0
		[DataMember]
		public IList<ExamRequestDTO> ExamRequests { get; set; }
	}
}
