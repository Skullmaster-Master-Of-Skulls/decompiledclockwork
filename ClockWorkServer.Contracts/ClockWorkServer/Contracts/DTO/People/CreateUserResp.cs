using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000379 RID: 889
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateUserResp
	{
		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x000099B7 File Offset: 0x00007BB7
		// (set) Token: 0x06001459 RID: 5209 RVA: 0x000099BF File Offset: 0x00007BBF
		[DataMember]
		public int PersonId { get; set; }
	}
}
