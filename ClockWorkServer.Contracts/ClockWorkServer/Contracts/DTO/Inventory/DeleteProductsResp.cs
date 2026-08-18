using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000591 RID: 1425
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteProductsResp
	{
		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06001D94 RID: 7572 RVA: 0x0000D844 File Offset: 0x0000BA44
		// (set) Token: 0x06001D95 RID: 7573 RVA: 0x0000D84C File Offset: 0x0000BA4C
		[DataMember]
		public IList<Guid> NotDeletedProducts { get; set; }
	}
}
