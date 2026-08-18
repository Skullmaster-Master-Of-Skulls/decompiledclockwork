using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x02000218 RID: 536
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateSurveyQueueItemStatusResp
	{
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x000058F7 File Offset: 0x00003AF7
		// (set) Token: 0x06000C28 RID: 3112 RVA: 0x000058FF File Offset: 0x00003AFF
		[DataMember]
		public SurveyQueueItemDTO RefreshedItem { get; set; }
	}
}
