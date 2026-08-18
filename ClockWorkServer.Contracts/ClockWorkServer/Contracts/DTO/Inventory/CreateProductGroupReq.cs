using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000525 RID: 1317
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateProductGroupReq : BaseMessageReq
	{
		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06001B9A RID: 7066 RVA: 0x0000CB04 File Offset: 0x0000AD04
		// (set) Token: 0x06001B9B RID: 7067 RVA: 0x0000CB0C File Offset: 0x0000AD0C
		[DataMember]
		public InventoryGroupDTO Group { get; set; }
	}
}
