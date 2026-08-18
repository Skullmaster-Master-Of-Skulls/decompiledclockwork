using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000539 RID: 1337
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveLoanByProductResp
	{
		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x0000CD82 File Offset: 0x0000AF82
		// (set) Token: 0x06001BF9 RID: 7161 RVA: 0x0000CD8A File Offset: 0x0000AF8A
		[DataMember]
		public InventoryLoanDTO Loan { get; set; }
	}
}
