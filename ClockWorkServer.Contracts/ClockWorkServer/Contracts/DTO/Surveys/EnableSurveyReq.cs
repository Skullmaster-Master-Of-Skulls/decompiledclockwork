using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x0200020E RID: 526
	[DataContract(Namespace = "http://tpro.ca")]
	public class EnableSurveyReq : BaseMessageReq
	{
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x000057D6 File Offset: 0x000039D6
		// (set) Token: 0x06000BFC RID: 3068 RVA: 0x000057DE File Offset: 0x000039DE
		[DataMember]
		public int SurveyId { get; set; }
	}
}
