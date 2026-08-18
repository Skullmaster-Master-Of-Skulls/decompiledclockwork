using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000535 RID: 1333
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveLoansResp
	{
		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06001BEA RID: 7146 RVA: 0x0000CD2D File Offset: 0x0000AF2D
		// (set) Token: 0x06001BEB RID: 7147 RVA: 0x0000CD35 File Offset: 0x0000AF35
		[DataMember]
		public IList<InventoryLoanDTO> ActiveLoans { get; set; }
	}
}
