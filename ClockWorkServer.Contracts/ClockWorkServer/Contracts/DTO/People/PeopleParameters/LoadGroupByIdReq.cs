using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003DF RID: 991
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadGroupByIdReq : BaseMessageReq
	{
		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060015C6 RID: 5574 RVA: 0x0000A2A2 File Offset: 0x000084A2
		// (set) Token: 0x060015C7 RID: 5575 RVA: 0x0000A2AA File Offset: 0x000084AA
		[DataMember]
		public int GroupId { get; set; }
	}
}
