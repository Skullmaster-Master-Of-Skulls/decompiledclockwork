using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x020001A0 RID: 416
	[DataContract(Namespace = "http://tpro.ca")]
	public class RecordConfidentialityAgreementSignedByTutorReq : BaseMessageReq
	{
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00004581 File Offset: 0x00002781
		// (set) Token: 0x060009A7 RID: 2471 RVA: 0x00004589 File Offset: 0x00002789
		[DataMember]
		public int TutorPersonId { get; set; }
	}
}
