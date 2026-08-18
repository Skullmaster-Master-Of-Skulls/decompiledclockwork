using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003DE RID: 990
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadGroupByIdResp
	{
		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x0000A291 File Offset: 0x00008491
		// (set) Token: 0x060015C4 RID: 5572 RVA: 0x0000A299 File Offset: 0x00008499
		[DataMember]
		public GroupDTO Group { get; set; }
	}
}
