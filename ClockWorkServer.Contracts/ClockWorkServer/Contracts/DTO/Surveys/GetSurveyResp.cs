using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x02000207 RID: 519
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetSurveyResp
	{
		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0000575F File Offset: 0x0000395F
		// (set) Token: 0x06000BE7 RID: 3047 RVA: 0x00005767 File Offset: 0x00003967
		[DataMember]
		public SurveyDTO Survey { get; set; }
	}
}
