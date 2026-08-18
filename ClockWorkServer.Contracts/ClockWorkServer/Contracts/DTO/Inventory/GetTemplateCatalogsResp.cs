using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000519 RID: 1305
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetTemplateCatalogsResp
	{
		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x0000CA05 File Offset: 0x0000AC05
		// (set) Token: 0x06001B71 RID: 7025 RVA: 0x0000CA0D File Offset: 0x0000AC0D
		[DataMember]
		public IList<InventoryCatalogDTO> TemplateCatalogs { get; set; }
	}
}
