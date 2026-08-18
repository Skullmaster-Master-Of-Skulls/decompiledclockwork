using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000569 RID: 1385
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetLocationsReq : BaseMessageReq
	{
		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06001C92 RID: 7314 RVA: 0x0000D107 File Offset: 0x0000B307
		// (set) Token: 0x06001C93 RID: 7315 RVA: 0x0000D10F File Offset: 0x0000B30F
		[DataMember]
		public string SearchingText { get; set; }
	}
}
