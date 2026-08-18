using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x020001AC RID: 428
	[DataContract(Namespace = "http://tpro.ca")]
	public class ActivateTutorReq : BaseMessageReq
	{
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x00004680 File Offset: 0x00002880
		// (set) Token: 0x060009D1 RID: 2513 RVA: 0x00004688 File Offset: 0x00002888
		[DataMember]
		public int TutorPersonId { get; set; }
	}
}
