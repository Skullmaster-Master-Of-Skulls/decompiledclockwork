using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200055B RID: 1371
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateLoanStatusResp
	{
		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06001C64 RID: 7268 RVA: 0x0000CFF7 File Offset: 0x0000B1F7
		// (set) Token: 0x06001C65 RID: 7269 RVA: 0x0000CFFF File Offset: 0x0000B1FF
		[DataMember]
		public int LoanStatusId { get; set; }
	}
}
