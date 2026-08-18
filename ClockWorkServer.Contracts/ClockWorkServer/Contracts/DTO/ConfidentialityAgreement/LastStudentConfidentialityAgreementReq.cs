using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ConfidentialityAgreement
{
	// Token: 0x0200083A RID: 2106
	[DataContract(Namespace = "http://tpro.ca")]
	public class LastStudentConfidentialityAgreementReq : ConfidentialityAgreementBaseMessageReq
	{
		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06002AFB RID: 11003 RVA: 0x00014697 File Offset: 0x00012897
		// (set) Token: 0x06002AFC RID: 11004 RVA: 0x0001469F File Offset: 0x0001289F
		[DataMember]
		public int PersonId { get; set; }
	}
}
