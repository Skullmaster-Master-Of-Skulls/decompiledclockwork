using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200055A RID: 1370
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateLoanStatusReq : BaseMessageReq
	{
		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06001C61 RID: 7265 RVA: 0x0000CFE6 File Offset: 0x0000B1E6
		// (set) Token: 0x06001C62 RID: 7266 RVA: 0x0000CFEE File Offset: 0x0000B1EE
		[DataMember]
		public InventoryLoanStatusDTO LoanStatus { get; set; }
	}
}
