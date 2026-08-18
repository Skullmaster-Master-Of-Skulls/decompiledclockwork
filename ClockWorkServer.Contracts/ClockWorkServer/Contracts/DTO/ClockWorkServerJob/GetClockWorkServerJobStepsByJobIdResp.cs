using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000864 RID: 2148
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetClockWorkServerJobStepsByJobIdResp
	{
		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06002BB2 RID: 11186 RVA: 0x00014B79 File Offset: 0x00012D79
		// (set) Token: 0x06002BB3 RID: 11187 RVA: 0x00014B81 File Offset: 0x00012D81
		[DataMember]
		public IList<ClockWorkServerJobStepDTO> ClockWorkServerJobStepList { get; set; }
	}
}
