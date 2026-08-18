using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002C3 RID: 707
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateRequestReq : BaseMessageReq
	{
		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x00007887 File Offset: 0x00005A87
		// (set) Token: 0x0600102C RID: 4140 RVA: 0x0000788F File Offset: 0x00005A8F
		[DataMember]
		public SPRequestWithSubItemsDTO RequestWithSubItems { get; set; }

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x00007898 File Offset: 0x00005A98
		// (set) Token: 0x0600102E RID: 4142 RVA: 0x000078A0 File Offset: 0x00005AA0
		[DataMember]
		public bool CreateSubItems { get; set; }
	}
}
