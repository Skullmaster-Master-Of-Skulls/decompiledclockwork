using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000501 RID: 1281
	[DataContract(Namespace = "http://tpro.ca")]
	public class InventoryCatalogDTO
	{
		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06001B1E RID: 6942 RVA: 0x0000C818 File Offset: 0x0000AA18
		// (set) Token: 0x06001B1F RID: 6943 RVA: 0x0000C820 File Offset: 0x0000AA20
		[DataMember]
		public int InventoryCatalogId { get; set; }

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06001B20 RID: 6944 RVA: 0x0000C829 File Offset: 0x0000AA29
		// (set) Token: 0x06001B21 RID: 6945 RVA: 0x0000C831 File Offset: 0x0000AA31
		[DataMember]
		public IList<InventoryCategoryDTO> Categories { get; set; }

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06001B22 RID: 6946 RVA: 0x0000C83A File Offset: 0x0000AA3A
		// (set) Token: 0x06001B23 RID: 6947 RVA: 0x0000C842 File Offset: 0x0000AA42
		[DataMember]
		public string Name { get; set; }

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06001B24 RID: 6948 RVA: 0x0000C84B File Offset: 0x0000AA4B
		// (set) Token: 0x06001B25 RID: 6949 RVA: 0x0000C853 File Offset: 0x0000AA53
		[DataMember]
		public string Description { get; set; }

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06001B26 RID: 6950 RVA: 0x0000C85C File Offset: 0x0000AA5C
		// (set) Token: 0x06001B27 RID: 6951 RVA: 0x0000C864 File Offset: 0x0000AA64
		[DataMember]
		public PersonBaseDTO WhoCreated { get; set; }

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06001B28 RID: 6952 RVA: 0x0000C86D File Offset: 0x0000AA6D
		// (set) Token: 0x06001B29 RID: 6953 RVA: 0x0000C875 File Offset: 0x0000AA75
		[DataMember]
		public DateTime CreationDate { get; set; }
	}
}
