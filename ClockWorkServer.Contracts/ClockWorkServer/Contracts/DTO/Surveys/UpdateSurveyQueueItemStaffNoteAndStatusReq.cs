using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x02000213 RID: 531
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateSurveyQueueItemStaffNoteAndStatusReq : BaseMessageReq
	{
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x0000585E File Offset: 0x00003A5E
		// (set) Token: 0x06000C11 RID: 3089 RVA: 0x00005866 File Offset: 0x00003A66
		[DataMember]
		public int PeopleSurveyId { get; set; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x0000586F File Offset: 0x00003A6F
		// (set) Token: 0x06000C13 RID: 3091 RVA: 0x00005877 File Offset: 0x00003A77
		[DataMember]
		public int? NewPeopleSurveyStatusId { get; set; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x00005880 File Offset: 0x00003A80
		// (set) Token: 0x06000C15 RID: 3093 RVA: 0x00005888 File Offset: 0x00003A88
		[DataMember]
		public string NewStaffNote { get; set; }
	}
}
