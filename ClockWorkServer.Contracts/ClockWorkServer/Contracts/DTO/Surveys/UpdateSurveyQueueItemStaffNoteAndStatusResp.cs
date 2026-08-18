using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x02000214 RID: 532
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateSurveyQueueItemStaffNoteAndStatusResp
	{
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x00005891 File Offset: 0x00003A91
		// (set) Token: 0x06000C18 RID: 3096 RVA: 0x00005899 File Offset: 0x00003A99
		[DataMember]
		public SurveyQueueItemDTO RefreshedItem { get; set; }
	}
}
