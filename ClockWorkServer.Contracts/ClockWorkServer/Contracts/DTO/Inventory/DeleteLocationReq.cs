using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200056D RID: 1389
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteLocationReq : BaseMessageReq
	{
		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06001C9E RID: 7326 RVA: 0x0000D14B File Offset: 0x0000B34B
		// (set) Token: 0x06001C9F RID: 7327 RVA: 0x0000D153 File Offset: 0x0000B353
		[DataMember]
		public int LocationId { get; set; }
	}
}
