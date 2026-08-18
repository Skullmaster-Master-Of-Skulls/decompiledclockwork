using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000702 RID: 1794
	[DataContract(Namespace = "http://tpro.ca")]
	public class DataSyncLookupCoursesResp
	{
		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06002482 RID: 9346 RVA: 0x00010A7E File Offset: 0x0000EC7E
		// (set) Token: 0x06002483 RID: 9347 RVA: 0x00010A86 File Offset: 0x0000EC86
		[DataMember]
		public IList<DataSyncExternalCourseSyncResultDTO> Results { get; set; }
	}
}
