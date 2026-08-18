using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200053C RID: 1340
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveLoansByPersonLoanedToInDateRangeReq : BaseMessageReq
	{
		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06001C01 RID: 7169 RVA: 0x0000CDB5 File Offset: 0x0000AFB5
		// (set) Token: 0x06001C02 RID: 7170 RVA: 0x0000CDBD File Offset: 0x0000AFBD
		[DataMember]
		public int PersonLoanToId { get; set; }

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x0000CDC6 File Offset: 0x0000AFC6
		// (set) Token: 0x06001C04 RID: 7172 RVA: 0x0000CDCE File Offset: 0x0000AFCE
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x0000CDD7 File Offset: 0x0000AFD7
		// (set) Token: 0x06001C06 RID: 7174 RVA: 0x0000CDDF File Offset: 0x0000AFDF
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
