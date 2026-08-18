using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000548 RID: 1352
	[DataContract(Namespace = "http://tpro.ca")]
	public class ReturnLoanReq : BaseMessageReq
	{
		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06001C27 RID: 7207 RVA: 0x0000CE92 File Offset: 0x0000B092
		// (set) Token: 0x06001C28 RID: 7208 RVA: 0x0000CE9A File Offset: 0x0000B09A
		[DataMember]
		public InventoryReturnedLoanDTO ReturnedLoan { get; set; }
	}
}
