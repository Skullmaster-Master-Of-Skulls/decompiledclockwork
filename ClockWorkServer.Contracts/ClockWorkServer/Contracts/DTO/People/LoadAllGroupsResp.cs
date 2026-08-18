using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000399 RID: 921
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllGroupsResp
	{
		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x00009BE9 File Offset: 0x00007DE9
		// (set) Token: 0x060014B9 RID: 5305 RVA: 0x00009BF1 File Offset: 0x00007DF1
		[DataMember]
		public IList<GroupDTO> Groups { get; set; }
	}
}
