using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200053A RID: 1338
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveLoansByPersonLoanedToReq : BaseMessageReq
	{
		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x0000CD93 File Offset: 0x0000AF93
		// (set) Token: 0x06001BFC RID: 7164 RVA: 0x0000CD9B File Offset: 0x0000AF9B
		[DataMember]
		public int PersonLoanToId { get; set; }
	}
}
