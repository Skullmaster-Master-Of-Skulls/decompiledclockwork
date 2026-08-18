using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020004F3 RID: 1267
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddAttachmentToProductReq : BaseMessageReq
	{
		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06001AF8 RID: 6904 RVA: 0x0000C74C File Offset: 0x0000A94C
		// (set) Token: 0x06001AF9 RID: 6905 RVA: 0x0000C754 File Offset: 0x0000A954
		[DataMember]
		public string ProductUniqueId { get; set; }

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06001AFA RID: 6906 RVA: 0x0000C75D File Offset: 0x0000A95D
		// (set) Token: 0x06001AFB RID: 6907 RVA: 0x0000C765 File Offset: 0x0000A965
		[DataMember]
		public InventoryAttachedFileDTO AttachedFile { get; set; }
	}
}
