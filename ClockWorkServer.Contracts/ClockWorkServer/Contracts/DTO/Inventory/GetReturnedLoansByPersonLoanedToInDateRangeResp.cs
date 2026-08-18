using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000557 RID: 1367
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetReturnedLoansByPersonLoanedToInDateRangeResp
	{
		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06001C58 RID: 7256 RVA: 0x0000CFB3 File Offset: 0x0000B1B3
		// (set) Token: 0x06001C59 RID: 7257 RVA: 0x0000CFBB File Offset: 0x0000B1BB
		[DataMember]
		public IList<InventoryArchivedLoanDTO> ReturnedLoans { get; set; }
	}
}
