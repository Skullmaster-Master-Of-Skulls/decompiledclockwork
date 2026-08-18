using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020004F1 RID: 1265
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductAttachmentsReq : BaseMessageReq
	{
		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06001AF2 RID: 6898 RVA: 0x0000C72A File Offset: 0x0000A92A
		// (set) Token: 0x06001AF3 RID: 6899 RVA: 0x0000C732 File Offset: 0x0000A932
		[DataMember]
		public string ProductUniqueId { get; set; }
	}
}
