using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync.Notetaking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000720 RID: 1824
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetNotetakerPreviewDataResp
	{
		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06002590 RID: 9616 RVA: 0x00011276 File Offset: 0x0000F476
		// (set) Token: 0x06002591 RID: 9617 RVA: 0x0001127E File Offset: 0x0000F47E
		[DataMember]
		public NotetakerWithExternalCoursesDTO NotetakerWithExternalCourses { get; set; }
	}
}
