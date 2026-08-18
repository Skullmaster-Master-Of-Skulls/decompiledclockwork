using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x02000216 RID: 534
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateSurveyQueueItemStaffNoteResp
	{
		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x000058C4 File Offset: 0x00003AC4
		// (set) Token: 0x06000C20 RID: 3104 RVA: 0x000058CC File Offset: 0x00003ACC
		[DataMember]
		public SurveyQueueItemDTO RefreshedItem { get; set; }
	}
}
