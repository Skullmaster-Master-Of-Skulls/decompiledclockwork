using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Tutoring;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x020001A7 RID: 423
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetTutorStatusResp
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x0000462B File Offset: 0x0000282B
		// (set) Token: 0x060009C2 RID: 2498 RVA: 0x00004633 File Offset: 0x00002833
		[DataMember]
		public eTutorStatus Status { get; set; }
	}
}
