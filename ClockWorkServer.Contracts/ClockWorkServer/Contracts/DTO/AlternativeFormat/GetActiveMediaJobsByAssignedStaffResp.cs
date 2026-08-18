using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BB3 RID: 2995
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveMediaJobsByAssignedStaffResp
	{
		// Token: 0x1700175B RID: 5979
		// (get) Token: 0x06003F5B RID: 16219 RVA: 0x0001F2F5 File Offset: 0x0001D4F5
		// (set) Token: 0x06003F5C RID: 16220 RVA: 0x0001F2FD File Offset: 0x0001D4FD
		[DataMember]
		public IList<MediaJobDTO> MediaJobList { get; set; }
	}
}
