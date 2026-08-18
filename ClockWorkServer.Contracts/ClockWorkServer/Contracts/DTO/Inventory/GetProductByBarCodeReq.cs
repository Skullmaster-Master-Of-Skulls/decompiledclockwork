using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200057E RID: 1406
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductByBarCodeReq : BaseMessageReq
	{
		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06001D4D RID: 7501 RVA: 0x0000D68A File Offset: 0x0000B88A
		// (set) Token: 0x06001D4E RID: 7502 RVA: 0x0000D692 File Offset: 0x0000B892
		[DataMember]
		public int WorkingCatalogId { get; set; }

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06001D4F RID: 7503 RVA: 0x0000D69B File Offset: 0x0000B89B
		// (set) Token: 0x06001D50 RID: 7504 RVA: 0x0000D6A3 File Offset: 0x0000B8A3
		[DataMember]
		public string ProductBarCode { get; set; }
	}
}
