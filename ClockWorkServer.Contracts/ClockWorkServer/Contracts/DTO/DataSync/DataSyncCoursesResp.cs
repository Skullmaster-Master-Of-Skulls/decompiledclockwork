using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x020006FE RID: 1790
	[DataContract(Namespace = "http://tpro.ca")]
	public class DataSyncCoursesResp
	{
		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06002472 RID: 9330 RVA: 0x00010A18 File Offset: 0x0000EC18
		// (set) Token: 0x06002473 RID: 9331 RVA: 0x00010A20 File Offset: 0x0000EC20
		[DataMember]
		public IList<DataSyncExternalCourseSyncResultDTO> Results { get; set; }
	}
}
