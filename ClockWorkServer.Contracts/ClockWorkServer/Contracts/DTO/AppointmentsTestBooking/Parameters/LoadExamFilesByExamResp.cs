using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A4A RID: 2634
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadExamFilesByExamResp
	{
		// Token: 0x17001424 RID: 5156
		// (get) Token: 0x06003763 RID: 14179 RVA: 0x0001AF30 File Offset: 0x00019130
		// (set) Token: 0x06003764 RID: 14180 RVA: 0x0001AF38 File Offset: 0x00019138
		[DataMember]
		public IList<ExamFileDTO> ExamFiles { get; set; }
	}
}
