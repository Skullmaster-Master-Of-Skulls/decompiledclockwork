using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x0200020D RID: 525
	[DataContract(Namespace = "http://tpro.ca")]
	public class DisableSurveyReq : BaseMessageReq
	{
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x000057C5 File Offset: 0x000039C5
		// (set) Token: 0x06000BF9 RID: 3065 RVA: 0x000057CD File Offset: 0x000039CD
		[DataMember]
		public int SurveyId { get; set; }
	}
}
