using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x0200037C RID: 892
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPersonReq : BaseMessageReq
	{
		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x000099FB File Offset: 0x00007BFB
		// (set) Token: 0x06001464 RID: 5220 RVA: 0x00009A03 File Offset: 0x00007C03
		[DataMember]
		public int PersonId { get; set; }
	}
}
