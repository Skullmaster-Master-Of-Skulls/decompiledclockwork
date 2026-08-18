using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000545 RID: 1349
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateLoanResp
	{
		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06001C20 RID: 7200 RVA: 0x0000CE70 File Offset: 0x0000B070
		// (set) Token: 0x06001C21 RID: 7201 RVA: 0x0000CE78 File Offset: 0x0000B078
		[DataMember]
		public int LoanGroupId { get; set; }
	}
}
