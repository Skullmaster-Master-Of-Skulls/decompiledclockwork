using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000588 RID: 1416
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductsByLoanReq : BaseMessageReq
	{
		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06001D73 RID: 7539 RVA: 0x0000D778 File Offset: 0x0000B978
		// (set) Token: 0x06001D74 RID: 7540 RVA: 0x0000D780 File Offset: 0x0000B980
		[DataMember]
		public int WorkingCatalogId { get; set; }

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06001D75 RID: 7541 RVA: 0x0000D789 File Offset: 0x0000B989
		// (set) Token: 0x06001D76 RID: 7542 RVA: 0x0000D791 File Offset: 0x0000B991
		[DataMember]
		public int LoanId { get; set; }
	}
}
