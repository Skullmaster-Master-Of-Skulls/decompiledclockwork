using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000533 RID: 1331
	[DataContract(Namespace = "http://tpro.ca")]
	public class InventoryLoanStatusDTO
	{
		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x0000CCFA File Offset: 0x0000AEFA
		// (set) Token: 0x06001BE3 RID: 7139 RVA: 0x0000CD02 File Offset: 0x0000AF02
		[DataMember]
		public int LoanStatusId { get; set; }

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x0000CD0B File Offset: 0x0000AF0B
		// (set) Token: 0x06001BE5 RID: 7141 RVA: 0x0000CD13 File Offset: 0x0000AF13
		[DataMember]
		public string Name { get; set; }

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06001BE6 RID: 7142 RVA: 0x0000CD1C File Offset: 0x0000AF1C
		// (set) Token: 0x06001BE7 RID: 7143 RVA: 0x0000CD24 File Offset: 0x0000AF24
		[DataMember]
		public string Description { get; set; }
	}
}
