using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007C7 RID: 1991
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadIsLookupCourseExemptFromDataSyncResp
	{
		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x060028BA RID: 10426 RVA: 0x000134C0 File Offset: 0x000116C0
		// (set) Token: 0x060028BB RID: 10427 RVA: 0x000134C8 File Offset: 0x000116C8
		[DataMember]
		public IDictionary<int, bool> IsExemptFromDataSyncList { get; set; }
	}
}
