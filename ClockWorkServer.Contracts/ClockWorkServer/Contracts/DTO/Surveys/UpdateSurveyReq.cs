using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x02000209 RID: 521
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateSurveyReq : BaseMessageReq
	{
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x00005781 File Offset: 0x00003981
		// (set) Token: 0x06000BED RID: 3053 RVA: 0x00005789 File Offset: 0x00003989
		[DataMember]
		public SurveyDTO Survey { get; set; }
	}
}
