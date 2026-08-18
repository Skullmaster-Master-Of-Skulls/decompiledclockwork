using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BB7 RID: 2999
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveExpiredMediaJobsResp
	{
		// Token: 0x1700175E RID: 5982
		// (get) Token: 0x06003F65 RID: 16229 RVA: 0x0001F328 File Offset: 0x0001D528
		// (set) Token: 0x06003F66 RID: 16230 RVA: 0x0001F330 File Offset: 0x0001D530
		[DataMember]
		public IList<MediaJobDTO> MediaJobList { get; set; }
	}
}
