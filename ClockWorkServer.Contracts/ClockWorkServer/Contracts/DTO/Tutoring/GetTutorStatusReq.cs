using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x020001A6 RID: 422
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetTutorStatusReq : BaseMessageReq
	{
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x0000461A File Offset: 0x0000281A
		// (set) Token: 0x060009BF RID: 2495 RVA: 0x00004622 File Offset: 0x00002822
		[DataMember]
		public int TutorPersonId { get; set; }
	}
}
