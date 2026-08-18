using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x02000215 RID: 533
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateSurveyQueueItemStaffNoteReq : BaseMessageReq
	{
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x000058A2 File Offset: 0x00003AA2
		// (set) Token: 0x06000C1B RID: 3099 RVA: 0x000058AA File Offset: 0x00003AAA
		[DataMember]
		public int PeopleSurveyId { get; set; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x000058B3 File Offset: 0x00003AB3
		// (set) Token: 0x06000C1D RID: 3101 RVA: 0x000058BB File Offset: 0x00003ABB
		[DataMember]
		public string NewStaffNote { get; set; }
	}
}
