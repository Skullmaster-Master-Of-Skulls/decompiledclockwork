using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000562 RID: 1378
	[DataContract(Namespace = "http://tpro.ca")]
	public class InventoryLocationDTO
	{
		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06001C75 RID: 7285 RVA: 0x0000D04C File Offset: 0x0000B24C
		// (set) Token: 0x06001C76 RID: 7286 RVA: 0x0000D054 File Offset: 0x0000B254
		[DataMember]
		public int LocationId { get; set; }

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06001C77 RID: 7287 RVA: 0x0000D05D File Offset: 0x0000B25D
		// (set) Token: 0x06001C78 RID: 7288 RVA: 0x0000D065 File Offset: 0x0000B265
		[DataMember]
		public string Campus { get; set; }

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06001C79 RID: 7289 RVA: 0x0000D06E File Offset: 0x0000B26E
		// (set) Token: 0x06001C7A RID: 7290 RVA: 0x0000D076 File Offset: 0x0000B276
		[DataMember]
		public string Building { get; set; }

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x0000D07F File Offset: 0x0000B27F
		// (set) Token: 0x06001C7C RID: 7292 RVA: 0x0000D087 File Offset: 0x0000B287
		[DataMember]
		public string RoomNumber { get; set; }

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06001C7D RID: 7293 RVA: 0x0000D090 File Offset: 0x0000B290
		// (set) Token: 0x06001C7E RID: 7294 RVA: 0x0000D098 File Offset: 0x0000B298
		[DataMember]
		public string Seat { get; set; }

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06001C7F RID: 7295 RVA: 0x0000D0A1 File Offset: 0x0000B2A1
		// (set) Token: 0x06001C80 RID: 7296 RVA: 0x0000D0A9 File Offset: 0x0000B2A9
		[DataMember]
		public string Notes { get; set; }
	}
}
