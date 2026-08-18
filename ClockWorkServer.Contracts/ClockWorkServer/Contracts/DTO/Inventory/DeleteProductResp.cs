using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200058F RID: 1423
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteProductResp
	{
		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x0000D822 File Offset: 0x0000BA22
		// (set) Token: 0x06001D8F RID: 7567 RVA: 0x0000D82A File Offset: 0x0000BA2A
		[DataMember]
		public bool WasDeleted { get; set; }
	}
}
