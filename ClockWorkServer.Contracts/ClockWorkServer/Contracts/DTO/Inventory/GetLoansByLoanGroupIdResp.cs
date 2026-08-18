using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000559 RID: 1369
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetLoansByLoanGroupIdResp
	{
		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06001C5E RID: 7262 RVA: 0x0000CFD5 File Offset: 0x0000B1D5
		// (set) Token: 0x06001C5F RID: 7263 RVA: 0x0000CFDD File Offset: 0x0000B1DD
		[DataMember]
		public IList<InventoryLoanDTO> Loans { get; set; }
	}
}
