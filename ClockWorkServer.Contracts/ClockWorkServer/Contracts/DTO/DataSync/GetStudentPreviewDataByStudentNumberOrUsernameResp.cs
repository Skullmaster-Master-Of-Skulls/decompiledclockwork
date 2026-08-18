using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync.Student;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000722 RID: 1826
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetStudentPreviewDataByStudentNumberOrUsernameResp
	{
		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06002598 RID: 9624 RVA: 0x000112A9 File Offset: 0x0000F4A9
		// (set) Token: 0x06002599 RID: 9625 RVA: 0x000112B1 File Offset: 0x0000F4B1
		[DataMember]
		public StudentDataSyncPreviewDataDTO DataSyncPreviewData { get; set; }
	}
}
