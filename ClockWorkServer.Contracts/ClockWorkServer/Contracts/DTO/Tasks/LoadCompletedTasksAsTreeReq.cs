using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001ED RID: 493
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCompletedTasksAsTreeReq : BaseMessageReq
	{
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x0000530E File Offset: 0x0000350E
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x00005316 File Offset: 0x00003516
		[DataMember]
		public bool IncludePrivateTasks { get; set; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x0000531F File Offset: 0x0000351F
		// (set) Token: 0x06000B4D RID: 2893 RVA: 0x00005327 File Offset: 0x00003527
		[DataMember]
		public bool IncludeSharedTasks { get; set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x00005330 File Offset: 0x00003530
		// (set) Token: 0x06000B4F RID: 2895 RVA: 0x00005338 File Offset: 0x00003538
		[DataMember]
		public bool IncludeAssignedTasks { get; set; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x00005341 File Offset: 0x00003541
		// (set) Token: 0x06000B51 RID: 2897 RVA: 0x00005349 File Offset: 0x00003549
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00005352 File Offset: 0x00003552
		// (set) Token: 0x06000B53 RID: 2899 RVA: 0x0000535A File Offset: 0x0000355A
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
