using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200057C RID: 1404
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductBySerialNumberReq : BaseMessageReq
	{
		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06001D45 RID: 7493 RVA: 0x0000D657 File Offset: 0x0000B857
		// (set) Token: 0x06001D46 RID: 7494 RVA: 0x0000D65F File Offset: 0x0000B85F
		[DataMember]
		public int WorkingCatalogId { get; set; }

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06001D47 RID: 7495 RVA: 0x0000D668 File Offset: 0x0000B868
		// (set) Token: 0x06001D48 RID: 7496 RVA: 0x0000D670 File Offset: 0x0000B870
		[DataMember]
		public string ProductSerialNumber { get; set; }
	}
}
