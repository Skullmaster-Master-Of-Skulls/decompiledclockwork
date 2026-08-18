using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BBB RID: 3003
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveJobsByStudentResp
	{
		// Token: 0x17001763 RID: 5987
		// (get) Token: 0x06003F73 RID: 16243 RVA: 0x0001F37D File Offset: 0x0001D57D
		// (set) Token: 0x06003F74 RID: 16244 RVA: 0x0001F385 File Offset: 0x0001D585
		[DataMember]
		public IList<MediaJobDTO> MediaJobList { get; set; }
	}
}
