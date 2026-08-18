using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200053F RID: 1343
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveLoansByDueDateInLessThanResp
	{
		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06001C0E RID: 7182 RVA: 0x0000CE0A File Offset: 0x0000B00A
		// (set) Token: 0x06001C0F RID: 7183 RVA: 0x0000CE12 File Offset: 0x0000B012
		[DataMember]
		public IList<InventoryLoanDTO> Loans { get; set; }
	}
}
