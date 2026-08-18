using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000556 RID: 1366
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetReturnedLoansByPersonLoanedToInDateRangeReq : BaseMessageReq
	{
		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06001C51 RID: 7249 RVA: 0x0000CF80 File Offset: 0x0000B180
		// (set) Token: 0x06001C52 RID: 7250 RVA: 0x0000CF88 File Offset: 0x0000B188
		[DataMember]
		public int PersonLoanedToId { get; set; }

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06001C53 RID: 7251 RVA: 0x0000CF91 File Offset: 0x0000B191
		// (set) Token: 0x06001C54 RID: 7252 RVA: 0x0000CF99 File Offset: 0x0000B199
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06001C55 RID: 7253 RVA: 0x0000CFA2 File Offset: 0x0000B1A2
		// (set) Token: 0x06001C56 RID: 7254 RVA: 0x0000CFAA File Offset: 0x0000B1AA
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
