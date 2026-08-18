using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200059A RID: 1434
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductSnapshotReq : BaseMessageReq
	{
		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x0000D8FF File Offset: 0x0000BAFF
		// (set) Token: 0x06001DB4 RID: 7604 RVA: 0x0000D907 File Offset: 0x0000BB07
		[DataMember]
		public Guid ProductUniqueId { get; set; }

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x0000D910 File Offset: 0x0000BB10
		// (set) Token: 0x06001DB6 RID: 7606 RVA: 0x0000D918 File Offset: 0x0000BB18
		[DataMember]
		public int LoanId { get; set; }
	}
}
