using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020004FB RID: 1275
	[DataContract(Namespace = "http://tpro.ca")]
	public class RemoveAllAttachmentsFromProductReq : BaseMessageReq
	{
		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x0000C7C3 File Offset: 0x0000A9C3
		// (set) Token: 0x06001B0F RID: 6927 RVA: 0x0000C7CB File Offset: 0x0000A9CB
		[DataMember]
		public string ProductUniqueId { get; set; }
	}
}
