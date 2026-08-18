using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x0200071C RID: 1820
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetNotetakerPreviewExternalCoursesByStudentNumberResp
	{
		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x06002582 RID: 9602 RVA: 0x00011221 File Offset: 0x0000F421
		// (set) Token: 0x06002583 RID: 9603 RVA: 0x00011229 File Offset: 0x0000F429
		[DataMember]
		public IList<DataSyncExternalCourseDTO> ExternalCourses { get; set; }
	}
}
