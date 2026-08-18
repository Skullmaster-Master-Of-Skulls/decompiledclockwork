using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000854 RID: 2132
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetClockWorkServerJobByIdResp
	{
		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06002B82 RID: 11138 RVA: 0x00014A69 File Offset: 0x00012C69
		// (set) Token: 0x06002B83 RID: 11139 RVA: 0x00014A71 File Offset: 0x00012C71
		[DataMember]
		public ClockWorkServerJobInfoDTO ClockWorkServerJob { get; set; }
	}
}
