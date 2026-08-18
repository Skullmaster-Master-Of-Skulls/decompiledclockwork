using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ConfidentialityAgreement
{
	// Token: 0x0200083B RID: 2107
	[DataContract(Namespace = "http://tpro.ca")]
	public class LastStudentConfidentialityAgreementResp
	{
		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06002AFE RID: 11006 RVA: 0x000146A8 File Offset: 0x000128A8
		// (set) Token: 0x06002AFF RID: 11007 RVA: 0x000146B0 File Offset: 0x000128B0
		[DataMember]
		public StudentConfidentialityAgreementDTO ConfidentialityAgreement { get; set; }
	}
}
