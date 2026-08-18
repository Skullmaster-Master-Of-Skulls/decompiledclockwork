using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200054E RID: 1358
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetReturnedLoanByIdReq : BaseMessageReq
	{
		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06001C33 RID: 7219 RVA: 0x0000CEC5 File Offset: 0x0000B0C5
		// (set) Token: 0x06001C34 RID: 7220 RVA: 0x0000CECD File Offset: 0x0000B0CD
		[DataMember]
		public int LoanId { get; set; }
	}
}
