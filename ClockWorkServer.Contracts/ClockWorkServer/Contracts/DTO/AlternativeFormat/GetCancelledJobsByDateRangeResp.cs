using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BC7 RID: 3015
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCancelledJobsByDateRangeResp
	{
		// Token: 0x17001776 RID: 6006
		// (get) Token: 0x06003FA5 RID: 16293 RVA: 0x0001F4C0 File Offset: 0x0001D6C0
		// (set) Token: 0x06003FA6 RID: 16294 RVA: 0x0001F4C8 File Offset: 0x0001D6C8
		[DataMember]
		public IList<CancelledMediaJobDTO> MediaJobList { get; set; }
	}
}
