using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000536 RID: 1334
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveLoanByIdReq : BaseMessageReq
	{
		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06001BED RID: 7149 RVA: 0x0000CD3E File Offset: 0x0000AF3E
		// (set) Token: 0x06001BEE RID: 7150 RVA: 0x0000CD46 File Offset: 0x0000AF46
		[DataMember]
		public int LoanId { get; set; }
	}
}
