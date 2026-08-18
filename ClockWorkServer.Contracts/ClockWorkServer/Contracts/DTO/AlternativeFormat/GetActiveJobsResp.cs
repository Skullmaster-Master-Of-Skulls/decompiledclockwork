using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BB9 RID: 3001
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveJobsResp
	{
		// Token: 0x17001760 RID: 5984
		// (get) Token: 0x06003F6B RID: 16235 RVA: 0x0001F34A File Offset: 0x0001D54A
		// (set) Token: 0x06003F6C RID: 16236 RVA: 0x0001F352 File Offset: 0x0001D552
		[DataMember]
		public IList<MediaJobDTO> MediaJobList { get; set; }
	}
}
