using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BCB RID: 3019
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCancelledJobsResp
	{
		// Token: 0x1700177A RID: 6010
		// (get) Token: 0x06003FB1 RID: 16305 RVA: 0x0001F504 File Offset: 0x0001D704
		// (set) Token: 0x06003FB2 RID: 16306 RVA: 0x0001F50C File Offset: 0x0001D70C
		[DataMember]
		public IList<CancelledMediaJobDTO> MediaJobList { get; set; }
	}
}
