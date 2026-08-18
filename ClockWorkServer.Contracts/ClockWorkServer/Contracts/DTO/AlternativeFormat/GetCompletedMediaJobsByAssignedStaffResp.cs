using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BC3 RID: 3011
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCompletedMediaJobsByAssignedStaffResp
	{
		// Token: 0x1700176E RID: 5998
		// (get) Token: 0x06003F91 RID: 16273 RVA: 0x0001F438 File Offset: 0x0001D638
		// (set) Token: 0x06003F92 RID: 16274 RVA: 0x0001F440 File Offset: 0x0001D640
		[DataMember]
		public IList<CompletedMediaJobDTO> MediaJobList { get; set; }
	}
}
