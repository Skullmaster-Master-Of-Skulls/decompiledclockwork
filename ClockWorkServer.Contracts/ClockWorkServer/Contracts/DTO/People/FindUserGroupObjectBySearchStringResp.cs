using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x0200038D RID: 909
	[DataContract(Namespace = "http://tpro.ca")]
	public class FindUserGroupObjectBySearchStringResp
	{
		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x00009AE9 File Offset: 0x00007CE9
		// (set) Token: 0x06001491 RID: 5265 RVA: 0x00009AF1 File Offset: 0x00007CF1
		[DataMember]
		public List<UserGroupObjectDTO> Matches { get; set; }

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x00009AFA File Offset: 0x00007CFA
		// (set) Token: 0x06001493 RID: 5267 RVA: 0x00009B02 File Offset: 0x00007D02
		[DataMember]
		public int TotalMatchesCount { get; set; }
	}
}
