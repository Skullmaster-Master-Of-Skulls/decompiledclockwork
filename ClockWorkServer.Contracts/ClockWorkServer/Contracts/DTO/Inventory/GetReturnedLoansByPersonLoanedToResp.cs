using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000555 RID: 1365
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetReturnedLoansByPersonLoanedToResp
	{
		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06001C4E RID: 7246 RVA: 0x0000CF6F File Offset: 0x0000B16F
		// (set) Token: 0x06001C4F RID: 7247 RVA: 0x0000CF77 File Offset: 0x0000B177
		[DataMember]
		public IList<InventoryArchivedLoanDTO> ReturnedLoans { get; set; }
	}
}
