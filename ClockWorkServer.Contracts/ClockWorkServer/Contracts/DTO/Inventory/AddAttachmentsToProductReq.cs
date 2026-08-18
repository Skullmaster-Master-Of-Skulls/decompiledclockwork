using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020004F5 RID: 1269
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddAttachmentsToProductReq : BaseMessageReq
	{
		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06001B00 RID: 6912 RVA: 0x0000C77F File Offset: 0x0000A97F
		// (set) Token: 0x06001B01 RID: 6913 RVA: 0x0000C787 File Offset: 0x0000A987
		[DataMember]
		public string ProductUniqueId { get; set; }

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x0000C790 File Offset: 0x0000A990
		// (set) Token: 0x06001B03 RID: 6915 RVA: 0x0000C798 File Offset: 0x0000A998
		[DataMember]
		public IList<InventoryAttachedFileDTO> AttachedFiles { get; set; }
	}
}
