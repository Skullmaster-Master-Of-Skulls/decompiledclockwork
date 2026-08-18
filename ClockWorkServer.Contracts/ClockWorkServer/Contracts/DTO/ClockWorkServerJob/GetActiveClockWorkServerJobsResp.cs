using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000852 RID: 2130
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveClockWorkServerJobsResp
	{
		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x06002B7C RID: 11132 RVA: 0x00014A47 File Offset: 0x00012C47
		// (set) Token: 0x06002B7D RID: 11133 RVA: 0x00014A4F File Offset: 0x00012C4F
		[DataMember]
		public IList<ClockWorkServerJobInfoDTO> ClockWorkServerJobInfoList { get; set; }
	}
}
