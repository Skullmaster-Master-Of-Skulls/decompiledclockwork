using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x02000190 RID: 400
	[DataContract(Namespace = "http://tpro.ca")]
	public class IsConfidentialityAgreementSigningRequiredForStudentReq : BaseReportMessageReq
	{
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00004350 File Offset: 0x00002550
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x00004358 File Offset: 0x00002558
		[DataMember]
		public int StudentPersonId { get; set; }
	}
}
