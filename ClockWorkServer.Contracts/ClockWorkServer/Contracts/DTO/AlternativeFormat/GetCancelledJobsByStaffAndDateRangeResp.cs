using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BD5 RID: 3029
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCancelledJobsByStaffAndDateRangeResp
	{
		// Token: 0x17001791 RID: 6033
		// (get) Token: 0x06003FE9 RID: 16361 RVA: 0x0001F68B File Offset: 0x0001D88B
		// (set) Token: 0x06003FEA RID: 16362 RVA: 0x0001F693 File Offset: 0x0001D893
		[DataMember]
		public IList<CancelledMediaJobDTO> MediaJobList { get; set; }
	}
}
