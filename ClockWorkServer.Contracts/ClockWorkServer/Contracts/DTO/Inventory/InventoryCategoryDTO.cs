using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200051A RID: 1306
	[DataContract(Namespace = "http://tpro.ca")]
	public class InventoryCategoryDTO
	{
		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06001B73 RID: 7027 RVA: 0x0000CA16 File Offset: 0x0000AC16
		// (set) Token: 0x06001B74 RID: 7028 RVA: 0x0000CA1E File Offset: 0x0000AC1E
		[DataMember]
		public string CategoryName { get; set; }

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06001B75 RID: 7029 RVA: 0x0000CA27 File Offset: 0x0000AC27
		// (set) Token: 0x06001B76 RID: 7030 RVA: 0x0000CA2F File Offset: 0x0000AC2F
		[DataMember]
		public int DynamicFormId { get; set; }

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06001B77 RID: 7031 RVA: 0x0000CA38 File Offset: 0x0000AC38
		// (set) Token: 0x06001B78 RID: 7032 RVA: 0x0000CA40 File Offset: 0x0000AC40
		[DataMember]
		public int CatalogId { get; set; }
	}
}
