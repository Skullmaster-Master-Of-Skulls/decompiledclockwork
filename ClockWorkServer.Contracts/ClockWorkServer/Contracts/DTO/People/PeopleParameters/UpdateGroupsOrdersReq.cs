using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003D6 RID: 982
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateGroupsOrdersReq : BaseMessageReq
	{
		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x0000A209 File Offset: 0x00008409
		// (set) Token: 0x060015AC RID: 5548 RVA: 0x0000A211 File Offset: 0x00008411
		[DataMember]
		public IDictionary<int, int> GroupIdsWithOrderNums { get; set; }
	}
}
