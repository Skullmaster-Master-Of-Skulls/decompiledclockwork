using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ConfidentialityAgreement
{
	// Token: 0x0200083E RID: 2110
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetStudentConfidentialityAgreementTextReq : ConfidentialityAgreementBaseMessageReq
	{
		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x06002B07 RID: 11015 RVA: 0x000146DB File Offset: 0x000128DB
		// (set) Token: 0x06002B08 RID: 11016 RVA: 0x000146E3 File Offset: 0x000128E3
		[DataMember]
		public int PersonId { get; set; }
	}
}
