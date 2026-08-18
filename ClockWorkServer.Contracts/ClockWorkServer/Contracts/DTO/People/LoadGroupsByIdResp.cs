using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000397 RID: 919
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadGroupsByIdResp
	{
		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x00009BD8 File Offset: 0x00007DD8
		// (set) Token: 0x060014B5 RID: 5301 RVA: 0x00009BE0 File Offset: 0x00007DE0
		[DataMember]
		public IList<GroupDTO> Groups { get; set; }
	}
}
