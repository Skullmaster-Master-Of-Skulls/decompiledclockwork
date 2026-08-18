using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x02000217 RID: 535
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateSurveyQueueItemStatusReq : BaseMessageReq
	{
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x000058D5 File Offset: 0x00003AD5
		// (set) Token: 0x06000C23 RID: 3107 RVA: 0x000058DD File Offset: 0x00003ADD
		[DataMember]
		public int PeopleSurveyId { get; set; }

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x000058E6 File Offset: 0x00003AE6
		// (set) Token: 0x06000C25 RID: 3109 RVA: 0x000058EE File Offset: 0x00003AEE
		[DataMember]
		public int? NewPeopleSurveyStatusId { get; set; }
	}
}
