using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005A6 RID: 1446
	[DataContract(Namespace = "http://tpro.ca")]
	public class ProductBarcodeAlreadyExistsReq : BaseMessageReq
	{
		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06001DE7 RID: 7655 RVA: 0x0000DA73 File Offset: 0x0000BC73
		// (set) Token: 0x06001DE8 RID: 7656 RVA: 0x0000DA7B File Offset: 0x0000BC7B
		[DataMember]
		public string Barcode { get; set; }

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06001DE9 RID: 7657 RVA: 0x0000DA84 File Offset: 0x0000BC84
		// (set) Token: 0x06001DEA RID: 7658 RVA: 0x0000DA8C File Offset: 0x0000BC8C
		[DataMember]
		public int ProductId { get; set; }
	}
}
