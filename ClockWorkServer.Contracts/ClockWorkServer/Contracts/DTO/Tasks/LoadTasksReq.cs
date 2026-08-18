using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001F3 RID: 499
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTasksReq : BaseMessageReq
	{
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x000053EB File Offset: 0x000035EB
		// (set) Token: 0x06000B6B RID: 2923 RVA: 0x000053F3 File Offset: 0x000035F3
		[DataMember]
		public bool IncludePrivateTasks { get; set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x000053FC File Offset: 0x000035FC
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x00005404 File Offset: 0x00003604
		[DataMember]
		public bool IncludeSharedTasks { get; set; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0000540D File Offset: 0x0000360D
		// (set) Token: 0x06000B6F RID: 2927 RVA: 0x00005415 File Offset: 0x00003615
		[DataMember]
		public bool IncludeAssignedTasks { get; set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0000541E File Offset: 0x0000361E
		// (set) Token: 0x06000B71 RID: 2929 RVA: 0x00005426 File Offset: 0x00003626
		[DataMember]
		public eTaskPartDTO TaskParts { get; set; }
	}
}
