using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003E6 RID: 998
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadUsersByGroupTitleResp
	{
		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x0000A2F7 File Offset: 0x000084F7
		// (set) Token: 0x060015D8 RID: 5592 RVA: 0x0000A2FF File Offset: 0x000084FF
		[DataMember]
		public IList<PersonBaseDTO> Users { get; set; }
	}
}
