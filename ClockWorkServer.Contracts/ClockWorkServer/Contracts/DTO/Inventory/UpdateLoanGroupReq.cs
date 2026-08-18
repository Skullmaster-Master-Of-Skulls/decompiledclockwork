using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000546 RID: 1350
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateLoanGroupReq : BaseMessageReq
	{
		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06001C23 RID: 7203 RVA: 0x0000CE81 File Offset: 0x0000B081
		// (set) Token: 0x06001C24 RID: 7204 RVA: 0x0000CE89 File Offset: 0x0000B089
		[DataMember]
		public InventoryLoanGroupDTO LoanGroup { get; set; }
	}
}
