using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x020001AA RID: 426
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllTutorsReq : BaseMessageReq
	{
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x0000465E File Offset: 0x0000285E
		// (set) Token: 0x060009CB RID: 2507 RVA: 0x00004666 File Offset: 0x00002866
		[DataMember]
		public int TutorPersonId { get; set; }
	}
}
