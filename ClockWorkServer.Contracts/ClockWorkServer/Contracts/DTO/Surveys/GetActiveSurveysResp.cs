using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x02000205 RID: 517
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveSurveysResp
	{
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0000574E File Offset: 0x0000394E
		// (set) Token: 0x06000BE3 RID: 3043 RVA: 0x00005756 File Offset: 0x00003956
		[DataMember]
		public List<SurveyDTO> Surveys { get; set; }
	}
}
