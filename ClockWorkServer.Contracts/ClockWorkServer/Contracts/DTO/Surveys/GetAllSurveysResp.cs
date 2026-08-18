using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x02000203 RID: 515
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetAllSurveysResp
	{
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x0000573D File Offset: 0x0000393D
		// (set) Token: 0x06000BDF RID: 3039 RVA: 0x00005745 File Offset: 0x00003945
		[DataMember]
		public List<SurveyDTO> Surveys { get; set; }
	}
}
