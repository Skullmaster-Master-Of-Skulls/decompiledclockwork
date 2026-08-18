using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Surveys;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x02000211 RID: 529
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadSurveyQueueItemsReq : BaseMessageReq
	{
		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x000057F8 File Offset: 0x000039F8
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x00005800 File Offset: 0x00003A00
		[DataMember]
		public int SurveyId { get; set; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x00005809 File Offset: 0x00003A09
		// (set) Token: 0x06000C05 RID: 3077 RVA: 0x00005811 File Offset: 0x00003A11
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x0000581A File Offset: 0x00003A1A
		// (set) Token: 0x06000C07 RID: 3079 RVA: 0x00005822 File Offset: 0x00003A22
		[DataMember]
		public DateTime? EndDate { get; set; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0000582B File Offset: 0x00003A2B
		// (set) Token: 0x06000C09 RID: 3081 RVA: 0x00005833 File Offset: 0x00003A33
		[DataMember]
		public int FilterByAssignedCounsellorPid { get; set; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0000583C File Offset: 0x00003A3C
		// (set) Token: 0x06000C0B RID: 3083 RVA: 0x00005844 File Offset: 0x00003A44
		[DataMember]
		public eSurveyStatusType[] SurveyTypesToExclude { get; set; }
	}
}
