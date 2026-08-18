using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BC9 RID: 3017
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCompletedJobsResp
	{
		// Token: 0x17001778 RID: 6008
		// (get) Token: 0x06003FAB RID: 16299 RVA: 0x0001F4E2 File Offset: 0x0001D6E2
		// (set) Token: 0x06003FAC RID: 16300 RVA: 0x0001F4EA File Offset: 0x0001D6EA
		[DataMember]
		public IList<CompletedMediaJobDTO> MediaJobList { get; set; }
	}
}
