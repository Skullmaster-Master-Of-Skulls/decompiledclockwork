using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000524 RID: 1316
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCategoriesByCatalogResp
	{
		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06001B97 RID: 7063 RVA: 0x0000CAF3 File Offset: 0x0000ACF3
		// (set) Token: 0x06001B98 RID: 7064 RVA: 0x0000CAFB File Offset: 0x0000ACFB
		[DataMember]
		public IList<InventoryCategoryDTO> Categories { get; set; }
	}
}
