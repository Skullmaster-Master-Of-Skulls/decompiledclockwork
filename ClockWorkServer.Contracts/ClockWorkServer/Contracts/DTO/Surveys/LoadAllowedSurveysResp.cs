using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x0200021A RID: 538
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllowedSurveysResp
	{
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x00005908 File Offset: 0x00003B08
		// (set) Token: 0x06000C2C RID: 3116 RVA: 0x00005910 File Offset: 0x00003B10
		[DataMember]
		public IList<SurveyDTO> AllowedSurveys { get; set; }
	}
}
