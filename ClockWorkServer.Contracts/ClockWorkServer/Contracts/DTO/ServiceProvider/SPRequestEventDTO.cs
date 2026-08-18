using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider
{
	// Token: 0x02000279 RID: 633
	[DataContract(Namespace = "http://tpro.ca")]
	public class SPRequestEventDTO
	{
		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x0000719F File Offset: 0x0000539F
		// (set) Token: 0x06000F12 RID: 3858 RVA: 0x000071A7 File Offset: 0x000053A7
		[DataMember]
		public int SPRequestEventId { get; set; }

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000F13 RID: 3859 RVA: 0x000071B0 File Offset: 0x000053B0
		// (set) Token: 0x06000F14 RID: 3860 RVA: 0x000071B8 File Offset: 0x000053B8
		[DataMember]
		public DateTime StartDateTime { get; set; }

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000F15 RID: 3861 RVA: 0x000071C1 File Offset: 0x000053C1
		// (set) Token: 0x06000F16 RID: 3862 RVA: 0x000071C9 File Offset: 0x000053C9
		[DataMember]
		public DateTime EndDateTime { get; set; }

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x000071D2 File Offset: 0x000053D2
		// (set) Token: 0x06000F18 RID: 3864 RVA: 0x000071DA File Offset: 0x000053DA
		[DataMember]
		public SPRequestStatusTypeDTO RequestStatus { get; set; }

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x000071E3 File Offset: 0x000053E3
		// (set) Token: 0x06000F1A RID: 3866 RVA: 0x000071EB File Offset: 0x000053EB
		[DataMember]
		public SPRequestAssignmentStatusTypeDTO AssignmentStatus { get; set; }

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x000071F4 File Offset: 0x000053F4
		// (set) Token: 0x06000F1C RID: 3868 RVA: 0x000071FC File Offset: 0x000053FC
		[DataMember]
		public SPUrgencyLevelTypeDTO UrgencyLevel { get; set; }

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x00007205 File Offset: 0x00005405
		// (set) Token: 0x06000F1E RID: 3870 RVA: 0x0000720D File Offset: 0x0000540D
		[DataMember]
		public string Notes { get; set; }

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x00007216 File Offset: 0x00005416
		// (set) Token: 0x06000F20 RID: 3872 RVA: 0x0000721E File Offset: 0x0000541E
		[DataMember]
		public SPRequestEventAssignmentDTO Assignment { get; set; }

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x00007227 File Offset: 0x00005427
		// (set) Token: 0x06000F22 RID: 3874 RVA: 0x0000722F File Offset: 0x0000542F
		[DataMember]
		public bool IsRequired { get; set; }
	}
}
