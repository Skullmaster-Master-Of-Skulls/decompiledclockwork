using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000509 RID: 1289
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetFullCatalogListResp
	{
		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06001B3C RID: 6972 RVA: 0x0000C8D3 File Offset: 0x0000AAD3
		// (set) Token: 0x06001B3D RID: 6973 RVA: 0x0000C8DB File Offset: 0x0000AADB
		[DataMember]
		public IList<InventoryCatalogDTO> Catalogs { get; set; }
	}
}
