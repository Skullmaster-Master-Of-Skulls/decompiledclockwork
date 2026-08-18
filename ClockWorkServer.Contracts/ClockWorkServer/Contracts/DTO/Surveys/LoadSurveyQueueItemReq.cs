using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x0200021F RID: 543
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadSurveyQueueItemReq : BaseMessageReq
	{
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x0000595D File Offset: 0x00003B5D
		// (set) Token: 0x06000C3B RID: 3131 RVA: 0x00005965 File Offset: 0x00003B65
		[DataMember]
		public int PeopleSurveyId { get; set; }
	}
}
