using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200053B RID: 1339
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveLoansByPersonLoanedToResp
	{
		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06001BFE RID: 7166 RVA: 0x0000CDA4 File Offset: 0x0000AFA4
		// (set) Token: 0x06001BFF RID: 7167 RVA: 0x0000CDAC File Offset: 0x0000AFAC
		[DataMember]
		public IList<InventoryLoanDTO> Loans { get; set; }
	}
}
