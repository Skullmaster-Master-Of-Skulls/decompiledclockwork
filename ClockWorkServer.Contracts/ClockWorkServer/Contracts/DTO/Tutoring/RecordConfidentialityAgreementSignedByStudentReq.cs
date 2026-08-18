using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x02000192 RID: 402
	[DataContract(Namespace = "http://tpro.ca")]
	public class RecordConfidentialityAgreementSignedByStudentReq : BaseReportMessageReq
	{
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x00004372 File Offset: 0x00002572
		// (set) Token: 0x06000972 RID: 2418 RVA: 0x0000437A File Offset: 0x0000257A
		[DataMember]
		public int StudentPersonId { get; set; }
	}
}
