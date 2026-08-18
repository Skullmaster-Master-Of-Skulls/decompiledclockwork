using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x020001A2 RID: 418
	[DataContract(Namespace = "http://tpro.ca")]
	public class IsConfidentialityAgreementSigningRequiredForTutorResp
	{
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x000045A3 File Offset: 0x000027A3
		// (set) Token: 0x060009AD RID: 2477 RVA: 0x000045AB File Offset: 0x000027AB
		[DataMember]
		public bool IsConfidentialityAgreementSigningRequired { get; set; }
	}
}
