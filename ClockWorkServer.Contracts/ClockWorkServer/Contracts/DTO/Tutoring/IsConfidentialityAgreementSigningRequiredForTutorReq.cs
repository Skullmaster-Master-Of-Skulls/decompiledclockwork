using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x020001A1 RID: 417
	[DataContract(Namespace = "http://tpro.ca")]
	public class IsConfidentialityAgreementSigningRequiredForTutorReq : BaseMessageReq
	{
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x00004592 File Offset: 0x00002792
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x0000459A File Offset: 0x0000279A
		[DataMember]
		public int TutorPersonId { get; set; }
	}
}
