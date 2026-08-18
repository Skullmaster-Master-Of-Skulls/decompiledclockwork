using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020004F4 RID: 1268
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddAttachmentToProductResp
	{
		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06001AFD RID: 6909 RVA: 0x0000C76E File Offset: 0x0000A96E
		// (set) Token: 0x06001AFE RID: 6910 RVA: 0x0000C776 File Offset: 0x0000A976
		[DataMember]
		public int AttachmentFileId { get; set; }
	}
}
