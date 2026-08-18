using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003DA RID: 986
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateGroupByTitleResp
	{
		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x0000A23C File Offset: 0x0000843C
		// (set) Token: 0x060015B6 RID: 5558 RVA: 0x0000A244 File Offset: 0x00008444
		[DataMember]
		public int GroupId { get; set; }
	}
}
