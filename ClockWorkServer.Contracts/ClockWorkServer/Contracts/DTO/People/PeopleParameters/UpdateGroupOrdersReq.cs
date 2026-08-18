using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003CE RID: 974
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateGroupOrdersReq : BaseMessageReq
	{
		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001595 RID: 5525 RVA: 0x0000A192 File Offset: 0x00008392
		// (set) Token: 0x06001596 RID: 5526 RVA: 0x0000A19A File Offset: 0x0000839A
		[DataMember]
		public IDictionary<int, int> GroupIdsWithNewOrderNum { get; set; }
	}
}
