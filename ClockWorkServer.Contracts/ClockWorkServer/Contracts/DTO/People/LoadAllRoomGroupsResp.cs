using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000383 RID: 899
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllRoomGroupsResp
	{
		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x00009A50 File Offset: 0x00007C50
		// (set) Token: 0x06001475 RID: 5237 RVA: 0x00009A58 File Offset: 0x00007C58
		[DataMember]
		public List<GroupDTO> Groups { get; set; }
	}
}
