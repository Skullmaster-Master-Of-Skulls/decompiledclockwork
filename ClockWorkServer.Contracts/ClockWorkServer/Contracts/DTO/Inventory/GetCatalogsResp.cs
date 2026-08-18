using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000507 RID: 1287
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCatalogsResp
	{
		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06001B38 RID: 6968 RVA: 0x0000C8C2 File Offset: 0x0000AAC2
		// (set) Token: 0x06001B39 RID: 6969 RVA: 0x0000C8CA File Offset: 0x0000AACA
		[DataMember]
		public IList<InventoryCatalogDTO> Catalogs { get; set; }
	}
}
