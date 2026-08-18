using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Email
{
	// Token: 0x02000615 RID: 1557
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAttachmentReq : BaseMessageReq
	{
		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06001FAE RID: 8110 RVA: 0x0000E63D File Offset: 0x0000C83D
		// (set) Token: 0x06001FAF RID: 8111 RVA: 0x0000E645 File Offset: 0x0000C845
		[DataMember]
		public int FileAttachmentId { get; set; }
	}
}
