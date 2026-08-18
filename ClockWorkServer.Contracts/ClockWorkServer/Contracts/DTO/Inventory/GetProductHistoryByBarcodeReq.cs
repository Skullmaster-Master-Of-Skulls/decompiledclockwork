using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200059E RID: 1438
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductHistoryByBarcodeReq : BaseMessageReq
	{
		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06001DC3 RID: 7619 RVA: 0x0000D965 File Offset: 0x0000BB65
		// (set) Token: 0x06001DC4 RID: 7620 RVA: 0x0000D96D File Offset: 0x0000BB6D
		[DataMember]
		public string ProductBarcode { get; set; }

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06001DC5 RID: 7621 RVA: 0x0000D976 File Offset: 0x0000BB76
		// (set) Token: 0x06001DC6 RID: 7622 RVA: 0x0000D97E File Offset: 0x0000BB7E
		[DataMember]
		public eInventoryProductSnapshotReason Reason { get; set; }
	}
}
