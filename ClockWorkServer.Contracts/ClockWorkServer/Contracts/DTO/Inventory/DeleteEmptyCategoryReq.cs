using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200051F RID: 1311
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteEmptyCategoryReq : BaseMessageReq
	{
		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06001B86 RID: 7046 RVA: 0x0000CA8D File Offset: 0x0000AC8D
		// (set) Token: 0x06001B87 RID: 7047 RVA: 0x0000CA95 File Offset: 0x0000AC95
		[DataMember]
		public int CatalogId { get; set; }

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06001B88 RID: 7048 RVA: 0x0000CA9E File Offset: 0x0000AC9E
		// (set) Token: 0x06001B89 RID: 7049 RVA: 0x0000CAA6 File Offset: 0x0000ACA6
		[DataMember]
		public string CategoryName { get; set; }
	}
}
