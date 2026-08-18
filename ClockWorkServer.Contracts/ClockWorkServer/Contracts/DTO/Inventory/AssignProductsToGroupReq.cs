using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000598 RID: 1432
	[DataContract(Namespace = "http://tpro.ca")]
	public class AssignProductsToGroupReq : BaseMessageReq
	{
		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06001DAB RID: 7595 RVA: 0x0000D8CC File Offset: 0x0000BACC
		// (set) Token: 0x06001DAC RID: 7596 RVA: 0x0000D8D4 File Offset: 0x0000BAD4
		[DataMember]
		public int WorkingCatalogId { get; set; }

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06001DAD RID: 7597 RVA: 0x0000D8DD File Offset: 0x0000BADD
		// (set) Token: 0x06001DAE RID: 7598 RVA: 0x0000D8E5 File Offset: 0x0000BAE5
		[DataMember]
		public IList<int> ProductIdList { get; set; }

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06001DAF RID: 7599 RVA: 0x0000D8EE File Offset: 0x0000BAEE
		// (set) Token: 0x06001DB0 RID: 7600 RVA: 0x0000D8F6 File Offset: 0x0000BAF6
		[DataMember]
		public int GroupId { get; set; }
	}
}
