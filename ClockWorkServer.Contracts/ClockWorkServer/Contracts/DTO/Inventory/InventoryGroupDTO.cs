using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000575 RID: 1397
	[DataContract(Namespace = "http://tpro.ca")]
	public class InventoryGroupDTO
	{
		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x0000D349 File Offset: 0x0000B549
		// (set) Token: 0x06001CE3 RID: 7395 RVA: 0x0000D351 File Offset: 0x0000B551
		[DataMember]
		public int ProductGroupId { get; set; }

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x0000D35A File Offset: 0x0000B55A
		// (set) Token: 0x06001CE5 RID: 7397 RVA: 0x0000D362 File Offset: 0x0000B562
		[DataMember]
		public string Name { get; set; }

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06001CE6 RID: 7398 RVA: 0x0000D36B File Offset: 0x0000B56B
		// (set) Token: 0x06001CE7 RID: 7399 RVA: 0x0000D373 File Offset: 0x0000B573
		[DataMember]
		public string Notes { get; set; }
	}
}
