using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200058B RID: 1419
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateProductResp
	{
		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06001D7E RID: 7550 RVA: 0x0000D7BC File Offset: 0x0000B9BC
		// (set) Token: 0x06001D7F RID: 7551 RVA: 0x0000D7C4 File Offset: 0x0000B9C4
		[DataMember]
		public string BarCode { get; set; }
	}
}
