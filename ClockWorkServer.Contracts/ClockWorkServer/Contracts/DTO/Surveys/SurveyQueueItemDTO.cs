using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x02000221 RID: 545
	[DataContract(Namespace = "http://tpro.ca")]
	public class SurveyQueueItemDTO
	{
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x0000597F File Offset: 0x00003B7F
		// (set) Token: 0x06000C41 RID: 3137 RVA: 0x00005987 File Offset: 0x00003B87
		[DataMember]
		public int PeopleSurveyId { get; set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x00005990 File Offset: 0x00003B90
		// (set) Token: 0x06000C43 RID: 3139 RVA: 0x00005998 File Offset: 0x00003B98
		[DataMember]
		public BasicPersonDTO Student { get; set; }

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x000059A1 File Offset: 0x00003BA1
		// (set) Token: 0x06000C45 RID: 3141 RVA: 0x000059A9 File Offset: 0x00003BA9
		[DataMember]
		public BasicPersonDTO AssignedCounsellor { get; set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x000059B2 File Offset: 0x00003BB2
		// (set) Token: 0x06000C47 RID: 3143 RVA: 0x000059BA File Offset: 0x00003BBA
		[DataMember]
		public SurveyForDisplayDTO Survey { get; set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x000059C3 File Offset: 0x00003BC3
		// (set) Token: 0x06000C49 RID: 3145 RVA: 0x000059CB File Offset: 0x00003BCB
		[DataMember]
		public DateTime DateEntered { get; set; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x000059D4 File Offset: 0x00003BD4
		// (set) Token: 0x06000C4B RID: 3147 RVA: 0x000059DC File Offset: 0x00003BDC
		[DataMember]
		public SurveyStatusDTO Status { get; set; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x000059E5 File Offset: 0x00003BE5
		// (set) Token: 0x06000C4D RID: 3149 RVA: 0x000059ED File Offset: 0x00003BED
		[DataMember]
		public string StudentEmail { get; set; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x000059F6 File Offset: 0x00003BF6
		// (set) Token: 0x06000C4F RID: 3151 RVA: 0x000059FE File Offset: 0x00003BFE
		[DataMember]
		public string StaffNote { get; set; }
	}
}
