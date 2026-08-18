using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x0200071B RID: 1819
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetNotetakerPreviewExternalCoursesByStudentNumberReq : BaseReportMessageReq
	{
		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x0600257F RID: 9599 RVA: 0x00011210 File Offset: 0x0000F410
		// (set) Token: 0x06002580 RID: 9600 RVA: 0x00011218 File Offset: 0x0000F418
		[DataMember]
		public string StudentNumber { get; set; }
	}
}
