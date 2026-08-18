using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x02000210 RID: 528
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLookupSurveyStatusesResp
	{
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x000057E7 File Offset: 0x000039E7
		// (set) Token: 0x06000C00 RID: 3072 RVA: 0x000057EF File Offset: 0x000039EF
		[DataMember]
		public IList<SurveyStatusDTO> Statuses { get; set; }
	}
}
