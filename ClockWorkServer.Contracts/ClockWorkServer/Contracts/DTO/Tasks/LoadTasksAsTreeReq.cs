using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001EB RID: 491
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTasksAsTreeReq : BaseMessageReq
	{
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x00005297 File Offset: 0x00003497
		// (set) Token: 0x06000B3B RID: 2875 RVA: 0x0000529F File Offset: 0x0000349F
		[DataMember]
		public bool IncludePrivateTasks { get; set; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x000052A8 File Offset: 0x000034A8
		// (set) Token: 0x06000B3D RID: 2877 RVA: 0x000052B0 File Offset: 0x000034B0
		[DataMember]
		public bool IncludeSharedTasks { get; set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x000052B9 File Offset: 0x000034B9
		// (set) Token: 0x06000B3F RID: 2879 RVA: 0x000052C1 File Offset: 0x000034C1
		[DataMember]
		public bool IncludeAssignedTasks { get; set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x000052CA File Offset: 0x000034CA
		// (set) Token: 0x06000B41 RID: 2881 RVA: 0x000052D2 File Offset: 0x000034D2
		[DataMember]
		public eTaskPartDTO PartsToLoad { get; set; }
	}
}
