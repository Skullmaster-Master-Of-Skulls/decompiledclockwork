using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000392 RID: 914
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteGroupReq : BaseMessageReq
	{
		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x00009B72 File Offset: 0x00007D72
		// (set) Token: 0x060014A4 RID: 5284 RVA: 0x00009B7A File Offset: 0x00007D7A
		[DataMember]
		public int GroupId { get; set; }
	}
}
