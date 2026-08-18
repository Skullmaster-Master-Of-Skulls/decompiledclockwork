using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200050E RID: 1294
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteEmptyCatalogReq : BaseMessageReq
	{
		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06001B49 RID: 6985 RVA: 0x0000C917 File Offset: 0x0000AB17
		// (set) Token: 0x06001B4A RID: 6986 RVA: 0x0000C91F File Offset: 0x0000AB1F
		[DataMember]
		public int CatalogId { get; set; }
	}
}
