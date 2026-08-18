using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BD1 RID: 3025
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCancelledJobsByStudentAndDateRangeResp
	{
		// Token: 0x17001787 RID: 6023
		// (get) Token: 0x06003FD1 RID: 16337 RVA: 0x0001F5E1 File Offset: 0x0001D7E1
		// (set) Token: 0x06003FD2 RID: 16338 RVA: 0x0001F5E9 File Offset: 0x0001D7E9
		[DataMember]
		public IList<CancelledMediaJobDTO> MediaJobList { get; set; }
	}
}
