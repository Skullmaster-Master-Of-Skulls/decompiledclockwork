using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003C6 RID: 966
	[DataContract(Namespace = "http://tpro.ca")]
	public class AdminCreateGroupReq : BaseMessageReq
	{
		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001581 RID: 5505 RVA: 0x0000A12C File Offset: 0x0000832C
		// (set) Token: 0x06001582 RID: 5506 RVA: 0x0000A134 File Offset: 0x00008334
		[DataMember]
		public GroupDTO Group { get; set; }
	}
}
