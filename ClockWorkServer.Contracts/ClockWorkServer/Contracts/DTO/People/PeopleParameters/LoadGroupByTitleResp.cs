using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003DC RID: 988
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadGroupByTitleResp
	{
		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x060015BB RID: 5563 RVA: 0x0000A25E File Offset: 0x0000845E
		// (set) Token: 0x060015BC RID: 5564 RVA: 0x0000A266 File Offset: 0x00008466
		[DataMember]
		public GroupDTO Group { get; set; }
	}
}
