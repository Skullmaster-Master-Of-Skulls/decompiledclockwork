using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BD3 RID: 3027
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCompletedJobsByStaffAndDateRangeResp
	{
		// Token: 0x1700178C RID: 6028
		// (get) Token: 0x06003FDD RID: 16349 RVA: 0x0001F636 File Offset: 0x0001D836
		// (set) Token: 0x06003FDE RID: 16350 RVA: 0x0001F63E File Offset: 0x0001D83E
		[DataMember]
		public IList<CompletedMediaJobDTO> MediaJobList { get; set; }
	}
}
