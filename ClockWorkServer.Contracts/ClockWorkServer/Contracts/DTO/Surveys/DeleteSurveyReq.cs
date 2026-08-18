using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x0200020C RID: 524
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteSurveyReq : BaseMessageReq
	{
		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x000057B4 File Offset: 0x000039B4
		// (set) Token: 0x06000BF6 RID: 3062 RVA: 0x000057BC File Offset: 0x000039BC
		[DataMember]
		public int SurveyId { get; set; }
	}
}
