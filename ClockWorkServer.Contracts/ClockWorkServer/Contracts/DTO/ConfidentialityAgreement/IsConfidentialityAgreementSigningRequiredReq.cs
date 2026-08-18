using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ConfidentialityAgreement
{
	// Token: 0x0200083C RID: 2108
	[DataContract(Namespace = "http://tpro.ca")]
	public class IsConfidentialityAgreementSigningRequiredReq : ConfidentialityAgreementBaseMessageReq
	{
		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x06002B01 RID: 11009 RVA: 0x000146B9 File Offset: 0x000128B9
		// (set) Token: 0x06002B02 RID: 11010 RVA: 0x000146C1 File Offset: 0x000128C1
		[DataMember]
		public int PersonId { get; set; }
	}
}
