using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200056A RID: 1386
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetLocationsResp
	{
		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06001C95 RID: 7317 RVA: 0x0000D118 File Offset: 0x0000B318
		// (set) Token: 0x06001C96 RID: 7318 RVA: 0x0000D120 File Offset: 0x0000B320
		[DataMember]
		public IList<InventoryLocationDTO> Locations { get; set; }
	}
}
