using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000713 RID: 1811
	[DataContract(Namespace = "http://tpro.ca")]
	public class RunFullDataSyncForExistingStudentReq : BaseReportMessageReq
	{
		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x06002565 RID: 9573 RVA: 0x00011177 File Offset: 0x0000F377
		// (set) Token: 0x06002566 RID: 9574 RVA: 0x0001117F File Offset: 0x0000F37F
		[DataMember]
		public string Student_no { get; set; }

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06002567 RID: 9575 RVA: 0x00011188 File Offset: 0x0000F388
		// (set) Token: 0x06002568 RID: 9576 RVA: 0x00011190 File Offset: 0x0000F390
		[DataMember]
		public bool DontSyncCourses { get; set; }

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06002569 RID: 9577 RVA: 0x00011199 File Offset: 0x0000F399
		// (set) Token: 0x0600256A RID: 9578 RVA: 0x000111A1 File Offset: 0x0000F3A1
		[DataMember]
		public bool DontSyncData { get; set; }
	}
}
