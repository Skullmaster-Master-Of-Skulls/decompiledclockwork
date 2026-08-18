using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000553 RID: 1363
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetReturnedLoansByProductInDateRangeResp
	{
		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06001C48 RID: 7240 RVA: 0x0000CF4D File Offset: 0x0000B14D
		// (set) Token: 0x06001C49 RID: 7241 RVA: 0x0000CF55 File Offset: 0x0000B155
		[DataMember]
		public IList<InventoryArchivedLoanDTO> ReturnedLoans { get; set; }
	}
}
