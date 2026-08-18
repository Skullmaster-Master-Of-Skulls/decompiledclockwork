using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x0200039C RID: 924
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPersonWithExtendedInfoReq : BaseMessageReq
	{
		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x060014C3 RID: 5315 RVA: 0x00009C2D File Offset: 0x00007E2D
		// (set) Token: 0x060014C4 RID: 5316 RVA: 0x00009C35 File Offset: 0x00007E35
		[DataMember]
		public int PersonId { get; set; }
	}
}
