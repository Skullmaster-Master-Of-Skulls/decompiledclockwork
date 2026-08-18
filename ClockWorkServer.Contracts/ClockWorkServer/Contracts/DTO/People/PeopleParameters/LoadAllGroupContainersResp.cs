using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003E3 RID: 995
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllGroupContainersResp
	{
		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x0000A2D5 File Offset: 0x000084D5
		// (set) Token: 0x060015D1 RID: 5585 RVA: 0x0000A2DD File Offset: 0x000084DD
		[DataMember]
		public IList<GroupContainerDTO> GroupContainers { get; set; }
	}
}
