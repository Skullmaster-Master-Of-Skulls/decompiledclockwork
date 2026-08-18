using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003D8 RID: 984
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadGroupMemberCountReq : BaseMessageReq
	{
		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x060015AF RID: 5551 RVA: 0x0000A21A File Offset: 0x0000841A
		// (set) Token: 0x060015B0 RID: 5552 RVA: 0x0000A222 File Offset: 0x00008422
		[DataMember]
		public int GroupId { get; set; }
	}
}
