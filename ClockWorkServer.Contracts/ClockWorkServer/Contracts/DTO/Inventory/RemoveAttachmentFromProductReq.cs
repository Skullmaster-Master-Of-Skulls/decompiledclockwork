using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020004F7 RID: 1271
	[DataContract(Namespace = "http://tpro.ca")]
	public class RemoveAttachmentFromProductReq : BaseMessageReq
	{
		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06001B06 RID: 6918 RVA: 0x0000C7A1 File Offset: 0x0000A9A1
		// (set) Token: 0x06001B07 RID: 6919 RVA: 0x0000C7A9 File Offset: 0x0000A9A9
		[DataMember]
		public int AttachmentFileId { get; set; }
	}
}
