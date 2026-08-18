using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000522 RID: 1314
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCategoryByNameResp
	{
		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06001B91 RID: 7057 RVA: 0x0000CAD1 File Offset: 0x0000ACD1
		// (set) Token: 0x06001B92 RID: 7058 RVA: 0x0000CAD9 File Offset: 0x0000ACD9
		[DataMember]
		public InventoryCategoryDTO Category { get; set; }
	}
}
