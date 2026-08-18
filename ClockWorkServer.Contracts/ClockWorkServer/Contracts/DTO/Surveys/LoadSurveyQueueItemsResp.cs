using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x02000212 RID: 530
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadSurveyQueueItemsResp
	{
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x0000584D File Offset: 0x00003A4D
		// (set) Token: 0x06000C0E RID: 3086 RVA: 0x00005855 File Offset: 0x00003A55
		[DataMember]
		public IList<SurveyQueueItemDTO> Items { get; set; }
	}
}
