using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x0200021D RID: 541
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadSurveyQueueItemFormDataItemsReq : BaseMessageReq
	{
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x0000593B File Offset: 0x00003B3B
		// (set) Token: 0x06000C35 RID: 3125 RVA: 0x00005943 File Offset: 0x00003B43
		[DataMember]
		public int PeopleSurveyId { get; set; }
	}
}
