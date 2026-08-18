using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x0200020B RID: 523
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateNewSurveyResp
	{
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x000057A3 File Offset: 0x000039A3
		// (set) Token: 0x06000BF3 RID: 3059 RVA: 0x000057AB File Offset: 0x000039AB
		[DataMember]
		public int SurveyId { get; set; }
	}
}
