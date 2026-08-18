using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200055F RID: 1375
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetLoanStatusByIdResp
	{
		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06001C6E RID: 7278 RVA: 0x0000D02A File Offset: 0x0000B22A
		// (set) Token: 0x06001C6F RID: 7279 RVA: 0x0000D032 File Offset: 0x0000B232
		[DataMember]
		public InventoryLoanStatusDTO LoanStatus { get; set; }
	}
}
