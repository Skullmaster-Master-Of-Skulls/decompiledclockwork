using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000541 RID: 1345
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetOverDueDateActiveLoansResp
	{
		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06001C12 RID: 7186 RVA: 0x0000CE1B File Offset: 0x0000B01B
		// (set) Token: 0x06001C13 RID: 7187 RVA: 0x0000CE23 File Offset: 0x0000B023
		[DataMember]
		public IList<InventoryLoanDTO> Loans { get; set; }
	}
}
