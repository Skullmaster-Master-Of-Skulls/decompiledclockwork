using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020004F2 RID: 1266
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductAttachmentsResp
	{
		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x0000C73B File Offset: 0x0000A93B
		// (set) Token: 0x06001AF6 RID: 6902 RVA: 0x0000C743 File Offset: 0x0000A943
		[DataMember]
		public IList<InventoryAttachedFileInfoDTO> AttachmentFiles { get; set; }
	}
}
