using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BC5 RID: 3013
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCompletedJobsByDateRangeResp
	{
		// Token: 0x17001772 RID: 6002
		// (get) Token: 0x06003F9B RID: 16283 RVA: 0x0001F47C File Offset: 0x0001D67C
		// (set) Token: 0x06003F9C RID: 16284 RVA: 0x0001F484 File Offset: 0x0001D684
		[DataMember]
		public IList<CompletedMediaJobDTO> MediaJobList { get; set; }
	}
}
