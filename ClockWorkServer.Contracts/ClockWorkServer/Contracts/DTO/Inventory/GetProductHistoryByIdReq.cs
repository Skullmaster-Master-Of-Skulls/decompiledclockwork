using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200059C RID: 1436
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductHistoryByIdReq : BaseMessageReq
	{
		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06001DBB RID: 7611 RVA: 0x0000D932 File Offset: 0x0000BB32
		// (set) Token: 0x06001DBC RID: 7612 RVA: 0x0000D93A File Offset: 0x0000BB3A
		[DataMember]
		public int ProductId { get; set; }

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06001DBD RID: 7613 RVA: 0x0000D943 File Offset: 0x0000BB43
		// (set) Token: 0x06001DBE RID: 7614 RVA: 0x0000D94B File Offset: 0x0000BB4B
		[DataMember]
		public eInventoryProductSnapshotReason Reason { get; set; }
	}
}
