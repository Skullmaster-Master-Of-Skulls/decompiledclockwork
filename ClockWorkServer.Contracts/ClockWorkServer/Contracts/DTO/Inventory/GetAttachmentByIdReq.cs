using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020004EF RID: 1263
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetAttachmentByIdReq : BaseMessageReq
	{
		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06001AEC RID: 6892 RVA: 0x0000C708 File Offset: 0x0000A908
		// (set) Token: 0x06001AED RID: 6893 RVA: 0x0000C710 File Offset: 0x0000A910
		[DataMember]
		public int AttachmentId { get; set; }
	}
}
