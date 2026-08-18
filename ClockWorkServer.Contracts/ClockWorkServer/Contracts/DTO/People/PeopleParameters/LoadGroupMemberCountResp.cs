using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003D9 RID: 985
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadGroupMemberCountResp
	{
		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x0000A22B File Offset: 0x0000842B
		// (set) Token: 0x060015B3 RID: 5555 RVA: 0x0000A233 File Offset: 0x00008433
		[DataMember]
		public int GroupMemberCount { get; set; }
	}
}
