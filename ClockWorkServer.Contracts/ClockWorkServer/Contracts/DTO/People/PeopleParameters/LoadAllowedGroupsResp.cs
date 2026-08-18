using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003E1 RID: 993
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllowedGroupsResp
	{
		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060015CC RID: 5580 RVA: 0x0000A2C4 File Offset: 0x000084C4
		// (set) Token: 0x060015CD RID: 5581 RVA: 0x0000A2CC File Offset: 0x000084CC
		[DataMember]
		public IList<GroupDTO> Groups { get; set; }
	}
}
