using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000592 RID: 1426
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteProductByBarCodeReq : BaseMessageReq
	{
		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06001D97 RID: 7575 RVA: 0x0000D855 File Offset: 0x0000BA55
		// (set) Token: 0x06001D98 RID: 7576 RVA: 0x0000D85D File Offset: 0x0000BA5D
		[DataMember]
		public string BarCode { get; set; }
	}
}
