using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider
{
	// Token: 0x02000276 RID: 630
	[DataContract(Namespace = "http://tpro.ca")]
	public class SPRequestAssignmentStatusTypeDTO
	{
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x0000705C File Offset: 0x0000525C
		// (set) Token: 0x06000EE9 RID: 3817 RVA: 0x00007064 File Offset: 0x00005264
		[DataMember]
		public int SPRequestAssignmentStatusTypeId { get; set; }

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000EEA RID: 3818 RVA: 0x0000706D File Offset: 0x0000526D
		// (set) Token: 0x06000EEB RID: 3819 RVA: 0x00007075 File Offset: 0x00005275
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x0000707E File Offset: 0x0000527E
		// (set) Token: 0x06000EED RID: 3821 RVA: 0x00007086 File Offset: 0x00005286
		[DataMember]
		public string Description { get; set; }

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x0000708F File Offset: 0x0000528F
		// (set) Token: 0x06000EEF RID: 3823 RVA: 0x00007097 File Offset: 0x00005297
		[DataMember]
		public bool AssignmentIsCompleted { get; set; }

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x000070A0 File Offset: 0x000052A0
		// (set) Token: 0x06000EF1 RID: 3825 RVA: 0x000070A8 File Offset: 0x000052A8
		[DataMember]
		public SPUrgencyLevelTypeDTO UrgencyLevel { get; set; }
	}
}
