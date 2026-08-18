using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200055C RID: 1372
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateLoanStatusReq : BaseMessageReq
	{
		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06001C67 RID: 7271 RVA: 0x0000D008 File Offset: 0x0000B208
		// (set) Token: 0x06001C68 RID: 7272 RVA: 0x0000D010 File Offset: 0x0000B210
		[DataMember]
		public InventoryLoanStatusDTO LoanStatus { get; set; }
	}
}
