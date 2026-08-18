using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x02000220 RID: 544
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadSurveyQueueItemResp
	{
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x0000596E File Offset: 0x00003B6E
		// (set) Token: 0x06000C3E RID: 3134 RVA: 0x00005976 File Offset: 0x00003B76
		[DataMember]
		public SurveyQueueItemDTO Item { get; set; }
	}
}
