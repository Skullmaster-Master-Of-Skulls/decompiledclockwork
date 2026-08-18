using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync.Notetaking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x0200071E RID: 1822
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetNotetakerPreviewDataByStudentNumberResp
	{
		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x0600258A RID: 9610 RVA: 0x00011254 File Offset: 0x0000F454
		// (set) Token: 0x0600258B RID: 9611 RVA: 0x0001125C File Offset: 0x0000F45C
		[DataMember]
		public NotetakerWithExternalCoursesDTO NotetakerWithExternalCourses { get; set; }
	}
}
