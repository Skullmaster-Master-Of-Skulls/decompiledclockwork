using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200051B RID: 1307
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateCategoryReq : BaseMessageReq
	{
		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06001B7A RID: 7034 RVA: 0x0000CA49 File Offset: 0x0000AC49
		// (set) Token: 0x06001B7B RID: 7035 RVA: 0x0000CA51 File Offset: 0x0000AC51
		[DataMember]
		public InventoryCategoryDTO Category { get; set; }
	}
}
