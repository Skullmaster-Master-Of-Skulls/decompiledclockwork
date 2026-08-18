using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x020001AD RID: 429
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeActivateTutorReq : BaseMessageReq
	{
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x00004691 File Offset: 0x00002891
		// (set) Token: 0x060009D4 RID: 2516 RVA: 0x00004699 File Offset: 0x00002899
		[DataMember]
		public int TutorPersonId { get; set; }
	}
}
