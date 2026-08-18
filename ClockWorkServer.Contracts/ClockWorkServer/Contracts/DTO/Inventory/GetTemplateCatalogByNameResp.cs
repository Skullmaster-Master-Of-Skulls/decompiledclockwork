using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000517 RID: 1303
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetTemplateCatalogByNameResp
	{
		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06001B6C RID: 7020 RVA: 0x0000C9F4 File Offset: 0x0000ABF4
		// (set) Token: 0x06001B6D RID: 7021 RVA: 0x0000C9FC File Offset: 0x0000ABFC
		[DataMember]
		public InventoryCatalogDTO Catalog { get; set; }
	}
}
