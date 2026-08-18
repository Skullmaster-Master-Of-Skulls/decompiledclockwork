using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BCF RID: 3023
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCompletedJobsByStudentAndDateRangeResp
	{
		// Token: 0x17001782 RID: 6018
		// (get) Token: 0x06003FC5 RID: 16325 RVA: 0x0001F58C File Offset: 0x0001D78C
		// (set) Token: 0x06003FC6 RID: 16326 RVA: 0x0001F594 File Offset: 0x0001D794
		[DataMember]
		public IList<CompletedMediaJobDTO> MediaJobList { get; set; }
	}
}
