using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000850 RID: 2128
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetClockWorkServerJobsExResp
	{
		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x06002B78 RID: 11128 RVA: 0x00014A36 File Offset: 0x00012C36
		// (set) Token: 0x06002B79 RID: 11129 RVA: 0x00014A3E File Offset: 0x00012C3E
		[DataMember]
		public IList<ClockWorkServerJobInfoExDTO> ClockWorkServerJobInfoExList { get; set; }
	}
}
