using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x0200037A RID: 890
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateUserReq : BaseMessageReq
	{
		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x000099C8 File Offset: 0x00007BC8
		// (set) Token: 0x0600145C RID: 5212 RVA: 0x000099D0 File Offset: 0x00007BD0
		[DataMember]
		public PersonBaseDTO User { get; set; }

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x000099D9 File Offset: 0x00007BD9
		// (set) Token: 0x0600145E RID: 5214 RVA: 0x000099E1 File Offset: 0x00007BE1
		[DataMember]
		public List<int> GroupIds { get; set; }
	}
}
