using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x0200021B RID: 539
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteSurveyQueueItemReq : BaseMessageReq
	{
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x00005919 File Offset: 0x00003B19
		// (set) Token: 0x06000C2F RID: 3119 RVA: 0x00005921 File Offset: 0x00003B21
		[DataMember]
		public int PeopleSurveyId { get; set; }
	}
}
