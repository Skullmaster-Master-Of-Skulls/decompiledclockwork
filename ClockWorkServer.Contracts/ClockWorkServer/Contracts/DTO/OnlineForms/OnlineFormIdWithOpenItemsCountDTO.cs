using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x020003EE RID: 1006
	[DataContract(Namespace = "http://tpro.ca")]
	public class OnlineFormIdWithOpenItemsCountDTO
	{
		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001623 RID: 5667 RVA: 0x0000A539 File Offset: 0x00008739
		// (set) Token: 0x06001624 RID: 5668 RVA: 0x0000A541 File Offset: 0x00008741
		[DataMember]
		public int OnlineFormId { get; set; }

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001625 RID: 5669 RVA: 0x0000A54A File Offset: 0x0000874A
		// (set) Token: 0x06001626 RID: 5670 RVA: 0x0000A552 File Offset: 0x00008752
		[DataMember]
		public int OpenItemsCount { get; set; }
	}
}
