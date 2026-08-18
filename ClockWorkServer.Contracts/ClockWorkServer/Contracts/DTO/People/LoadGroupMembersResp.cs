using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x0200037F RID: 895
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadGroupMembersResp
	{
		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x00009A1D File Offset: 0x00007C1D
		// (set) Token: 0x0600146B RID: 5227 RVA: 0x00009A25 File Offset: 0x00007C25
		[DataMember]
		public List<PersonBaseDTO> GroupMembers { get; set; }
	}
}
