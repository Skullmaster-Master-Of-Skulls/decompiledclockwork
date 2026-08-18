using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ConfidentialityAgreement
{
	// Token: 0x02000838 RID: 2104
	[DataContract(Namespace = "http://tpro.ca")]
	public class SignedConfidentialityAgreementReq : ConfidentialityAgreementBaseMessageReq
	{
		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x06002AF7 RID: 10999 RVA: 0x0001467D File Offset: 0x0001287D
		// (set) Token: 0x06002AF8 RID: 11000 RVA: 0x00014685 File Offset: 0x00012885
		[DataMember]
		public int PersonId { get; set; }
	}
}
