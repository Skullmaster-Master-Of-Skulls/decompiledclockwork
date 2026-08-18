using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000568 RID: 1384
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetAllLocationsResp
	{
		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06001C8F RID: 7311 RVA: 0x0000D0F6 File Offset: 0x0000B2F6
		// (set) Token: 0x06001C90 RID: 7312 RVA: 0x0000D0FE File Offset: 0x0000B2FE
		[DataMember]
		public IList<InventoryLocationDTO> Locations { get; set; }
	}
}
