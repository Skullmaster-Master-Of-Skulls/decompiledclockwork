using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x0200071A RID: 1818
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetNotetakerPreviewExternalCoursesByUserNameResp
	{
		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x0600257C RID: 9596 RVA: 0x000111FF File Offset: 0x0000F3FF
		// (set) Token: 0x0600257D RID: 9597 RVA: 0x00011207 File Offset: 0x0000F407
		[DataMember]
		public IList<DataSyncExternalCourseDTO> ExternalCourses { get; set; }
	}
}
