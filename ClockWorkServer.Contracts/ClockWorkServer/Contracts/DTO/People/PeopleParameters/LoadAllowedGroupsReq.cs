using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003E0 RID: 992
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllowedGroupsReq : BaseMessageReq
	{
		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060015C9 RID: 5577 RVA: 0x0000A2B3 File Offset: 0x000084B3
		// (set) Token: 0x060015CA RID: 5578 RVA: 0x0000A2BB File Offset: 0x000084BB
		[DataMember]
		public bool OnlyReturnVisibleInCalendarGroups { get; set; }
	}
}
