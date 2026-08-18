using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x02000208 RID: 520
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetSurveyReq : BaseMessageReq
	{
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x00005770 File Offset: 0x00003970
		// (set) Token: 0x06000BEA RID: 3050 RVA: 0x00005778 File Offset: 0x00003978
		[DataMember]
		public int SurveyId { get; set; }
	}
}
