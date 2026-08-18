using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x0200037D RID: 893
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadGroupsResp
	{
		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x00009A0C File Offset: 0x00007C0C
		// (set) Token: 0x06001467 RID: 5223 RVA: 0x00009A14 File Offset: 0x00007C14
		[DataMember]
		public List<GroupDTO> Groups { get; set; }
	}
}
