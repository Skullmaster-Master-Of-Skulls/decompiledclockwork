using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x02000198 RID: 408
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetTutorStatusesReq : BaseMessageReq
	{
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x000044E8 File Offset: 0x000026E8
		// (set) Token: 0x0600098D RID: 2445 RVA: 0x000044F0 File Offset: 0x000026F0
		[DataMember]
		public int[] TutorPersonIds { get; set; }
	}
}
