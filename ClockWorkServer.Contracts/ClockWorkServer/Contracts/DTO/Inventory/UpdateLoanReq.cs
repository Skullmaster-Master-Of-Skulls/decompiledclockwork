using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000544 RID: 1348
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateLoanReq : BaseMessageReq
	{
		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06001C1D RID: 7197 RVA: 0x0000CE5F File Offset: 0x0000B05F
		// (set) Token: 0x06001C1E RID: 7198 RVA: 0x0000CE67 File Offset: 0x0000B067
		[DataMember]
		public InventoryLoanDTO Loan { get; set; }
	}
}
