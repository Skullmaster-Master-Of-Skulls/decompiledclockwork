using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ConfidentialityAgreement
{
	// Token: 0x0200083F RID: 2111
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetStudentConfidentialityAgreementTextResp
	{
		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x06002B0A RID: 11018 RVA: 0x000146EC File Offset: 0x000128EC
		// (set) Token: 0x06002B0B RID: 11019 RVA: 0x000146F4 File Offset: 0x000128F4
		[DataMember]
		public string ConfidentialityAgreementText { get; set; }
	}
}
