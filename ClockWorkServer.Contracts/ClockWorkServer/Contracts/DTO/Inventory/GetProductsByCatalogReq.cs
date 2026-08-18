using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000580 RID: 1408
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductsByCatalogReq : BaseMessageReq
	{
		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06001D55 RID: 7509 RVA: 0x0000D6BD File Offset: 0x0000B8BD
		// (set) Token: 0x06001D56 RID: 7510 RVA: 0x0000D6C5 File Offset: 0x0000B8C5
		[DataMember]
		public int CatalogId { get; set; }
	}
}
