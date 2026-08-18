using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003E5 RID: 997
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllGroupForEditsResp
	{
		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x0000A2E6 File Offset: 0x000084E6
		// (set) Token: 0x060015D5 RID: 5589 RVA: 0x0000A2EE File Offset: 0x000084EE
		[DataMember]
		public IList<GroupForEditDTO> AllGroupForEdits { get; set; }
	}
}
