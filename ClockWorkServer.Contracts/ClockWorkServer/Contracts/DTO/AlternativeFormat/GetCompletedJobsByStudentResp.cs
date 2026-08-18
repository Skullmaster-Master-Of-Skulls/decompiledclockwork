using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BCD RID: 3021
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCompletedJobsByStudentResp
	{
		// Token: 0x1700177D RID: 6013
		// (get) Token: 0x06003FB9 RID: 16313 RVA: 0x0001F537 File Offset: 0x0001D737
		// (set) Token: 0x06003FBA RID: 16314 RVA: 0x0001F53F File Offset: 0x0001D73F
		[DataMember]
		public IList<CompletedMediaJobDTO> MediaJobList { get; set; }
	}
}
