using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200055E RID: 1374
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetLoanStatusByIdReq : BaseMessageReq
	{
		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06001C6B RID: 7275 RVA: 0x0000D019 File Offset: 0x0000B219
		// (set) Token: 0x06001C6C RID: 7276 RVA: 0x0000D021 File Offset: 0x0000B221
		[DataMember]
		public int LoanStatusId { get; set; }
	}
}
