using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003C5 RID: 965
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllGroupsAndContainersResp
	{
		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x0600157C RID: 5500 RVA: 0x0000A10A File Offset: 0x0000830A
		// (set) Token: 0x0600157D RID: 5501 RVA: 0x0000A112 File Offset: 0x00008312
		[DataMember]
		public IList<GroupDTO> Groups { get; set; }

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x0000A11B File Offset: 0x0000831B
		// (set) Token: 0x0600157F RID: 5503 RVA: 0x0000A123 File Offset: 0x00008323
		[DataMember]
		public IList<GroupContainerDTO> GroupContainers { get; set; }
	}
}
