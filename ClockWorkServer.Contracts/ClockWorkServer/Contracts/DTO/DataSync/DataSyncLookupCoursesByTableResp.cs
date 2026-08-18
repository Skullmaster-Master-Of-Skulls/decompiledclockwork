using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000700 RID: 1792
	[DataContract(Namespace = "http://tpro.ca")]
	public class DataSyncLookupCoursesByTableResp
	{
		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x0600247A RID: 9338 RVA: 0x00010A4B File Offset: 0x0000EC4B
		// (set) Token: 0x0600247B RID: 9339 RVA: 0x00010A53 File Offset: 0x0000EC53
		[DataMember]
		public IList<DataSyncExternalCourseSyncResultDTO> Results { get; set; }
	}
}
