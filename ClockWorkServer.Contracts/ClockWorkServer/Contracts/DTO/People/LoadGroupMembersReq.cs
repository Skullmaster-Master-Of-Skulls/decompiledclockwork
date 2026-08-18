using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000380 RID: 896
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadGroupMembersReq : BaseMessageReq
	{
		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x00009A2E File Offset: 0x00007C2E
		// (set) Token: 0x0600146E RID: 5230 RVA: 0x00009A36 File Offset: 0x00007C36
		[DataMember]
		public int GroupId { get; set; }
	}
}
