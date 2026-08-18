using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000558 RID: 1368
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetLoansByLoanGroupIdReq : BaseMessageReq
	{
		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06001C5B RID: 7259 RVA: 0x0000CFC4 File Offset: 0x0000B1C4
		// (set) Token: 0x06001C5C RID: 7260 RVA: 0x0000CFCC File Offset: 0x0000B1CC
		[DataMember]
		public int LoanGroupId { get; set; }
	}
}
