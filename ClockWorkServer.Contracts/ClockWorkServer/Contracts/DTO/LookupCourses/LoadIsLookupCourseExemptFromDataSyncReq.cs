using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007C6 RID: 1990
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadIsLookupCourseExemptFromDataSyncReq : BaseMessageReq
	{
		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x060028B7 RID: 10423 RVA: 0x000134AF File Offset: 0x000116AF
		// (set) Token: 0x060028B8 RID: 10424 RVA: 0x000134B7 File Offset: 0x000116B7
		[DataMember]
		public IList<int> LuCourseIds { get; set; }
	}
}
