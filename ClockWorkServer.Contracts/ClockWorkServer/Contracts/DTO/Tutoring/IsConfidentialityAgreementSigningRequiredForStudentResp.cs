using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x02000191 RID: 401
	[DataContract(Namespace = "http://tpro.ca")]
	public class IsConfidentialityAgreementSigningRequiredForStudentResp
	{
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x00004361 File Offset: 0x00002561
		// (set) Token: 0x0600096F RID: 2415 RVA: 0x00004369 File Offset: 0x00002569
		[DataMember]
		public bool IsConfidentialityRequired { get; set; }
	}
}
