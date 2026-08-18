using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A11 RID: 2577
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadClassTestsForExamRequestByDateRangeResp
	{
		// Token: 0x17001342 RID: 4930
		// (get) Token: 0x0600356C RID: 13676 RVA: 0x00019F35 File Offset: 0x00018135
		// (set) Token: 0x0600356D RID: 13677 RVA: 0x00019F3D File Offset: 0x0001813D
		[DataMember]
		public IList<ClassTestForExamRequestDTO> ClassTestsForExamRequest { get; set; }
	}
}
