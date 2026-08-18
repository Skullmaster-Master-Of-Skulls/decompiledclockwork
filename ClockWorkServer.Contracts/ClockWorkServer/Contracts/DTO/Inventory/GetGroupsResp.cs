using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200052E RID: 1326
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetGroupsResp
	{
		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06001BB1 RID: 7089 RVA: 0x0000CB7B File Offset: 0x0000AD7B
		// (set) Token: 0x06001BB2 RID: 7090 RVA: 0x0000CB83 File Offset: 0x0000AD83
		[DataMember]
		public IList<InventoryGroupDTO> Groups { get; set; }
	}
}
