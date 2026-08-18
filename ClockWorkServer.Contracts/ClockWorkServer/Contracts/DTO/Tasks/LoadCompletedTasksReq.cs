using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001F5 RID: 501
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCompletedTasksReq : BaseMessageReq
	{
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x00005440 File Offset: 0x00003640
		// (set) Token: 0x06000B77 RID: 2935 RVA: 0x00005448 File Offset: 0x00003648
		[DataMember]
		public bool IncludePrivateTasks { get; set; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x00005451 File Offset: 0x00003651
		// (set) Token: 0x06000B79 RID: 2937 RVA: 0x00005459 File Offset: 0x00003659
		[DataMember]
		public bool IncludeSharedTasks { get; set; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x00005462 File Offset: 0x00003662
		// (set) Token: 0x06000B7B RID: 2939 RVA: 0x0000546A File Offset: 0x0000366A
		[DataMember]
		public bool IncludeAssignedTasks { get; set; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x00005473 File Offset: 0x00003673
		// (set) Token: 0x06000B7D RID: 2941 RVA: 0x0000547B File Offset: 0x0000367B
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x00005484 File Offset: 0x00003684
		// (set) Token: 0x06000B7F RID: 2943 RVA: 0x0000548C File Offset: 0x0000368C
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
