using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000394 RID: 916
	[DataContract(Namespace = "http://tpro.ca")]
	public class UnDeleteUserReq : BaseMessageReq
	{
		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x00009BA5 File Offset: 0x00007DA5
		// (set) Token: 0x060014AC RID: 5292 RVA: 0x00009BAD File Offset: 0x00007DAD
		[DataMember]
		public int PersonId { get; set; }
	}
}
