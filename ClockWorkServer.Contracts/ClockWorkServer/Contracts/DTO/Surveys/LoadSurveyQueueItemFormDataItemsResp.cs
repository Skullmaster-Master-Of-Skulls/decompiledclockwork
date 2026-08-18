using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x0200021E RID: 542
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadSurveyQueueItemFormDataItemsResp
	{
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0000594C File Offset: 0x00003B4C
		// (set) Token: 0x06000C38 RID: 3128 RVA: 0x00005954 File Offset: 0x00003B54
		[DataMember]
		public IList<DynamicDataDTO> DataItems { get; set; }
	}
}
