using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200054D RID: 1357
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetReturnedLoansResp
	{
		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06001C30 RID: 7216 RVA: 0x0000CEB4 File Offset: 0x0000B0B4
		// (set) Token: 0x06001C31 RID: 7217 RVA: 0x0000CEBC File Offset: 0x0000B0BC
		[DataMember]
		public IList<InventoryArchivedLoanDTO> ReturnerdLoans { get; set; }
	}
}
