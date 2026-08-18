using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000561 RID: 1377
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetLoanStatusListResp
	{
		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x0000D03B File Offset: 0x0000B23B
		// (set) Token: 0x06001C73 RID: 7283 RVA: 0x0000D043 File Offset: 0x0000B243
		[DataMember]
		public IList<InventoryLoanStatusDTO> LoanStatusList { get; set; }
	}
}
