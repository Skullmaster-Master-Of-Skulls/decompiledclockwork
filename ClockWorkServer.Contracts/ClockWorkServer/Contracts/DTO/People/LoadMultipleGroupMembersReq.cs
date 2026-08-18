using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000381 RID: 897
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMultipleGroupMembersReq : BaseMessageReq
	{
		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x00009A3F File Offset: 0x00007C3F
		// (set) Token: 0x06001471 RID: 5233 RVA: 0x00009A47 File Offset: 0x00007C47
		[DataMember]
		public int[] GroupIds { get; set; }
	}
}
