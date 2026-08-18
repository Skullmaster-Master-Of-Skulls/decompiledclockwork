using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x020001A5 RID: 421
	[DataContract(Namespace = "http://tpro.ca")]
	public class RegisterTutorByExistingPersonIdReq : BaseMessageReq
	{
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x00004609 File Offset: 0x00002809
		// (set) Token: 0x060009BC RID: 2492 RVA: 0x00004611 File Offset: 0x00002811
		[DataMember]
		public int TutorPersonId { get; set; }
	}
}
