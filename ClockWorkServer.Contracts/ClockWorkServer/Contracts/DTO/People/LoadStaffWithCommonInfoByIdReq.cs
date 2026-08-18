using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003B3 RID: 947
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStaffWithCommonInfoByIdReq : BaseMessageReq
	{
		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x00009E2F File Offset: 0x0000802F
		// (set) Token: 0x06001515 RID: 5397 RVA: 0x00009E37 File Offset: 0x00008037
		[DataMember]
		public int PersonId { get; set; }
	}
}
