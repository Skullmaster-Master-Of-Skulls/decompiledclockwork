using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x0200020A RID: 522
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateNewSurveyReq : BaseMessageReq
	{
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x00005792 File Offset: 0x00003992
		// (set) Token: 0x06000BF0 RID: 3056 RVA: 0x0000579A File Offset: 0x0000399A
		[DataMember]
		public SurveyDTO Survey { get; set; }
	}
}
