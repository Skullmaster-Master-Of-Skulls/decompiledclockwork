using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x0200070F RID: 1807
	[DataContract(Namespace = "http://tpro.ca")]
	public class RunCourseDataSyncByStudentNumberReq : BaseReportMessageReq
	{
		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06002559 RID: 9561 RVA: 0x00011133 File Offset: 0x0000F333
		// (set) Token: 0x0600255A RID: 9562 RVA: 0x0001113B File Offset: 0x0000F33B
		[DataMember]
		public string Student_no { get; set; }
	}
}
