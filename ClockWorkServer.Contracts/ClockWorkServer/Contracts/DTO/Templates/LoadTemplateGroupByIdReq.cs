using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Templates
{
	// Token: 0x020001D3 RID: 467
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTemplateGroupByIdReq : BaseMessageReq
	{
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x00004E9B File Offset: 0x0000309B
		// (set) Token: 0x06000AAD RID: 2733 RVA: 0x00004EA3 File Offset: 0x000030A3
		[DataMember]
		public string TemplateGroupId { get; set; }
	}
}
