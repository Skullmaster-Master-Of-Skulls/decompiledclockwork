using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200056E RID: 1390
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteLocationResp
	{
		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06001CA1 RID: 7329 RVA: 0x0000D15C File Offset: 0x0000B35C
		// (set) Token: 0x06001CA2 RID: 7330 RVA: 0x0000D164 File Offset: 0x0000B364
		[DataMember]
		public bool WasDeleted { get; set; }
	}
}
