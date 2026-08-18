using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000543 RID: 1347
	[DataContract(Namespace = "http://tpro.ca")]
	public class MakeLoanResp
	{
		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06001C1A RID: 7194 RVA: 0x0000CE4E File Offset: 0x0000B04E
		// (set) Token: 0x06001C1B RID: 7195 RVA: 0x0000CE56 File Offset: 0x0000B056
		[DataMember]
		public int LoanId { get; set; }
	}
}
