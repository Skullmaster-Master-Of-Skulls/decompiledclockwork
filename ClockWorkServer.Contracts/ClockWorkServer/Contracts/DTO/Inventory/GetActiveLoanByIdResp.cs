using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000537 RID: 1335
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveLoanByIdResp
	{
		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x0000CD4F File Offset: 0x0000AF4F
		// (set) Token: 0x06001BF1 RID: 7153 RVA: 0x0000CD57 File Offset: 0x0000AF57
		[DataMember]
		public InventoryLoanDTO Loan { get; set; }
	}
}
