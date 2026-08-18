using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200053D RID: 1341
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveLoansByPersonLoanedToInDateRangeResp
	{
		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06001C08 RID: 7176 RVA: 0x0000CDE8 File Offset: 0x0000AFE8
		// (set) Token: 0x06001C09 RID: 7177 RVA: 0x0000CDF0 File Offset: 0x0000AFF0
		[DataMember]
		public IList<InventoryLoanDTO> Loans { get; set; }
	}
}
