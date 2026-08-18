using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003DB RID: 987
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateGroupByTitleReq : BaseMessageReq
	{
		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x060015B8 RID: 5560 RVA: 0x0000A24D File Offset: 0x0000844D
		// (set) Token: 0x060015B9 RID: 5561 RVA: 0x0000A255 File Offset: 0x00008455
		[DataMember]
		public string GroupTitle { get; set; }
	}
}
