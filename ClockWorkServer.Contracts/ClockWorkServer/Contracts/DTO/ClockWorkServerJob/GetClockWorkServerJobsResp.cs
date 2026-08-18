using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x0200084E RID: 2126
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetClockWorkServerJobsResp
	{
		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x06002B74 RID: 11124 RVA: 0x00014A25 File Offset: 0x00012C25
		// (set) Token: 0x06002B75 RID: 11125 RVA: 0x00014A2D File Offset: 0x00012C2D
		[DataMember]
		public IList<ClockWorkServerJobInfoDTO> ClockWorkServerJobInfoList { get; set; }
	}
}
